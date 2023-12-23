
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PoC.Application.Queries.Medical.GetMedical;

namespace PoC.Api.Controllers
{
    public class MedicalController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> GetMedicalRecords()
        {
           return Ok(await Mediator.Send(new GetMedicalRecordsQuery()));
        }
    }
}
