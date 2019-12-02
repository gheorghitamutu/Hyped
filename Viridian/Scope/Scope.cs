using System.Management;

namespace Viridian.Scopes
{
    public class Scope
    {
        private Scope(string Server, string NamespacePath)
        {
            SpecificScope = new ManagementScope(new ManagementPath { Server = Server, NamespacePath = NamespacePath }, null);
        }

        public ManagementScope SpecificScope { get; private set; }

        public static Scope Virtualization => new Scope(Properties.Environment.Default.Server, Properties.Environment.Default.Virtualization);
        public static Scope Storage => new Scope(Properties.Environment.Default.Server, Properties.Environment.Default.Storage);
    }
}
