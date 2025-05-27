using Xunit;
using SharpConsole.Domain;
using SharpConsole.Platform.Infrastructure;

namespace SharpConsole.Tests
{
  public class AutoCompleteTests
  {
    private readonly IAutoComplete _autoComplete;

    public AutoCompleteTests()
    {
      _autoComplete = new AutoComplete();
    }

    [Fact]
    public void GetSuggestions_WhenInputIsEmpty_ReturnsEmptyList()
    {
      // Arrange
      // Act
      var suggestions = _autoComplete.GetSuggestions(string.Empty);

      // Assert
      Assert.Empty(suggestions);
    }

    [Fact]
    public void GetSuggestions_WhenInputHasNoMatches_ReturnsEmptyList()
    {
      // Arrange
      // Act
      var suggestions = _autoComplete.GetSuggestions("nonexistent");

      // Assert
      Assert.Empty(suggestions);
    }

    [Fact]
    public void GetNextSuggestion_WhenNoSuggestions_ReturnsEmptyString()
    {
      // Arrange
      // Act
      var suggestion = _autoComplete.GetNextSuggestion();

      // Assert
      Assert.Equal(string.Empty, suggestion);
    }

    [Fact]
    public void GetPreviousSuggestion_WhenNoSuggestions_ReturnsEmptyString()
    {
      // Arrange
      // Act
      var suggestion = _autoComplete.GetPreviousSuggestion();

      // Assert
      Assert.Equal(string.Empty, suggestion);
    }
  }
}
