using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDLog
    {
        void LogException(string methodName, string exceptionMessage, string stackTrace, string userName);
    }
}
