using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Infrastructure.Exceptions
{
    public class EntityExistException : Exception
    {
        public EntityExistException(string exception) : base(exception)
        {

        }
    }
}
