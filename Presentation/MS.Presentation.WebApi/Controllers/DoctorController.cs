using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Doctor.Commands;
using MS.Application.Contracts.Features.Doctor.Queries;

namespace MS.Presentation.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ISender _sender;

        public DoctorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult>> Create([FromBody] CreateDoctorCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResult>> Update([FromBody] UpdateDoctorCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);

        }

        [HttpGet("id")]
        public async Task<ActionResult<DoctorViewModel>> GetBy(int id)
        {
            var result = await _sender.Send(new GetDoctorDetailsByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorViewModel>>> GetAll()
        {
            var result = await _sender.Send(new GetAllDoctorsQuery());
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
