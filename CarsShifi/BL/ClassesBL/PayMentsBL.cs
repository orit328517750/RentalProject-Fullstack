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
    public class PayMentsBL
    {

        DBConection conn = new DBConection();
        //לפי הID
        public PayMentsDTO GetCityById(int id)
        {
            List<PayMentsDTO> list = Convert(conn.GetDbSet<PayMents>());
            return list.FirstOrDefault(c => c.paymentCode == id);
        }
        //הוספה
        public bool Add(PayMentsDTO c)
        {
            PayMents c1 = Convert(c);
            try
            {
                conn.Execute<PayMents>(c1, ExecuteActions.Insert);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //עידכון
        public bool Update(PayMentsDTO c)
        {
            PayMents c1 = Convert(c);
            try
            {
                conn.Execute<PayMents>(c1, ExecuteActions.Update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //מחיקה
        public bool Delete(PayMentsDTO c)
        {
            PayMents c1 = Convert(c);
            try
            {
                conn.Execute<PayMents>(c1, ExecuteActions.Delete);
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
            int code = conn.GetDbSet<PayMents>().Count();
            return ++code;
        }
        //את הכל
        public List<PayMentsDTO> GetCustomers()
        {
            List<PayMentsDTO> list = Convert(conn.GetDbSet<PayMents>());
            return list;
        }
        public PayMentsDTO Convert(PayMents c)
        {
            PayMentsDTO cdt = new PayMentsDTO();
            cdt.paymentCode = c.paymentCode;
            cdt.creditCard= c.creditCard;
            cdt.validit = c.validit;
            cdt.cvc = c.cvc;
            return cdt;
        }
        public PayMents Convert(PayMentsDTO cdt)
        {
            PayMents c = new PayMents();

            c.paymentCode = cdt.paymentCode;
            c.creditCard = cdt.creditCard;
            c.validit = cdt.validit;
            c.cvc = cdt.cvc;
            return c;

        }
        public List<PayMentsDTO> Convert(List<PayMents> list)
        {
            List<PayMentsDTO> lcdt = new List<PayMentsDTO>();
            foreach (PayMents c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
        public List<PayMents> Convert(List<PayMentsDTO> list)
        {
            List<PayMents> lcdt = new List<PayMents>();
            foreach (PayMentsDTO c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
    }
}
