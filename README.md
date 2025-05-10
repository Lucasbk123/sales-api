### 📦 Descrição do Projeto

Este projeto é uma **Web API de vendas** que tem como objetivo disponibilizar endpoints para gerenciar vendas, incluindo:

- Criar Venda  
- Atualizar Venda  
- Deletar Venda  
- Atualizar Item da Venda  
- Cancelar Item da Venda  
- Cancelar Venda

As regras de negócio desses métodos serão explicadas na seção [📋Regras de Negócio](#-regras-de-negócio).

### 🧰 Tecnologias Utilizadas
| Tecnologia   | Descrição                        |
|--------------|----------------------------------|
| [.NET 8](https://dotnet.microsoft.com) | Framework principal da API |
| [PostgreSQL](https://www.postgresql.org)      | Banco de dados NoSQL             |
| [Docker](https://www.docker.com/)       | Containerização de aplicação e banco |
| [xUnit](https://xunit.net/)  | Testes  |
| [Swagger](https://swagger.io/)      | Documentação interativa da API   |


### 📋 Regras de Negócio
A seguir estão as regras implementadas na API, com seus respectivos comportamentos e endpoints relacionados:

### ✅ 1.Cria Venda
**Endpoint:** `POST api/sales`

**Campos obrigatórios:**
*  `customerId (obrigatório)` – ID do cliente.
*  `customerName (obrigatório)` – Nome do cliente.
*  `branchId (obrigatório)` – ID da filial.
*  `branchName (obrigatório)` – Nome da filial.
*  `Item.productId (obrigatório)` – ID do produto.
*  `Item.productName (obrigatório)` – Nome do produto.
*  `Item.unitPrice (obrigatório)` – Preço do valor unitário.
*  `Item.quantity (obrigatório)` –  Quantidade de items do produto.


**Body:**
```json
{
  "customerName": "Lucas Pereira Alves" ,
  "customerId": "67c9b6af-37a2-4e35-b378-56710c328fc5",
  "branchId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
  "branchName": "Araguaina-To",
  "items": [
    {
      "productId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
      "productName": "budweiser",
      "unitPrice": 40,
      "quantity": 13
    },
    {
      "productId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
      "productName": "heineken",
      "unitPrice": 40,
      "quantity": 8
    },
    {
      "productId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
      "productName": "skol",
      "unitPrice": 40,
      "quantity": 2
    }
  ]
}
```
- **Descrição**: Ao tentar criar um novo venda, a API verifica se o usuário já possui um carrinho com status `Ativo`. Se sim, retorna o mesmo carrinho em vez de criar outro.
- **Endpoint relacionado**:
  - `POST /carrinho`
  - `GET /carrinho/ativo`


<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)