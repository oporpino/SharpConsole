using SharpConsole.Domain.Inbound;

namespace SharpConsole.Domain.Entities;

public readonly struct ConsoleInput
{
  public IInputState State { get; }
  public ConsoleKeyInfo Key { get; }

  public ConsoleInput(IInputState state, ConsoleKeyInfo key)
  {
    State = state;
    Key = key;
  }
}
