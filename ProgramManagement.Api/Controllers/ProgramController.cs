using Microsoft.AspNetCore.Mvc;
using ProgramManagement.Api.Attribute;
using ProgramManagement.Core.DTOs.ApplicationFlow;
using ProgramManagement.Core.DTOs.Program;
using ProgramManagement.Core.DTOs.WorkFlow;
using ProgramManagement.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[ServiceFilter(typeof(ValidationFilterAttribute))]
public class ProgramController : ControllerBase
{
    private readonly IProgramInterface _programService;

    public ProgramController(IProgramInterface programService)
    {
        _programService = programService;
    }

    [HttpPost("addprogram")]
    public async Task<object> AddProgram([FromBody] AddProgramRequest request)
    {
        var result = await _programService.AddProgramAsync(request);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpGet("getprogram")]
    public async Task<object> GetProgram([Required][FromQuery] string id)
    {
        var result = await _programService.GetProgramAsync(id);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpPut("updateprogram")]
    public async Task<object> GetProgram([FromBody] UpdateProgramRequest request)
    {
        var result = await _programService.UpdateProgramAsync(request);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpPost("addapplicationform")]
    public async Task<object> AddApplicationForm([FromBody] AddApplicationFormRequest request)
    {
        var result = await _programService.AddApplicationFormAsync(request);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpGet("getapplicationform")]
    public async Task<object> GetApplicationForm([Required][FromQuery] string id)
    {
        var result = await _programService.GetApplicationFormAsync(id);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpPost("addworkflow")]
    public async Task<object> AddWorkFlow([FromBody] AddWorkFlowRequest request)
    {
        var result = await _programService.AddAddWorkFlowAsync(request);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpGet("getworkflow")]
    public async Task<object> GetWorkFlow([Required][FromQuery] string id)
    {
        var result = await _programService.GetWorkFlowAsync(id);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }

    [HttpGet("summary")]
    public async Task<object> GetSummary([Required][FromQuery] string programId)
    {
        var result = await _programService.GetSummaryAsync(programId);

        return result.Code == 200 ? (object)Ok(result) : BadRequest(result);
    }
}