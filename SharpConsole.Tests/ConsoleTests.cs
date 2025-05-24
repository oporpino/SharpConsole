using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using SharpConsole.Infrastructure;
using Xunit;
using Moq;

namespace SharpConsole.Tests;

public class TestData
{
  public int Value { get; set; }
}

public class TestDataWithArray
{
  public int[] Numbers { get; set; } = Array.Empty<int>();
}

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
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new TestConsoleUI(new[] { "Value" });
    var commandHistory = new CommandHistory();

    var createConsole = new CreateConsole(scriptEngine, consoleUI, commandHistory);
    var console = createConsole.Execute();
    var runConsole = new RunConsole(console, consoleUI);

    // Act
    var runTask = runConsole.Execute();
    runConsole.Stop();
    await runTask;

    // Assert
    var outputs = ((TestConsoleUI)consoleUI).Outputs;
    Assert.Equal(2, outputs.Count);
    Assert.Equal("Welcome", outputs[0]);
    Assert.Equal("42", outputs[1]);
  }

  [Fact]
  public async Task Run_WithInvalidInput_ShouldShowError()
  {
    // Arrange
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new TestConsoleUI(new[] { "InvalidCode" });
    var commandHistory = new CommandHistory();

    var createConsole = new CreateConsole(scriptEngine, consoleUI, commandHistory);
    var console = createConsole.Execute();
    var runConsole = new RunConsole(console, consoleUI);

    // Act
    var runTask = runConsole.Execute();
    runConsole.Stop();
    await runTask;

    // Assert
    var outputs = ((TestConsoleUI)consoleUI).Outputs;
    Assert.Equal(2, outputs.Count);
    Assert.Equal("Welcome", outputs[0]);
    Assert.Contains("error", outputs[1].ToLower());
  }

  [Fact]
  public async Task Run_WithComplexCode_ShouldProcessCorrectly()
  {
    // Arrange
    var context = new TestContext(new TestDataWithArray { Numbers = new[] { 1, 2, 3, 4, 5 } });
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new TestConsoleUI(new[] { "Numbers[0]" });
    var commandHistory = new CommandHistory();

    var createConsole = new CreateConsole(scriptEngine, consoleUI, commandHistory);
    var console = createConsole.Execute();
    var runConsole = new RunConsole(console, consoleUI);

    // Act
    var runTask = runConsole.Execute();
    runConsole.Stop();
    await runTask;

    // Assert
    var outputs = ((TestConsoleUI)consoleUI).Outputs;
    Assert.Equal(2, outputs.Count);
    Assert.Equal("Welcome", outputs[0]);
    Assert.Equal("1", outputs[1]);
  }

  [Fact]
  public async Task Run_WithEmptyInput_ShouldSkip()
  {
    // Arrange
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new TestConsoleUI(new[] { "", "Value" });
    var commandHistory = new CommandHistory();

    var createConsole = new CreateConsole(scriptEngine, consoleUI, commandHistory);
    var console = createConsole.Execute();
    var runConsole = new RunConsole(console, consoleUI);

    // Act
    var runTask = runConsole.Execute();
    runConsole.Stop();
    await runTask;

    // Assert
    var outputs = ((TestConsoleUI)consoleUI).Outputs;
    Assert.Equal(2, outputs.Count);
    Assert.Equal("Welcome", outputs[0]);
    Assert.Equal("42", outputs[1]);
  }
}
