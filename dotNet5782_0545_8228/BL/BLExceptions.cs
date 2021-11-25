using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// This Exception class is meant for when an invalid DalObject is selected.
        /// </summary>
        public class InvalidBlObjectException : Exception
        {
            public InvalidBlObjectException(string message)
                : base(message) { }
            public InvalidBlObjectException() : base() { }
        }


        /// <summary>
        /// Dedicated exception class meant for when there is an index out of bounds on accessing a DalObject
        /// </summary>
        public class BlObjectAccessException : Exception
        {
            public BlObjectAccessException(string message) : base(message) { }
            public BlObjectAccessException() : base() { }
        }

        public class InvalidIDException : Exception
        {
            public InvalidIDException(string message) : base(message) {}
            public InvalidIDException() : base() {}
        }

        public class OperationNotPossibleException : Exception
        {
            public OperationNotPossibleException(string message) : base(message) {}
            public OperationNotPossibleException() : base() {}
        }
    }
}
