[Back to README](../README.md)

### 📋 Regras de Negócio
A seguir estão as regras implementadas na API, com seus respectivos comportamentos e endpoints relacionados:


### 💸 Regra de desconto

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


### ✅ 1.Cria Venda
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
### ✅ 2.Atualizar Venda
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

### ✅ 3.Deletar Venda
**Endpoint:** `Delete api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.


### ✅ 4.Atualizar Item da Venda  
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
### ✅ 5.Cancelar Item da Venda  
**Endpoint:** `Patch api/sales/{id}/product/{productId}/cancel`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.
*  `productId:"guid" (obrigatório)` – ID do produto.

### ✅ 6.Cancelar  Venda  
**Endpoint:** `Patch api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.

### ✅ 6.Busca Venda por Id  
**Endpoint:** `GET api/sales/{id}`

**Campos obrigatórios:**
*  `Id:"guid" (obrigatório)` – ID da venda.

**Body:**
```json
{
  "data": {
    "id": "8d1ce78e-6b26-4041-bae2-1bcfa23f79bc",
    "number": 4,
    "createdAt": "2025-05-10T20:10:00.710212Z",
    "customerName": "Lucas Pereira Alves",
    "totalValue": 1414,
    "branchName": "Araguaina-TO",
    "status": 1,
    "items": [
      {
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "productName": "skol",
        "unitPrice": 20,
        "totalPrice": 300,
        "discount": 60,
        "quantity": 15,
        "cancelled": false
      },
      {
        "productId": "7dbb3bfa-bcde-4b8a-90a3-123456789001",
        "productName": "heineken",
        "unitPrice": 35,
        "totalPrice": 70,
        "discount": 0,
        "quantity": 2,
        "cancelled": false
      },
      {
        "productId": "8d13b7cd-2c1e-4ed4-90e0-cb102dba3145",
        "productName": "budweiser",
        "unitPrice": 25,
        "totalPrice": 100,
        "discount": 10,
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