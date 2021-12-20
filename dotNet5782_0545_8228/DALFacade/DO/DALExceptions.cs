using System;


namespace DO
{
    /// <summary>
    /// This Exception class is meant for when an invalid DalObject is selected to be constructed.
    /// </summary>
    public class InvalidDalObjectException : Exception
    {
        public InvalidDalObjectException(string message)
            : base(message) { }
        public InvalidDalObjectException() : base() { }
    }

    /// <summary>
    /// This exception class is meant for when adding to the Data layer but there is no more memory for it.
    /// </summary>
    public class DataSourceException : Exception
    {
        public DataSourceException(string message) : base(message) { }
        public DataSourceException() : base() { }
    }

    /// <summary>
    /// Dedicated exception class meant for when there is an index out of bounds on accessing a DalObject
    /// </summary>
    public class DalObjectAccessException : Exception
    {
        public DalObjectAccessException(string message) : base(message) { }
        public DalObjectAccessException() : base() { }
    }
}

