using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CustomTestFramework.Core;
using CustomTestFramework.Core.Attributes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using MoreLinq;

namespace CustomTestFramework.TestAdapter
{
    [ExtensionUri(ExecutorUriString)]
    public class TestExecutor : ITestExecutor
    {
        public const string ExecutorUriString = "executor://testexecutor";
        public static readonly Uri ExecutorUri = new Uri(ExecutorUriString);

        private bool _testingStopped;

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var testExecutor = new TestRunner();

            var testCases = tests as IList<TestCase> ?? tests.ToList();

            var allTestClassInstances = testCases.Select(testCase => testCase.LocalExtensionData).ToList();

            var uniqueTestClassInstances = allTestClassInstances.DistinctBy(o => o.GetType().FullName).ToList();

            RunMethodsWithAttribute<BeforeAllAttribute>(uniqueTestClassInstances);

            foreach (var testCase in testCases)
            {
                var instance = testCase.LocalExtensionData;

                var instanceAsArray = new[] { instance };

                RunMethodsWithAttribute<BeforeEachAttribute>(instanceAsArray);

                var methodInfo = instance.GetType().GetMethod(testCase.FullyQualifiedName);

                frameworkHandle.RecordStart(testCase);

                var testRunnerContext =
                    new TestRunnerContext(instance, 
                                          methodInfo, 
                                          testCase, 
                                          frameworkHandle, 
                                          _testingStopped);
                testExecutor.Run(testRunnerContext);

                RunMethodsWithAttribute<AfterEachAttribute>(instanceAsArray);
            }

            RunMethodsWithAttribute<AfterAllAttribute>(uniqueTestClassInstances);
        }

        private static void RunMethodsWithAttribute<TAttribure>(IEnumerable<object> tests)
            where TAttribure : Attribute
        {
            tests.ForEach(instance =>
            {
                var methods = instance?
                                .GetType()
                                .GetMethods()
                                .Where(m => m.GetCustomAttributes()
                                            .Any(a => a is TAttribure));

                methods?.ForEach(m => m.Invoke(instance, null));
            });
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            ICollection<TestCase> testCases = TestDiscoverer.GetTestCases(sources, null);

            RunTests(testCases, runContext, frameworkHandle);
        }

        public void Cancel()
        {
            _testingStopped = true;
        }
    }
}