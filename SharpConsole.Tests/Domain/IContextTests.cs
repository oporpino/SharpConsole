using SharpConsole.Domain.Inbound;
using Xunit;

namespace SharpConsole.Tests.Domain;

public class IContextTests
{
  [Fact]
  public void GetContext_ShouldReturnContextObject()
  {
    // Arrange
    var context = new TestContext(new { Value = 42 });

    // Act
    var result = context.GetContext();

    // Assert
    Assert.NotNull(result);
    Assert.Equal(42, ((dynamic)result).Value);
  }

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
}
