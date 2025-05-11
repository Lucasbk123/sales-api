# 📦 Descrição do Projeto

Este projeto é uma **API de vendas** que tem como objetivo disponibilizar endpoints para gerenciar vendas, incluindo:

- Criar Venda  
- Atualizar Venda  
- Deletar Venda  
- Atualizar Item da Venda  
- Cancelar Item da Venda  
- Cancelar Venda
- Busca Venda Por Id

As regras de negócio desses métodos serão explicadas na seção [📋Regras de Negócio](/.doc/regras-de-negocio.md).

# 🛠️ Configuração do Projeto 

 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org)

### 🧾 1. Clone o repositório 

Abra o terminal e execute:

```bash
git clone https://github.com/Lucasbk123/sales-api.git
```

### 🐳 2. Iniciando a aplicação com docker compose

- Abra o terminal e navegue até o diretório raiz onde está localizado o `docker-compose.yml`:
```bash
cd  sales-api\template\backend
```
- Depois, execute o comando:
```bash
docker-compose up --build
```
- Caso não queira que o terminal fique travado, execute o comando com o modo desacoplado (-d):
```bash
docker-compose up --build -d
```
✅ Pronto! A API estará disponível no seguinte endereço:
```bash
https://localhost:5051/swagger/index.html
```
### 💻 3. Inciado a aplicação pelo o visual studio
- Abra o projeto no Visual Studio e edite a ConnectionString do arquivo appsettings.json de acordo com a imagem
![image](https://github.com/user-attachments/assets/8e989637-d202-4df0-86b5-473ee79e67eb)

- Abra o terminal ou o PowerShell do Visual Studio e acesse a pasta **src**.  

- Depois, será necessário aplicar as migrations do Entity Framework. Caso já tenha o pacote **Entity Framework Core .NET Command-line Tools**, não será necessário executar o seguinte comando
 ```bash
    dotnet tool install --global dotnet-ef
```
- Aplicado as migrations

```bash
   dotnet ef database update -p .\Ambev.DeveloperEvaluation.ORM\Ambev.DeveloperEvaluation.ORM.csproj -s .\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj -c DefaultContext
```
- Depois, é só executar o projeto.


# 🧰 Tecnologias Utilizadas
| Tecnologia   | Descrição                        |
|--------------|----------------------------------|
| [.NET 8](https://dotnet.microsoft.com) | Framework principal da API |
| [PostgreSQL](https://www.postgresql.org)      | Banco de dados NoSQL             |
| [Docker](https://www.docker.com/)       | Containerização de aplicação e banco |
| [xUnit](https://xunit.net/)  | Testes  |
| [Swagger](https://swagger.io/)      | Documentação interativa da API   |