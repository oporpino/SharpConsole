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

run.entity-inmemory:
	docker compose build sharpconsole.entity-inmemory
	docker compose run --rm sharpconsole.entity-inmemory

run.examples.simple:
	docker compose -f .examples/docker-compose.yml build sharpconsole.examples.simple
	docker compose -f .examples/docker-compose.yml run --rm sharpconsole.examples.simple

run.examples.entity-in-memory:
	docker compose -f .examples/docker-compose.yml build sharpconsole.examples.entity-in-memory
	docker compose -f .examples/docker-compose.yml run --rm sharpconsole.examples.entity-in-memory

run.examples.entity-with-database:
	docker compose -f .examples/docker-compose.yml build sharpconsole.examples.entity-with-database
	docker compose -f .examples/docker-compose.yml run --rm sharpconsole.examples.entity-with-database

# Publish NuGet package
nuget.publish:
	@.commons/scripts/make/nuget/publish SharpConsole.Core SharpConsole.Domain

# List NuGet package versions
nuget.versions:
	@.commons/scripts/make/nuget/list SharpConsole.Core
