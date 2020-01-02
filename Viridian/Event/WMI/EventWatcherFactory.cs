using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace Viridian.Event.WMI
{
    public static class EventWatcherFactory
    {
        public static List<ManagementPath> GetEventClassesFromNamespacePath(string NamespacePath)
        {
            var events = new List<ManagementPath>();

            try
            {
                EnumerationOptions options = new EnumerationOptions
                {
                    ReturnImmediately = true,
                    Rewindable = false
                };

                using (var searcher =
                    new ManagementObjectSearcher(NamespacePath, @"Select * From Meta_Class Where __This Isa '__Event'", options))
                {
                    foreach (ManagementClass cls in searcher.Get())
                    {
                        events.Add(cls.Path);
                    }
                }
            }
            catch (ManagementException exception)
            {
                Trace.WriteLine(exception.Message);
            }

            return events;
        }

        public static ManagementEventWatcher GetWatcher(ManagementScope scope, string eventClassName, TimeSpan withinInterval, string TargetInstance)
        {
            var wqlEventQuery = new WqlEventQuery($"{eventClassName}", withinInterval, $@"TargetInstance Isa '{TargetInstance}'");

            return new ManagementEventWatcher(scope, wqlEventQuery);
        }
    }
}
