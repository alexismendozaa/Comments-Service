### 📘 `delete-comment-ms/README.md`


# 🗑️ Delete Comment Microservice

This microservice handles the deletion of comments by ID. It's optimized for secure and safe removal operations.

## 🚀 Features

- DELETE endpoint with comment ID
- Secure authorization via JWT
- Lightweight container setup
- Plug-and-play with docker-compose

## 📦 Project Structure

delete-comment-ms/
└── delete-comment-ms/
├── Controllers/
├── Middleware/
├── Models/
├── Program.cs
├── Dockerfile
└── appsettings.json

pgsql
Copiar
Editar

## 🛠️ Usage


cd delete-comment-ms/delete-comment-ms
dotnet run
Or run with Docker:

bash
Copiar
Editar
docker build -t delete-comment-ms .
docker run -p 5003:80 delete-comment-ms
yaml
Copiar
Editar

---