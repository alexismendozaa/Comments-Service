# Add Comment Microservice

This project is a simple microservice built with **.NET 8** that allows adding comments to a system. It is designed with microservice principles and containerization in mind using Docker.

## 🚀 Features

- Minimal API using ASP.NET Core
- JSON configuration support
- Docker and Docker Compose support
- Designed for scalability and easy integration

## 🧰 Technologies Used

- .NET 8
- ASP.NET Core Minimal API
- Docker
- Docker Compose

## 📂 Project Structure

comments-service/
├── docker-compose.yml
└── add-comment-ms/
├── add-comment-ms.sln
└── add-comment-ms/
├── add-comment-ms.csproj
├── Program.cs
├── WeatherForecast.cs
├── appsettings.json
├── appsettings.Development.json
└── Dockerfile

markdown
Copiar
Editar

## 🛠️ How to Run

1. Make sure you have Docker installed.
2. Run the microservice using Docker Compose:


docker-compose up --build
The service will be available at http://localhost:5000.

📬 API Endpoints
GET /weatherforecast: Test endpoint included by default in new projects.

📦 Build and Run Without Docker
bash
Copiar
Editar
cd add-comment-ms/add-comment-ms
dotnet run
👤 Author
Generated and customized with ❤️ by OpenAI's ChatGPT.

yaml
Copiar
Editar

---
