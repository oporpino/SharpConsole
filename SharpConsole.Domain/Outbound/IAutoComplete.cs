using System.Collections.Generic;

namespace SharpConsole.Domain.Outbound;

public interface IAutoComplete
{
  List<string> GetSuggestions(string input);
  string GetNextSuggestion();
  string GetPreviousSuggestion();
}

