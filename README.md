# 📦 Warehouse Inventory Management System

*(ASP.NET Core 8 – Razor Pages – Clean Architecture)*

A full-featured **Warehouse Inventory Management System** built with **ASP.NET Core 8 Razor Pages** and **Entity Framework Core**, following **Clean Architecture principles**.

This project demonstrates how to structure a real-world ASP.NET Core application with:

* Domain layer
* Application layer
* Infrastructure layer
* Web (UI) layer

It includes authentication, role-based access (Admin & Customer), inventory management, orders, and clean separation of concerns.

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-green.svg)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red.svg)](https://www.microsoft.com/sql-server)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

 

## 🚀 Key Features

### 🔐 Authentication & Authorization

* ASP.NET Identity integration
* Separate **Admin** and **Customer** roles
* Login & Register for both roles
* Role-based page access

### 📦 Inventory Management (Admin)

* Create and edit inventory items
* Manage categories and suppliers
* Track item status and stock quantity
* View warehouse logs

### 🛒 Orders & Cart System (Customer)

* Browse item catalog
* View item details
* Add items to cart
* Place orders
* View order history

### 🧱 Clean Architecture Design

* Domain entities isolated from UI & database
* Repository pattern
* Service layer for business logic
* DTOs for data transfer
* AutoMapper for mapping entities to DTOs

### 🗃️ Database & Data Management

* Entity Framework Core (Code First)
* SQL Server database
* Migrations support
* Database seeding
* Generic repository implementation

### 🧪 Error Handling & Validation

* Custom exceptions (NotFound, OperationFailed)
* Server-side validation
* Model binding

 

## 🛠️ Technologies

* **ASP.NET Core 8.0** (Razor Pages)
* **Entity Framework Core 8.0**
* **SQL Server**
* **ASP.NET Identity**
* **AutoMapper**
* **Clean Architecture**

 

## 📸 Snapshots / Screenshots

* Customer Login

![customer login](https://github.com/user-attachments/assets/ca4e953b-722e-451f-8a67-06f17c544cce)

* Customer Dashboard

![customer dashboard](https://github.com/user-attachments/assets/c01a7aef-ee8e-4c4f-a664-23e84bc05c81)

* Catalog View (Customer)
![view items in catalog(customer)](https://github.com/user-attachments/assets/2ec9ce37-6f29-41f8-b813-e2ec5ba13580)

* View Item Details

![view item](https://github.com/user-attachments/assets/1d5ebbe2-c3a8-4263-ac35-483fda78b292)

* Customer Cart

![customer cart](https://github.com/user-attachments/assets/25ac6249-d2a6-48aa-95a9-9e27aa7461b4)

* Admin Login

![admin login](https://github.com/user-attachments/assets/51d2ad1c-cbb4-40ac-91e0-b2a40ec0beae)

* Admin Dashboard

![admin dashboard](https://github.com/user-attachments/assets/aa0959ee-ad6c-466a-b28e-c7dd04278c42)

* Warehouse Item List

![items in warehouse](https://github.com/user-attachments/assets/e4986df6-6e57-46a3-af99-1526fb16622a)

* Add New Item

![add new item to warehouse](https://github.com/user-attachments/assets/71990a6f-a41f-473e-ab43-899e661b54dd)

* Filter Items Modal

![filter items modal](https://github.com/user-attachments/assets/b25eac21-47af-4203-91bc-f5c5c69fa365)

* Supplier List

![suppliers in warehouse](https://github.com/user-attachments/assets/d000bc2c-981d-45c4-99f4-fea3d04aaeed)

* Add New Supplier

![add new supplier to warehouse](https://github.com/user-attachments/assets/17fa0792-0e19-4017-8498-1b0ebcf22980)

* Warehouse Orders List

![orders in warehouse](https://github.com/user-attachments/assets/f17b4c45-8016-42d4-8a7f-419660ebdfa3)

* Order Details View

![view order details](https://github.com/user-attachments/assets/ed54a2ca-0472-4c4c-9fde-c7db6c051a0e)

* Receive Shipment

![recieve shipment](https://github.com/user-attachments/assets/8d438c9e-acc0-41bf-9a74-7c4933762631)

* Adjust Stock Levels

![adjust stock](https://github.com/user-attachments/assets/73b3cce4-980f-46b9-8af0-286aa56a60cd)

* Add Item-Supplier Relation

![add relation to items](https://github.com/user-attachments/assets/cd04d093-17b2-41bc-b16e-71f758569a58)


## 📂 Project Structure (Clean Architecture – Detailed)

```text
📦 ASP.NET-Core-8-Warehouse-Inventory-System
│
├── Domain                        → Core business layer
│   ├── Entities (Item, Category, Supplier, Order, OrderItem, WarehouseLog, Status enums)
│   ├── Interfaces (IRepository, IOrderItemRepository)
│   └── Domain.csproj
│
├── Application                   → Application contracts & DTOs
│   ├── DTOs (ItemDto, OrderDto, SupplierDto, CategoryDto, ItemFilterDto, WarehouseLogDto)
│   ├── Interfaces (IItemsService, IOrderService, ISupplierService, ICategoryService, IAdminService, ICustomerService)
│   ├── Mappings (AutoMapper profile)
│   ├── Common/Exceptions (NotFoundException, OperationFailedException)
│   └── Application.csproj
│
├── Infrastructure                → Implementations & data access
│   ├── Data (AppDbContext, Repository, OrderItemRepository)
│   ├── Services (ItemService, OrderService, SupplierService, CategoryService, WarehouseLogService, ItemStatusService)
│   ├── Identity (ApplicationUser, IdentityService, AdminService, CustomerService, Role seeding)
│   ├── Migrations
│   └── Infrastructure.csproj
│
├── Web                           → Presentation layer (Razor Pages UI)
│   ├── Pages
│   │   ├── Admin (Auth, Items, Orders, Suppliers)
│   │   ├── Customer (Auth, Catalogs, Carts)
│   │   ├── Shared (_Layout, Sidebars)
│   │   └── Index.cshtml
│   │
│   ├── wwwroot (CSS, JS, images)
│   ├── Program.cs (Dependency Injection & middleware)
│   ├── appsettings.json
│   └── Web.csproj
│
└── Warehouse.sln
```

 

## 🧠 Architecture Overview

This project follows **Clean Architecture**:

### 🔹 Domain Layer

Contains only business entities and repository contracts:

* `Item`, `Order`, `Supplier`, `WarehouseLog`
* `IRepository`

No dependency on EF Core or UI.

 

### 🔹 Application Layer

Defines use-cases and data transfer:

* DTOs
* Service interfaces
* AutoMapper profile
* Custom exceptions

 

### 🔹 Infrastructure Layer

Implements application contracts using:

* EF Core (`AppDbContext`)
* Repository pattern
* Business services
* ASP.NET Identity
* Database migrations

 

### 🔹 Web Layer

Razor Pages UI:

* Admin area
* Customer area
* Authentication pages
* Layout & shared components
* Dependency injection configuration

 

## ⚙️ Installation & Setup

1. Clone the repository:

```bash
git clone https://github.com/Alireza-Jafari-tech/ASP.NET-Core-8-Warehouse-Inventory-System.git
cd ASP.NET-Core-8-Warehouse-Inventory-System
```

2. Update connection string in `Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=WarehouseDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

3. Apply migrations:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run --project Web
```

5. Open in browser:

```
[http://localhost:5258]
```

6. Demo Accounts:

- Admin User:
* Email: michael.anderson@warehouse.com
* Password: Admin123!

- Customer Account:
* Email: john.smith@amazon.com
* Password: Amazon12!

 

## 🧑‍💻 Usage

### Admin

* Login/Register as Admin
* Manage items, categories, suppliers
* View and manage orders
* Track warehouse logs

### Customer

* Register/Login
* Browse catalog
* Add items to cart
* Place orders
* View order history

 

## 🎯 Learning Goals

* Apply Clean Architecture in ASP.NET Core
* Implement Repository & Service patterns
* Work with ASP.NET Identity
* Build real-world Razor Pages UI
* Practice EF Core migrations
* Implement DTOs and AutoMapper
* Understand layered architecture

 

## 📝 License

This project is licensed under the MIT License.
See the `LICENSE` file for details.

 

## 🤝 Contributing

This project is for learning purposes, but feel free to fork and improve it.
Suggestions, issues, and pull requests are welcome!
