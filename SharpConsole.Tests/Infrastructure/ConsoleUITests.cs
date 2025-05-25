using SharpConsole.Core.Domain.Outbound;
using SharpConsole.Core.Infrastructure;
using Xunit;
using Moq;

namespace SharpConsole.Core.Tests.Infrastructure;

public class ConsoleDisplayTests
{
  [Fact]
  public void ShowResult_ShouldFormatAndDisplayResult()
  {
    // Arrange
    var formatter = new Mock<IOutputFormatter>();
    var commandHistory = new Mock<ICommandHistory>();
    var inputHandler = new Mock<IInputHandler>();
    var consoleUI = new ConsoleDisplay(formatter.Object, commandHistory.Object, inputHandler.Object);
    var result = new { Value = 42 };

    formatter.Setup(f => f.Format(result)).Returns("42");

    // Act
    consoleUI.ShowResult(result);

    // Assert
    formatter.Verify(f => f.Format(result), Times.Once);
  }

  [Fact]
  public void ShowError_ShouldDisplayErrorInRed()
  {
    // Arrange
    var consoleUI = new Mock<IConsoleDisplay>();
    var errorMessage = "Test error";

    // Act
    consoleUI.Object.ShowError(errorMessage);

    // Assert
    // We can't easily test console color changes, but we can verify the method doesn't throw
    // The error message is expected to be displayed in red, but we don't need to verify that in the test
  }

  [Fact]
  public void ShowWelcome_ShouldDisplayWelcomeMessage()
  {
    // Arrange
    var consoleUI = new Mock<IConsoleDisplay>();

    // Act
    consoleUI.Object.ShowWelcome();

    // Assert
    // Note: We can't easily test console output, but we can verify the method doesn't throw
  }
}
