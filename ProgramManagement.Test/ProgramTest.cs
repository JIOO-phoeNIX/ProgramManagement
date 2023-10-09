using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ProgramManagement.Core.DTOs.Program;
using ProgramManagement.Core.Helpers;
using ProgramManagement.Core.Interfaces;
using ProgramManagement.Core.Services;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Test;

public class ProgramTest
{
    private readonly Mock<ILogger<ProgramService>> _logger;
    private readonly Mock<IProgramRepository<Persistence.Entity.Program>> _programRepository;
    private readonly Mock<IGeneralInterface> _generalService;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IApplicationFormRepository<ApplicationForm>> _applicationFormRepository;
    private readonly Mock<IWorkFlowRepository<WorkFlow>> _workFlowRepository;

    public ProgramTest()
    {
        _logger = new Mock<ILogger<ProgramService>>();
        _programRepository = new Mock<IProgramRepository<Persistence.Entity.Program>>();
        _generalService = new Mock<IGeneralInterface>();
        _mapper = new Mock<IMapper>();
        _applicationFormRepository = new Mock<IApplicationFormRepository<ApplicationForm>>();
        _workFlowRepository = new Mock<IWorkFlowRepository<WorkFlow>>();
    }

    [Theory]
    [InlineData("Internshi")]
    [InlineData("Jo")]
    public async Task AddProgram_InvalidType(string type)
    {
        //Arrange
        var programService = new ProgramService(_logger.Object, _programRepository.Object, _generalService.Object, _mapper.Object,
            _applicationFormRepository.Object, _workFlowRepository.Object);
        var request = new AddProgramRequest { Type = type };

        //Act
        var apiResponse = await programService.AddProgramAsync(request);

        //Assert
        Assert.Equal(ApplicationString.InvalidProgramType, apiResponse.Message);
    }
}