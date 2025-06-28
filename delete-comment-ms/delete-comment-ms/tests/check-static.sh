#!/bin/bash
set -e

echo "==> Analizando sintaxis y formato del microservicio... "

dotnet format --verify-no-changes
dotnet build --no-restore

echo "¡Chequeo estático OK! 🚦"
