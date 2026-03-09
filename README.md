# TaskHub

## About the Author

Hello 👋
My name is **Osama Magdy**, and I’m a **Senior Full-Stack Developer** specializing in **.NET Core and Angular**.

This project was built as a demonstration of the architecture and development practices I typically use in real-world applications.
While the implementation was assisted by an **AI development agent**, I provided the **architecture design, technical decisions, and detailed specifications** that guided the implementation.

Before generating the project, I discussed several design choices with the AI agent to evaluate different approaches and select the most suitable architecture and patterns. After the initial implementation, I **reviewed the generated code, applied additional refinements, and fully tested the application** to ensure it aligns with the standards and practices I normally follow.

The goal of this repository is to showcase a **Clean Architecture-based full-stack application** using modern tooling while demonstrating how AI-assisted development can be effectively guided by an experienced developer.


# TaskHub

TaskHub is a professional CRUD application demonstrating Clean Architecture with ASP.NET Core 8 and Angular 17+ standalone components.

## Project Structure

- **backend/**: .NET 8 Web API
  - **TaskHub.Domain**: Entities, Enums, Domain Events and Domain Logic.
  - **TaskHub.Application**: MediatR Commands/Queries, Behaviors, and Interfaces.
  - **TaskHub.Infrastructure**: EF Core, SQL Server, Repositories, and SaveChanges Interceptors.
  - **TaskHub.WebApi**: Controllers and API configuration.
- **frontend/taskhub-web**: Angular 17 Standalone Application utilizing **PrimeNG** with the modern **Aura** theme for a rich, responsive UI.

## Authentication & Security
- **Auth0 Integration**: Secured using OAuth2 and OpenID Connect (OIDC) Authorization Code Flow with PKCE.
- **Just-in-Time (JIT) Provisioning**: Automatically synchronizes and provisions users in the local database upon their first login using identity provider claims.

## Architecture Highlights
- **Clean Architecture**: Domain-driven design with strict dependency flow.
- **CQRS**: Separation of read and write operations using MediatR.
- **Unit of Work**: Automated save changes via MediatR pipeline behavior.
- **Interceptors**: Automated auditing and domain event dispatching.
- **Generic Repository**: Simplified data access with entity-specific extensions.

