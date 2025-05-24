using System.Collections.Generic;

namespace SharpConsole.Domain.Outbound;

public interface ICommandHistory
{
  void AddCommand(string command);
  IEnumerable<string> GetHistory();
  void Clear();
  string NavigateUp();
  string NavigateDown();
  void ResetNavigation();
}
