using SharpConsole.Domain.Inbound;
using Xunit;
using Moq;

namespace SharpConsole.Tests;

public class IContextTests
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

  [Fact]
  public void GetContext_ShouldReturnProvidedData()
  {
    // Arrange
    var expectedData = new { Name = "Test" };
    var context = new TestContext(expectedData);

    // Act
    var result = context.GetContext();

    // Assert
    Assert.Same(expectedData, result);
  }
}
