using SharpConsole.Core.Inbound;
using Xunit;

namespace SharpConsole.Platform.Tests.Domain;

public class ISharpConsoleTests
{
  [Fact]
  public void GetContext_ShouldReturnContext()
  {
    // Arrange
    var context = new TestContext(new { Name = "Test" });

    // Act
    var result = context.GetContext();

    // Assert
    Assert.NotNull(result);
  }

  private class TestContext : ISharpConsole
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
}
