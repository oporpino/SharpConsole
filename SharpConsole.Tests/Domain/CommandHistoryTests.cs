using SharpConsole.Domain.Outbound;
using SharpConsole.Core.Infrastructure;
using Xunit;

namespace SharpConsole.Core.Tests.Domain;

public class CommandHistoryTests
{
  [Fact]
  public void AddCommand_WithDuplicateConsecutiveCommands_ShouldOnlyStoreOnce()
  {
    // Arrange
    var history = new CommandHistory();
    const string command = "test";

    // Act
    history.AddCommand(command);
    history.AddCommand(command);

    // Assert
    var commands = history.GetHistory().ToList();
    Assert.Single(commands);
    Assert.Equal(command, commands[0]);
  }

  [Fact]
  public void AddCommand_WithNonConsecutiveDuplicateCommands_ShouldStoreBoth()
  {
    // Arrange
    var history = new CommandHistory();
    const string command = "test";

    // Act
    history.AddCommand(command);
    history.AddCommand("other");
    history.AddCommand(command);

    // Assert
    var commands = history.GetHistory().ToList();
    Assert.Equal(3, commands.Count);
    Assert.Equal(command, commands[0]);
    Assert.Equal("other", commands[1]);
    Assert.Equal(command, commands[2]);
  }

  [Fact]
  public void NavigateUp_WithEmptyHistory_ShouldReturnEmptyString()
  {
    // Arrange
    var history = new CommandHistory();

    // Act
    var result = history.NavigateUp();

    // Assert
    Assert.Equal(string.Empty, result);
  }

  [Fact]
  public void NavigateUp_WithHistory_ShouldReturnLastCommand()
  {
    // Arrange
    var history = new CommandHistory();
    history.AddCommand("first");
    history.AddCommand("second");

    // Act
    var result = history.NavigateUp();

    // Assert
    Assert.Equal("second", result);
  }

  [Fact]
  public void NavigateUp_WithMultipleCommands_ShouldNavigateUp()
  {
    // Arrange
    var history = new CommandHistory();
    history.AddCommand("first");
    history.AddCommand("second");
    history.AddCommand("third");

    // Act & Assert
    Assert.Equal("third", history.NavigateUp());
    Assert.Equal("second", history.NavigateUp());
    Assert.Equal("first", history.NavigateUp());
    Assert.Equal("first", history.NavigateUp()); // Should stay at first
  }

  [Fact]
  public void NavigateDown_WithEmptyHistory_ShouldReturnEmptyString()
  {
    // Arrange
    var history = new CommandHistory();

    // Act
    var result = history.NavigateDown();

    // Assert
    Assert.Equal(string.Empty, result);
  }
}
