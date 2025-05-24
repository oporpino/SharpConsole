namespace SharpConsole.Domain.Entities;

public readonly struct ConsoleInput
{
  public string Text { get; }
  public int Position { get; }
  public ConsoleKeyInfo Key { get; }

  public ConsoleInput(string text, int position, ConsoleKeyInfo key)
  {
    Text = text;
    Position = position;
    Key = key;
  }
}

public readonly struct InputResult
{
  public string Input { get; }
  public int Position { get; }
  public bool IsComplete { get; }

  public InputResult(string input, int position, bool isComplete)
  {
    Input = input;
    Position = position;
    IsComplete = isComplete;
  }
}
