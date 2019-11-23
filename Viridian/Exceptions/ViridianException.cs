using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Linq;

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

        public ViridianException(string message, string[] xmls)
        {
            Trace.WriteLine("Main error message: {0}\n", message);
            Trace.WriteLine("Detailed errors: \n");

            if (xmls == null)
                throw new NullReferenceException("Null error messages array!");

            foreach (var xml in xmls)
            {
                var stringBuilder = new StringBuilder();
                var element = XElement.Parse(xml);
                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = true,
                    NewLineOnAttributes = true
                };

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                    element.Save(xmlWriter);

                Trace.WriteLine(stringBuilder.ToString());
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
                throw new ArgumentNullException(nameof(info));

            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
