# 📝 Add Comment Microservice

This microservice allows creating new comments in the system. It's built with ASP.NET Core and is designed to run as part of a containerized architecture.

## 🚀 Features

- POST endpoint to create comments
- Configurable via environment and `appsettings.json`
- Docker-ready
- JWT authentication middleware (if enabled)

## 📦 Project Structure

add-comment-ms/
└── add-comment-ms/
├── Controllers/
├── Models/
├── Program.cs
├── Dockerfile
├── appsettings.json
└── .env

pgsql
Copiar
Editar

## 🛠️ Usage


cd add-comment-ms/add-comment-ms
dotnet run
Or with Docker:

bash
Copiar
Editar
docker build -t add-comment-ms .
docker run -p 5001:80 add-comment-ms
🔐 Environment
Set your environment variables in .env or use appsettings.json.

pgsql
Copiar
Editar

---