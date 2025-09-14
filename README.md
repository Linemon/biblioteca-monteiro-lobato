# Biblioteca Pública Municipal "Monteiro Lobato"

Sistema de gerenciamento de biblioteca desenvolvido com arquitetura moderna, separando frontend e backend para facilitar manutenção e escalabilidade.

## 📋 Sobre o Projeto

Este é um sistema completo de gerenciamento de biblioteca que permite:
- Cadastro e gerenciamento de livros
- Cadastro e controle de leitores
- Sistema de empréstimos e devoluções
- Dashboard com estatísticas
- Interface moderna e responsiva

## 🏗️ Arquitetura do Sistema

O projeto está organizado em uma arquitetura de separação de responsabilidades:

```
biblioteca-monteiro-lobato/
├── backend/                 # API REST em .NET 8
│   ├── src/
│   │   └── WebApi/         # Projeto principal da API
│   │       ├── Controllers/ # Controladores da API
│   │       ├── Models/      # Modelos de dados
│   │       ├── Context/     # Contexto do Entity Framework
│   │       ├── Repositories/# Repositórios para acesso a dados
│   │       └── Migrations/  # Migrações do banco de dados
│   ├── docker-compose.yml   # Configuração do PostgreSQL
│   └── global.json         # Configuração do .NET SDK
├── frontend/               # Interface em Vue.js 3
│   ├── components/         # Componentes Vue
│   ├── styles/            # Estilos CSS
│   └── vite.config.js     # Configuração do Vite
└── README.md              # Este arquivo
```

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 8.0** - Framework principal
- **Entity Framework Core 9.0.9** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados relacional
- **Npgsql** - Driver PostgreSQL para .NET
- **JWT Authentication** - Sistema de autenticação com tokens
- **BCrypt** - Hash seguro de senhas
- **Swagger/OpenAPI** - Documentação da API
- **Docker Compose** - Containerização do banco de dados

### Frontend
- **Vue.js 3.4.0** - Framework JavaScript
- **Vite 5.0.0** - Build tool e servidor de desenvolvimento
- **Bootstrap 5.3.2** - Framework CSS
- **Bootstrap Icons 1.11.0** - Ícones

## 📋 Pré-requisitos

### Para o Backend
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (para PostgreSQL)

