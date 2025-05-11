[Back to README](../README.md)

# 💸 Regra de desconto

Para tornar as regras de desconto mais flexíveis, o sistema permite configurá-las diretamente no appsettings da aplicação, conforme exemplo abaixo:
  ```json
        "DiscountRangeParametres": [
          {
            "Min": 4,
            "Max": 9,
            "DiscountPercent": 0.1
          },
          {
            "Min": 10,
            "Max": 20,
            "DiscountPercent": 0.2
          }
        ]
  ```
⚠️ Caso tenha mais de uma correspondência no intervalo configurado o sistema vai considera o maior desconto.

**Campos obrigatórios:**

- `Min:"short" (obrigatório)` – Valor mínimo para a validação dentro do intervalo definido.
- `Max:"short" (obrigatório)` – Valor máximo para a validação dentro do intervalo definido.
- `DiscountPercent:"decimal" (obrigatório)` – Valor de desconto em porcentagem, caso haja correspondência no intervalo configurado.


# 📋 Regras de Negócio
A seguir estão as regras implementadas na API, com seus respectivos comportamentos e endpoints relacionados:



### 1. Busca de Vendas Paginadas
**Endpoint:** `GET api/sales/{id}`

**Campos obrigatórios:**
*  `page:"int" (obrigatório)` – número da página a ser retornada na consulta de dado.
*  `pageSize:"int" (obrigatório)` – número de itens que devem ser retornados por página

