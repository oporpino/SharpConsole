using SharpConsole.Domain.Inbound;
using SharpConsole.Infrastructure;
using Xunit;
using Moq;

namespace SharpConsole.Tests.Infrastructure;

public class ScriptEngineTests
{
  [Fact]
  public async Task Execute_ValidCode_ShouldReturnResult()
  {
    // Arrange
    var context = new Mock<IContext>();
    context.Setup(c => c.GetContext()).Returns(new TestContext { Value = 42 });
    var scriptEngine = new ScriptEngine(context.Object);

    // Act
    var result = await scriptEngine.Execute("Value * 2");

    // Assert
    Assert.Equal(84, result);
  }

  [Fact]
  public async Task Execute_InvalidCode_ShouldThrowException()
  {
    // Arrange
    var context = new Mock<IContext>();
    context.Setup(c => c.GetContext()).Returns(new TestContext { Value = 42 });
    var scriptEngine = new ScriptEngine(context.Object);

    // Act & Assert
    await Assert.ThrowsAsync<Exception>(() => scriptEngine.Execute("InvalidCode"));
  }

  [Fact]
  public async Task Execute_WithComplexObject_ShouldAccessProperties()
  {
    // Arrange
    var context = new Mock<IContext>();
    context.Setup(c => c.GetContext()).Returns(new TestContextWithArray { Numbers = new[] { 1, 2, 3 } });
    var scriptEngine = new ScriptEngine(context.Object);

    // Act
    var result = await scriptEngine.Execute("Numbers.Sum()");

    // Assert
    Assert.Equal(6, result);
  }
}

public class TestContext
{
  public int Value { get; set; }
}

public class TestContextWithArray
{
  public int[] Numbers { get; set; } = Array.Empty<int>();
}
