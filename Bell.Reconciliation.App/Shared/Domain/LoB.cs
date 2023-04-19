using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell.Reconciliation.App.Shared
{
    public class LoB
    {
        public string Name { get; set; }
        public List<string> SubLoBs { get; set; }
    }
}