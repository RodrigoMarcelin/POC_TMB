FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev-env
WORKDIR /app

COPY src/ .

WORKDIR OrderManagement.API

EXPOSE 5000

CMD ["dotnet", "watch", "run"]