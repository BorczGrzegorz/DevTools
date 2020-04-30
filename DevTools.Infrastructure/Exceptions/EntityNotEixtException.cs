using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Infrastructure.Exceptions
{
    public class EntityNotEixtException : Exception
    {
        public EntityNotEixtException(string message) : base(message)
        {

        }
    }
}
