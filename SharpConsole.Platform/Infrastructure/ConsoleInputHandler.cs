using System;
using System.Linq;
using SharpConsole.Domain.Entities;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain;

namespace SharpConsole.Platform.Infrastructure;

public class ConsoleInputHandler : IInputHandler
{
  private readonly IConsoleManager _consoleManager;
  private readonly IAutoComplete _autoComplete;
  private const string PROMPT = "> ";

  public ConsoleInputHandler(IConsoleManager consoleManager, IAutoComplete autoComplete)
  {
    _consoleManager = consoleManager;
    _autoComplete = autoComplete;
  }

  public InputResult Handle(ConsoleInput input)
  {
    return input.Key.Key switch
    {
      ConsoleKey.Enter => HandleEnter(input),
      ConsoleKey.UpArrow => HandleUpArrow(input),
      ConsoleKey.DownArrow => HandleDownArrow(input),
      ConsoleKey.Backspace => HandleBackspace(input),
      ConsoleKey.Tab => HandleTab(input),
      _ => HandleCharacter(input)
    };
  }

  private InputResult HandleEnter(ConsoleInput input)
  {
    return new InputResult(new InputState(input.State.Text, 0), true);
  }

  private InputResult HandleUpArrow(ConsoleInput input)
  {
    var historyCommand = _consoleManager.GetPreviousCommand();
    if (string.IsNullOrEmpty(historyCommand))
      return new InputResult(input.State, false);

    _consoleManager.ClearCurrentLine();
    _consoleManager.Write(PROMPT);
    _consoleManager.Write(historyCommand);
    return new InputResult(new InputState(historyCommand, historyCommand.Length), false);
  }

  private InputResult HandleDownArrow(ConsoleInput input)
  {
    var historyCommand = _consoleManager.GetNextCommand();
    _consoleManager.ClearCurrentLine();
    _consoleManager.Write(PROMPT);
    _consoleManager.Write(historyCommand);
    return new InputResult(new InputState(historyCommand, historyCommand.Length), false);
  }

  private InputResult HandleBackspace(ConsoleInput input)
  {
    if (input.State.Position == 0)
      return new InputResult(input.State, false);

    var newText = input.State.Text.Remove(input.State.Position - 1, 1);
    var newPosition = input.State.Position - 1;
    _consoleManager.WriteBackspace();
    return new InputResult(new InputState(newText, newPosition), false);
  }

  private InputResult HandleTab(ConsoleInput input)
  {
    var suggestions = _autoComplete.GetSuggestions(input.State.Text);
    if (!suggestions.Any())
      return new InputResult(input.State, false);

    var suggestion = _autoComplete.GetNextSuggestion();
    _consoleManager.ClearCurrentLine();
    _consoleManager.Write(PROMPT);
    _consoleManager.Write(suggestion);
    return new InputResult(new InputState(suggestion, suggestion.Length), false);
  }

  private InputResult HandleCharacter(ConsoleInput input)
  {
    var newText = input.State.Text.Insert(input.State.Position, input.Key.KeyChar.ToString());
    var newPosition = input.State.Position + 1;
    _consoleManager.Write(input.Key.KeyChar.ToString());
    return new InputResult(new InputState(newText, newPosition), false);
  }
}
