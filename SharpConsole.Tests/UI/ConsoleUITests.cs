using SharpConsole.UI;
using Xunit;

namespace SharpConsole.Tests.UI;

public class ConsoleUITests
{
  [Fact]
  public void ShowWelcome_ShouldDisplayWelcomeMessage()
  {
    // Arrange
    var consoleUI = new ConsoleUI();
    var stringWriter = new StringWriter();
    System.Console.SetOut(stringWriter);

    // Act
    consoleUI.ShowWelcome();

    // Assert
    var output = stringWriter.ToString();
    Assert.Contains("Welcome to SharpConsole", output);
  }

  [Fact]
  public void ShowResult_WithNull_ShouldDisplayNull()
  {
    // Arrange
    var consoleUI = new ConsoleUI();
    var stringWriter = new StringWriter();
    System.Console.SetOut(stringWriter);

    // Act
    consoleUI.ShowResult(null);

    // Assert
    var output = stringWriter.ToString();
    Assert.Contains("null", output);
  }

  [Fact]
  public void ShowResult_WithValue_ShouldDisplayValue()
  {
    // Arrange
    var consoleUI = new ConsoleUI();
    var stringWriter = new StringWriter();
    System.Console.SetOut(stringWriter);

    // Act
    consoleUI.ShowResult("test");

    // Assert
    var output = stringWriter.ToString();
    Assert.Contains("test", output);
  }

  [Fact]
  public void ShowError_ShouldDisplayErrorMessageInRed()
  {
    // Arrange
    var consoleUI = new ConsoleUI();
    var stringWriter = new StringWriter();
    System.Console.SetOut(stringWriter);

    // Act
    consoleUI.ShowError("test error");

    // Assert
    var output = stringWriter.ToString();
    Assert.Contains("Error: test error", output);
  }
}
