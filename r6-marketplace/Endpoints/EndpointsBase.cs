using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public abstract class EndpointsBase
    {
        internal Web web;
        internal EndpointsBase(Web web)
        {
            this.web = web;
        }
    }
}
