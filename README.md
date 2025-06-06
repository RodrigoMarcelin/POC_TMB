# POC_TMB

Este projeto é uma prova de conceito (PoC) para gerenciamento de pedidos. Ele foi desenvolvido utilizando uma arquitetura em camadas com os seguintes componentes:

- API Backend (C#/.NET): Responsável pela lógica de negócios, regras de domínio e comunicação com o banco de dados PostgreSQL.

- Frontend (React + Vite): Interface web para visualização, criação, edição e deleção de pedidos.

- Banco de Dados (PostgreSQL): Armazena os dados dos pedidos.

- Azure Service Bus: para mensageria.

## Pré-Requisitos
- Docker
- Docker Compose
- Criar uma queue no Azure Service Bus com o nome "order"

## Pré-Requisitos sem docker
- .NET 8.0 SDK (se não usar Docker)
- Node.js v 23.11 (se não usar Docker)
- Banco de dados PostgreSql v 17.4 (se não usar Docker)

# Execução do projeto
- Clonar o repositório do projeto
```git clone https://github.com/RodrigoMarcelin/POC_TMB.git```

- Entrar no [portal.azure](https://portal.azure.com/), criar uma fila no Azure Service Bus, do tipo Manager, com o nome order e copiar a Cadeia de conexão primária

- Entrar no docker-compose.yml da raiz do projeto, e colar a cadeia de conexão primária na variavel "ServiceBus__OrderManagementUpdate=" no service api.
```api:
    build:
      context: ./backend
      dockerfile: Dockerfile
    container_name: OrderManagement
    ports:
      - "5000:5000"  
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://0.0.0.0:5000
      - ConnectionStrings__DefaultConnection=Server=postgres;Port=5432;Database=orderManager;Username=postgres;Password=postgres
      - ServiceBus__OrderManagementUpdate={ AQUI }
    depends_on:
      - postgres  
    networks:
      - app_network
```

- Executar o comando:
```docker-compose up```

- Acessar [http://localhost:5173](http://localhost:5173), após o container inicializar.

- Video mostrando a configuração [Vídeo do YouTube](https://youtu.be/6wPsG8hB0t8)


## Projeto Order Management



A API foi desenvolvida seguindo uma arquitetura em camadas, onde cada camada possui responsabilidades bem definidas:

 ### Domain
 - Propósito:
    - Contém as entidades de domínio, regras de negócio e contratos que definem a lógica central da aplicação.

 - Exemplos:

    - Entidades (ex.: Order)

    - Interfaces dos repositórios

    - Validações e regras de negócio

 ### Application
 - Propósito:
    - Orquestra os casos de uso (Use Cases) da aplicação, definindo a lógica de interação entre as camadas.

 - Exemplos:

    - DTOs (Data Transfer Objects)

    - Casos de uso (ex.: DeleteOrderUseCase, CreateOrderUseCase)

 ### Infra
 - Propósito:
    - Responsável pela comunicação com a infraestrutura, como o acesso ao banco de dados e mensageria.

 - Exemplos:

    - Implementações dos repositórios

    - Configuração do Entity Framework Core

 ### API
 - Propósito:
    - Expor os endpoints HTTP e gerenciar as requisições, atuando como camada de apresentação.

 - Exemplos:

    - Controllers
    - Hubs

Configuração de middlewares e serviços (ex.: SignalR para comunicação em tempo real)

 ## Tecnologias Utilizadas
 - Backend: .NET 8.0, ASP.NET Core, Entity Framework Core, SinalR, Azure Service Bus

 - Frontend: React, Vite, SignalR, TailwindCSS 

 - Banco de Dados: PostgreSQL

 - Containerização: Docker, Docker Compose