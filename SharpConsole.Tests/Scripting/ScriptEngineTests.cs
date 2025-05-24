using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Infrastructure;
using Xunit;
using System;
using Moq;

namespace SharpConsole.Tests;

public class ScriptEngineTests
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

  public class TestData
  {
    public int Value { get; set; }
  }

  public class TestDataWithArray
  {
    public int[] Numbers { get; set; } = Array.Empty<int>();
  }

  [Fact]
  public async Task Execute_WithValidCode_ShouldReturnResult()
  {
    // Arrange
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);

    // Act
    var result = await scriptEngine.Execute("Value");

    // Assert
    Assert.Equal(42, result);
  }

  [Fact]
  public async Task Execute_WithInvalidCode_ShouldReturnErrorMessage()
  {
    // Arrange
    var context = new TestContext(new TestData { Value = 42 });
    var scriptEngine = new ScriptEngine(context);

    // Act & Assert
    var exception = await Assert.ThrowsAsync<Exception>(() => scriptEngine.Execute("InvalidCode"));
    Assert.Contains("error", exception.Message, StringComparison.OrdinalIgnoreCase);
  }

  [Fact]
  public async Task Execute_WithComplexCode_ShouldReturnResult()
  {
    // Arrange
    var context = new TestContext(new TestDataWithArray { Numbers = new[] { 1, 2, 3, 4, 5 } });
    var scriptEngine = new ScriptEngine(context);

    // Act
    var result = await scriptEngine.Execute("Numbers[0]");

    // Assert
    Assert.Equal(1, result);
  }
}
