using BL.ClassesBL;
using BL.ClassesDTO;
using System.Web.Http;
using System.Web.Http.Cors;

[RoutePrefix("api/payments")]
[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
public class PaymentsController : ApiController
{
    PayMentsBL pbl = new PayMentsBL();

    [Route("add")]
    [HttpPost]
    public bool Add(PayMentsDTO payment)
    {
        if (payment == null) return false;
        return pbl.Add(payment);
    }
}