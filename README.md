# Inflow

![Modular Monolith](https://cdn.devmentors.io/courses/modular-monolith/modular-monolith-overview.png)

## About

**Inflow** is the sample **virtual payments** app built as **[Modular Monolith](https://modularmonolith.net)**, written in **[.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)**. Each module is an independent **vertical slice** with its custom architecture, and the overall integration between the modules is mostly based on the **event-driven** approach to achieve **greater autonomy** between the modules. The shared components (such as cross-cutting concerns & abstractions) can be also found in a **[modular framework](https://github.com/devmentors/modular-framework)**.

## Starting the application

Start the infrastructure ([PostgreSQL](https://www.postgresql.org) & [RabbitMQ](https://www.rabbitmq.com)) using [Docker](https://docs.docker.com/get-docker/):

```
docker-compose up -d
```

Start API Gateway:

```
cd src/APIGateway/Inflow.APIGateway
dotnet run
```

Start Bootstrapper:

```
cd src/Bootstrapper/Inflow.Bootstrapper
dotnet run
```

Start Microservice:

```
cd src/Services/Customers/Inflow.Services.Customers.Api
dotnet run
```

## Solution structure

### API Gateway

An entry point to the system, acting as the only API (public facade), hiding the internal complexity of the distributed system, which now consists of the modular monolith + single microservices, transitioned from the existing module.

### Customers service

The very first microservice extracted out from the existing customers' module.

### Bootstrapper

Web application responsible for initializing and starting all the modules - loading configurations, running DB migrations, exposing public APIs etc.

### Modules

**Autonomous modules** with the different set of responsibilities, highly decoupled from each other - there's no reference between the modules at all (such as shared projects for the common data contracts), and the synchronous communication & asynchronous integration (via events) is based on **local contracts** approach.

- Customers - managing the customers (create, complete, verify, browse) - transitioned to microservice.
- Payments - managing the money deposits & withdrawals (to/from actual bank account).
- Wallets -managing the virtual wallets & money transfers between them.
- Users - managing the users/identity (register, login, permissions etc.).

Each module contains its own HTTP requests definitions file (`.rest`) using [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension.

### Saga

Sample Saga pattern implementation for transactional handling the business processes spanning across the distinct modules.

### Shared

The set of shared components for the common abstractions & cross-cutting concerns. In order to achieve even better decoupling, it's split into the separate *Abstractions* and *Infrastructure*, where the former does contain public abstractions and the latter their implementation.

## Microservices transition

You're browsing now (on `microservices` branch) the sample module to microservice transition. The complete, microservices based solution can be found in **[Inflow-micro](https://github.com/devmentors/Inflow-micro)** repository.

## Additional resources

- [Modular Framework](https://github.com/devmentors/modular-framework) project containing the shared components for own usage.

- [Knowledge base](https://modular-monolith.knowledge.devmentors.io) with articles.

- [Mini-course](https://www.youtube.com/watch?v=MkdutzVB3pY) about the fundamentals of the modular monolith based on sample [NPay](https://github.com/devmentors/NPay) repository.

- [Building Modular Monolith](https://www.youtube.com/playlist?list=PLqqD43D6Mqz1QLbHRgQ-poMpBpJ4lYi42) series on YouTube

- **[Comprehensive course](https://devmentors.io/courses/modular-monolith)** about building the modular monoliths. 

[![Modular Monolith](https://cdn.devmentors.io/courses/modular-monolith/modular_monolith_course_trailer_thumbnail.webp)](https://www.youtube.com/watch?v=i9783r9jSpQ)