using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void ViridianException_GetObjectData_throws_exception_when_info_null()
        {
            // Arrange
            var sut = new ViridianException("message") { ResourceReferenceProperty = "MyReferenceProperty" };

            // Act
            sut.GetObjectData(null, new StreamingContext());

            // Assert
            // [ExpectedException(typeof(ArgumentNullException))]
        }
    }
}
