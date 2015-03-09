using FileFields.FiniteFieldsAlgebra.GMath.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FiniteFields.FiniteFieldsAlgebraTest
{
    [TestClass]
    public class GTableBuilderTest
    {
        [TestMethod]
        [DeploymentItem("./TestData/log_w2.ff", "TestData")]
        [DeploymentItem("./TestData/ilog_w2.ff", "TestData")]
        public void Test_CreateLogTablesInMemory_W2()
        {
            TestLogTableInMemory(2, "./TestData/log_w2.ff", "./TestData/ilog_w2.ff");
        }

        [TestMethod]
        [DeploymentItem("./TestData/log_w4.ff", "TestData")]
        [DeploymentItem("./TestData/ilog_w4.ff", "TestData")]
        public void Test_CreateLogTablesInMemory_W4()
        {
            TestLogTableInMemory(4, "./TestData/log_w4.ff", "./TestData/ilog_w4.ff");
        }

        [TestMethod]
        [DeploymentItem("./TestData/log_w8.ff", "TestData")]
        [DeploymentItem("./TestData/ilog_w8.ff", "TestData")]
        public void Test_CreateLogTablesInMemory_W8()
        {
            TestLogTableInMemory(8, "./TestData/log_w8.ff", "./TestData/ilog_w8.ff");
        }

        [TestMethod]
        [DeploymentItem("./TestData/log_w16.ff", "TestData")]
        [DeploymentItem("./TestData/ilog_w16.ff", "TestData")]
        public void Test_CreateLogTablesInMemory_W16()
        {
            TestLogTableInMemory(16, "./TestData/log_w16.ff", "./TestData/ilog_w16.ff");
        }

        [TestMethod]
        [DeploymentItem("./TestData/mult_w2.ff", "TestData")]
        [DeploymentItem("./TestData/div_w2.ff", "TestData")]
        public void Test_CreateMultTablesInMemory_W2()
        {
            TestMultTableInMemory(2, "./TestData/mult_w2.ff", "./TestData/div_w2.ff");
        }

        [TestMethod]
        [DeploymentItem("./TestData/mult_w4.ff", "TestData")]
        [DeploymentItem("./TestData/div_w4.ff", "TestData")]
        public void Test_CreateMultTablesInMemory_W4()
        {
            TestMultTableInMemory(4, "./TestData/mult_w4.ff", "./TestData/div_w4.ff");
        }

        [TestMethod]
        [DeploymentItem("./TestData/mult_w8.ff", "TestData")]
        [DeploymentItem("./TestData/div_w8.ff", "TestData")]
        public void Test_CreateMultTablesInMemory_W8()
        {
            TestMultTableInMemory(8, "./TestData/mult_w8.ff", "./TestData/div_w8.ff");
        }

        private void TestLogTableInMemory(int dim, string mulTableFilepath, string divTableFilepath)
        {
            var expectedLogTable = LoadTableFromFile(mulTableFilepath);
            var expectedILogTable = LoadTableFromFile(divTableFilepath);
            int[] logTable = null;
            int[] ilogTable = null;

            var builder = new GTableBuilder();
            builder.CreateLogTablesInMemory(dim, out logTable, out ilogTable);

            CollectionAssert.AreEquivalent(expectedLogTable, logTable);
            CollectionAssert.AreEquivalent(expectedILogTable, ilogTable);
        }

        private static void TestMultTableInMemory(int dim, string mulTableFilepath, string divTableFilepath)
        {
            var expectedMultTable = LoadTableFromFile(mulTableFilepath);
            var expectedDivTable = LoadTableFromFile(divTableFilepath);
            int[] multTable = null;
            int[] divTable = null;

            var builder = new GTableBuilder();
            builder.CreateMultTablesInMemory(dim, out multTable, out divTable);

            CollectionAssert.AreEquivalent(expectedMultTable, multTable);
        }

        private static int[] LoadTableFromFile(string filePath)
        {
            using (var fStream = File.OpenRead(filePath))
            using (var mStream = new MemoryStream())
            {
                var rBytes = 0;
                var block = new byte[4096];

                while ((rBytes = fStream.Read(block, 0, block.Length)) != 0)
                {
                    mStream.Write(block, 0, rBytes);
                }

                var bArray = mStream.ToArray();
                var arraySize = bArray.Length / sizeof(int);
                var iArray = new int[arraySize];

                for (var i = 0; i < iArray.Length; i++)
                {
                    iArray[i] = BitConverter.ToInt32(bArray, 4 * i);
                }

                return iArray;
            }
        }
    }
}
