using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using SharpConsole.Infrastructure;
using Xunit;
using Moq;

namespace SharpConsole.Tests;

public class ConsoleMockTests
{
  [Fact]
  public async Task Run_WithMockedDependencies_ShouldProcessCorrectly()
  {
    // Arrange
    var mockContext = new Mock<IContext>();
    var mockScriptEngine = new Mock<IScriptEngine>();
    var mockConsoleUI = new Mock<IConsoleUI>();
    var mockCommandHistory = new Mock<ICommandHistory>();

    mockContext.Setup(x => x.GetContext()).Returns(new { Value = 100 });
    mockConsoleUI.SetupSequence(x => x.ReadInput())
        .Returns("Value")
        .Returns("exit");
    mockScriptEngine.Setup(x => x.Execute(It.IsAny<string>()))
        .ReturnsAsync((string input) => input == "Value" ? 100 : throw new Exception("Invalid input"));

    var createConsole = new CreateConsole(mockScriptEngine.Object, mockConsoleUI.Object, mockCommandHistory.Object);
    var console = createConsole.Execute();
    var runConsole = new RunConsole(console, mockConsoleUI.Object);

    // Act
    await runConsole.Execute();

    // Assert
    mockConsoleUI.Verify(x => x.ShowWelcome(), Times.Once);
    mockConsoleUI.Verify(x => x.ShowResult(100), Times.Once);
    mockConsoleUI.Verify(x => x.ReadInput(), Times.Exactly(2));
  }
}
