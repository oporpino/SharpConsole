using System.Collections.Generic;

namespace SharpConsole.Core
{
  public interface ICommandHistory
  {
    void AddCommand(string command);
    IEnumerable<string> GetHistory();
    void Clear();
    string NavigateUp();
    string NavigateDown();
    void ResetNavigation();
  }
}
