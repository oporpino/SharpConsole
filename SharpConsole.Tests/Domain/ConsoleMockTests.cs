using SharpConsole.Core.Inbound;
using SharpConsole.Core.Outbound;
using SharpConsole.Core.Application.UseCases;
using ConsoleEntity = SharpConsole.Core.Domain.Entities.Console;
using Xunit;
using Moq;

namespace SharpConsole.Platform.Tests.Domain;

public class ConsoleMockTests
{
  [Fact]
  public async Task ExecuteCommand_ShouldAddCommandToHistory()
  {
    // Arrange
    var scriptEngine = new Mock<IScriptEngine>();
    var consoleUI = new Mock<IConsoleDisplay>();
    var commandHistory = new Mock<ICommandHistory>();
    var console = new ConsoleEntity(scriptEngine.Object, consoleUI.Object, commandHistory.Object);
    var command = "test command";

    // Act
    await console.ExecuteCommand(command);

    // Assert
    commandHistory.Verify(h => h.AddCommand(command), Times.Once);
  }

  [Fact]
  public async Task ExecuteCommand_ShouldShowResult()
  {
    // Arrange
    var scriptEngine = new Mock<IScriptEngine>();
    var consoleUI = new Mock<IConsoleDisplay>();
    var commandHistory = new Mock<ICommandHistory>();
    var console = new ConsoleEntity(scriptEngine.Object, consoleUI.Object, commandHistory.Object);
    var result = "test result";

    scriptEngine.Setup(s => s.Execute(It.IsAny<string>())).ReturnsAsync(result);

    // Act
    await console.ExecuteCommand("test");

    // Assert
    consoleUI.Verify(u => u.ShowResult(result), Times.Once);
  }
}