**Body:**
```json
{
  "data": [
    {
      "id": "977a7cd6-95a9-4cdd-8296-7f9198fbd38b",
      "number": 1,
      "createdAt": "2025-05-10T03:24:43.548372Z",
      "customerName": "Lucas Pereira Alves",
      "totalValue": 180,
      "branchName": "Araguaina-TO",
      "status": "Canceled"
    },
    {
      "id": "27b5dfe1-f221-41af-a009-0ec4027a71b0",
      "number": 2,
      "createdAt": "2025-05-10T16:09:38.757914Z",
      "customerName": "Lucas Pereira Alves",
      "totalValue": 2100,
      "branchName": "Araguaina-TO",
      "status": "Pending"
    },
    {
      "id": "71858d36-2adf-449b-97e4-dd04274a9b4a",
      "number": 3,
      "createdAt": "2025-05-11T22:33:59.765762Z",
      "customerName": "Lucas Pereira Alves",
      "totalValue": 531.05,
      "branchName": "Araguaina-TO",
      "status": "Confirmed"
    }
  ],
  "currentPage": 1,
  "totalCount": 72,
  "totalPages": 24,
  "success": true,
  "message": "",
  "errors": []
}
```
![Information](https://img.shields.io/badge/Information-blue)
- Este endpoint retorna apenas as informações de cabeçalho da venda. Caso deseje todas as informações, utilize a busca por ID.

### 2. Busca Venda por Id  
**Endpoint:** `GET api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.

**Body:**
```json
{
  "data": {
    "id": "f9395622-21ed-46f5-a1df-9954bd2cbf6d",
    "number": 72,
    "createdAt": "2025-05-11T23:41:27.191681Z",
    "customerName": "Lucas Pereira Alves",
    "totalValue": 347.08,
    "branchName": "Araguaina-TO",
    "status": "Confirmed",
    "items": [
      {
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "productName": "budweiser",
        "unitPrice": 20.59,
        "totalPrice": 102.95,
        "discount": 10.3,
        "quantity": 5,
        "cancelled": false
      },
      {
        "productId": "7dbb3bfa-bcde-4b8a-90a3-123456789001",
        "productName": "skol",
        "unitPrice": 35.45,
        "totalPrice": 70.9,
        "discount": 0,
        "quantity": 2,
        "cancelled": false
      },
      {
        "productId": "b99a77e1-9a54-4b6f-b49f-abcdef123456",
        "productName": "heineken",
        "unitPrice": 50.98,
        "totalPrice": 203.92,
        "discount": 20.39,
        "quantity": 4,
        "cancelled": false
      }
    ]
  },
  "success": true,
  "message": "Sale retrived successfully",
  "errors": []
}
```
### 3. Cria Venda
**Endpoint:** `POST api/sales`

**Campos obrigatórios:**
*  `customerId:"guid" (obrigatório)` – ID do cliente.
*  `customerName:"string" (obrigatório)` – Nome do cliente. **Limite de caracteres:** 100
*  `branchId:"guid" (obrigatório)` – ID da filial.
*  `branchName:"string" (obrigatório)` – Nome da filial. **Limite de caracteres:** 100
*  `Item.productId:"guid" (obrigatório)` – ID do produto.
*  `Item.productName:"string" (obrigatório)` – Nome do produto. **Limite de caracteres:** 500
*  `Item.unitPrice:"decimal" (obrigatório)` – Preço do valor unitário.
*  `Item.quantity:"short" (obrigatório)` –  Quantidade de items do produto.**Limite de Item:** 20


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
      "unitPrice": 40.00,
      "quantity": 13
    },
    {
      "productId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
      "productName": "heineken",
      "unitPrice": 40.00,
      "quantity": 8
    },
    {
      "productId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
      "productName": "skol",
      "unitPrice": 40.00,
      "quantity": 2
    }
  ]
}
```
- **Descrição**: Ao tentar criar uma nova venda, a Api aplicará o desconto de acordo com a quantidade de itens de cada produto.
    #### ⚠️ Atenção: A estratégia de desconto utilizará os dados configurados no appsettings. Caso exista sobreposição entre faixas (ranges), o sistema aplicará o maior desconto. Os dados cadastrados são os mesmos solicitados no teste.
  ```json
        "DiscountRangeParametres": [
          {
            "Min": 4,
            "Max": 9,
            "DiscountPercent": 0.1
          },
          {
            "Min": 10,
            "Max": 20,
            "DiscountPercent": 0.2
          }
  ```
### 4. Atualizar Venda
**Endpoint:** `PUT api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.
*  `customerId:"guid" (obrigatório)` – ID do cliente.
*  `customerName:"string" (obrigatório)` – Nome do cliente. **Limite de caracteres:** 100
*  `branchId:"guid" (obrigatório)` – ID da filial.
*  `branchName:"string" (obrigatório)` – Nome da filial. **Limite de caracteres:** 100
*  `Item.productId:"guid" (obrigatório)` – ID do produto.
*  `Item.productName:"string" (obrigatório)` – Nome do produto. **Limite de caracteres:** 500
*  `Item.unitPrice:"decimal" (obrigatório)` – Preço do valor unitário.
*  `Item.quantity:"short" (obrigatório)` –  Quantidade de items do produto. **Limite de Item:** 20


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
      "unitPrice": 40.00,
      "quantity": 13
    },
    {
      "productId": "f94e52fc-c1c9-4e40-be7b-15a56048c4e0",
      "productName": "heineken",
      "unitPrice": 40.00,
      "quantity": 8
    }
  ]
}
```
### ⚠️ Atenção: 
* Ao atualizar uma venda, todos os campos do corpo (body) devem ser enviados, inclusive aqueles que não sofreram alteração. Caso nem todos os itens sejam enviados, os itens ausentes serão excluídos. Somente os itens com status "cancelado" não serão excluídos
* Vendas canceladas não podem ser atualizadas.

###  5. Deletar Venda
**Endpoint:** `Delete api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.


###  6. Atualizar Item da Venda  
**Endpoint:** `Patch api/sales/{id}/product/{productId}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.
*  `productId:"guid" (obrigatório)` – ID do produto.
*  `productName:"string" (obrigatório)` – Nome do produto. **Limite de caracteres:** 500
*  `unitPrice:"decimal" (obrigatório)` – Preço do valor unitário.
*  `quantity:"short" (obrigatório)` –  Quantidade de items do produto. **Limite de Item:** 20

**Body:**
```json
{
  "productName": "budweiser",
  "unitPrice": 90.00,
  "quantity": 2
}
```
###  7. Cancelar Item da Venda  
**Endpoint:** `Patch api/sales/{id}/product/{productId}/cancel`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.
*  `productId:"guid" (obrigatório)` – ID do produto.

###  8 .Cancelar  Venda  
**Endpoint:** `Patch api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.
