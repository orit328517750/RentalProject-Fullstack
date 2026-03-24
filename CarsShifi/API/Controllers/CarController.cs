using BL.ClassesBL;
using BL.ClassesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [RoutePrefix("api/cars")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CarController : ApiController
    {
        CarsBL cbl = new CarsBL();

        // קבלת רכב לפי קוד
        [Route("getcarsbyid/{carCode}")]
        [HttpGet]
        public CarsDTO GetCarById(int carCode)
        {
            return cbl.GetCarById(carCode);
        }

        // הוספת רכב
        [Route("add")]
        [HttpPost]
        public bool Add(CarsDTO c)
        {
            return cbl.Add(c);
        }

        // עדכון רכב
        [Route("update")]
        [HttpPost]
        public bool Update(CarsDTO c)
        {
            return cbl.Update(c);
        }

        // מחיקת רכב
        [Route("delete")]
        [HttpPost]
        public bool Delete(CarsDTO c)
        {
            return cbl.Delete(c);
        }

        // קבלת כל הרכבים
        [Route("getallcars")]
        [HttpGet]
        public List<CarsDTO> GetAllCars()
        {
            return cbl.GetAllCars();
        }

        // סינון משולב - התיקון הקריטי כאן בסוגריים המסולסלים
        [Route("getcarsbycriteria/{numMekomot}/{maxRama}/{maxPrice}")]
        [HttpGet]
        public List<CarsDTO> GetCarByPriceandlevelandplace(int numMekomot, int maxRama, int maxPrice)
        {
            return cbl.GetCarByPriceandlevelandplace(numMekomot, maxRama, maxPrice);
        }

        // פונקציות סינון בודדות (אם תצטרכי בעתיד)
        [Route("getcarsbynummekomot/{numMekomot}")]
        [HttpGet]
        public List<CarsDTO> GetCarByPlace(int numMekomot)
        {
            return cbl.GetCarByPlace(numMekomot);
        }

        [Route("getcarsbyrama/{maxRama}")]
        [HttpGet]
        public List<CarsDTO> GetCarByLevel(int maxRama)
        {
            return cbl.GetCarByLevel(maxRama);
        }

        [Route("getcarsbypriceperday/{maxPrice}")]
        [HttpGet]
        public List<CarsDTO> GetCarByPrice(int maxPrice)
        {
            return cbl.GetCarByPrice(maxPrice);
        }
    }
}