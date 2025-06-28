# 🧱 Comment Service

This monorepo contains multiple .NET microservices related to comments management.

## 📦 Microservices

- `add-comment-ms` – Add a new comment
- `edit-comment-ms` – Edit existing comments
- `delete-comment-ms` – Remove comments
- `list-comment-ms` – Retrieve and list comments

Each microservice:
- Is independently deployable
- Has its own Dockerfile
- Uses ASP.NET Core and .NET 8
- Has a `Program.cs`, controller(s), and `appsettings.json`

## 🚀 Running the stack

Use Docker Compose to run all services together:

```bash
docker-compose up --build
