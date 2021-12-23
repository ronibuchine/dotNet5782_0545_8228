using System;


namespace BL
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

    /// <summary>
    /// An exception class used for when ID validation fails.
    /// </summary>
    public class InvalidIDException : Exception
    {
        public InvalidIDException(string message) : base(message) {}
        public InvalidIDException() : base() {}
    }

    /// <summary>
    /// An exception class which is thrown when an invalid operation is attempted.
    /// Examples of this are sending a drone for delivery when there is not enough battery or releasing from charge when it is not in maintenance.
    /// </summary>
    public class OperationNotPossibleException : Exception
    {
        public OperationNotPossibleException(string message) : base(message) {}
        public OperationNotPossibleException() : base() {}
    }
}

