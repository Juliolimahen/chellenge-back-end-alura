# Controle de Finanças Pessoais API

Esta é uma API .NET Core para o controle de finanças pessoais, que foi desenvolvida como resolução de um desafio proposto pela Alura. Ela fornece endpoints para gerenciar receitas, despesas e resumos financeiros, além de contar também com um sistema de login e confirmarção por email.

## Requisitos

- .NET 6: [Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- Entity Framework Core 6.0: [Guia EF Core](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
- SQL Server 2019: [Download SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou Docker)
- Docker: [Download Docker](https://www.docker.com/get-started) (opcional)

## Instalação

1. Clone o repositório:

    ```shell
    git clone https://github.com/Juliolimahen/chellenge-back-end-alura.git
    cd FinancialControl
    ```

2. Configure a conexão do banco de dados e do servidor de envio de e-mails no arquivo`appsettings.json`.

3. Execute as migrações para criar o banco de dados:

    ```shell
    dotnet ef database update
    ```

4. Execute a aplicação:

    ```shell
    dotnet run
    ```

A API estará disponível em ... 

## Endpoints

A API fornece os seguintes endpoints:

### Receitas

- **GET** `/api/revenues`: Lista todas as receitas.
- **GET** `/api/revenue/{id}`: Retorna detalhes de uma receita específica.
- **POST** `/api/revenue`: Cria uma nova receita.
- **PUT** `/api/revenue/{id}`: Atualiza uma receita existente.
- **DELETE** `/api/revenue/{id}`: Exclui uma receita.
- **GET** `/api/revenue/year/month`: Busca as receitas do ano e mês.

### Despesas

- **GET** `/api/expenses`: Lista todas as despesas.
- **GET** `/api/expense{id}`: Retorna detalhes de uma despesa específica.
- **POST** `/api/expense`: Cria uma nova despesa.
- **PUT** `/api/expense/{id}`: Atualiza uma despesa existente.
- **DELETE** `/api/expenses/{id}`: Exclui uma despesa.
- **GET** `/api/expenses/year/month`: Busca as despesas do ano e mês.

### Resumos financeiros

- **GET** `/api/summary/year/month`: Faz um resumo do ano e mês.

### Login

Os endpoints de login permitem que os usuários autentiquem-se na API.

#### Login de Usuário

Autentica um usuário e fornece um token de acesso.

- **Método**: `POST`
- **Endpoint**: `/api/login`
- **Corpo da Requisição**:
    - `username` (string, obrigatório): Nome de usuário do usuário.
    - `password` (string, obrigatório): Senha do usuário.

- **Resposta de Sucesso**:
    - **Código**: `200 OK`
    - **Corpo da Resposta**:
        - `id` (int): ID do usuário.
        - `username` (string): Nome de usuário.
        - `token` (string): Token de acesso.

- **Resposta de Falha**:
    - **Código**: `401 Unauthorized`
    - **Corpo da Resposta**:
        - `success` (boolean): `false`.
        - `erros` (array de strings): Lista de erros, por exemplo, "Login inválido."

### Registro

Os endpoints de registro permitem que os usuários criem uma nova conta na API.

#### Registro de Usuário

Cria uma nova conta de usuário.

- **Método**: `POST`
- **Endpoint**: `/api/register`
- **Corpo da Requisição**:
    - `username` (string, obrigatório): Nome de usuário desejado.
    - `password` (string, obrigatório): Senha desejada.

- **Resposta de Sucesso**:
    - **Código**: `201 Created`
    - **Corpo da Resposta**: Nenhum corpo na resposta.

- **Resposta de Falha**:
    - **Código**: `400 Bad Request`
    - **Corpo da Resposta**:
        - `success` (boolean): `false`.
        - `erros` (array de strings): Lista de erros, por exemplo, "Erro ao criar conta de usuário."

## Contribuição

Contribuições são bem-vindas! Se você deseja contribuir, siga estas etapas:

1. Faça um fork do repositório.
2. Crie um branch de recurso (`git checkout -b feature/nova-feature`).
3. Commit suas mudanças (`git commit -m 'Adicionar nova feature'`).
4. Push para o branch (`git push origin feature/nova-feature`).
5. Abra um pull request.


## Licença

Este projeto é licenciado sob a Licença MIT - consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
