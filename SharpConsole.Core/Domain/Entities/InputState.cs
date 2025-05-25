using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore.Domain.Entities;

public readonly struct InputState : IInputState
{
  public string Text { get; }
  public int Position { get; }

  public InputState(string text, int position)
  {
    Text = text;
    Position = position;
  }
}
