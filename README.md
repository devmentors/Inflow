# Inflow

## About

Inflow is a simple virtual payments app based on [modular framework](https://github.com/devmentors/modular-framework). The overall architecture is mostly built using event-driven approach.

**How to start the solution?**
----------------

Start the infrastructure using [Docker](https://docs.docker.com/get-docker/):

```
docker-compose up -d
```

Start API located under Bootstrapper project:

```
cd src/Bootstrapper/Inflow.Bootstrapper
dotnet run
```