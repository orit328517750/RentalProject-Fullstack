using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ClassesDTO
{
    public class CarsDTO
    {
        public int carCode { get; set; }
        public int numPlace { get; set; }
        public int level { get; set; }
        public int priceOfDay { get; set; }
        public int priceOfWeek { get; set; }
        public string carName { get; set; }

    }
}
