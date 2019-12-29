using System;
using Viridian.Event.WMI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Scopes;
using System.Diagnostics;
using System.Management;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
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
                var sut = EventWatcherFactory.GetWatcher(Scope.Virtualization.ScopeObject, "__InstanceCreationEvent", new TimeSpan(0, 0, 100), "Msvm_VirtualSystemSettingData");

                var moInstanceCreationEvent =  sut.WaitForNextEvent();

                sut.Stop();

                return moInstanceCreationEvent;
            }).ConfigureAwait(true);

            using (var virtualSystemManagementService = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(WatchVirtualSystemSettingDataCreation_ExpectTheSameConfigurationIDInObjectResultedAsTheOneDefined);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = virtualSystemManagementService.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var instanceCreationEvent = new InstanceCreationEvent(await watcherTask);
                instanceCreationEvent.LateBoundObject.Properties.Cast<PropertyData>().ToList().ForEach((p) => Trace.WriteLine($"{p.Name} [{p.Value}]"));
                Trace.WriteLine(instanceCreationEvent.LateBoundObject.ClassPath);

                var virtualSystemSettingDataFromEvent = new VirtualSystemSettingData(instanceCreationEvent.TargetInstance);
                virtualSystemSettingDataFromEvent.LateBoundObject.Properties.Cast<PropertyData>().ToList().ForEach((p) => Trace.WriteLine($"{p.Name} [{p.Value}]"));

                var computerSystemAsDefineSystemResult = new ComputerSystem(ResultingSystem);

                var virtualSystemSettingDataFromResultingSystem =
                   SettingsDefineState.GetInstances()
                       .Cast<SettingsDefineState>()
                       .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystemAsDefineSystemResult.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                       .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                       .ToList()
                       .First();

                Assert.IsNotNull(ResultingSystem);
                Assert.IsNotNull(virtualSystemSettingDataFromEvent);
                Assert.IsTrue(string.Compare(virtualSystemSettingDataFromResultingSystem.ConfigurationID, virtualSystemSettingDataFromEvent.ConfigurationID, false, CultureInfo.InvariantCulture) == 0);
                Assert.AreEqual(0U, ReturnValue);

                virtualSystemManagementService.DestroySystem(ResultingSystem, out Job);
            }
        }
    }
}
