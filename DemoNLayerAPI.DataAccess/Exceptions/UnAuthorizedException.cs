using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException()
        {
            
        }
        public UnAuthorizedException(string message):base(message) { }

        public UnAuthorizedException(string message, Exception ex): base(message, ex) { }


    }
}
