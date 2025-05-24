using SharpConsole.Domain.Entities;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Infrastructure;

public class ConsoleInputHandler : IInputHandler
{
  private readonly IConsoleManager _consoleManager;

  public ConsoleInputHandler(IConsoleManager consoleManager)
  {
    _consoleManager = consoleManager;
  }

  public InputResult Handle(ConsoleInput input)
  {
    return input.Key.Key switch
    {
      ConsoleKey.Enter => HandleEnter(input),
      ConsoleKey.UpArrow => HandleUpArrow(input),
      ConsoleKey.DownArrow => HandleDownArrow(input),
      ConsoleKey.Backspace => HandleBackspace(input),
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
    _consoleManager.Write(historyCommand);
    return new InputResult(new InputState(historyCommand, historyCommand.Length), false);
  }

  private InputResult HandleDownArrow(ConsoleInput input)
  {
    var historyCommand = _consoleManager.GetNextCommand();
    _consoleManager.ClearCurrentLine();
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

  private InputResult HandleCharacter(ConsoleInput input)
  {
    var newText = input.State.Text.Insert(input.State.Position, input.Key.KeyChar.ToString());
    var newPosition = input.State.Position + 1;
    _consoleManager.Write(input.Key.KeyChar.ToString());
    return new InputResult(new InputState(newText, newPosition), false);
  }
}
