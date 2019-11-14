using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Viridian
{
    namespace Exceptions
    {
        [Serializable]
        public class ViridianException : Exception
        {
            public string ResourceReferenceProperty { get; set; }

            public ViridianException()
            {
            }

            public ViridianException(string message)
                : base(message)
            {
            }

            public ViridianException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected ViridianException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
            }

            [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
                base.GetObjectData(info, context);
            }
        }
    }
}
