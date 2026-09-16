using MediatR;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Schedule.Commands;
using MS.Application.Contracts.Features.Schedule.Queries;

namespace MS.Presentation.WebApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult>> Create(CreateScheduleCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<OperationResult>> Update(UpdateScheduleCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("doctorId")]
    public async Task<ActionResult<List<ScheduleViewModel>>> GetSchedulesBy(int doctorId)
    {
        var result = await _mediator.Send(new GetSchedulesByDoctorIdQuery(doctorId));
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<AppointmentTime>>> GetDoctorWorkingHoursForSpecificDate([FromQuery]GetDoctorWorkingHoursForSpecificDateQuery query)
    {
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}

