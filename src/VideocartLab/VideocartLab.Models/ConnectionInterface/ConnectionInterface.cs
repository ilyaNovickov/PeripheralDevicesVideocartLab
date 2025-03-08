using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Models.ConnectionInterface
{
    public abstract class ConnectionInterface
    {
        public abstract int Bandwidth
        {
            get;
        }
    }
}
