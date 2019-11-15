using System;
using System.Security.Permissions;

namespace Viridian.Storage.Handle
{
    [SecurityPermission(SecurityAction.Demand)]
    public class SafeHandle : System.Runtime.InteropServices.SafeHandle
    {
        public SafeHandle() : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid => (IsClosed) || (handle == IntPtr.Zero);

        public override string ToString()
        {
            return handle.ToString();
        }

        protected override bool ReleaseHandle()
        {
            return Native.NativeAPI.CloseHandle(handle);
        }
    }
}
