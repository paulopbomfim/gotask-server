# GoTask (Backend) - EM DESENVOLVIMENTO

Este repositório contém a API do GoTask construída em .NET 9 (ASP.NET Core). Abaixo você encontra como iniciar o projeto rapidamente com Docker (recomendado) ou rodando localmente com o .NET SDK.

## Pré‑requisitos
- Docker e Docker Compose (para o modo com containers)
- .NET SDK 9.0 (para rodar localmente via `dotnet`)

## Subir tudo com Docker (recomendado)
O projeto já inclui um `compose.yaml` que sobe:
- API (.NET) na porta local 5284
- MySQL 8.4.6 na porta local 3306 (com volume persistente)

Passos:
1. Na raiz do projeto (onde está o `compose.yaml`), execute:
   - Linux/macOS: `docker compose up -d --build`
   - Windows (PowerShell/CMD): `docker compose up -d --build`
2. Aguarde a API iniciar. Na primeira execução, as migrações do EF Core são aplicadas automaticamente.
3. Acesse:
   - API: http://localhost:5284
   - Documentação (Scalar): http://localhost:5284/scalar/v1
   - OpenAPI/Swagger JSON: http://localhost:5284/openapi/v1.json

Variáveis relevantes já definidas no compose:
- `ASPNETCORE_ENVIRONMENT=Development`
- `ASPNETCORE_HTTP_PORTS=80` (porta interna do container)

Banco de dados (MySQL):
- Host: `localhost` (a partir da sua máquina) / `database` (de dentro da rede docker)
- Porta: `3306`
- Database: `gotask`
- Usuário: `gotaskuser`
- Senha: `Password123!`

Para derrubar os containers: `docker compose down`

## Rodar localmente com .NET SDK
Você pode usar o banco via Docker e rodar a API localmente com o SDK.

1) Suba somente o banco de dados (opcional, caso você não tenha MySQL local):
- `docker compose up -d database`

2) Ajuste a Connection String para apontar para `localhost` (quando a API roda fora do Docker):
- A configuração padrão de Desenvolvimento (`appsettings.Development.json`) usa `Server=database;...` (funciona dentro do Docker).
- Para rodar localmente, sobrescreva a connection string para `Server=localhost;Database=gotask;Port=3306;Uid=gotaskuser;Pwd=Password123!;`.

Formas de sobrescrever:
- Via variável de ambiente (PowerShell):
  - `$env:ConnectionStrings__MySqlConnection="Server=localhost;Database=gotask;Port=3306;Uid=gotaskuser;Pwd=Password123!;"`
- Ou use User Secrets (recomendado para desenvolvimento):
  - Dentro do projeto `src/GoTask.API`: `dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=gotask;Port=3306;Uid=gotaskuser;Pwd=Password123!;"`

3) Exportar ambiente de desenvolvimento (se necessário):
- PowerShell: `$env:ASPNETCORE_ENVIRONMENT="Development"`

4) Restaurar, compilar e executar a API:
- `dotnet restore`
- `dotnet build`
- `dotnet run --project src/GoTask.API`

URLs locais (launchSettings):
- HTTP: http://localhost:5267
- HTTPS: https://localhost:7178
- Docs (Scalar): http://localhost:5267/scalar/v1

Observações:
- Em ambiente Development, as migrações do EF Core são aplicadas automaticamente no startup.
- A API utiliza autenticação JWT. A chave e o tempo de expiração estão em `Settings:Jwt` do `appsettings.Development.json`. Para produção, configure suas próprias chaves seguras.

## Rodando os testes
Na raiz do repositório:
- `dotnet test`

Isso executa as baterias de testes localizadas na pasta `test/`.

## Problemas comuns (Troubleshooting)
- Porta 3306 em uso: pare outros serviços MySQL locais ou altere a porta do serviço no `compose.yaml`.
- API não conecta no banco ao rodar localmente: verifique se a connection string aponta para `Server=localhost` (quando fora do Docker) e se o container do MySQL está de pé (`docker ps`).
- Migrações demorando na primeira execução: a aplicação aplica as migrações automaticamente; espere alguns segundos após o primeiro start.

## Estrutura do projeto (resumo)
- `src/GoTask.API`: projeto Web API (endpoints, Program.cs, configurações)
- `src/GoTask.Application`: casos de uso e serviços de aplicação
- `src/GoTask.Domain`: entidades e contratos de domínio
- `src/GoTask.Infrastructure`: EF Core, repositórios, migrations, autenticação JWT
- `test/`: projetos de teste