using AutoMapper;
using Microsoft.Extensions.Logging;
using ProgramManagement.Core.DTOs.ApplicationFlow;
using ProgramManagement.Core.DTOs.Program;
using ProgramManagement.Core.DTOs.WorkFlow;
using ProgramManagement.Core.Enums;
using ProgramManagement.Core.Helpers;
using ProgramManagement.Core.Interfaces;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;
using System.Net;

namespace ProgramManagement.Core.Services;

public class ProgramService : IProgramInterface
{
    private readonly ILogger<ProgramService> _logger;
    private readonly IProgramRepository<Persistence.Entity.Program> _programRepository;
    private readonly IGeneralInterface _generalService;
    private readonly IMapper _mapper;
    private readonly IApplicationFormRepository<ApplicationForm> _applicationFormRepository;
    private readonly IWorkFlowRepository<WorkFlow> _workFlowRepository;

    public ProgramService(ILogger<ProgramService> logger, IProgramRepository<Persistence.Entity.Program> programRepository, IGeneralInterface generalService,
        IMapper mapper, IApplicationFormRepository<ApplicationForm> applicationFormRepository, IWorkFlowRepository<WorkFlow> workFlowRepository)
    {
        _logger = logger;
        _programRepository = programRepository;
        _generalService = generalService;
        _mapper = mapper;
        _applicationFormRepository = applicationFormRepository;
        _workFlowRepository = workFlowRepository;
    }

    public async Task<ApiResponse> AddProgramAsync(AddProgramRequest request)
    {
        try
        {
            //check if the program type is valid
            var canParseProgramType = Enum.TryParse<ProgramType>(request.Type, true, out _);
            if (canParseProgramType is false)
                return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidProgramType, null);

            if (!string.IsNullOrEmpty(request?.MinQualifications))
            {
                //check if the qualification is valid
                var canParseQualification = Enum.TryParse<Qualification>(request?.MinQualifications, true, out _);
                if (canParseQualification is false)
                    return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidQualification, null);
            }

