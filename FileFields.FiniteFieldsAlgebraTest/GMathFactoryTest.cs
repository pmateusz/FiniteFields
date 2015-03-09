using FileFields.FiniteFieldsAlgebra.GMath;
using FileFields.FiniteFieldsAlgebra.GMath.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FiniteFields.FiniteFieldsAlgebraTest
{
    [TestClass]
    public class GMathFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetInstatnce_W0()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            gmathFactory.GetInstance(0);
        }

        [TestMethod]
        public void Test_Operations()
        {
            // given
            var x = 3;
            var y = 7;
            var gmathFactory = new GMathFactory();
            var gmath = gmathFactory.GetInstance(4);

            // when
            var addresult = gmath.Add(x, y);
            var subresult = gmath.Sub(x, y);
            var mulresult = gmath.Mul(x, y);
            var divresult = gmath.Div(mulresult, y);

            // then
            Assert.AreEqual(addresult, subresult);
            Assert.AreEqual(divresult, x);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetInstatnce_W33()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            gmathFactory.GetInstance(33);
        }

        [TestMethod]
        public void Test_GetInstatnce_W8()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var gmath = gmathFactory.GetInstance(8);

            // then
            Assert.IsInstanceOfType(gmath, typeof(GMathTableVersion));
        }

        [TestMethod]
        public void Test_GetInstatnce_W16()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var gmath = gmathFactory.GetInstance(16);

            // then
            Assert.IsInstanceOfType(gmath, typeof(GMathLogVersion));
        }

        [TestMethod]
        public void Test_GetSolver_W16()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var solver = gmathFactory.GetSolver(16);

            // then
            Assert.IsNotNull(solver);
        }

        [TestMethod]
        public void Test_GetGenerator_W16()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var equationGenerator = gmathFactory.GetGenerator(16);

            // then
            Assert.IsNotNull(equationGenerator);
        }

        [TestMethod]
        public void Test_GetVectorTools_W16()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var vectorTools = gmathFactory.GetVectorTools(16);

            // then
            Assert.IsNotNull(vectorTools);
        }

        [TestMethod]
        public void Test_GetInstatnce_W24()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var gmath = gmathFactory.GetInstance(24);

            // then
            Assert.IsInstanceOfType(gmath, typeof(GMathShiftVersion));
        }

        [TestMethod]
        public void Test_GetInstatnce_W32()
        {
            // given
            var gmathFactory = new GMathFactory();

            // when
            var gmath = gmathFactory.GetInstance(32);

            // then
            Assert.IsInstanceOfType(gmath, typeof(GMathSplitW8Version));
        }
    }
}
