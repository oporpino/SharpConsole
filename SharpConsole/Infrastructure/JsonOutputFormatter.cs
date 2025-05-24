using System.Text.Json;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Infrastructure;

public class JsonOutputFormatter : IOutputFormatter
{
  private readonly JsonSerializerOptions _options;

  public JsonOutputFormatter()
  {
    _options = new JsonSerializerOptions
    {
      WriteIndented = true
    };
  }

  public string Format(object? value)
  {
    if (value == null) return "null";
    return JsonSerializer.Serialize(value, _options);
  }
}
