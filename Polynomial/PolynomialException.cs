using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialNamespace
{
        [Serializable]
        public class NullPolynomialInitializationListException : Exception
        {
            public NullPolynomialInitializationListException() { }
            public NullPolynomialInitializationListException(string message) : base(message) { }
            public NullPolynomialInitializationListException(string message, Exception inner) : base(message, inner) { }
            protected NullPolynomialInitializationListException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
}
