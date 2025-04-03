# Web API de Gerenciamento de Produtos

Esta é uma API REST desenvolvida em .NET para o gerenciamento de produtos, utilizando autenticação JWT para segurança dos usuários. O banco de dados utilizado é o SQL Server, rodando em um contêiner Docker, e a API utiliza o Entity Framework para interação com os dados.

## Tecnologias Utilizadas

- **.NET** - Framework para desenvolvimento da API
- **SQL Server** - Banco de dados relacional
- **Entity Framework Core** - ORM para acesso ao banco de dados
- **JWT (JSON Web Token)** - Autenticação e segurança da API
- **Docker** - Containerização da aplicação e do banco de dados

## Como Executar o Projeto

### 1. Clonar o Repositório

```sh
git clone  https://github.com/Jjunior112/StoreApi.git
cd StoreApi
```

### 2. Configurar o Banco de Dados no Docker

Se ainda não tiver o SQL Server rodando em um contêiner Docker, execute:

```sh
docker run --name sqlserver -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuaSenhaForte!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

### 3. Configurar a Conexão com o Banco de Dados

No arquivo `appsettings.json`, configure a string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=sqlserverDb;User Id=sa;Password=SuaSenhaForte!;"
}
```

### 4. Aplicar as Migrações do Entity Framework

```sh
dotnet ef database update
```

### 5. Executar a API

```sh
dotnet run
```

A API estará disponível em `http://localhost:5000` ou `https://localhost:5001`.

## Autenticação JWT

A API utiliza JWT para autenticação. Para acessar os endpoints protegidos:

1. Registre um usuário com `POST /api/v{version}/User/signup`
2. Autentique-se com `POST /api/v{version}/User/signin`
3. Utilize o token retornado nos headers das requisições:

```sh
Authorization: Bearer <seu-token-jwt>
```

## Endpoints Principais

### Autenticação

- `POST /api/v{version}/User/signup` - Registro de usuários
- `POST /api/v{version}/User/signin` - Autenticação e obtenção do token JWT

### Produtos

- `GET /api/v{versio}/Product` - Listar produtos
- `GET /api/v{versio}/Product{id}` - Obter detalhes de um produto
- `POST /api/v{versio}/Product` - Criar um novo produto
- `PUT /api/v{versio}/Product/Edit/{id}` - Atualizar um produto
- `PUT /api/v{versio}/Product/Increase/{id}` - adicionar quantidades ao produto
- `PUT /api/v{versio}/Product/Decrease/{id}` -  remover quantidades de um produto
- `DELETE /api/v{versio}/Product{id}` - Remover um produto

## Contribuição

1. Fork este repositório
2. Crie uma branch (`git checkout -b minha-feature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Faça o push para a branch (`git push origin minha-feature`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
