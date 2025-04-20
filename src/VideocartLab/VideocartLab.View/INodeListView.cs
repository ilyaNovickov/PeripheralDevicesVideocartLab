using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.View
{
    public interface INodeListView
    {
        public IEnumerable? ItemsList { get; set; }
    }
}
