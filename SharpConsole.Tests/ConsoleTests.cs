using SharpConsole;
using SharpConsole.Core;
using SharpConsole.Scripting;
using SharpConsole.UI;
using Xunit;

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
      return _inputs.Dequeue();
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
  }

  [Fact]
  public async Task Run_ShouldProcessInputsAndShowResults()
  {
    // Arrange
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new TestConsoleUI(new[] { "Value", "exit" });
    var console = new Console(context, scriptEngine, consoleUI);

    // Act
    await console.Run();

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
    var consoleUI = new TestConsoleUI(new[] { "InvalidCode", "exit" });
    var console = new Console(context, scriptEngine, consoleUI);

    // Act
    await console.Run();

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
    var consoleUI = new TestConsoleUI(new[] { "Numbers.Sum()", "exit" });
    var console = new Console(context, scriptEngine, consoleUI);

    // Act
    await console.Run();

    // Assert
    var outputs = ((TestConsoleUI)consoleUI).Outputs;
    Assert.Equal(2, outputs.Count);
    Assert.Equal("Welcome", outputs[0]);
    Assert.Equal("15", outputs[1]);
  }

  [Fact]
  public async Task Run_WithEmptyInput_ShouldSkip()
  {
    // Arrange
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new TestConsoleUI(new[] { "", "Value", "exit" });
    var console = new Console(context, scriptEngine, consoleUI);

    // Act
    await console.Run();

    // Assert
    var outputs = ((TestConsoleUI)consoleUI).Outputs;
    Assert.Equal(2, outputs.Count);
    Assert.Equal("Welcome", outputs[0]);
    Assert.Equal("42", outputs[1]);
  }
}
