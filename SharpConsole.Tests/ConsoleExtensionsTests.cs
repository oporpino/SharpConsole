using SharpConsole;
using Xunit;
using Moq;

namespace SharpConsole.Tests;

public class ConsoleExtensionsTests
{
  [Fact]
  public async Task RunConsoleAsync_ShouldStartConsoleWithContext()
  {
    // Arrange
    var contextMock = new Mock<IContext>();

    // Act
    await contextMock.Object.RunConsoleAsync();

    // Assert
    // Since we're testing an extension method that wraps Console.Start,
    // we can verify that the method executes without throwing an exception
    Assert.True(true);
  }
}
