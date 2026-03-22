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
    public class CitiesBL
    {
        DBConection conn = new DBConection();
        //לפי הID
        public CitiesDTO GetCityById(int id)
        {
            List<CitiesDTO> list = Convert(conn.GetDbSet<Cities>());
            return list.FirstOrDefault(c => c.cityCode == id);
        }
        //הוספה
        public bool Add(CitiesDTO c)
        {
            Cities c1 = Convert(c);
            try
            {
                conn.Execute<Cities>(c1, ExecuteActions.Insert);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //עידכון
        public bool Update(CitiesDTO c)
        {
            Cities c1 = Convert(c);
            try
            {
                conn.Execute<Cities>(c1, ExecuteActions.Update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //מחיקה
        public bool Delete(CitiesDTO c)
        {
            Cities c1 = Convert(c);
            try
            {
                conn.Execute<Cities>(c1, ExecuteActions.Delete);
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
            int code = conn.GetDbSet<Cities>().Count();
            return ++code;
        }
        //את הכל
        public List<CitiesDTO> GetCity()
        {
            List<CitiesDTO> list = Convert(conn.GetDbSet<Cities>());
            return list;
        }
        public CitiesDTO Convert(Cities c)
        {
            CitiesDTO cdt = new CitiesDTO();
            cdt.cityCode = c.cityCode;
            cdt.cityName = c.cityName;

            return cdt;
        }
        public Cities Convert(CitiesDTO cdt)
        {
            Cities c = new Cities();

            c.cityCode = cdt.cityCode;
            c.cityName = cdt.cityName;   
            return c;

        }
        public List<CitiesDTO> Convert(List<Cities> list)
        {
            List<CitiesDTO> lcdt = new List<CitiesDTO>();
            foreach (Cities c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
        public List<Cities> Convert(List<CitiesDTO> list)
        {
            List<Cities> lcdt = new List<Cities>();
            foreach (CitiesDTO c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
    }
}
