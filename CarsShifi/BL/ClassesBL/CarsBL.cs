using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.ClassesDTO;
using DAL;
using static DAL.DBConection;

namespace BL.ClassesBL
{
    public class CarsBL
    {
        DBConection conn=new DBConection();
        //לפי הID
        public CarsDTO GetCarById(int id)
        {
            List<CarsDTO> list= Convert(conn.GetDbSet<Cars>());
            return list.FirstOrDefault(c=>c.carCode == id);
        }
        //הוספה
        public bool Add(CarsDTO c)
        {
            Cars c1 = Convert(c);
            try
            {
                conn.Execute<Cars>(c1, ExecuteActions.Insert);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //עידכון
        public bool Update(CarsDTO c)
        {
            Cars c1 = Convert(c);
            try
            {
                conn.Execute<Cars>(c1, ExecuteActions.Update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //מחיקה
        public bool Delete(CarsDTO c)
        {
            Cars c1 = Convert(c);
            try
            {
                conn.Execute<Cars>(c1, ExecuteActions.Delete);
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
            int code=conn.GetDbSet<Cars>().Count();
            return ++code;
        }
        //את הכל
        public List<CarsDTO> GetAllCars()
        {
            List<CarsDTO> list = Convert(conn.GetDbSet<Cars>());
            return list;
        }
        //קבלת רשימת רכבים ע"פ מס' המקומות שהתקבל כפרמטר
        public List<CarsDTO> GetCarByPlace(int place)
        {
            List<CarsDTO> list = Convert(conn.GetDbSet<Cars>()).ToList();
            return list.FindAll(Cars=>Cars.numPlace==place).ToList();
        }
        //קבלת רשימת רכבים ע"פ רמה )עד רמה זו(
        public List<CarsDTO> GetCarByLevel(int level)
        {
            List<CarsDTO> list = Convert(conn.GetDbSet<Cars>()).ToList();
            return list.FindAll(Cars => Cars.level <= level).ToList();
        }
        //קבלת רשימת רכבים ע"פ מחיר ליום )עד מחיר זה(
        public List<CarsDTO> GetCarByPrice(int price)
        {
            List<CarsDTO> list = Convert(conn.GetDbSet<Cars>()).ToList();
            return list.FindAll(Cars => Cars.priceOfDay<= price).ToList();
        }
        //קבלת רשימת רכבים ע"פ 3 הקריטריונים הנ"ל
        // קבלת רשימת רכבים ע"פ 3 הקריטריונים הנ"ל
        // שים לב: שיניתי את סדר הפרמטרים כדי שיתאים ל-Controller ול-Angular
        public List<CarsDTO> GetCarByPriceandlevelandplace(int numMekomot, int maxRama, int maxPrice)
        {
            // טעינת כל הרכבים והמרתם ל-DTO
            List<CarsDTO> list = Convert(conn.GetDbSet<Cars>()).ToList();

            // ביצוע הסינון
            return list.FindAll(car =>
                car.priceOfDay <= maxPrice &&    // מחיר עד המקסימום
                car.level <= maxRama &&          // רמה עד המקסימום
                car.numPlace >= numMekomot       // לפחות מספר המקומות המבוקש (או == אם את רוצה בדיוק)
            ).ToList();
        }
        public CarsDTO Convert(Cars c)
        {
            CarsDTO cdt = new CarsDTO();
            cdt.carCode = c.carCode;
            cdt.numPlace = c.numPlace;
            cdt.level = c.level;
            cdt.priceOfDay = c.priceOfDay;
            cdt.priceOfWeek = c.priceOfWeek;
            return cdt;
        }
        public Cars Convert(CarsDTO cdt)
        {
            Cars c = new Cars();

            c.carCode = cdt.carCode;
            c.numPlace = cdt.numPlace;
            c.level = cdt.level;
            c.priceOfDay = cdt.priceOfDay;
            c.priceOfWeek = cdt.priceOfWeek;
            return c;

        }
        public List<CarsDTO> Convert(List<Cars> list)
        {
            List<CarsDTO> lcdt = new List<CarsDTO>();
            foreach (Cars c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }
        public List<Cars> Convert(List<CarsDTO> list)
        {
            List<Cars> lcdt = new List<Cars>();
            foreach (CarsDTO c in list)
            {
                lcdt.Add(Convert(c));
            }
            return lcdt;
        }


    }
}
