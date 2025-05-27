using System.Collections.Generic;
using System.Linq;
using SharpConsole.Core.Outbound;

namespace SharpConsole.Platform.Infrastructure;

public class CommandHistory : ICommandHistory
{
  private readonly List<string> _commands = new();
  private const int MAX_HISTORY_SIZE = 100;
  private int _currentIndex;
  private string _currentInput;

  public CommandHistory()
  {
    _currentIndex = -1;
    _currentInput = string.Empty;
  }

  public void AddCommand(string command)
  {
    if (string.IsNullOrWhiteSpace(command))
      return;

    if (_commands.Any() && _commands.Last() == command)
      return;

    _commands.Add(command);
    ResetNavigation();

    if (_commands.Count > MAX_HISTORY_SIZE)
      _commands.RemoveAt(0);
  }

  public IEnumerable<string> GetHistory()
  {
    return _commands;
  }

  public void Clear()
  {
    _commands.Clear();
    ResetNavigation();
  }

  public string NavigateUp()
  {
    if (!_commands.Any())
      return string.Empty;

    if (_currentIndex == -1)
    {
      _currentInput = string.Empty;
      _currentIndex = _commands.Count - 1;
      return _commands[_currentIndex];
    }

    if (_currentIndex > 0)
    {
      _currentIndex--;
      return _commands[_currentIndex];
    }

    return _commands[_currentIndex];
  }

  public string NavigateDown()
  {
    if (!_commands.Any() || _currentIndex == -1)
      return string.Empty;

    if (_currentIndex < _commands.Count - 1)
    {
      _currentIndex++;
      return _commands[_currentIndex];
    }

    _currentIndex = -1;
    return _currentInput;
  }

  public void ResetNavigation()
  {
    _currentIndex = -1;
    _currentInput = string.Empty;
  }
}
