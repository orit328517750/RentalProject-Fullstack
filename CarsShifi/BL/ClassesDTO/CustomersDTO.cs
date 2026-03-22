using BL.ClassesBL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ClassesDTO
{
    public class CustomersDTO
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int idCity { get; set; }
        public string email { get; set; }
        public int numOfLending { get; set; }
        public int codePayment { get; set; }

        public virtual Cities Cities { get; set; }
        public virtual PayMents PayMents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }







      
    }
}
