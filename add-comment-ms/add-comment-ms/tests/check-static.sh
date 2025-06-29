echo "==> Analizando sintaxis y formato del microservicio..."


cd "$(dirname "$0")/.."

dotnet format $(ls *.csproj)

echo "==> Análisis completado" 
