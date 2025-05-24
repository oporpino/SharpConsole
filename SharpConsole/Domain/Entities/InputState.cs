using SharpConsole.Domain.Inbound;

namespace SharpConsole.Domain.Entities;

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
