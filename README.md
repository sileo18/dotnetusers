# Track API

🌟 **Track API** é uma aplicação BaaS (Backend as a Service) que permite a produtores musicais gerenciar suas tracks de forma simples e eficiente. Os usuários podem criar contas, fazer login e fazer o upload de suas músicas para uma plataforma pública. A API oferece autenticação JWT e armazenamento de arquivos na AWS, além de fornecer uma interface para visualizar as tracks mais recentes.

## 📖 Visão Geral
A Track API foi desenvolvida para facilitar o processo de upload e compartilhamento de tracks de produtores musicais. A aplicação permite que os produtores se registrem, façam login e carreguem suas faixas, com a página inicial exibindo as tracks mais recentes. O projeto utiliza ASP.NET Core para o desenvolvimento da API, PostgreSQL para armazenamento de dados e AWS S3 para o armazenamento de arquivos.

## 🛠️ Tecnologias Utilizadas
- **ASP.NET Core**: O framework utilizado para construir APIs web robustas e escaláveis.
- **ASP.NET Core Identity**: Usado para gerenciar contas de usuário, autenticação e autorização com JWT.
- **Entity Framework Core**: Um ORM (Object-Relational Mapper) para interação fluida com o PostgreSQL.
- **PostgreSQL**: O banco de dados relacional utilizado para persistência de dados.
- **AWS SDK for .NET**: Para interagir com o AWS S3 para armazenamento de arquivos na nuvem (imagens e tracks).
- **Fluent Migrator** (ou Migrations do Entity Framework Core): Uma ferramenta para gerenciar migrações de esquema de banco de dados.
- **Docker**: Para conteinerizar a aplicação, garantindo fácil implantação e portabilidade.
- **JWT (JSON Web Tokens)**: Utilizado para autenticação segura de usuários.

## ⚙️ Funcionalidades Implementadas
### Gerenciamento de Usuários:
- 🆕 **Registro de Usuário**: Criação de conta com validação de dados (nome, e-mail, senha).
- 🆕 **Autenticação JWT**: Login de usuários com autenticação segura utilizando JSON Web Tokens.
- 📸 **Upload de Imagens**: Envio de imagens de perfil do usuário para o AWS S3.
- 🎤 **Registro de Tracks**: Registro de tracks com metadados (título, artista, gênero).
- 🛠️ **Filtros de Exibição**: Adição de filtros para pesquisa e visualização de tracks (por gênero, BPM, data, etc.).
- 🔒 **Controle de Acesso**: Proteção de endpoints da API com autenticação JWT.
- 🔄 **Atualização de Dados**: Permite que os usuários atualizem suas informações pessoais.

## 🚀 Funcionalidades a Implementar
- 🐳 **Dockerização Completa**: Finalizar a conteinerização completa da aplicação com Docker para facilitar a implantação.
- 🛠️ **Documentação da API**: Documentar os endpoints da API utilizando Swagger/OpenAPI.
