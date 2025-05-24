using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using SharpConsole.Infrastructure;
using ConsoleEntity = SharpConsole.Domain.Entities.Console;
using Xunit;
using Moq;

namespace SharpConsole.Tests.Domain;

public class ConsoleTests
{
  private class TestContext : IContext
  {
    private readonly object _data;

    public TestContext(object data)
    {
      _data = data;
    }

    public object GetContext()
    {
      return _data;
    }
  }

  private class TestConsoleUI : IConsoleUI
  {
    private readonly Queue<string> _inputs;
    private readonly List<string> _outputs;

    public TestConsoleUI(IEnumerable<string> inputs)
    {
      _inputs = new Queue<string>(inputs);
      _outputs = new List<string>();
    }

    public string ReadInput()
    {
      if (!_inputs.Any())
        return "exit";
      return _inputs.Dequeue();
    }

    public void WriteLine(string message)
    {
      _outputs.Add(message);
    }

    public void ShowResult(object? result)
    {
      _outputs.Add(result?.ToString() ?? "null");
    }

    public void ShowWelcome()
    {
      _outputs.Add("Welcome");
    }

    public void ShowError(string message)
    {
      _outputs.Add($"Error: {message}");
    }

    public IReadOnlyList<string> Outputs => _outputs;

    public void Dispose()
    {
      // Nothing to dispose in test implementation
    }
  }

  [Fact]
  public async Task Run_ShouldProcessInputsAndShowResults()
  {
    // Arrange
    var scriptEngine = new Mock<IScriptEngine>();
    var consoleUI = new TestConsoleUI(new[] { "2 + 2", "exit" });
    var commandHistory = new Mock<ICommandHistory>();
    var console = new ConsoleEntity(scriptEngine.Object, consoleUI, commandHistory.Object);

    scriptEngine.Setup(s => s.Execute("2 + 2")).ReturnsAsync(4);

    // Act
    await console.ExecuteCommand("2 + 2");

    // Assert
    Assert.Contains("4", consoleUI.Outputs);
  }

  [Fact]
  public async Task Run_WithError_ShouldShowError()
  {
    // Arrange
    var scriptEngine = new Mock<IScriptEngine>();
    var consoleUI = new TestConsoleUI(new[] { "invalid", "exit" });
    var commandHistory = new Mock<ICommandHistory>();
    var console = new ConsoleEntity(scriptEngine.Object, consoleUI, commandHistory.Object);

    scriptEngine.Setup(s => s.Execute("invalid")).ThrowsAsync(new Exception("Invalid expression"));

    // Act
    await console.ExecuteCommand("invalid");

    // Assert
    Assert.Contains("Error: Invalid expression", consoleUI.Outputs);
  }
}
