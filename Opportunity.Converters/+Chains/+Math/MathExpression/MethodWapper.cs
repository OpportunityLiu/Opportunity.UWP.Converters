using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.MathExpression
{
    internal struct MethodWapper
    {
        public object Instance { get; }
        public MethodInfo Method { get; }

        public MethodWapper(object instance,MethodInfo method)
        {
            this.Instance = instance;
            this.Method = method;
        }
    }
}
