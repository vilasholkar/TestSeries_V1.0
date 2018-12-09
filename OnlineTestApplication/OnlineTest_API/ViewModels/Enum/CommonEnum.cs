using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CommonEnum
    {
        public enum Status
        {
            Success,
            Failed
        }
        public enum TestStatus
        {
            NotStarted = 1,
            Started = 2,
            Completed = 3
        }
    }
}
