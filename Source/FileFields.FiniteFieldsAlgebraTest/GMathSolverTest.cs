using FileFields.FiniteFieldsAlgebra.GMath;
using FileFields.FiniteFieldsAlgebra.GMath.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FiniteFields.FiniteFieldsAlgebraTest
{
    [TestClass]
    public class GMathSolverTest
    {
        private IGMathFactory gmathFactory;
        private TestContext testContextInstance;

        [TestInitialize]
        public void SetUp()
        {
            this.gmathFactory = new GMathFactory();
        }

        public IGMathFactory GMathFactory
        {
            get { return this.gmathFactory; }
        }

        public TestContext TestContext
        {
            get { return this.testContextInstance; }
            set { this.testContextInstance = value; }
        }

        [TestMethod]
        public void Test_Solve_W2()
        {
            // given
            var dim = 2;
            var A = new int[][]
            {
                new [] {0, 1, 0, 0},
                new [] {1, 0, 0, 0},
                new [] {0, 0, 0, 1},
                new [] {0, 0, 1, 0}
            };
            var X = new int[] { 0, 1, 2, 3 };
            var vTools = this.gmathFactory.GetVectorTools(dim);
            var Y = A.Select(r => vTools.SumMul(r, X, 0, X.Length)).ToArray();

            // when
            var solver = this.gmathFactory.GetSolver(dim);
            solver.Solve(A, Y);

            // then
            CollectionAssert.AreEquivalent(X, Y);
        }

        [TestMethod]
        public void Test_Solve_W8()
        {
            // given
            var vectors = new int[][] { new[] { 29, 0, 05 }, new[] { 44, 0, 21 }, new[] { 13, 0, 52 } };

            foreach (var x in vectors)
            {
                // when
                Internal_Test_Solve_W8(x);
            }
        }

        private void Internal_Test_Solve_W8(int[] x)
        {
            // given
            var dim = 6;
            var generator = GMathFactory.GetGenerator(dim);

            var A = new int[][]
            {
                 new int[] {47, 44, 9},
                 new int[] {36, 45, 7},
                 new int[] {54, 33, 23}
            };

            var vTools = this.gmathFactory.GetVectorTools(dim);
            var Y = A.Select(r => vTools.SumMul(r, x, 0, x.Length)).ToArray();

            // when
            var solver = this.gmathFactory.GetSolver(dim);
            solver.Solve(A, Y);

            // then
            CollectionAssert.AreEquivalent(x, Y);
        }

        [TestMethod]
        public void Test_Solve_W16()
        {
            // given
            var dim = 16;
            var seed = 23;
            var generator = GMathFactory.GetGenerator(dim);
            var A = generator.Generate(dim, seed);
            var X = Enumerable.Range(0, dim).ToArray();
            var vTools = this.gmathFactory.GetVectorTools(dim);
            var Y = A.Select(r => vTools.SumMul(r, X, 0, X.Length)).ToArray();

            // when
            var solver = this.gmathFactory.GetSolver(dim);
            solver.Solve(A, Y);

            // then
            CollectionAssert.AreEquivalent(X, Y);
        }
    }
}
