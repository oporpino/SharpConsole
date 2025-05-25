using System.Text.Json;
using SharpConsoleCore.Domain.Outbound;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SharpConsoleCore.Infrastructure;

public class JsonOutputFormatter : IOutputFormatter
{
  private readonly JsonSerializerOptions _options;

  public JsonOutputFormatter()
  {
    _options = new JsonSerializerOptions
    {
      WriteIndented = true,
      DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
      ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };
  }

  public string Format(object? value)
  {
    if (value == null) return "null";

    if (value is string str) return str;

    var type = value.GetType();
    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DbSet<>))
    {
      var list = ((dynamic)value).ToList();
      return FormatEnumerable(list);
    }

    if (value is IEnumerable<object> enumerable)
    {
      return FormatEnumerable(enumerable);
    }

    return JsonSerializer.Serialize(value, _options);
  }

  private string FormatEnumerable(IEnumerable<object> enumerable)
  {
    var items = enumerable.Select(item => Format(item)).ToList();
    return $"[{string.Join(", ", items)}]";
  }
}
