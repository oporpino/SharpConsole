using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Domain.Entities;

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
