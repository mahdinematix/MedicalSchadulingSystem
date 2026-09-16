using MediatR;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Appointment.Commands;
using MS.Application.Contracts.Features.Appointment.Queries;

namespace MS.Presentation.WebApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AppointmentController : ControllerBase
{

    private readonly ISender _sender;

    public AppointmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult>> Book(AppointmentBookingCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<OperationResult>> Book(ChangeAppointmentCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("appointmentId")]
    public async Task<ActionResult<OperationResult>> Cancel(int appointmentId)
    {
        var result = await _sender.Send(new CancelAppointmentCommand(appointmentId));
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("patientId")]
    public async Task<ActionResult<List<AppointmentViewModel>>> GetAppointmentsBy([FromQuery] int patientId)
    {
        var result = await _sender.Send(new GetAppointmentsByPatientIdQuery(patientId));
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}

