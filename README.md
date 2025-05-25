# SharpConsole.Core

A powerful console application framework for .NET that provides auto-completion, command execution, and more.

## Features

- Auto-completion for properties, methods, and fields
- Command execution in a REPL-like environment
- Support for nested object navigation
- Modern .NET 8.0 support

## Installation

```bash
dotnet add package SharpConsole.Core
```

## Quick Start

```csharp
using SharpConsole.Core.Infrastructure;
using SharpConsole.Core.Domain;

var console = new ConsoleContext();
var autoComplete = new AutoComplete();

// Get suggestions for a partial input
var suggestions = autoComplete.GetSuggestions("Console.");
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.
