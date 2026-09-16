using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Patient.Commands;
using MS.Application.Contracts.Features.Patient.Queries;

namespace MS.Presentation.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ISender _sender;

        public PatientController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult>> Create(CreatePatientCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResult>> Update(UpdatePatientCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }


        [HttpGet("id")]
        public async Task<ActionResult<PatientViewModel>> GetBy(int id)
        {
            var result = await _sender.Send(new GetPatientDetailsQuery(id));
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientViewModel>>> GetAll()
        {
            var result = await _sender.Send(new GetAllPatientsQuery());
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
