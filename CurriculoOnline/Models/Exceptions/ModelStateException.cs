using System;

namespace CurriculoOnline.Models.Exceptions
{
    class ModelStateException : ApplicationException
    {
        public ModelStateException(string message) : base(message) { }
    }
}
