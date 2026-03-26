using BL.ClassesBL;
using BL.ClassesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/rents")]
    public class RentsController:ApiController
    {
        RentsBL rbl = new RentsBL();
        [AcceptVerbs("Get", "Post")]

        [Route("getrentsbyid/rentsCode")]
        [HttpGet]
        public RentsDTO GetRentsById(int rentsCode)
        {
            return rbl.GetRentsById(rentsCode);
        }

        [Route("add")]
        [HttpPost]
        public bool Add(RentsDTO c)
        {
            return rbl.Add(c);
        }

        [Route("update/c")]
        [HttpPost]
        public bool Update(RentsDTO c)
        {
            return rbl.Update(c);
        }

        [Route("delete/c")]
        [HttpPost]
        public bool Delete(RentsDTO c)
        {
            return rbl.Delete(c);
        }

        [Route("getallrents")]
        [HttpGet]
        public List<RentsDTO> GetRents()
        {
            return rbl.GetRents();
        }

        [Route("getnext")]
        [HttpGet]
        public int GetNext()
        {
            return rbl.GetNext();
        }

        //public RentsDTO Convert(Rents c)
        //{
        //    RentsDTO cdto = new RentsDTO();
        //    cdto.rentsCode = c.rentsCode;
        //    cdto.custCode = c.custCode;
        //    cdto.carCode = c.carCode;
        //    cdto.dateStart = c.dateStart;
        //    cdto.dateEnd = c.dateEnd;
        //    cdto.matara = c.matara;
        //    cdto.Cars = c.Cars;
        //    cdto.Costomers = c.Costomers;
        //    return cdto;
        //}

        //public Rents Convert(RentsDTO cdto)
        //{
        //    Rents c = new Rents();
        //    c.rentsCode = cdto.rentsCode;
        //    c.custCode = cdto.custCode;
        //    c.carCode = cdto.carCode;
        //    c.dateStart = cdto.dateStart;
        //    c.dateEnd = cdto.dateEnd;
        //    c.matara = cdto.matara;
        //    c.Cars = cdto.Cars;
        //    c.Costomers = cdto.Costomers;
        //    return c;
        //}

        //public List<RentsDTO> Convert(List<Rents> list)
        //{
        //    List<RentsDTO> ldto = new List<RentsDTO>();
        //    foreach (Rents c in list)
        //    {
        //        ldto.Add(Convert(c));
        //    }
        //    return ldto;
        //}

        //public List<Rents> Convert(List<RentsDTO> list)
        //{
        //    List<Rents> Rents = new List<Rents>();
        //    foreach (RentsDTO cdto in list)
        //    {
        //        Rents.Add(Convert(cdto));
        //    }
        //    return Rents;
        //}

        [Route("getrentsfromlastweek")]
        [HttpGet]
        public List<RentsDTO> GetRentsfromWeeek()
        {
            return rbl.GetRentsfromWeeek();
        }

        [Route("getrentsfromlastmonth")]
        [HttpGet]
        public List<RentsDTO> GetRentsFromLastMonth()
        {
            return rbl.GetRentsFromLastMonth();
        }

        [Route("getrentsstartingtoday")]
        [HttpGet]
        public List<RentsDTO> GetRentsStartToday()
        {
            return rbl.GetRentsStartToday();
        }

        [Route("getrentsstartingondate/startDate")]
        [HttpGet]
        public List<RentsDTO> GetRentsStartDate(DateTime startDate)
        {
            return rbl.GetRentsStartDate(startDate);
        }

        [Route("getrentsreturningondate/returnDate")]
        [HttpGet]
        public List<RentsDTO> GetRentsBackDate(DateTime returnDate)
        {
            return rbl.GetRentsBackDate(returnDate);
        }

        [Route("getavailablecarsbetweendates/startDate/endDate")]
        [HttpGet]
        public List<CarsDTO> GetAvailableCarsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return rbl.GetAvailableCarsBetweenDates(startDate, endDate);
        }

        //[Route("getcarsbyrentalpurpose/rentalPurpose")]
        //[HttpGet]
        //public List<CarsDTO> GetCarsByRentalPurpose(string rentalPurpose)
        //{
        //    return rbl.GetCarsByRentalPurpose(rentalPurpose);
        //}

        [Route("getrentsbycustomerid/{customerId}")]
        [HttpGet]
        public List<RentsDTO> GetRentsByCustomerId(int customerId)
        {
            return rbl.GetRentsByCustomerId(customerId);
        }

        [Route("getrentsbycarcode/carCode")]
        [HttpGet]
        public List<RentsDTO> GetRentsByCarCode(int carCode)
        {
            return rbl.GetRentsByCarCode(carCode);
        }

        [Route("getallrentssortedbystartdate")]
        [HttpGet]
        public List<RentsDTO> GetAllRentsSortedByStartDate()
        {
            return rbl.GetAllRentsSortedByStartDate();
        }
    }
}