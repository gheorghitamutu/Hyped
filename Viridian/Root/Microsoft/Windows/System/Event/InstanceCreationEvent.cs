using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Microsoft.Windows.System.Event
{
    public class InstanceCreationEvent : SystemBase
    {
        public static string ClassName => $"__{nameof(InstanceCreationEvent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public InstanceCreationEvent() : base(ClassName) { }
    
        public InstanceCreationEvent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public InstanceCreationEvent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public InstanceCreationEvent(ManagementPath path) : base(path, ClassName) { }

        public InstanceCreationEvent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public InstanceCreationEvent(ManagementObject theObject) : base(theObject, ClassName) { }

        public InstanceCreationEvent(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public byte[] SECURITY_DESCRIPTOR
        {
            get
            {
                return (byte[])LateBoundObject[nameof(SECURITY_DESCRIPTOR)];
            }
            set
            {
                LateBoundObject[nameof(SECURITY_DESCRIPTOR)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public ManagementBaseObject TargetInstance
        {
            get
            {
                return (ManagementBaseObject)LateBoundObject[nameof(TargetInstance)];
            }
            set
            {
                LateBoundObject[nameof(TargetInstance)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public ulong TIME_CREATED
        {
            get
            {
                if (LateBoundObject[nameof(TIME_CREATED)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(TIME_CREATED)];
            }
            set
            {
                LateBoundObject[nameof(TIME_CREATED)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetSECURITY_DESCRIPTOR()
        {
            LateBoundObject[nameof(SECURITY_DESCRIPTOR)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTargetInstance()
        {
            LateBoundObject[nameof(TargetInstance)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTIME_CREATED()
        {
            LateBoundObject[nameof(TIME_CREATED)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<InstanceCreationEvent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public new static List<InstanceCreationEvent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static List<InstanceCreationEvent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static List<InstanceCreationEvent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static List<InstanceCreationEvent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static List<InstanceCreationEvent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static List<InstanceCreationEvent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static List<InstanceCreationEvent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InstanceCreationEvent(mo)).ToList();

        public static InstanceCreationEvent CreateInstance() => new InstanceCreationEvent(CreateInstance(ClassName));
    }
}
