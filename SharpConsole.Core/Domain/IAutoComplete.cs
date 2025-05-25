namespace SharpConsoleCore.Domain
{
  public interface IAutoCompletePort
  {
    List<string> GetSuggestions(string input);
    string GetNextSuggestion();
    string GetPreviousSuggestion();
  }
}
