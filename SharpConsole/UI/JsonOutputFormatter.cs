using System.Text.Json;

namespace SharpConsole.UI;

public class JsonOutputFormatter : IOutputFormatter
{
  private static readonly JsonSerializerOptions _options = new()
  {
    WriteIndented = true
  };

  public string Format(object? value)
  {
    return value == null
        ? "null"
        : JsonSerializer.Serialize(value, _options);
  }
}
