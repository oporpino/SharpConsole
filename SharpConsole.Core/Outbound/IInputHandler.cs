using SharpConsole.Core.Entities;

namespace SharpConsole.Core.Outbound;

public interface IInputHandler
{
  InputResult Handle(ConsoleInput input);
}
