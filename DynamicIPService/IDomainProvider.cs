using DynamicIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicIPService
{
    public interface IDomainProvider
    {
        void UpdateIP(DynamicIPConfig config);
    }
}
