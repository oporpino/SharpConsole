using SharpConsole.Domain.Inbound;
using SharpConsole.Core.Infrastructure;
using Xunit;
using Moq;
using SharpConsole.Core.Application;
using System.Dynamic;

namespace SharpConsole.Core.Tests.Infrastructure;

public class ScriptEngineTests
{
  [Fact]
  public async Task Execute_ValidCode_ShouldReturnResult()
  {
    // Arrange
    dynamic context = new ExpandoObject();
    context.Value = 42;
    AplicationContext.Set(context);
    var scriptEngine = new ScriptEngine();

    // Act
    var result = await scriptEngine.Execute("Value * 2");

    // Assert
    Assert.Equal(84, result);
  }

  [Fact]
  public async Task Execute_InvalidCode_ShouldThrowException()
  {
    // Arrange
    dynamic context = new ExpandoObject();
    context.Value = 42;
    AplicationContext.Set(context);
    var scriptEngine = new ScriptEngine();

    // Act & Assert
    await Assert.ThrowsAsync<Exception>(() => scriptEngine.Execute("InvalidCode"));
  }

  [Fact]
  public async Task Execute_WithComplexObject_ShouldAccessProperties()
  {
    // Arrange
    dynamic context = new ExpandoObject();
    context.Numbers = new[] { 1, 2, 3 };
    AplicationContext.Set(context);
    var scriptEngine = new ScriptEngine();

    // Act
    var result = await scriptEngine.Execute("Numbers.Sum()");

    // Assert
    Assert.Equal(6, result);
  }

  [Fact]
  public async Task Execute_WithDynamicObject_ShouldAccessProperties()
  {
    // Arrange
    dynamic context = new ExpandoObject();
    context.Name = "Test";
    context.Age = 25;
    context.Tags = new[] { "tag1", "tag2" };
    AplicationContext.Set(context);
    var scriptEngine = new ScriptEngine();

    // Act
    var result = await scriptEngine.Execute("Name + \" is \" + Age + \" years old\"");

    // Assert
    Assert.Equal("Test is 25 years old", result);
  }

  [Fact]
  public async Task Execute_WithDynamicObject_ShouldAccessArrayProperties()
  {
    // Arrange
    dynamic context = new ExpandoObject();
    context.Name = "Test";
    context.Age = 25;
    context.Tags = new[] { "tag1", "tag2" };
    AplicationContext.Set(context);
    var scriptEngine = new ScriptEngine();

    // Act
    var result = await scriptEngine.Execute("Tags.Length");

    // Assert
    Assert.Equal(2, result);
  }
}
