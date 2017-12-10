using System;
using System.Diagnostics;
using System.Reflection;
using CustomTestFramework.Core.Exceptions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace CustomTestFramework.Core
{
    public class TestRunnerContext
    {
        public object Instance { get; }
        public MethodInfo Method { get; }
        public TestCase TestCase { get; }
        public IFrameworkHandle FrameworkHandle { get; }
        public bool TestCancelled { get; }

        public TestRunnerContext(object instance, MethodInfo method, TestCase testCase, IFrameworkHandle frameworkHandle, bool testCancelled = false)
        {
            Instance = instance;
            Method = method;
            TestCase = testCase;
            FrameworkHandle = frameworkHandle;
            TestCancelled = testCancelled;
        }
    }
    
    
    public class TestRunner
    {
        public void Run(TestRunnerContext testRunnerContext)
        {
            var testCase = testRunnerContext.TestCase;
            var testResult = new TestResult(testCase);

            IFrameworkHandle frameworkHandle = testRunnerContext.FrameworkHandle;
            
            if (testRunnerContext.TestCancelled)
            {
                testResult.Outcome = TestOutcome.Skipped;
                frameworkHandle.RecordResult(testResult);

                return;
            }

            var stopwatch = Stopwatch.StartNew();
            try
            {
                testRunnerContext.Method.Invoke(testRunnerContext.Instance, null);

                testResult.Outcome = TestOutcome.Passed;
            }
            catch (TargetInvocationException e)
            {
                Console.WriteLine("OOOPS");
                testResult.ErrorMessage = e.InnerException?.Message;
                testResult.Outcome = TestOutcome.Failed;
            }
            finally
            {
                stopwatch.Stop();
                testResult.Duration = stopwatch.Elapsed;

                frameworkHandle.RecordResult(testResult);
                frameworkHandle.RecordEnd(testCase, testResult.Outcome);
            }
        }
    }
}