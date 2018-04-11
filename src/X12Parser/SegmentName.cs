using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X12Parser
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SegmentName : Attribute
    {
        public string Description {get;private set;}
        public SegmentName(string description)
        {
            Description = description;
        }
    }
}
