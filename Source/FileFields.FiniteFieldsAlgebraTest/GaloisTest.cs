using FileFields.FiniteFieldsAlgebra.GMath.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FiniteFields.FiniteFieldsAlgebraTest
{
    [TestClass]
    public class GaloisTest
    {
        [TestMethod]
        public void Test_GaloisSingleDivide_W4()
        {
            // given
            var x = 9;
            var y = 3;

            // when
            var result = Galois.Divide(x, y, 4);
            
            // then
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Test_GaloisSingleDivide2_W4()
        {
            // given
            var x = 9;
            var y = 7;

            // when
            var result = Galois.Divide(x, y, 4);
            
            // then
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Test_GaloisSingleMultiply_W4()
        {
            // given
            var x = 3;
            var y = 7;
            
            // when
            var result = Galois.Multiply(x, y, 4);
            
            // then
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Test_GaloisSingleMultiply_W16()
        {
            // given
            var x = 1717;
            var y = 2323;
            
            // when
            var result = Galois.Multiply(x, y, 16);
            
            // then
            Assert.AreEqual(31839, result);
        }

        [TestMethod]
        public void Test_GaloisSingleDivide_W16()
        {
            // given
            var x = 31839;
            var y = 2323;
            
            // when
            var result = Galois.Divide(x, y, 16);
            
            // then
            Assert.AreEqual(1717, result);
        }
    }
}
