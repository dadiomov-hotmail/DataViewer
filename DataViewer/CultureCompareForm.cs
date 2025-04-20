using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewer
{
    public class CultureCompareForm : CompareForm
    {
        public CultureCompareForm(string location) : base(location, "CultureDistances.csv")
        {
        }
    }
}
