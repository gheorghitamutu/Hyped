using System.Management;

namespace Viridian.Msvm
{
    // Embedded class to represent WMI system Properties.
    public class ManagementSystemProperties
    {
        private readonly ManagementBaseObject PrivateLateBoundObject;

        public ManagementSystemProperties(ManagementBaseObject ManagedObject) => PrivateLateBoundObject = ManagedObject;

        public int GENUS => (int)PrivateLateBoundObject[$"__{nameof(GENUS)}"];
        public string CLASS => (string)PrivateLateBoundObject[$"__{nameof(CLASS)}"];
        public string SUPERCLASS => (string)PrivateLateBoundObject[$"__{nameof(SUPERCLASS)}"];
        public string DYNASTY => (string)PrivateLateBoundObject[$"__{nameof(DYNASTY)}"];
        public string RELPATH => (string)PrivateLateBoundObject[$"__{nameof(RELPATH)}"];
        public int PROPERTY_COUNT => (int)PrivateLateBoundObject[$"__{nameof(PROPERTY_COUNT)}"];
        public string[] DERIVATION => (string[])PrivateLateBoundObject[$"__{nameof(DERIVATION)}"];
        public string SERVER => (string)PrivateLateBoundObject[$"__{nameof(SERVER)}"];
        public string NAMESPACE => (string)PrivateLateBoundObject[$"__{nameof(NAMESPACE)}"];
        public string PATH => (string)PrivateLateBoundObject[$"__{nameof(PATH)}"];
    }
}
