using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DifferentMatrixSizeException : Exception
    {
        public DifferentMatrixSizeException() { }
        public DifferentMatrixSizeException(string message) : base(message) { }
        public DifferentMatrixSizeException(string message, Exception inner) : base(message, inner) { }
        protected DifferentMatrixSizeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class InitializeIndexOutOfRangeException : Exception
    {
        public InitializeIndexOutOfRangeException() { }
        public InitializeIndexOutOfRangeException(string message) : base(message) { }
        public InitializeIndexOutOfRangeException(string message, Exception inner) : base(message, inner) { }
        protected InitializeIndexOutOfRangeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
