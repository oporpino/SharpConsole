using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using SharpConsole.Infrastructure;
using SharpConsole.Domain.Entities;
using Xunit;
using Moq;

namespace SharpConsole.Tests;

public class RunConsoleTests
{
  private class TestConsoleUI : IConsoleUI
  {
    public string ReadInput() => "exit";
    public void WriteLine(string message) { }
    public void ShowResult(object? result) { }
    public void ShowError(string message) { }
    public void ShowWelcome() { }
    public void Dispose() { }
  }

  [Fact]
  public async Task Execute_ShouldProcessInputAndStop()
  {
    // Arrange
    var consoleMock = new Mock<IConsole>();
    var consoleUI = new TestConsoleUI();
    var runConsole = new RunConsole(consoleMock.Object, consoleUI);

    // Act
    var task = runConsole.Execute();
    runConsole.Stop();
    await task;

    // Assert
    consoleMock.Verify(x => x.ShowWelcome(), Times.Once);
  }
}
