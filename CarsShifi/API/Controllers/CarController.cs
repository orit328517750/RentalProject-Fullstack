using BL.ClassesBL;
using BL.ClassesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/cars")]
    public class CarController:ApiController
    {
        CarsBL cbl = new CarsBL();
        [AcceptVerbs("Get", "Post")]
        [Route("getcarsbyid/carCode")]
        [HttpGet]
        public CarsDTO GetCarById(int carCode)
        {
            return cbl.GetCarById(carCode);
        }

        [Route("add/c")]
        [HttpPost]
        public bool Add(CarsDTO c)
        {
            return cbl.Add(c);
        }

        [Route("update/c")]
        [HttpPost]
        public bool Update(CarsDTO c)
        {
            return cbl.Update(c);
        }

        [Route("delete/c")]
        [HttpPost]
        public bool Delete(CarsDTO c)
        {
            return cbl.Delete(c);
        }

        [Route("getallcars")]
        [HttpGet]
        public List<CarsDTO> GetAllCars()
        {
            return cbl.GetAllCars();
        }

        [Route("getnext")]
        [HttpGet]
        public int GetNext()
        {
            return cbl.GetNext();
        }



        [Route("getcarsbynummekomot/numMekomot")]
        [HttpGet]
        public List<CarsDTO> GetCarByPlace(int numMekomot)
        {
            return cbl.GetCarByPlace(numMekomot);
        }

        [Route("getcarsbyrama/maxRama")]
        [HttpGet]
        public List<CarsDTO> GetCarByLevel(int maxRama)
        {
            return cbl.GetCarByLevel(maxRama);
        }

        [Route("getcarsbypriceperday/maxPrice")]
        [HttpGet]
        public List<CarsDTO> GetCarByPrice(int maxPrice)
        {
            return cbl.GetCarByPrice(maxPrice);
        }

        [Route("getcarsbycriteria/numMekomot/maxRama/maxPrice")]
        [HttpGet]
        public List<CarsDTO> GetCarByPriceandlevelandplace(int numMekomot, int maxRama, int maxPrice)
        {
            return cbl.GetCarByPriceandlevelandplace(numMekomot, maxRama, maxPrice);
        }
    }
}