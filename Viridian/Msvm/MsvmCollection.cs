using System;
using System.Collections;
using System.Management;

namespace Viridian.Msvm
{
    // Enumerator implementation for enumerating instances of the class.
    public class MsvmCollection<T> : object, ICollection where T : IDisposable, new()
    {
        private readonly ManagementObjectCollection privColObj;

        public MsvmCollection(ManagementObjectCollection objCollection) => privColObj = objCollection;

        public virtual int Count => privColObj.Count;

        public virtual bool IsSynchronized => privColObj.IsSynchronized;

        public virtual object SyncRoot => this;

        public virtual void CopyTo(Array array, int index)
        {
            privColObj.CopyTo(array, index);
            int nCtr;
            for (nCtr = 0; nCtr < array?.Length; nCtr += 1)
            {
                using (ManagementObject theObj = (ManagementObject)array.GetValue(nCtr))
                {
                    using (var vsmsObj = Activator.CreateInstance(typeof(T), theObj) as IDisposable)
                    {
                        array.SetValue(vsmsObj, nCtr);
                    }
                }
            }
        }

        public virtual IEnumerator GetEnumerator() => new VirtualSystemManagementServiceEnumerator(privColObj.GetEnumerator());

        public class VirtualSystemManagementServiceEnumerator : object, IEnumerator
        {
            private readonly ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

            public VirtualSystemManagementServiceEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) => privObjEnum = objEnum;

            public virtual object Current => Activator.CreateInstance(typeof(T), (ManagementObject)privObjEnum.Current);

            public virtual bool MoveNext() => privObjEnum.MoveNext();

            public virtual void Reset() => privObjEnum.Reset();
        }
    }
}
