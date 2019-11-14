using System;
using System.IO;
using System.Management;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

// TODO: add test for <public ViridianException(string message, string[] messages, Exception inner)>

namespace Viridian.Exceptions
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

        public ViridianException(string message, string[] messages, Exception inner)
        {
            if (inner is ManagementException)
            {
                Console.WriteLine("Main error message: {0}\n", message);

                Console.WriteLine("Detailed errors: \n");

                foreach (var error in messages)
                {
                    var errorSource = string.Empty;
                    var errorMessage = string.Empty;
                    var propId = 0;

                    using (var reader = XmlReader.Create(new StringReader(error)))
                    {
                        while (reader.Read())
                        {
                            if (reader.Name.Equals("PROPERTY", StringComparison.OrdinalIgnoreCase))
                            {
                                propId = 0;

                                if (!reader.HasAttributes)
                                    continue;

                                var propName = reader.GetAttribute(0);

                                if (propName == null)
                                    continue;

                                if (propName.Equals("ErrorSource", StringComparison.OrdinalIgnoreCase))
                                    propId = 1;
                                else if (propName.Equals("Message", StringComparison.OrdinalIgnoreCase))
                                    propId = 2;
                            }
                            else if (reader.Name.Equals("VALUE", StringComparison.OrdinalIgnoreCase))
                            {
                                switch (propId)
                                {
                                    case 1:
                                        errorSource = reader.ReadElementContentAsString();
                                        break;
                                    case 2:
                                        errorMessage = reader.ReadElementContentAsString();
                                        break;
                                    default:
                                        errorMessage = reader.ReadElementContentAsString();
                                        break;
                                }

                                propId = 0;
                            }
                            else
                            {
                                propId = 0;
                            }
                        }

                        Console.WriteLine("Error Message: {0}\n", errorMessage);
                        Console.WriteLine("Error Source:  {0}\n", errorSource);
                    }
                }
            }
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
