using BL.ClassesDTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.DBConection;

namespace BL.ClassesBL
{
    public class PayMentsBL
    {
        DBConection conn = new DBConection();

        public bool Add(PayMentsDTO cdt)
        {
            try
            {
                PayMents c1 = new PayMents();

                // חשוב: אנחנו לא מעתיקים את paymentCode!
                // ה-SQL יוצר אותו לבד כי הגדרנו IDENTITY
                c1.creditCard = cdt.creditCard;
                c1.validit = cdt.validit;
                c1.cvc = System.Convert.ToInt32(cdt.cvc);

                conn.Execute<PayMents>(c1, ExecuteActions.Insert);
                return true;
            }
            catch (Exception ex)
            {
                // בזמן פיתוח השורה הזו תעזור לך לראות שגיאות ב-Debug
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public PayMentsDTO Convert(PayMents c)
        {
            PayMentsDTO cdt = new PayMentsDTO();
            cdt.paymentCode = c.paymentCode;
            cdt.creditCard = c.creditCard;
            cdt.validit = c.validit;
            cdt.cvc = c.cvc;
            return cdt;
        }

        public List<PayMentsDTO> Convert(List<PayMents> list)
        {
            List<PayMentsDTO> lcdt = new List<PayMentsDTO>();
            if (list == null) return lcdt;
            foreach (PayMents c in list) lcdt.Add(Convert(c));
            return lcdt;
        }

        public List<PayMentsDTO> GetCustomers()
        {
            return Convert(conn.GetDbSet<PayMents>());
        }
    }
}