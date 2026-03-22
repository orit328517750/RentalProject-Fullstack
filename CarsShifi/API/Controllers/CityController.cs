using BL.ClassesBL;
using BL.ClassesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/city")]
    public class CityController:ApiController
    {
        CitiesBL cbl = new CitiesBL();
        [AcceptVerbs("Get", "Post")]
        [Route("getcitybyid/cityCode")]
        [HttpGet]
        public CitiesDTO GetCityById(int cityCode)
        {
            return cbl.GetCityById(cityCode);
        }

        [Route("add/c")]
        [HttpPost]
        public bool Add(CitiesDTO c)
        {
            return cbl.Add(c);
        }

        [Route("update/c")]
        [HttpPost]
        public bool Update(CitiesDTO c)
        {
            return cbl.Update(c);
        }

        [Route("delete/c")]
        [HttpPost]
        public bool Delete(CitiesDTO c)
        {
            return cbl.Delete(c);
        }

        [Route("getallcities")]
        [HttpGet]
        public List<CitiesDTO> GetCity()
        {
            return cbl.GetCity();
        }

        [Route("getnext")]
        [HttpGet]
        public int GetNext()
        {
            return cbl.GetNext();
        }
    }
}