using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using Xunit;
using Moq;

namespace SharpConsole.Tests.Domain;

public class RunConsoleTests
{
  [Fact]
  public async Task Execute_ShouldProcessCommandsUntilExit()
  {
    // Arrange
    var console = new Mock<IConsole>();
    var consoleUI = new Mock<IConsoleUI>();
    var runConsole = new RunConsole(console.Object, consoleUI.Object);

    consoleUI.SetupSequence(u => u.ReadInput())
      .Returns("command1")
      .Returns("command2")
      .Returns("exit");

    // Act
    await runConsole.Execute();

    // Assert
    console.Verify(c => c.ShowWelcome(), Times.Once);
    console.Verify(c => c.ExecuteCommand("command1"), Times.Once);
    console.Verify(c => c.ExecuteCommand("command2"), Times.Once);
  }

  [Fact]
  public async Task Execute_ShouldStopWhenStopIsCalled()
  {
    // Arrange
    var console = new Mock<IConsole>();
    var consoleUI = new Mock<IConsoleUI>();
    var runConsole = new RunConsole(console.Object, consoleUI.Object);

    // Act
    runConsole.Stop();
    await runConsole.Execute();

    // Assert
    console.Verify(c => c.ShowWelcome(), Times.Once);
    console.Verify(c => c.ExecuteCommand(It.IsAny<string>()), Times.Never);
  }

  [Fact]
  public async Task Execute_ShouldIgnoreEmptyInput()
  {
    // Arrange
    var console = new Mock<IConsole>();
    var consoleUI = new Mock<IConsoleUI>();
    var runConsole = new RunConsole(console.Object, consoleUI.Object);

    consoleUI.SetupSequence(u => u.ReadInput())
      .Returns("")
      .Returns("command1")
      .Returns("exit");

    // Act
    await runConsole.Execute();

    // Assert
    console.Verify(c => c.ShowWelcome(), Times.Once);
    console.Verify(c => c.ExecuteCommand("command1"), Times.Once);
  }
}
