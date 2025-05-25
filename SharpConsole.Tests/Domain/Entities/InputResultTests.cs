using SharpConsoleCore.Domain.Entities;
using Xunit;

namespace SharpConsoleCore.Tests.Domain.Entities;

public class InputResultTests
{
  [Fact]
  public void InputResult_ShouldHaveCorrectProperties()
  {
    // Arrange
    var text = "test";
    var position = 2;
    var isComplete = true;
    var state = new InputState(text, position);

    // Act
    var result = new InputResult(state, isComplete);

    // Assert
    Assert.Equal(text, result.State.Text);
    Assert.Equal(position, result.State.Position);
    Assert.Equal(isComplete, result.IsComplete);
  }
}
