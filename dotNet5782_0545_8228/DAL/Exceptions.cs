using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public class InvalidDalObjectException : Exception
        {
            public InvalidDalObjectException(string message)
                : base(message) { }
            public InvalidDalObjectException() : base() { }
        }

        public class DataSourceException : Exception
        {
            public DataSourceException(string message) : base(message) { }
            public DataSourceException() : base() { }
        }

        public class DalObjectAccessException : Exception
        {
            public DalObjectAccessException(string message) : base(message) { }
            public DalObjectAccessException() : base() { }
        }
    }
}
