using SharpConsoleCore.Domain.Entities;

namespace SharpConsoleCore.Domain.Outbound;

public interface IInputHandler
{
  InputResult Handle(ConsoleInput input);
}
