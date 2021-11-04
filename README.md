# API

A API utiliza o ASP.NET Core e o Entity Framework, foi desenvolvida utilizando a abordagem code-first criando o modelo do Produto e gerando as migrações e rotas a partir dele. Possui a interface Swagger para visualização de rotas e testes. A classe DataSeed possui um método responsável por popular o banco de dados no início da aplicação caso este esteja vazio.

# Web App

O aplicativo que consome a API utiliza o ASP.NET Core e Razor Pages, com suas opções de CRUD efetuando as operações diretamente pelos endpoints fornecidos pela API descrita acima. A configuração do endereço da API é feita no parâmetro "APIurl" dentro do arquivo appsettings.json.

# Testes

Foram desenvolvidos testes de integração simples afim de testar as rotas da API.
