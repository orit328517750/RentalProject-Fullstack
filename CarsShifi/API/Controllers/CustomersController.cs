using BL.ClassesBL;
using BL.ClassesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomersController : ApiController
    {
        CustomersBL cbl = new CustomersBL();

        [AcceptVerbs("Get", "Post")]

        [Route("getcustemerbyid/id")]
        [HttpGet]
        public CustomersDTO GetCustemerById(int id)
        {
            return cbl.GetCustomersById(id);
        }
        //הוספה
        [Route("add")]
        [HttpPost]
        public bool Add(CustomersDTO c)
        {
            return cbl.Add(c);
        }


        [Route("update/c")]
        [HttpPost]
        public bool Update(CustomersDTO c)
        {
            return cbl.Update(c);
        }

        [Route("delete/c")]
        [HttpPost]
        public bool Delete(CustomersDTO c)
        {
            return cbl.Delete(c);
        }

        [Route("getallcustomers")]
        [HttpGet]
        public List<CustomersDTO> GetCustomers()
        {
            return cbl.GetCustomers();
        }


        [Route("getallcustomerssortedbyname")]
        [HttpGet]
        public List<CustomersDTO> GetCustomerBySort()
        {
            return cbl.GetCustomerBySort();
        }
        //לקבל לפי תעודת זהות

        //[Route("getcustomerbyidentitynumber/identityNumber")]
        //[HttpGet]
        //public CustomersDTO GetCustomerByIdentityNumber(int identityNumber)
        //{
        //    return cbl.GetCustomerByIdentityNumber(identityNumber);
        //}

        [Route("gettopthreeoldestcustomers")]
        [HttpGet]
        public List<CustomersDTO> GetThreeOld()
        {
            return cbl.GetThreeOld();
        }

        [Route("getcustomersbycity/cityCode")]
        [HttpGet]
        public List<CustomersDTO> GetCustomerByCity(int cityCode)
        {
            return cbl.GetCustomerByCity(cityCode);
        }

        [Route("getpaymentdetailsbyidentitynumber/identityNumber")]
        [HttpGet]
        public PayMentsDTO GetCustomerPayMent(int identityNumber)
        {
            return GetCustomerPayMent(identityNumber);
        }




        // פונקציית GET לקבלת לקוח
        [HttpGet]
        [Route("GetCostomerByID/{id}")]
        public CustomersDTO GetCostomerByID(int id)
        {
            // קריאה ל-cbl שכבר מוגדר אצלך
            return cbl.Customerbyid(id);
        }

        // פונקציית POST להוספת לקוח
        [HttpPost]
        [Route("insertclient")]
        public IHttpActionResult InsertClient([FromBody] CustomersDTO clients)
        {
            if (clients == null) return BadRequest("Data is null");

            try
            {
                var result = cbl.InsertClient(clients);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




    }
}