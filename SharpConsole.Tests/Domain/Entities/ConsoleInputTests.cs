using SharpConsole.Core.Entities;
using Xunit;

namespace SharpConsole.Platform.Tests.Domain.Entities;

public class ConsoleInputTests
{
  [Fact]
  public void ConsoleInput_ShouldHaveCorrectProperties()
  {
    // Arrange
    var text = "test";
    var position = 2;
    var key = new ConsoleKeyInfo('t', ConsoleKey.T, false, false, false);
    var state = new InputState(text, position);

    // Act
    var input = new ConsoleInput(state, key);

    // Assert
    Assert.Equal(text, input.State.Text);
    Assert.Equal(position, input.State.Position);
    Assert.Equal(key, input.Key);
  }
}
