# 📚 Biblioteca Pública Municipal "Monteiro Lobato"

Sistema de gerenciamento de biblioteca desenvolvido em **Vue.js 3 + Bootstrap 5**.

## 🚀 Tecnologias

- **Vue.js 3.4+** (Composition API)
- **Bootstrap 5.3+** 
- **Bootstrap Icons**
- **Vite** (build tool)
- **Node.js 16+**

## 📦 Instalação

```bash
# Instalar dependências
npm install

# Executar em desenvolvimento
npm run dev

# Build para produção
npm run build
```

## 🎨 Design System

- **Fonte:** Roboto
- **Cor primária:** #2D8CFF (azul)
- **Dimensões:** 1920x1080px (responsivo)
- **Framework CSS:** Bootstrap 5

## 📱 Telas do Sistema

1. **Login** - Autenticação do usuário
2. **Dashboard** - Visão geral do sistema
3. **Livros** - Listagem e gerenciamento de livros
4. **Formulário de Livro** - Cadastro/edição de livros
5. **Leitores** - Listagem e gerenciamento de leitores
6. **Formulário de Leitor** - Cadastro/edição de leitores
7. **Empréstimos** - Controle de empréstimos
8. **Formulário de Empréstimo** - Registro de novos empréstimos
9. **Notificações** - Sistema de alertas
10. **Esqueci Senha** - Recuperação de senha

## 📂 Estrutura do Projeto

```
├── App.vue                    # Componente principal
├── main.js                    # Entry point
├── index.html                 # HTML base
├── components/                # Componentes Vue da biblioteca
│   ├── LoginScreen.vue
│   ├── Dashboard.vue
│   ├── BooksScreen.vue
│   ├── BookFormScreen.vue
│   ├── ReadersScreen.vue
│   ├── ReaderFormScreen.vue
│   ├── LoansScreen.vue
│   ├── LoanFormScreen.vue
│   ├── NotificationsScreen.vue
│   └── ForgotPasswordScreen.vue
└── styles/
    └── globals.css           # Estilos globais + Bootstrap customização
```

## 🔧 Comandos Úteis

```bash
# Desenvolvimento
npm run dev

# Build
npm run build

# Preview do build
npm run preview
```

## 📋 Compatibilidade

- **Vue.js:** 3.0+ (recomendado 3.4+)
- **Bootstrap:** 5.0+ (recomendado 5.3+)
- **Node.js:** 16+ (recomendado 18+)
- **Navegadores:** Modernos (ES6+)