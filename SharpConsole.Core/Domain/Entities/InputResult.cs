using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Domain.Entities;

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
