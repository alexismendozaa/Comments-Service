### 📘 `list-comment-ms/README.md`


# 📄 List Comment Microservice

This service lists comments with optional filters and pagination. It connects to a PostgreSQL database and is designed for read efficiency.

## 🚀 Features

- GET endpoint for listing comments
- Pagination support
- PostgreSQL + Dapper integration
- Docker-ready and .env based config

## 📦 Project Structure

list-comment-ms/
└── list-comment-ms/
├── Controllers/
├── Models/
├── Services/
├── Program.cs
├── Dockerfile
├── appsettings.json
└── .env

bash
Copiar
Editar

## 🛠️ Usage


cd list-comment-ms/list-comment-ms
dotnet run
Or with Docker:

bash
Copiar
Editar
docker build -t list-comment-ms .
docker run -p 5004:80 list-comment-ms
🛢️ Environment Variables
Define DB connection in .env:

env
Copiar
Editar
ConnectionStrings__Default=Host=localhost;Database=comments;Username=postgres;Password=password;