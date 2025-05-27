using SharpConsole.Core.Inbound;
using SharpConsole.Core.Outbound;
using SharpConsole.Core.Application.UseCases;
using Xunit;
using Moq;

namespace SharpConsole.Platform.Tests.Domain;

public class RunConsoleTests
{
  [Fact]
  public void Call_ShouldCreateInstanceAndExecute()
  {
    // Arrange
    var executor = new Mock<IConsoleExecutor>();
    var display = new Mock<IConsoleDisplay>();

    // Act
    RunConsole.Call(executor.Object, display.Object);

    // Assert
    executor.Verify(e => e.Execute(), Times.Once);
  }

  [Fact]
  public async Task Execute_ShouldProcessCommandsUntilExit()
  {
    // Arrange
    var console = new Mock<ISharpConsole>();
    var consoleUI = new Mock<IConsoleDisplay>();
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
    var console = new Mock<ISharpConsole>();
    var consoleUI = new Mock<IConsoleDisplay>();
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
    var console = new Mock<ISharpConsole>();
    var consoleUI = new Mock<IConsoleDisplay>();
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
