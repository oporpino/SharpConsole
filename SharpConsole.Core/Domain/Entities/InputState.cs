using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Domain.Entities;

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