            //check if the dates are valid
            var canParseOpenDate = DateTime.TryParse(request?.Open.Trim(), out var parsedOpenDate);
            if (canParseOpenDate is false)
                return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidOpenDate, null);

            var canParseCloseDate = DateTime.TryParse(request?.Close.Trim(), out var parsedCloseDate);
            if (canParseCloseDate is false)
                return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidCloseDate, null);

            var programToSave = _mapper.Map<Persistence.Entity.Program>(request);

            if (!string.IsNullOrEmpty(request?.Start))
            {
                var canParseStartDate = DateTime.TryParse(request?.Start?.Trim(), out var parsedStartDate);
                if (canParseStartDate is false)
                    return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidStartDate, null);
                programToSave.StartDate = parsedStartDate;
            }

            programToSave.DateCreated = DateTime.UtcNow;
            programToSave.DateOpen = parsedOpenDate;
            programToSave.DateClose = parsedCloseDate;
            programToSave.id = _generalService.GenerateUniqueId();

            await _programRepository.AddAsync(programToSave, programToSave.id);

            var response = new AddProgramResponse
            {
                id = programToSave.id
            };

            return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in AddProgramAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> GetProgramAsync(string id)
    {
        try
        {
            var program = await _programRepository.GetAsync(id);
            if (program is not null && program?.Title is not null)
            {
                var response = new GetProgramResponse();

                response.Program = _mapper.Map<Models.Program>(program);
                response.Program.StartDate = program?.StartDate?.ToString("dd/MM/yyyy");
                response.Program.DateOpen = program.DateOpen.ToString("dd/MM/yyyy");
                response.Program.DateClose = program.DateClose.ToString("dd/MM/yyyy");

                return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, response);
            }

            return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetProgramAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> UpdateProgramAsync(UpdateProgramRequest request)
    {
        try
        {
            if (request.Program is null)
                return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidRequest, null);

            var program = await _programRepository.GetAsync(request.id);
            
            //confirm that the program exist before updating
            if (program is not null && program?.Title is not null)
            {
                var canParseProgramType = Enum.TryParse<ProgramType>(request.Program.Type.Trim(), true, out _);
                if (canParseProgramType is false)
                    return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidProgramType, null);

                if (!string.IsNullOrEmpty(request.Program?.MinQualifications))
                {
                    var canParseQualification = Enum.TryParse<Qualification>(request.Program?.MinQualifications?.Trim(), true, out _);
                    if (canParseQualification is false)
                        return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidQualification, null);
                }

                var canParseOpenDate = DateTime.TryParse(request.Program?.Open.Trim(), out var parsedOpenDate);
                if (canParseOpenDate is false)
                    return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidOpenDate, null);

                var canParseCloseDate = DateTime.TryParse(request.Program?.Close.Trim(), out var parsedCloseDate);
                if (canParseCloseDate is false)
                    return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidCloseDate, null);

                var programToUpdate = _mapper.Map<Persistence.Entity.Program>(request.Program);

                if (!string.IsNullOrEmpty(request.Program?.Start))
                {
                    var canParseStartDate = DateTime.TryParse(request.Program?.Start?.Trim(), out var parsedStartDate);
                    if (canParseStartDate is false)
                        return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidStartDate, null);
                    programToUpdate.StartDate = parsedStartDate;
                }

                programToUpdate.DateLastUpdated = DateTime.UtcNow;
                programToUpdate.DateOpen = parsedOpenDate;
                programToUpdate.DateClose = parsedCloseDate;
                programToUpdate.id = request.id;

                await _programRepository.UpdateAsync(request.id, programToUpdate);

                return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, null);
            }

            return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in UpdateProgramAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> AddApplicationFormAsync(AddApplicationFormRequest request)
    {
        try
        {
            var program = await _programRepository.GetAsync(request.ProgramId);
            if (program is not null && program?.Title is not null)
            {
                //only add if it does not exist
                if (program.ApplicationFormId is not null)
                    return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.ApplicationFormExist, null);

                //validate the question types and sections
                if (request.Questions.Any())
                {
                    foreach (var question in request.Questions)
                    {
                        var canParseQuestionType = Enum.TryParse<QuestionType>(question.Type.Trim(), true, out _);
                        if (canParseQuestionType is false)
                            return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidQuestionType, null);

                        var canParseQuestionSection = Enum.TryParse<QuestionSection>(question.Section.Trim(), true, out _);
                        if (canParseQuestionSection is false)
                            return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidQuestionSection, null);
                    }
                }

                var applicationForm = _mapper.Map<ApplicationForm>(request);
                applicationForm.DateCreated = DateTime.UtcNow;
                var applicationFormId = _generalService.GenerateUniqueId();
                applicationForm.id = applicationFormId;

                await _applicationFormRepository.AddAsync(applicationForm, applicationForm.id);

                program.ApplicationFormId = applicationFormId;
                program.DateLastUpdated = DateTime.UtcNow;
                await _programRepository.UpdateAsync(program.id, program);

                return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, null);
            }

            return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in AddApplicationFormAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> GetApplicationFormAsync(string id)
    {
        try
        {
            var applicationForm = await _applicationFormRepository.GetAsync(id);
            if (applicationForm is not null && applicationForm?.ImageUrl is not null)
            {
                var response = new GetApplicationFormResponse();

                response.ApplicationForm = _mapper.Map<AddApplicationFormRequest>(applicationForm);

                return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, response);
            }

            return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in AddApplicationFormAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> AddAddWorkFlowAsync(AddWorkFlowRequest request)
    {
        try
        {
            if (request.Stages.Any())
            {
                var program = await _programRepository.GetAsync(request.ProgramId);
                if (program is not null && program?.Title is not null)
                {
                    if (program.WorkFlowId is not null)
                        return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.WorklFlowExist, null);

                    //validate the stage types
                    foreach (var stage in request.Stages)
                    {
                        var canParseStageType = Enum.TryParse<StageType>(stage.Type.Trim(), true, out _);
                        if (canParseStageType is false)
                            return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidStageType, null);
                    }

                    var workFlow = _mapper.Map<WorkFlow>(request);
                    workFlow.DateCreated = DateTime.UtcNow;
                    var workFlowId = _generalService.GenerateUniqueId();
                    workFlow.id = workFlowId;

                    await _workFlowRepository.AddAsync(workFlow, workFlow.id);

                    program.WorkFlowId = workFlowId;
                    program.DateLastUpdated = DateTime.UtcNow;
                    await _programRepository.UpdateAsync(program.id, program);

                    return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, null);
                }

                return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
            }

            return new ApiResponse((int)HttpStatusCode.BadRequest, ApplicationString.InvalidRequest, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in AddAddWorkFlowAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> GetWorkFlowAsync(string id)
    {
        try
        {
            var workFlow = await _workFlowRepository.GetAsync(id);
            if (workFlow is not null)
            {
                var response = new GetWorkFlowResponse();

                response.WorkFlow = _mapper.Map<AddWorkFlowRequest>(workFlow);

                return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, response);
            }

            return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetWorkFlowAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }

    public async Task<ApiResponse> GetSummaryAsync(string programId)
    {
        try
        {
            var program = await _programRepository.GetAsync(programId);
            if (program is not null && program?.Title is not null)
            {
                var response = new SummaryResponse();

                response.Program = _mapper.Map<Models.Program>(program);
                response.Program.StartDate = program?.StartDate?.ToString("dd/MM/yyyy");
                response.Program.DateOpen = program.DateOpen.ToString("dd/MM/yyyy");
                response.Program.DateClose = program.DateClose.ToString("dd/MM/yyyy");

                if (program.ApplicationFormId is not null)
                {
                    var applicationForm = await _applicationFormRepository.GetAsync(program.ApplicationFormId);
                    if (applicationForm is not null && applicationForm?.ImageUrl is not null)
                    {
                        response.ApplicationForm = _mapper.Map<AddApplicationFormRequest>(applicationForm);
                    }
                }

                if (program.WorkFlowId is not null)
                {
                    var workFlow = await _workFlowRepository.GetAsync(program.WorkFlowId);
                    if (workFlow is not null)
                    {
                        response.WorkFlow = _mapper.Map<AddWorkFlowRequest>(workFlow);
                    }
                }

                return new ApiResponse((int)HttpStatusCode.OK, ApplicationString.Success, response);
            }

            return new ApiResponse((int)HttpStatusCode.NotFound, ApplicationString.NotFound, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetSummaryAsync message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
            return new ApiResponse((int)HttpStatusCode.InternalServerError, ApplicationString.InternalServerError, null);
        }
    }
}