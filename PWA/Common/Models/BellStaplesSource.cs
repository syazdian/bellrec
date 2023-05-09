using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell.Reconciliation.Common.Models
{
    public class BellStaplesSource
    {
        public BellSource_new[] BellSources { get; set; }
        public StaplesSource_new[] StaplesSources { get; set; }
    }
}
