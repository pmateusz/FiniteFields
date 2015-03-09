using System;

namespace FileFields.FiniteFieldsAlgebra.GMath
{
    [Serializable]
    public class GMathException : Exception
    {
        public GMathException(string message) : base(message) { }
    }
}
