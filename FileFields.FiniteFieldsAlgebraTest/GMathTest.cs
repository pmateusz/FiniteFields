using FileFields.FiniteFieldsAlgebra.GMath;
using FileFields.FiniteFieldsAlgebra.GMath.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FiniteFields.FiniteFieldsAlgebraTest.TestData
{
    [TestClass]
    public class GMathW4_8_16Test
    {
        private IGMathFactory gmathFactory;
        private TestContext testContextInstance;

        [TestInitialize]
        public void SetUp()
        {
            gmathFactory = new GMathFactory();
        }

        public IGMathFactory GMathFactory
        {
            get { return gmathFactory; }
        }

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w4.txt", "testData_mult_w4#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w4.txt"), TestMethod]
        public void Test_Mul_W4()
        {
            // given
            var x = int.Parse(TestContext.DataRow["x"].ToString());
            var y = int.Parse(TestContext.DataRow["y"].ToString());
            var expectedResult = int.Parse(TestContext.DataRow["result"].ToString());

            // then
            this.TestMul(x, y, expectedResult, 4);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w4.txt", "testData_mult_w4#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w4.txt"), TestMethod]
        public void Test_Div_W4()
        {
            // given
            int x = int.Parse(TestContext.DataRow["result"].ToString());
            int y = int.Parse(TestContext.DataRow["x"].ToString());
            int expectedResult = int.Parse(TestContext.DataRow["y"].ToString());

            // when
            this.TestDiv(x, y, expectedResult, 4);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w8.txt", "testData_mult_w8#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w8.txt"), TestMethod]
        public void Test_Mul_W8()
        {
            // given
            var x = int.Parse(TestContext.DataRow["x"].ToString());
            var y = int.Parse(TestContext.DataRow["y"].ToString());
            var expectedResult = int.Parse(TestContext.DataRow["result"].ToString());

            // then
            this.TestMul(x, y, expectedResult, 8);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w8.txt", "testData_mult_w8#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w8.txt"), TestMethod]
        public void Test_Div_W8()
        {
            // given
            var x = int.Parse(TestContext.DataRow["result"].ToString());
            var y = int.Parse(TestContext.DataRow["x"].ToString());
            var expectedResult = int.Parse(TestContext.DataRow["y"].ToString());

            // then
            this.TestDiv(x, y, expectedResult, 8);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w16.txt", "testData_mult_w16#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w16.txt"), TestMethod]
        public void Test_Mul_W16()
        {
            // then
            int x = int.Parse(TestContext.DataRow["x"].ToString());
            int y = int.Parse(TestContext.DataRow["y"].ToString());
            int expectedResult = int.Parse(TestContext.DataRow["result"].ToString());

            // when
            this.TestMul(x, y, expectedResult, 16);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w16_reduced.txt", "testData_mult_w16_reduced#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w16_reduced.txt"), TestMethod]
        public void Test_Mul_W16_Reduced()
        {
            // given
            var x = int.Parse(TestContext.DataRow["x"].ToString());
            var y = int.Parse(TestContext.DataRow["y"].ToString());
            var expectedResult = int.Parse(TestContext.DataRow["result"].ToString());

            // when
            this.TestMul(x, y, expectedResult, 16);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w16.txt", "testData_mult_w16#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w16.txt"), TestMethod]
        public void Test_Div_W16()
        {
            // given
            var x = int.Parse(TestContext.DataRow["result"].ToString());
            var y = int.Parse(TestContext.DataRow["x"].ToString());
            var expectedResult = int.Parse(TestContext.DataRow["y"].ToString());

            // when
            this.TestDiv(x, y, expectedResult, 16);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "testData_mult_w16_reduced.txt", "testData_mult_w16_reduced#txt", DataAccessMethod.Sequential), DeploymentItem("TestData//testData_mult_w16_reduced.txt"), TestMethod]
        public void Test_Div_W16_Reduced()
        {
            // given
            var x = int.Parse(TestContext.DataRow["result"].ToString());
            var y = int.Parse(TestContext.DataRow["x"].ToString());
            var expectedResult = int.Parse(TestContext.DataRow["y"].ToString());

            // when
            this.TestDiv(x, y, expectedResult, 16);
        }

        private void TestMul(int x, int y, int expectedResult, int dim)
        {
            // given
            var gmath = GMathFactory.GetInstance(dim);

            // when
            var result = gmath.Mul(x, y);

            // then
            Assert.AreEqual(expectedResult, result);
        }

        private void TestDiv(int x, int y, int expectedResult, int dim)
        {
            try
            {
                var gmath = GMathFactory.GetInstance(dim);
                var result = gmath.Div(x, y);

                if (y != 0)
                {
                    Assert.AreEqual(expectedResult, result);
                }
            }
            catch (DivideByZeroException)
            {
                if (y != 0)
                {
                    Assert.Fail();
                }
            }
        }
    }
}
