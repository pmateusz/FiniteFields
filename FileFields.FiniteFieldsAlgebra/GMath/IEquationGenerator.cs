
namespace FileFields.FiniteFieldsAlgebra.GMath
{
    public interface IEquationGenerator
    {
        int[][] Generate(int size);
        int[][] Generate(int size, int seed);
        int[] GenerateEquation(int size, int seed);
    }
}
