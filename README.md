# SoundHub 🎧

*Seu universo musical em um só lugar. Um projeto de streaming de música da FillGaps.*

---

## 📖 Sobre o Projeto

O **SoundHub** é um projeto acadêmico que visa construir uma plataforma de streaming de música. A aplicação está sendo desenvolvida com foco na aplicação de boas práticas de arquitetura e design de software.

O sistema permitirá que os usuários criem contas, gerenciem assinaturas através de planos, criem playlists personalizadas, favoritem suas músicas e artistas preferidos, e explorem um vasto catálogo musical.

## Status do Projeto

🚧 **Em Desenvolvimento** 🚧

O projeto está atualmente na fase de modelagem de domínio e estruturação inicial da arquitetura. As funcionalidades ainda não estão implementadas.

## 🛠️ Tecnologias Utilizadas

A solução é construída sobre a plataforma .NET com a linguagem C#, utilizando as seguintes tecnologias:

* **Backend:**
    * .NET / C#
    * ASP.NET Core Web API
* **Frontend:**
    * ASP.NET Core MVC com Razor Pages
* **Banco de Dados:**
    * SQL Server
    * Entity Framework Core (ORM)

## 🏛️ Arquitetura

O SoundHub foi projetado com base em princípios de **Arquitetura em Camadas (Layered Architecture)** e **Domain-Driven Design (DDD)**. O objetivo é garantir um código limpo, testável, escalável e com a lógica de negócio bem isolada das preocupações de infraestrutura.

A estrutura da solução reflete essa decisão:

* `FillGaps.SoundHub.Domain`: O coração da aplicação. Contém as entidades, agregados, objetos de valor e as regras de negócio puras (o domínio).
* `FillGaps.SoundHub.Application`: Orquestra o domínio para executar os casos de uso da aplicação (features).
* `FillGaps.SoundHub.Infrastructure`: Contém as implementações técnicas, como acesso ao banco de dados com EF Core, integração com gateways de pagamento, serviços de e-mail, etc.
* `FillGaps.SoundHub.WebAPI`: A camada de apresentação da API RESTful, que servirá os dados para o front-end e futuros clientes.
* `FillGaps.SoundHub.WebApp`: A aplicação front-end em ASP.NET Core MVC que o usuário final irá interagir.

## 🚀 Como Começar

Instruções sobre como configurar o ambiente de desenvolvimento e rodar o projeto serão adicionadas futuramente.

## 👤 Autor

* **[Leandro Machado]** - [leandro@fillgaps.com.br](mailto:leandro@fillgaps.com.br)

---