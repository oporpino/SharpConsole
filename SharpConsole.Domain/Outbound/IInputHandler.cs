using SharpConsole.Domain.Entities;

namespace SharpConsole.Domain.Outbound;

public interface IInputHandler
{
  InputResult Handle(ConsoleInput input);
}
