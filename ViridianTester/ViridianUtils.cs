using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using Viridian.Root.Virtualization.v2.Msvm.Integration;
using Viridian.Root.Virtualization.v2.Msvm.Storage;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace ViridianTester
{
    public class ViridianUtils : IDisposable
    {
        public string VSSD_ConfigurationDataRoot { get; private set; } = @"ConfigurationDataRoot";
        public string VSSD_VirtualSystemSubtype { get; private set; } = "Microsoft:Hyper-V:SubType:2";

        public VirtualSystemManagementService VSMS {get; private set; }

       public  List<ManagementPath> VSSD_ToDestroy { get; private set; } = new List<ManagementPath>();

        public ViridianUtils()
        {
            VSMS = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First();
        }

        public void SUT_ComputerSystemMO(string ElementName, out uint ReturnValue, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            using (var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance())
            {
                virtualSystemSettingData.LateBoundObject["ElementName"] = ElementName;
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = VSSD_ConfigurationDataRoot;
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = VSSD_VirtualSystemSubtype;

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                ReturnValue = VSMS.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out Job, out ResultingSystem);

                VSSD_ToDestroy.Add(ResultingSystem); // avoid using the object that will probably be disposed before your destructor gets to it
            }
        }

        public static void WaitForConcreteJobToEnd(ManagementPath Job)
        {
            if (string.IsNullOrEmpty(Job?.ClassName) == false)
            {
                using (var concreteJob = new ConcreteJob(Job))
                {
                    while (
                        concreteJob.JobState != 7 &&     // Completed
                        concreteJob.JobState != 8 &&     // Terminated
                        concreteJob.JobState != 9 &&     // Killed
                        concreteJob.JobState != 10 &&    // Exception
                        concreteJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)concreteJob.LateBoundObject).Get();
                    }
                }
            }
        }

        public static void WaitForStorageJobToEnd(ManagementPath Job)
        {
            if (string.IsNullOrEmpty(Job?.ClassName) == false)
            {
                using (var storageJob = new StorageJob(Job))
                {
                    while (
                        storageJob.JobState != 7 &&     // Completed
                        storageJob.JobState != 8 &&     // Terminated
                        storageJob.JobState != 9 &&     // Killed
                        storageJob.JobState != 10 &&    // Exception
                        storageJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)storageJob.LateBoundObject).Get();
                    }
                }
            }
        }

        public static void WaitForCopyFileToGuestJobToEnd(ManagementPath Job)
        {
            if (string.IsNullOrEmpty(Job?.ClassName) == false)
            {
                using (var copyFileToGuestJob = new CopyFileToGuestJob(Job))
                {
                    while (
                        copyFileToGuestJob.JobState != 7 &&     // Completed
                        copyFileToGuestJob.JobState != 8 &&     // Terminated
                        copyFileToGuestJob.JobState != 9 &&     // Killed
                        copyFileToGuestJob.JobState != 10 &&    // Exception
                        copyFileToGuestJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)copyFileToGuestJob.LateBoundObject).Get();
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
              VSSD_ToDestroy?.ForEach((vssd) => VSMS?.DestroySystem(vssd, out ManagementPath Job));
            }
            catch (Exception e)
            {
                Trace.WriteLine($"[{DateTime.Now}] [{e.Message}]");
            }

            if (disposing)
            {
                VSMS?.Dispose();
            }

            VSSD_ToDestroy.Clear();
        }

        ~ViridianUtils()
        {
            Dispose(false);
        }
    }
}
