using System;
using Viridian.Event.WMI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Scopes;
using System.Diagnostics;
using System.Management;
using System.Linq;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using System.Threading.Tasks;
using System.Globalization;
using Viridian.Root.Microsoft.Windows.System.Event;

namespace ViridianTester.Event.WMI
{
    [TestClass]
    public class EventWatcherFactoryTest
    {
        [TestMethod]
        public void GetAllWMIEventClassesForVirtualizationV2_Expect33()
        {
            var virtualizationV2EventClasses = EventWatcherFactory.GetEventClassesFromNamespacePath(Scope.Virtualization.NamespacePath);

            virtualizationV2EventClasses.ForEach((cls) => Trace.WriteLine(cls));

            Assert.AreEqual(33, virtualizationV2EventClasses.Count);
        }

        [TestMethod]
        public void GetAllWMIEventClassesForStorageManagement_Expect21()
        {
            var windowsStorageManagementEventClasses = EventWatcherFactory.GetEventClassesFromNamespacePath(Scope.Storage.NamespacePath);

            windowsStorageManagementEventClasses.ForEach((cls) => Trace.WriteLine(cls));

            Assert.AreEqual(21, windowsStorageManagementEventClasses.Count);
        }

        [TestMethod]
        public async Task WatchVirtualSystemSettingDataCreation_ExpectTheSameConfigurationIDInObjectResultedAsTheOneDefined()
        {
            var watcherTask = Task.Run(() =>
            {
                var sut = EventWatcherFactory.GetWatcher(Scope.Virtualization.ScopeObject, InstanceCreationEvent.ClassName, new TimeSpan(0, 0, 100), VirtualSystemSettingData.ClassName);

                var moInstanceCreationEvent =  sut.WaitForNextEvent();

                sut.Stop();

                return moInstanceCreationEvent;
            }).ConfigureAwait(true);

            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var instanceCreationEvent = new InstanceCreationEvent(await watcherTask))
                using (var virtualSystemSettingDataFromEvent = new VirtualSystemSettingData(instanceCreationEvent.TargetInstance))
                using (var computerSystemAsDefineSystemResult = new ComputerSystem(ResultingSystem))
                using (var virtualSystemSettingDataFromResultingSystem = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystemAsDefineSystemResult).First())
                {
                    Assert.IsNotNull(virtualSystemSettingDataFromEvent);
                    Assert.IsTrue(string.Compare(virtualSystemSettingDataFromResultingSystem.ConfigurationID, virtualSystemSettingDataFromEvent.ConfigurationID, false, CultureInfo.InvariantCulture) == 0);
                }

                Assert.AreEqual(0U, ReturnValue);
            }
        }
    }
}
