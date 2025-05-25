using SharpConsole.Core.Domain.Entities;

namespace SharpConsole.Core.Domain.Outbound;

public interface IInputHandler
{
  InputResult Handle(ConsoleInput input);
}
