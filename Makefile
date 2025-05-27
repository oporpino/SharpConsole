include .commons/Makefile

.PHONY: build run clean

# Build the container
build:
	docker compose build

# Run the container
run.simple:
	docker compose build sharpconsole.simple
	docker compose run --rm sharpconsole.simple

run.entity-in-memory:
	docker compose build sharpconsole.entity-in-memory
	docker compose run --rm sharpconsole.entity-in-memory

run.entity-with-database:
	docker compose build sharpconsole.entity-with-database
	docker compose run --rm sharpconsole.entity-with-database

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
	@.commons/scripts/make/nuget/publish SharpConsole.Platform SharpConsole.Core

# List NuGet package versions
nuget.versions:
	@.commons/scripts/make/nuget/list SharpConsole.Platform
