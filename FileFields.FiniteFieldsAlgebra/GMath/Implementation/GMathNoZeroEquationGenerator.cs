using System;
using System.Linq;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathNoZeroEquationGenerator : IEquationGenerator
    {
        private readonly IGMath _gmath;
        private readonly int _maxValue;

        public GMathNoZeroEquationGenerator(IGMath gmath)
        {
            _gmath = gmath;
            _maxValue = (1 << _gmath.Dim);
        }

        public int Dim { get { return _gmath.Dim; } }

        public int[][] Generate(int size)
        {
            return Generate(size, (int) DateTime.Now.ToBinary());
        }

        private void CheckSize(int size) 
        {
            if (size < 1 || size > (_maxValue - 1))
            {
                throw new ArgumentException("size");
            }
        }

        /// <summary>
        /// Thread safe
        /// </summary>
        public int[][] Generate(int size, int seed)
        {
            CheckSize(size);

            Random random = new Random(seed);

            int[][] equations = new int[size][];

            int[] pattern = new int[size];
            for (int i = 0; i < size; i++)
            {
                pattern[i] = random.Next(1, _maxValue);
            }
            
            equations[0] = pattern;
            for (int i = 1; i < size; i++)
            {
                equations[i] = pattern.ToArray();
                equations[i][i] = GenerateValue(random, pattern[i]);
            }

            return equations;
        }

        private int GenerateValue(Random random, int forbiddenNumber)
        {
            int value = random.Next(1, Dim - 1);

            while (value == forbiddenNumber)
            {
                value = random.Next(1, Dim - 1);
            }

            return value;
        }


        /// <summary>
        /// Thread safe
        /// </summary>
        public int[] GenerateEquation(int size, int seed)
        {
            //TODO: TEST
            //CheckSize(size);

            Random random = new Random(seed);

            int[] pattern = new int[size];
            for (int i = 0; i < size; i++)
            {
                pattern[i] = random.Next(1, _maxValue);
            }

            return pattern;
        }
    }
}
