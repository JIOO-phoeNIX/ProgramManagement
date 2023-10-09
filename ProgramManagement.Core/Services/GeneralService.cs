using ProgramManagement.Core.Interfaces;

namespace ProgramManagement.Core.Services;

public class GeneralService : IGeneralInterface
{
    public GeneralService()
    {
    }

    public string GenerateUniqueId()
    {
        return Guid.NewGuid().ToString("N");
    }
}