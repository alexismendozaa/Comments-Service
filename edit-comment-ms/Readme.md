### 📘 `edit-comment-ms/README.md`


# ✏️ Edit Comment Microservice

This service is responsible for editing existing comments. It receives an ID and updated data to perform modifications securely.

## 🚀 Features

- PUT endpoint for comment updates
- JWT-based authorization
- Docker support
- Environment-based configuration

## 📦 Project Structure
 
edit-comment-ms/
└── edit-comment-ms/
├── Controllers/
├── Models/
├── Program.cs
├── Dockerfile
├── appsettings.json
└── .env

bash
Copiar
Editar

## 🛠️ Usage


cd edit-comment-ms/edit-comment-ms
dotnet run
Or:

bash
Copiar
Editar
docker build -t edit-comment-ms .
docker run -p 5002:80 edit-comment-ms
🔐 Configuration
Use .env for secrets and database connection strings.

yaml
Copiar
Editar

---