# E-Commerce Backend Project

## Project Overview

This is a backend solution for an e-commerce platform built with .NET 6. The project includes core functionalities such as user authentication, product management, category management, and order processing.

## Features

- **User Management**:
  - Register new users (customers and admins).
  - User authentication with JWT tokens.
  - Role-based access control (Admin, Customer).
- **Product Management**:

  - Admin can create, update, and delete products.
  - Customers can view products.
  - Products are organized by categories.

- **Category Management**:

  - Admin can create, update, and delete categories.
  - Categories are used to organize products.

- **Order Management**:

  - Customers can create orders based on products in their cart.
  - Admin can view all orders.

- **Authentication & Authorization**:
  - JWT-based authentication.
  - Role-based authorization (Customer, Admin).

## Technologies Used

- **.NET 6**: Web API framework.
- **Entity Framework Core**: ORM for database interactions.
- **SQL Server**: Relational database for storing data.
- **JWT**: For user authentication and authorization.
- **AutoMapper**: For object mapping.
- **Swagger**: API documentation.

## Prerequisites

- .NET 6 SDK
- SQL Server
- Visual Studio or any other C# IDE

## Getting Started

### 1. Clone the repository:

```bash
git clone https://github.com/your-username/e-commerce-backend.git
```

### 2. Setup database:

- Make sure SQL Server is running.
- Update the connection string in `appsettings.json` with your SQL Server credentials.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ECommerceDb;User Id=your_username;Password=your_password;"
  }
}
```

- Run migrations to create the database:

```bash
dotnet ef database update
```

### 3. Run the application:

```bash
dotnet watch
```

The API will be available at `https://localhost:5000`.

### 4. Swagger:

- Navigate to `https://localhost:5001/swagger` to explore the API endpoints.

## Project Structure

\`\`\`bash
|-- ECommerce
|-- Controllers # API Controllers
|-- Data # DbContext and Database Configurations
|-- Entities # Database Entities (User, Product, Category, Order)
|-- DTOs # Data Transfer Objects
|-- Repositories # Repository Layer for database operations
|-- Services # Business Logic Layer
|-- Migrations # Entity Framework Migrations
|-- Program.cs # Application Entry Point
|-- Startup.cs # Configuration and Middleware setup
\`\`\`

## API Endpoints

### User

- **POST** \`/api/users/register\` – Register a new user.
- **POST** \`/api/users/login\` – Login and get JWT token.

### Product

- **GET** \`/api/products\` – Get all products (Paginated).
- **POST** \`/api/products\` – Create a new product (Admin only).

### Category

- **GET** \`/api/categories\` – Get all categories.
- **POST** \`/api/categories\` – Create a new category (Admin only).

### Order

- **POST** \`/api/orders\` – Create a new order.
- **GET** \`/api/orders\` – Get all orders (Admin only).

## Deployment

- The application is deployed and can be accessed at: [https://your-deploy-link.com](https://your-deploy-link.com)

## Team Members

- **Team Lead**: Your Name (@githubusername)
- **Backend Developer**: Member 1 (@githubusername)
- **Backend Developer**: Member 2 (@githubusername)
- **Database Administrator**: Member 3 (@githubusername)

## Contributing

- Fork the repository.
- Create a new branch for your feature.
- Submit a pull request with a clear description of the changes.

## License

This project is licensed under the MIT License.
