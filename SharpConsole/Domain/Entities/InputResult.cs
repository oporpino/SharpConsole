using SharpConsole.Domain.Inbound;

namespace SharpConsole.Domain.Entities;

public readonly struct InputResult
{
  public IInputState State { get; }
  public bool IsComplete { get; }

  public InputResult(IInputState state, bool isComplete)
  {
    State = state;
    IsComplete = isComplete;
  }
}
