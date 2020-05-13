using System;

namespace CoreArk.Packages.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string msg): base(msg)
        {
            
        }

        public AlreadyExistsException(string entity, string property, string value): base($"{entity} with {property} = '{value}' already exists.")
        {
        }
    }
}