using SharpConsole.Core.Domain.Entities;

namespace SharpConsole.Core.Outbound;

public interface IInputHandler
{
  InputResult Handle(ConsoleInput input);
}
