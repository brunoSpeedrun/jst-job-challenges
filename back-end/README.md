# Data Validation Api

Executa validações de E-mail, cpf, cnpj e número de telefone.

## Descrição

Uma **REST API** feita em AspNet Core 3.1 e PostgreSQL.

## Execução do Projeto

Execute `docker-compose build` e logo após `docker-compose up` e a aplicação vai estar disponível em `http://localhost:5000`

> Necessário a instalação do [Docker](https://docs.docker.com/install/) e [Docker Compose](https://docs.docker.com/compose/install/).

Um usuário padrão está disponível para utilzação:

**UserName:** admin
**Password:** admin@123

## Estrutura do Projeto

O projeto foi estruturado da seguinte forma:

```
- src
    - Application
        - MediatR
            - Requests
            - Handlers
        - Services
    - Controllers
    - Configuration
    - Identity
- test
```

* **MediatR/Handlers** - Contém a lógica executada nas Controllers. Cada *Action* de uma *Controller* delega a responsabilidade para um *Handler* que irá processar o *Request*.

* **Services** - Contém os Serviços utilizados pela aplicação (JwtService, DataValidation)

* **Configuration** - Configura toda a parte de injeção de dependências, Swagger e Autenticação via Jwt.

* **Identity** - Configuração da parte de usuários da aplicação.

* **test** - Contém os testes unitários.

## Public Apis

O Projeto utiliza as apis [numverify](https://numverify.com/) para a validação de número de telefone, e [mailboxlayer](https://mailboxlayer.com/) para a validação de e-mail.