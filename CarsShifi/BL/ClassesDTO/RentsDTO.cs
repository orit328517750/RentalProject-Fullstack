using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ClassesDTO
{
    public class RentsDTO
    {
        public int rentCode { get; set; }
        public int customerId { get; set; }
        public int carCode { get; set; }
        public System.DateTime beginDate { get; set; }
        public System.DateTime finishDate { get; set; }
        public string rentGoal { get; set; }

        public virtual Cars Cars { get; set; }
        public virtual Customers Customers { get; set; }
    }
}
