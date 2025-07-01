# SoundHub 🎧

*Seu universo musical em um só lugar. Um projeto de streaming de música da FillGaps.*

---

## 🚀 Aplicação Publicada

O projeto está publicado no Microsoft Azure e pode ser acessado pelos seguintes links:

* **Aplicação Web (Front-end):** [https://soundhubapp.azurewebsites.net/](https://soundhubapp.azurewebsites.net/)
* **API (Back-end):** [https://soundhubapi.azurewebsites.net/](https://soundhubapi.azurewebsites.net/) (Acessar este link leva para a documentação do Swagger)

---

## 📖 Sobre o Projeto

O **SoundHub** é uma aplicação full-stack de streaming de música desenvolvida como um projeto acadêmico. O principal objetivo é demonstrar a construção de uma solução robusta e escalável utilizando tecnologias da plataforma .NET e aplicando as melhores práticas de arquitetura de software, como **Arquitetura em Camadas** e **Domain-Driven Design (DDD)**.

A plataforma permite que os usuários explorem um catálogo de artistas e músicas, criem contas, gerenciem assinaturas, favoritem seu conteúdo preferido e muito mais.

## ✨ Status do Projeto

✅ **Funcional e Implantado**

As principais funcionalidades do sistema foram implementadas e a aplicação está publicada no Microsoft Azure. O projeto está em um estágio onde o fluxo principal do usuário, desde o registro até o acesso ao conteúdo, está completo.

## ⚙️ Casos de Uso Implementados

* **Gerenciamento de Conta:**
    * Criação de conta de usuário (Registro).
    * Autenticação com Token JWT (Login).
* **Assinaturas:**
    * Visualização de planos disponíveis.
    * Criação de uma nova assinatura para o usuário logado.
    * Validação de assinatura para acesso a áreas restritas.
* **Catálogo Musical:**
    * Listagem e pesquisa de artistas.
    * Listagem e pesquisa de músicas.
    * Criação de artistas, álbuns, músicas e gêneros via API.
* **Engajamento do Usuário:**
    * Funcionalidade de "Favoritar" e "Desfavoritar" artistas e músicas.
    * Dashboard personalizado que exibe os artistas e músicas favoritas do usuário.

## 🛠️ Tecnologias e Arquitetura

### Tecnologias Principais

* **Backend:** .NET 9, ASP.NET Core Web API, Entity Framework Core
* **Frontend:** ASP.NET Core MVC com Razor Pages
* **Banco de Dados:** Azure SQL Database (Produção) / SQL Server (Desenvolvimento)
* **Autenticação:** Token JWT (Bearer Token)
* **Hospedagem:** Microsoft Azure App Services

### Arquitetura

O SoundHub foi projetado com base nos princípios de **Arquitetura Limpa (Clean Architecture)**, garantindo um código desacoplado, testável e de fácil manutenção.

* `FillGaps.SoundHub.Domain`: O coração da aplicação. Contém as entidades, agregados e as regras de negócio puras. Não depende de nenhuma outra camada.
* `FillGaps.SoundHub.Application`: Orquestra os casos de uso do negócio. Contém os *services* e DTOs, funcionando como uma ponte entre a apresentação e o domínio.
* `FillGaps.SoundHub.Infrastructure`: Contém as implementações técnicas, como o `DbContext` do Entity Framework, os repositórios e o acesso ao banco de dados.
* `FillGaps.SoundHub.WebAPI`: A camada de apresentação da API RESTful. Expõe os endpoints para serem consumidos pelo front-end.
* `FillGaps.SoundHub.WebApp`: A aplicação front-end em ASP.NET Core MVC que o usuário final interage. Funciona como um cliente independente da API.

## 🚀 Como Rodar Localmente

Siga os passos abaixo para configurar e executar o projeto em seu ambiente de desenvolvimento.

### Pré-requisitos

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (ou a versão utilizada no projeto)
* Visual Studio 2022 ou superior (com a carga de trabalho ASP.NET e desenvolvimento web)
* SQL Server (LocalDB, Express ou uma instância completa)

### 1. Configuração do Banco de Dados

A API está configurada para usar um banco de dados SQL Server.

1.  Abra o arquivo `appsettings.json` no projeto `FillGaps.SoundHub.WebAPI`.
2.  Encontre a seção `ConnectionStrings` e ajuste a `DefaultConnection` para apontar para sua instância local do SQL Server. Exemplo para LocalDB:
    ```json
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SoundHubDb_Dev;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    ```

### 2. Executando as Migrations

Com a string de conexão configurada, o banco de dados e suas tabelas precisam ser criados.

1.  Abra um terminal (Package Manager Console ou .NET CLI) na raiz da solução.
2.  Execute o comando para aplicar as migrations. Isso criará o banco de dados `SoundHubDb_Dev`.
    ```powershell
    dotnet ef database update --project src/FillGaps.SoundHub.Infrastructure --startup-project src/FillGaps.SoundHub.WebAPI
    ```
    *(Ajuste os caminhos se sua estrutura de pastas for diferente)*

### 3. Seeding de Dados (Populando o Banco)

O projeto está configurado para popular o banco de dados com dados iniciais (artistas, álbuns, músicas, etc.) na primeira vez que a API é iniciada. Esse processo é automático e garante que você tenha conteúdo para testar assim que executar a aplicação.

### 4. Executando a Aplicação

Para que o front-end consiga se comunicar com o back-end, ambos os projetos precisam ser executados simultaneamente.

1.  No Visual Studio, clique com o botão direito na **Solução** no "Solution Explorer".
2.  Selecione **"Configure Startup Projects..."**.
3.  Escolha a opção **"Multiple startup projects"**.
4.  Para os projetos `FillGaps.SoundHub.WebAPI` e `FillGaps.SoundHub.WebApp`, defina a "Action" como **`Start`**.
5.  Clique em **OK**.
6.  Pressione **F5** ou clique no botão de "Start". O Visual Studio irá iniciar ambos os projetos, abrindo duas janelas no seu navegador. Agora você pode usar o `WebApp` para interagir com a `WebAPI`.

## 👤 Autor

* **Leandro Machado** - [leandro@fillgaps.com.br](mailto:leandro@fillgaps.com.br)
