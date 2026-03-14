using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.GCommon.Exceptions
{
    public class DatabaseEntityCreatePersistFailure : Exception
    {
        public DatabaseEntityCreatePersistFailure() { }
        public DatabaseEntityCreatePersistFailure(string message)
            : base(message)
        {
        }
    }
}
