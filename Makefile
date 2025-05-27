include .commons/Makefile

.PHONY: build run clean

# Build the container
build:
	docker compose build

# Clean up containers and volumes
clean:
	docker compose down -v

# Run the container
run.simple:
	docker compose build sharpconsole.simple
	docker compose run --rm sharpconsole.simple

run.entity.inmemory:
	docker compose build sharpconsole.entity.inmemory
	docker compose run --rm sharpconsole.entity.inmemory

# Publish NuGet package
nuget.publish:
	@.commons/scripts/make/nuget/publish SharpConsole.Core

# List NuGet package versions
nuget.versions:
	@.commons/scripts/make/nuget/list SharpConsole.Core
