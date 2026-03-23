using BL.ClassesDTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.DBConection;


namespace BL.ClassesBL
{
    public class CustomersBL
    {
        DBConection conn = new DBConection();
        //לפי הID
        public CustomersDTO GetCustomersById(int id)
        {
            List<CustomersDTO> list = Convert(conn.GetDbSet<Customers>());
            return list.FirstOrDefault(c => c.Id == id);
        }
        //הוספה
        public bool Add(CustomersDTO c)
        {
            Customers c1 = Convert(c);
            try
            {
                conn.Execute<Customers>(c1, ExecuteActions.Insert);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //עידכון
        public bool Update(CustomersDTO c)
        {
            Customers c1 = Convert(c);
            try
            {
                conn.Execute<Customers>(c1, ExecuteActions.Update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //מחיקה
        public bool Delete(CustomersDTO c)
        {
            Customers c1 = Convert(c);
            try
            {
                conn.Execute<Customers>(c1, ExecuteActions.Delete);
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
            int code = conn.GetDbSet<Customers>().Count();
            return ++code;
        }
        //את הכל
        public List<CustomersDTO> GetCustomers()
        {
            List<CustomersDTO> list = Convert(conn.GetDbSet<Customers>());
            return list;
        }
        //מחזיר רשימה ממוינת
        public List<CustomersDTO> GetCustomerBySort()
        {
            List<CustomersDTO> list = Convert(conn.GetDbSet<Customers>().ToList());
            return list.OrderBy(Customers => Customers.firstName).ThenBy(Customers => Customers.lastName).ToList();
        }
        //שאני יקבל לקוח לפי תעודת זהות
        //public CustomersDTO GetCustomerByIdentityNumber(int identityNumber)
        //{
        //    List<CustomersDTO> list = Convert(conn.GetDbSet<Costomers>());
        //    return list.FirstOrDefault(c => c.misHascarot == identityNumber);
        //}

        //קבלץ 3 לקוחות הוותקים ביותר
        public List<CustomersDTO> GetThreeOld()
        {
            List<CustomersDTO> list = Convert(conn.GetDbSet<Customers>().ToList());
            return list.OrderBy(Customers => Customers.numOfLending).Take(3).ToList();

        }
        //קבלת כל הלקוחות מעיר מסוימת
        public List<CustomersDTO> GetCustomerByCity(int cityCode)
        {
            List<CustomersDTO> list = Convert(conn.GetDbSet<Customers>().ToList());
            return list.OrderBy(Customers => Customers.idCity == cityCode).ToList();

        }
        //קבלת פרטי התשלום של לקוח ע"י ת.זהות
        public PayMentsDTO GetCustomerPayMent(int id)
        {
            PayMentsBL payMentsBL = new PayMentsBL();
            List<CustomersDTO> list = Convert(conn.GetDbSet<Customers>().ToList());
            CustomersDTO c = list.FirstOrDefault(Customers => Customers.Id == id);
            PayMentsDTO payMent = payMentsBL.Convert(c.PayMents);
            return payMent;
        }

        public CustomersDTO Convert(Customers c)
        {
            CustomersDTO cdt = new CustomersDTO();
            cdt.Id = c.Id;
            cdt.firstName = c.firstName;
            cdt.lastName = c.lastName;
            cdt.email = c.email;
            cdt.idCity = c.idCity;
            cdt.numOfLending = c.numOfLending;
            cdt.codePayment = c.codePayment;
            return cdt;
        }
        public Customers Convert(CustomersDTO cdt)
        {
            Customers c = new Customers();

            c.Id = cdt.Id;
            c.firstName = cdt.firstName;
            c.lastName = cdt.lastName;
            c.email = cdt.email;
            c.idCity = cdt.idCity;
            c.numOfLending = cdt.numOfLending;
            c.codePayment = cdt.codePayment;
            return c;

        }
        public List<CustomersDTO> Convert(List<Customers> list)
        {
            List<CustomersDTO> lcdt = new List<CustomersDTO>();
            foreach (Customers c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
        public List<Customers> Convert(List<CustomersDTO> list)
        {
            List<Customers> lcdt = new List<Customers>();
            foreach (CustomersDTO c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }





        public string InsertClient(CustomersDTO clients)
        {
            try
            {
                // כאן ה-BL משתמש ב-Convert כדי לשמור ב-DAL
                conn.Execute<Customers>(Convert(clients), DBConection.ExecuteActions.Insert);
                return clients.firstName;
            }
            catch (Exception ex)
            {
                // זה יחפור פנימה ויביא את השגיאה האמיתית מה-SQL
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += " -> " + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        message += " -> " + ex.InnerException.InnerException.Message;
                }
                return "שגיאת SQL עמוקה: " + message;
            }
        }

        public CustomersDTO Customerbyid(int id)
        {
            var customer = conn.GetDbSet<Customers>().FirstOrDefault(x => x.Id == id);
            if (customer == null) return null;
            return Convert(customer);
        }

    }



}

