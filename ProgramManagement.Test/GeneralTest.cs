using ProgramManagement.Core.Services;

namespace ProgramManagement.Test;

public class GeneralTest
{
    public GeneralTest()
    {
    }

    [Fact]
    public void UniqueId_NotNull()
    {
        //Arrange
        var generalService = new GeneralService();

        //Act
        var id = generalService.GenerateUniqueId();

        //Assert
        Assert.NotNull(id);
    }

    [Fact]
    public void UniqueId_IsDifferent()
    {
        //Arrange
        var generalService = new GeneralService();

        //Act
        var id1 = generalService.GenerateUniqueId();
        var id2 = generalService.GenerateUniqueId();

        //Assert
        Assert.NotEqual(id1, id2);
    }
}