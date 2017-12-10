using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CustomTestFramework.Core.Attributes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using MoreLinq;

namespace CustomTestFramework.TestAdapter
{
    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [DefaultExecutorUri(TestExecutor.ExecutorUriString)]
    public class TestDiscoverer : ITestDiscoverer
    {
        public static ICollection<TestCase> GetTestCases(IEnumerable<string> sources,
            ITestCaseDiscoverySink discoverySink)
        {
            List<TestCase> testCases = new List<TestCase>();
            foreach (string source in sources)
            {
                string assemblyName = source;
                if (!Path.IsPathRooted(assemblyName))
                {
                    assemblyName = Path.Combine(Directory.GetCurrentDirectory(), assemblyName);
                }
                var currentAssemblyBytes = File.ReadAllBytes(assemblyName);

                IEnumerable<Type> testingTypes = Assembly
                    .Load(currentAssemblyBytes)
                    .GetTypes()
                    .Where(type =>
                        type.GetMethods()
                            .Any(m => m.GetCustomAttributes()
                                .Any(a => a is CustomTestAttribute)));

                testingTypes.ForEach(type =>
                {
                    var testCasesForType = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Where(m => m.GetCustomAttributes().Any(a => a is CustomTestAttribute))
                        .Select(m => new TestCase(m.Name, TestExecutor.ExecutorUri, source)
                        {
                            LocalExtensionData = Activator.CreateInstance(type)
                        });

                    testCases.AddRange(testCasesForType);
                });
            }
            if (discoverySink != null)
            {
                testCases.ForEach(discoverySink.SendTestCase);
            }

            return testCases;
        }

        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext,
            IMessageLogger logger,
            ITestCaseDiscoverySink discoverySink)
        {
            GetTestCases(sources, discoverySink);
        }
    }
}