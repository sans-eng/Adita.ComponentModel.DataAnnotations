using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adita.ComponentModel.DataAnnotations.Test
{
    [TestClass]
    public class AnnotationTest
    {
        [TestMethod]
        public void NumericStringAttributeTest()
        {
            //sbyte
            NumericStringAttribute attributeSbyte = new(typeof(sbyte));

            var sByteResult = attributeSbyte.IsValid("127");
            Assert.IsTrue(sByteResult);

            var sByteResult1 = attributeSbyte.IsValid("127122");
            Assert.IsFalse(sByteResult1);

            //byte
            NumericStringAttribute attributeByte = new(typeof(byte));

            var byteResult = attributeByte.IsValid("255");
            Assert.IsTrue(byteResult);

            var byteResult1 = attributeByte.IsValid("2551");
            Assert.IsFalse(byteResult1);

            //short
            NumericStringAttribute attributeShort = new(typeof(short));

            var shortResult = attributeShort.IsValid("32767");
            Assert.IsTrue(shortResult);

            var shortResult1 = attributeShort.IsValid("3276722");
            Assert.IsFalse(shortResult1);

            //ushort
            NumericStringAttribute attributeUshort = new(typeof(ushort));

            var ushortResult = attributeUshort.IsValid("65535");
            Assert.IsTrue(ushortResult);

            var ushortResult1 = attributeUshort.IsValid("6553512");
            Assert.IsFalse(ushortResult1);

            //int
            NumericStringAttribute attributeInt = new(typeof(int));

            var intResult = attributeInt.IsValid("2147483647");
            Assert.IsTrue(intResult);

            var intResult1 = attributeInt.IsValid("2147483647122");
            Assert.IsFalse(intResult1);


            //double
            NumericStringAttribute attributeDouble = new(typeof(double));

            var doubleResult = attributeDouble.IsValid("200.12");
            Assert.IsTrue(doubleResult);

            var doubleResult1 = attributeDouble.IsValid("1223ww");
            Assert.IsFalse(doubleResult1);
        }
    }
}