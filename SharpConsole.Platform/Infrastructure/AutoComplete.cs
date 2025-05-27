using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpConsole.Core.Outbound;
using SharpConsole.Platform.Application;

namespace SharpConsole.Platform.Infrastructure;

public class AutoComplete : IAutoComplete
{
  private readonly List<string> _suggestions;
  private int _currentIndex;
  private const string DOT = ".";

  public AutoComplete()
  {
    _suggestions = new List<string>();
    _currentIndex = -1;
  }

  public List<string> GetSuggestions(string input)
  {
    _suggestions.Clear();
    _currentIndex = -1;

    if (string.IsNullOrEmpty(input))
      return _suggestions;

    var context = AplicationContext.Get();
    var parts = input.Split(DOT);

    if (parts.Length == 1)
    {
      AddMemberSuggestions(context, input);
    }
    else
    {
      var currentObject = GetNestedObject(context, parts.Take(parts.Length - 1));
      if (currentObject != null)
      {
        var prefix = string.Join(DOT, parts.Take(parts.Length - 1)) + DOT;
        AddMemberSuggestions(currentObject, parts.Last(), prefix);
      }
    }

    return _suggestions;
  }

  private void AddMemberSuggestions(object obj, string prefix, string fullPrefix = "")
  {
    var type = obj.GetType();

    // Add properties
    var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        .Select(p => fullPrefix + p.Name);
    _suggestions.AddRange(properties);

    // Add methods
    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Where(m => m.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        .Select(m => fullPrefix + m.Name + "()");
    _suggestions.AddRange(methods);

    // Add enum values if the property is an enum
    if (type.IsEnum)
    {
      var enumValues = Enum.GetNames(type)
          .Where(v => v.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
          .Select(v => fullPrefix + v);
      _suggestions.AddRange(enumValues);
    }

    // Add fields
    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
        .Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        .Select(f => fullPrefix + f.Name);
    _suggestions.AddRange(fields);
  }

  private object? GetNestedObject(object obj, IEnumerable<string> path)
  {
    var current = obj;
    foreach (var part in path)
    {
      var property = current.GetType().GetProperty(part, BindingFlags.Public | BindingFlags.Instance);
      if (property != null)
      {
        current = property.GetValue(current);
        if (current == null) return null;
        continue;
      }

      var field = current.GetType().GetField(part, BindingFlags.Public | BindingFlags.Instance);
      if (field != null)
      {
        current = field.GetValue(current);
        if (current == null) return null;
        continue;
      }

      return null;
    }
    return current;
  }

  public string GetNextSuggestion()
  {
    if (_suggestions.Count == 0)
      return string.Empty;

    _currentIndex = (_currentIndex + 1) % _suggestions.Count;
    return _suggestions[_currentIndex];
  }

  public string GetPreviousSuggestion()
  {
    if (_suggestions.Count == 0)
      return string.Empty;

    _currentIndex = (_currentIndex - 1 + _suggestions.Count) % _suggestions.Count;
    return _suggestions[_currentIndex];
  }
}
