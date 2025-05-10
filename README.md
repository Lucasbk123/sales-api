### 📦 Descrição do Projeto

Este projeto é uma **API de vendas** que tem como objetivo disponibilizar endpoints para gerenciar vendas, incluindo:

- Criar Venda  
- Atualizar Venda  
- Deletar Venda  
- Atualizar Item da Venda  
- Cancelar Item da Venda  
- Cancelar Venda
- Busca Venda Por Id

As regras de negócio desses métodos serão explicadas na seção [📋Regras de Negócio](/.doc/regras-de-negocio.md).

### 🛠️ Configuração do Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org)

### 🧾 1. Clone o repositório

Abra o terminal e execute:

```bash
git clone https://github.com/Lucasbk123/sales-api.git
```

### 🐳 2. Iniciando a Aplicação com Docker Compose

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


### 🧰 Tecnologias Utilizadas
| Tecnologia   | Descrição                        |
|--------------|----------------------------------|
| [.NET 8](https://dotnet.microsoft.com) | Framework principal da API |
| [PostgreSQL](https://www.postgresql.org)      | Banco de dados NoSQL             |
| [Docker](https://www.docker.com/)       | Containerização de aplicação e banco |
| [xUnit](https://xunit.net/)  | Testes  |
| [Swagger](https://swagger.io/)      | Documentação interativa da API   |