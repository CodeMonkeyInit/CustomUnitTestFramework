using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using Microsoft.VisualStudio.TestWindow.Extensibility.Model;

namespace CustomTestFramework.TestAdapter
{
//    public class CustomTestContainer : ITestContainer
//    {
//        private DateTime _timeStamp;
//        public ITestContainerDiscoverer Discoverer { get; }
//        public string Source { get; }
//        public IEnumerable<Guid> DebugEngines { get; }
//        public FrameworkVersion TargetFramework { get; }
//        public Architecture TargetPlatform { get; }
//        public bool IsAppContainerTestContainer { get; }
//
//        public CustomTestContainer(ITestContainerDiscoverer discoverer, string source, Uri executorUri, IEnumerable<Guid> debugEngines)
//        {
//            Source = source;
//            ExecutorUri = executorUri;
//            DebugEngines = debugEngines;
//            Discoverer = discoverer;
//            TargetFramework = FrameworkVersion.None;
//            TargetPlatform = Architecture.AnyCPU;
//            //TODO _timeStamp = GetTimeStamp();
//        }
//
//        private CustomTestContainer(CustomTestContainer copy)
//            : this(copy.Discoverer, copy.Source, copy.ExecutorUri, Enumerable.Empty<Guid>())
//        {
//            _timeStamp = copy._timeStamp;
//        }
//
//        public override string ToString()
//        {
//            return $"{ExecutorUri}/{Source}";
//        }
//
//        public Uri ExecutorUri { get; set; }
//
//        public IDeploymentData DeployAppContainer()
//        {
//            return null;
//        }
//
//        public int CompareTo(ITestContainer other)
//        {
//            if (other is CustomTestContainer testContainer)
//            {
//                var result = String.Compare(this.Source, testContainer.Source, StringComparison.OrdinalIgnoreCase);
//                if (result != 0)
//                {
//                    return result;
//                }
//
//                return _timeStamp.CompareTo(testContainer._timeStamp);
//            }
//            return -1;
//        }
//
//        public ITestContainer Snapshot()
//        {
//            return new CustomTestContainer(this);
//        }
//    }
    //[Export(typeof(ITestContainerDiscoverer))]
    //public class CustomTestContainerDiscoverer : ITestContainerDiscoverer
    //{
    //    public Uri ExecutorUri { get; }
    //    public IEnumerable<ITestContainer> TestContainers { get; }
    //    public event EventHandler TestContainersUpdated;

    //    [ImportingConstructor]
    //    public XmlTestContainerDiscoverer(
    //        [Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider,
    //        ILogger logger,
    //        ISolutionEventsListener solutionListener,
    //        ITestFilesUpdateWatcher testFilesUpdateWatcher,
    //        ITestFileAddRemoveListener testFilesAddRemoveListener)

    //    public CustomTestContainerDiscoverer(Uri executorUri, IEnumerable<ITestContainer> testContainers)
    //    {
    //        ExecutorUri = executorUri;
    //        TestContainers = testContainers;
    //    }
    //}
}