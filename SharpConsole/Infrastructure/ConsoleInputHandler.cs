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
    _consoleManager.WriteLine("");
    return new InputResult(input.Text, 0, true);
  }

  private InputResult HandleUpArrow(ConsoleInput input)
  {
    var historyCommand = _consoleManager.GetPreviousCommand();
    if (string.IsNullOrEmpty(historyCommand))
      return new InputResult(input.Text, input.Position, false);

    _consoleManager.ClearCurrentLine();
    _consoleManager.Write(historyCommand);
    return new InputResult(historyCommand, historyCommand.Length, false);
  }

  private InputResult HandleDownArrow(ConsoleInput input)
  {
    var historyCommand = _consoleManager.GetNextCommand();
    _consoleManager.ClearCurrentLine();
    _consoleManager.Write(historyCommand);
    return new InputResult(historyCommand, historyCommand.Length, false);
  }

  private InputResult HandleBackspace(ConsoleInput input)
  {
    if (input.Position == 0)
      return new InputResult(input.Text, input.Position, false);

    var newText = input.Text.Remove(input.Position - 1, 1);
    var newPosition = input.Position - 1;
    _consoleManager.WriteBackspace();
    return new InputResult(newText, newPosition, false);
  }

  private InputResult HandleCharacter(ConsoleInput input)
  {
    var newText = input.Text.Insert(input.Position, input.Key.KeyChar.ToString());
    var newPosition = input.Position + 1;
    _consoleManager.Write(input.Key.KeyChar.ToString());
    return new InputResult(newText, newPosition, false);
  }
}
