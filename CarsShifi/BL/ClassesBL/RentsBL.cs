using BL.ClassesDTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.DBConection;

namespace BL.ClassesBL
{
    public class RentsBL
    {
        DBConection conn = new DBConection();
        //לפי הID
        public RentsDTO GetRentsById(int id)
        {
            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list.FirstOrDefault(c => c.rensCode == id);
        }
        //הוספה
        public bool Add(RentsDTO c)
        {
            Rents c1 = Convert(c);
            try
            {
                conn.Execute<Rents>(c1, ExecuteActions.Insert);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //עידכון
        public bool Update(RentsDTO c)
        {
            Rents c1 = Convert(c);
            try
            {
                conn.Execute<Rents>(c1, ExecuteActions.Update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //מחיקה
        public bool Delete(RentsDTO c)
        {
            Rents c1 = Convert(c);
            try
            {
                conn.Execute<Rents>(c1, ExecuteActions.Delete);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //קבלץ הקוד הבא
        public int GetNext()
        {
            int code = conn.GetDbSet<Rents>().Count();
            return ++code;
        }
        //את הכל
        public List<RentsDTO> GetRents()
        {
            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list;
        }
        //קבלת ההשכרות מהשבוע האחרון
        public List<RentsDTO> GetRentsfromWeeek()
        {
            DateTime x = DateTime.Now;
            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list.FindAll(r=>r.beginDate.Date>=x.AddDays(-7).Date && r.beginDate.Date<=x).ToList();
        }
        //קבלת ההשכרות מהחודש האחרון
        public List<RentsDTO> GetRentsFromLastMonth()
        {
            DateTime x = DateTime.Now;
            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list.FindAll(r => r.beginDate.Date >= x.AddDays(-28).Date && r.beginDate.Date <= x).ToList();
        }
        //קבלת ההשכרות שמתחילות היום
        public List<RentsDTO> GetRentsStartToday()
        {
            DateTime x = DateTime.Now;
            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list.FindAll(r => r.beginDate.Date == x.Date).ToList();
        }
        //קבלת ההשכרות שמתחילות בתאריך מסוים
        public List<RentsDTO> GetRentsStartDate(DateTime datetime)
        {
            
            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list.FindAll(r => r.beginDate.Date == datetime.Date).ToList();
        }
        //קבלת הרכבים שחוזרים בתאריך מסוים
        public List<RentsDTO> GetRentsBackDate(DateTime datetime)
        {

            List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
            return list.FindAll(r => r.finishDate.Date == datetime.Date).ToList();
        }
        //קבלת רשימת רכבים פנויים מתאריך מסוים ועד לתאריך שני
        public List<CarsDTO> GetAvailableCarsBetweenDates(DateTime startDate, DateTime endDate)
        {
            CarsBL cbl = new CarsBL();
            List<RentsDTO> rents = Convert(conn.GetDbSet<Rents>());
            List<CarsDTO> allCars = cbl.Convert(conn.GetDbSet<Cars>());

            var unavailableCars = rents
                .Where(r => (r.beginDate< endDate && r.finishDate > startDate))
                .Select(r => r.carCode)
                .Distinct()
                .ToList();

            return allCars.Where(car => !unavailableCars.Contains(car.carCode)).ToList();
        }

        //קבלת הרכבים שחוזרים בתאריך מסוים
        //public List<RentsDTO> GetRentsBackDateandStart(DateTime datetime1, DateTime datetime2)
        //{

        //    List<RentsDTO> list = Convert(conn.GetDbSet<Rents>());
        //    return list.FindAll(r => r.finishDate.Date == datetime.Date).ToList();
        //}

        public List<RentsDTO> GetAllRentsSortedByStartDate()
        {
            List<RentsDTO> rents = Convert(conn.GetDbSet<Rents>());
            return rents.OrderBy(r => r.beginDate).ToList();
        }
        public List<RentsDTO> GetRentsByCustomerId(int customerId)
        {
            List<RentsDTO> rents = Convert(conn.GetDbSet<Rents>());
            return rents.Where(r => r.customerId == customerId).ToList();
        }

        public List<RentsDTO> GetRentsByCarCode(int carCode)
        {
            List<RentsDTO> rents = Convert(conn.GetDbSet<Rents>());
            return rents.Where(r => r.carCode == carCode).ToList();
        }
    
        public RentsDTO Convert(Rents c)
        {
            RentsDTO cdt = new RentsDTO();
            cdt.rensCode = c.rensCode;
            cdt.customerId = c.customerId;
            cdt.carCode = c.carCode;
            cdt.beginDate = c.beginDate;
            cdt.finishDate = c.finishDate;
            cdt.rentGoal = c.rentGoal;

            return cdt;
        }
        public Rents Convert(RentsDTO cdt)
        {
            Rents c = new Rents();

            c.rensCode = cdt.rensCode;
            c.customerId = cdt.customerId;
            c.carCode = cdt.carCode;
            c.beginDate = cdt.beginDate;
            c.finishDate = cdt.finishDate;
            c.rentGoal = cdt.rentGoal;
            return c;

        }
        public List<RentsDTO> Convert(List<Rents> list)
        {
            List<RentsDTO> lcdt = new List<RentsDTO>();
            foreach (Rents c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
        public List<Rents> Convert(List<RentsDTO> list)
        {
            List<Rents> lcdt = new List<Rents>();
            foreach (RentsDTO c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
    }
}