### Para o Frontend
- [Node.js 16+](https://nodejs.org/)
- [npm 8+](https://www.npmjs.com/) (vem com Node.js)

## 🚀 Como Executar o Projeto

### 1. Configuração do Banco de Dados

Primeiro, inicie o PostgreSQL usando Docker Compose:

```bash
cd backend
docker-compose up -d
```

Isso irá:
- Criar um container PostgreSQL na porta 5432
- Configurar o banco `biblioteca-monteiro-lobato`
- Usuário: `admin`, Senha: `root`

### 2. Configuração do Backend

```bash
cd backend/src/WebApi

# Restaurar dependências
dotnet restore

# Aplicar migrações do banco de dados
dotnet ef database update

# Executar a API
dotnet run
```

A API estará disponível em:
- **HTTPS**: `https://localhost:7069`
- **HTTP**: `http://localhost:5088`
- **Swagger**: `https://localhost:7069/swagger`

### 3. Configuração do Frontend

```bash
cd frontend

# Instalar dependências
npm install

# Executar em modo de desenvolvimento
npm run dev
```

O frontend estará disponível em: `http://localhost:3000`

## 📊 Funcionalidades

### Gestão de Leitores ✅
- Cadastro de leitores (nome, email, telefone)
- Controle de status do leitor
- Edição e atualização de informações
- API REST completa implementada

### Gestão de Livros ⚠️
- Cadastro de novos livros (título, autor, ISBN, data de publicação)
- Controle de status (disponível, emprestado, reservado)
- Edição e atualização de informações
- **Nota**: Funcionalidades implementadas no modelo Employee, sem API REST específica

### Sistema de Empréstimos ⚠️
- Registro de empréstimos
- Controle de datas de devolução
- Devolução de livros
- **Nota**: Funcionalidades implementadas no modelo Employee, sem API REST específica

### Dashboard ⚠️
- Interface frontend implementada
- Navegação entre módulos
- **Nota**: Algumas funcionalidades podem estar limitadas devido à falta de APIs REST para livros e empréstimos

## 🔧 Configurações

### Backend
- **Connection String**: Configurada em `appsettings.Development.json`
- **Porta**: Configurada em `Properties/launchSettings.json`
- **CORS**: Configurado para aceitar requisições do frontend

### Frontend
- **Porta**: 3000 (configurada em `vite.config.js`)
- **API Base URL**: Configurada para `http://localhost:5088`

## 📁 Estrutura de Dados

### Entidades Principais
- **Book**: Livros da biblioteca
- **Reader**: Leitores cadastrados
- **Employee**: Funcionários da biblioteca
- **BookLoan**: Empréstimos realizados

### Relacionamentos
- Um livro pode ter um empréstimo ativo
- Um leitor pode ter múltiplos empréstimos
- Um funcionário registra os empréstimos

## 🐳 Docker

O projeto inclui configuração Docker para o banco PostgreSQL:

```yaml
services:
  postgres:
    image: postgres:16
    environment:
      POSTGRES_USER: admin
      POSTGRES_PASSWORD: root
      POSTGRES_DB: biblioteca-monteiro-lobato
    ports:
      - "5432:5432"
```

## 🔍 API Endpoints

### Autenticação
- `POST /api/v1/auth/login` - Login de funcionário
- `POST /api/v1/auth/refresh` - Renovar tokens
- `POST /api/v1/auth/logout` - Logout (requer autenticação)
- `GET /api/v1/auth/me` - Informações do usuário atual (requer autenticação)

### Leitores (Requer Autenticação)
- `GET /api/v1/readers` - Listar todos os leitores
- `GET /api/v1/readers/{id}` - Buscar leitor por ID
- `POST /api/v1/readers` - Criar novo leitor
- `PUT /api/v1/readers/{id}` - Atualizar leitor

### Livros (Requer Autenticação)
*Nota: Os endpoints de livros são gerenciados através dos métodos do modelo Employee. Não há controladores REST específicos implementados.*

### Empréstimos (Requer Autenticação)
*Nota: Os endpoints de empréstimos são gerenciados através dos métodos do modelo Employee. Não há controladores REST específicos implementados.*

*Nota: Todos os endpoints requerem autenticação JWT.*

## 🎨 Interface

A interface foi desenvolvida com foco na usabilidade:
- Design responsivo com Bootstrap
- Componentes Vue.js reutilizáveis
- Navegação intuitiva
- Feedback visual para ações do usuário

## 🔒 Segurança

- **Autenticação JWT** com access tokens e refresh tokens
- **BCrypt** para hash seguro de senhas
- Validação de dados no backend
- Controle de permissões por funcionário
- Sanitização de inputs
- CORS configurado adequadamente
- Tokens com expiração configurável
- Sistema de logout que revoga tokens

## 🔐 Sistema de Autenticação

### Login
```json
POST /api/v1/auth/login
{
  "email": "admin@biblioteca.com",
  "password": "senha123"
}
```

### Response
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "base64encodedrefreshtoken...",
  "expiresAt": "2024-01-15T10:30:00Z",
  "employee": {
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Administrador",
    "email": "admin@biblioteca.com"
  }
}
```

### Refresh Token
```json
POST /api/v1/auth/refresh
{
  "refreshToken": "base64encodedrefreshtoken..."
}
```

### Logout
```bash
POST /api/v1/auth/logout
Authorization: Bearer {accessToken}
```

### Informações do Usuário
```bash
GET /api/v1/auth/me
Authorization: Bearer {accessToken}
```

## 🔑 BCrypt para Senhas

### Gerar Hash de Senha
```csharp
var hashedPassword = AuthController.HashPassword("minhasenha123");
```

### Criar Funcionário Admin
```csharp
var adminPassword = "admin123";
var hashedPassword = AuthController.HashPassword(adminPassword);

var adminEmployee = new Employee(
    "Administrador", 
    "admin@biblioteca.com", 
    "123456789", 
    hashedPassword
);
```

### Características do BCrypt
- **Salt automático**: Cada hash é único
- **Custo configurável**: 12 rounds (seguro)
- **Resistente a ataques**: Rainbow tables, timing attacks
- **Verificação segura**: No login automaticamente

## 🏗️ Arquitetura Simplificada

O projeto foi desenvolvido com foco na simplicidade:

### Backend
- **Sem injeção de dependência** - Serviços instanciados diretamente
- **Sem interfaces** - Classes concretas usadas diretamente
- **Sem async/await** - Operações síncronas para simplicidade
- **Sem tags [FromBody]** - Parâmetros vêm automaticamente do body
- **Sem modelos de request/response** - Usa objetos anônimos
- **BCrypt para senhas** - Hash seguro implementado
- **Gerenciamento através de modelos** - Livros e empréstimos são gerenciados através dos métodos do modelo Employee

### Padrão de Controllers
```csharp
[ApiController]
[Route("api/v1/readers")]
[Authorize]
public class ReadersController : ControllerBase
{
    // Instanciação direta de repositórios
    private EmployeeRepository _employeeRepository = new(context);
    
    // Métodos síncronos
    public ActionResult GetAll()
    {
        // Objetos anônimos para responses
        return Ok(readers.Select(x => new { x.Id, x.CreatedAt }));
    }
}
```

### Gerenciamento de Livros e Empréstimos

No sistema atual, livros e empréstimos são gerenciados através dos métodos do modelo `Employee`:

```csharp
// Gerenciamento de Livros
employee.RegisterBook(title, author, isbn, publicationDate, status);
employee.UpdateBook(bookId, bookData);
employee.RemoveBook(bookId);

// Gerenciamento de Empréstimos
employee.RegisterBookLoan(book, reader);
employee.RegisterBookReturn(bookLoanId);
```

**Nota**: Atualmente não há controladores REST específicos para livros e empréstimos. As funcionalidades estão implementadas no modelo `Employee` e podem ser acessadas através dos repositórios correspondentes.

## ⚠️ Status Atual do Projeto

### Implementado ✅
- Sistema de autenticação JWT completo
- Gestão de leitores com API REST
- Modelos de dados para livros e empréstimos
- Interface frontend em Vue.js
- Banco de dados PostgreSQL configurado

### Parcialmente Implementado ⚠️
- Gestão de livros (modelos e repositórios prontos, sem API REST)
- Sistema de empréstimos (modelos e repositórios prontos, sem API REST)
- Dashboard (interface pronta, funcionalidades limitadas)

### Melhorias Futuras Sugeridas
- Implementar controladores REST para livros (`BooksController`)
- Implementar controladores REST para empréstimos (`BookLoansController`)
- Adicionar endpoints para estatísticas do dashboard
- Implementar sistema de notificações para atrasos
- Adicionar validações mais robustas
- Implementar testes unitários

## 📝 Desenvolvimento

### Comandos Úteis

**Backend:**
```bash
# Criar nova migração
dotnet ef migrations add NomeDaMigracao

# Aplicar migrações
dotnet ef database update

# Remover última migração
dotnet ef migrations remove
```

**Frontend:**
```bash
# Build para produção
npm run build

# Preview da build
npm run preview
```
