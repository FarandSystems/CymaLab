using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CymaLAB_Ver_1._0.Enums;

namespace CymaLAB_Ver_1._0
{
    public sealed class Commands
    {
        private readonly Communication communication;

        public Commands(Communication communication)
        {
            this.communication = communication;
        }
    }
}
