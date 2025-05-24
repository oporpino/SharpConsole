.PHONY: run test clean build

# Run the example project
run:
	dotnet run --project SharpConsole.Example

# Run all tests
test:
	dotnet test --logger "console;verbosity=detailed"

# Build the solution
build:
	dotnet build

# Clean build artifacts
clean:
	dotnet clean
	rm -rf **/bin/ **/obj/
