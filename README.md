# SharpConsole

A powerful console application framework for .NET that provides auto-completion, command execution, and more.

## Features

- Auto-completion for properties, methods, and fields
- Command execution in a REPL-like environment
- Support for nested object navigation
- Modern .NET 8.0 support
- Entity Framework Core integration
- Dependency Injection support

## Installation

Add the SharpConsole package to your console project:

```bash
dotnet add package SharpConsole
```

## Usage

Here's how to use SharpConsole.Core in your project:

1. Create a class that inherits from SharpConsoleBase:

```csharp
public class CustomConsole : SharpConsoleBase
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string[] Tags { get; set; }
    public AppDbContext db;
}
```

2. In your Program.cs, create an instance of your console class and start it:

```csharp
using Microsoft.EntityFrameworkCore;
using SharpConsole.Core.Application;
using SharpConsole.Core.Domain.Entities;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase(databaseName: "ExampleDb")
    .Options;

using var context = new AppDbContext(options);

context.Users.Add(new User { Name = "Guga", Age = 38 });
context.Users.Add(new User { Name = "Bia", Age = 13 });
context.SaveChanges();

var console = new CustomConsole()
{
    db = context,
    Name = "Guga",
    Age = 38,
    Tags = new[] { "tag1", "tag2" }
};

console.Start();
```

Now you can interact with the console:
- Type `Name` to see the value
- Type `Age` to see the value
- Type `Tags` to see the array
- Type `db.Users` to see the database context
- Type `exit` to quit

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the GNU General Public License v3.0 - see the LICENSE file for details.
