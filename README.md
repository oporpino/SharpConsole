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

Here's how to use SharpConsole.Platform in your project:

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
using SharpConsole.Platform.Application;
using SharpConsole.Core.Entities;

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

## Example Project

To help you get started quickly, we've created a comprehensive example project that demonstrates all the features of SharpConsole. The example includes:

- Basic property access and navigation
- Entity Framework Core integration
- Custom command implementation
- Dependency injection setup
- Best practices and patterns

You can find the example project at: [SharpConsole.Examples](https://github.com/oporpino/SharpConsole.Examples)

## Contributing

Contributions are welcome! Feel free to submit a Pull Request.

## License

This project is licensed under a custom license based on the Apache License 2.0 with additional restrictions to protect the source code:

- Use of this software is permitted for both commercial and non-commercial purposes via official package distribution channels only (e.g., NuGet.org).
- The source code may not be copied, forked, mirrored, or redistributed in whole or in part, whether modified or unmodified, without explicit written permission from the author.
- Republishing or re-hosting the source code, including forks or derived public repositories, is strictly prohibited.
- This license grants permission to use, execute, and link against the compiled version of the software.

Please refer to the full license in the [LICENSE](LICENSE) file.

## License of the `.examples` folder

The files in this folder are provided solely for demonstration and learning purposes.

You may **copy and adapt code snippets** for your own projects, including commercial ones.

However, **redistributing, forking, or publishing this folder as a whole or in derived form is prohibited without explicit permission from the author**.

For more details, please see the main license file in the root repository.
