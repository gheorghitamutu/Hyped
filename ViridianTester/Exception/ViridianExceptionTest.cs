using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Exceptions;

namespace ViridianTester.Exceptions
{
    [TestClass]
    public class ViridianExceptionTest
    {
        [TestMethod]
        public void ViridianException_default_ctor()
        {
            // Arrange
            const string expectedMessage = "Exception of type 'Viridian.Exceptions.ViridianException' was thrown.";

            // Act
            var sut = new ViridianException();

            // Assert
            Assert.IsNull(sut.ResourceReferenceProperty);
            Assert.IsNull(sut.InnerException);
            Assert.AreEqual(expectedMessage, sut.Message);
        }

        [TestMethod]
        public void ViridianException_ctor_string()
        {
            // Arrange
            const string expectedMessage = "message";

            // Act
            var sut = new ViridianException(expectedMessage);

            // Assert
            Assert.IsNull(sut.ResourceReferenceProperty);
            Assert.IsNull(sut.InnerException);
            Assert.AreEqual(expectedMessage, sut.Message);
        }

        [TestMethod]
        public void ViridianException_ctor_string_ex()
        {
            // Arrange
            const string expectedMessage = "message";
            var innerEx = new Exception("foo");

            // Act
            var sut = new ViridianException(expectedMessage, innerEx);

            // Assert
            Assert.IsNull(sut.ResourceReferenceProperty);
            Assert.AreEqual(innerEx, sut.InnerException);
            Assert.AreEqual(expectedMessage, sut.Message);
        }

        [TestMethod]
        public void ViridianException_serialization_deserialization()
        {
            // Arrange
            var innerEx = new Exception("inner");
            var origEx = new ViridianException("original", innerEx) { ResourceReferenceProperty = "MyReferenceProperty" };
            var buffer = new byte[4096];
            var ms = new MemoryStream(buffer);
            var ms2 = new MemoryStream(buffer);
            var formatter = new BinaryFormatter();

            // Act
            formatter.Serialize(ms, origEx);
            var deserializedException = (ViridianException)formatter.Deserialize(ms2);

            // Assert
            Assert.AreEqual(origEx.ResourceReferenceProperty, deserializedException.ResourceReferenceProperty);
            Assert.AreEqual(origEx.InnerException.Message, deserializedException.InnerException.Message);
            Assert.AreEqual(origEx.Message, deserializedException.Message);
        }

        [TestMethod]
        public void ViridianException_GetObjectData_throws_exception_when_info_null()
        {
            // Arrange
            var sut = new ViridianException("message") { ResourceReferenceProperty = "MyReferenceProperty" };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetObjectData(null, new StreamingContext()));
        }

        [TestMethod]
        public void ViridianException_ctor_ex_throws_when_messages_array_is_null()
        {
            // Arrange
            const string message = "message";
            string[] nullArray = null;

            // Act && Assert
            Assert.ThrowsException<NullReferenceException>(() => new ViridianException(message, nullArray));
        }

        [TestMethod]
        public void ViridianException_ctor_ex_throws_when_messages_array_is_not_xml_serialized()
        {
            // Arrange
            const string message = "message";
            string[] messages = { "a", "b", "c"};

            // Act && Assert
            Assert.ThrowsException<XmlException>(() => new ViridianException(message, messages));
        }

        [TestMethod]
        public void ViridianException_ctor_ex_messages_array_is_xml_serialized()
        {
            // Arrange
            const string message = "message";
            string[] messages = { "<?xml version=\"1.0\" encoding=\"utf-8\"?><PROPERTY>prop</PROPERTY>" };
            var sut = new ViridianException(message, messages);

            // Act && Assert
            Assert.AreEqual(sut.GetType(), typeof(ViridianException));
        }
    }
}
