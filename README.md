# Online Bookstore Web Application
**Introduction**
This repository contains the source code for an Online Bookstore web application built with ASP.NET Core. The application includes functionalities for managing books, shopping cart operations, and processing purchases. It also demonstrates a layered architecture with a clear separation of concerns, making it maintainable and scalable.

**Features**
**Book Management:** Add, update, view, and delete books.
**Shopping Cart:** Add items to the cart, view cart items, and remove items from the cart.
**Purchases:** Create and complete purchase orders with multiple payment methods (Web, USSD, Transfer).
**Pagination:** Support for paginated views for books and cart items.

**Prerequisites**
.NET 6.0 SDK or later
SQL Server or any other database supported by EF Core
Visual Studio 2022 or later (optional but recommended)

**Setup Instructions**
Step 1: Clone the Repository
Clone this repository to your local machine using the following command:
git clone https://github.com/yourusername/onlinebookstore.git
Step 2: Configure the Database
Open the appsettings.json file located in the OnlineBookstore.API project.
Update the ConnectionStrings section with your database connection string.
Step 3: Apply Migrations and Seed the Database
Open the terminal or command prompt and navigate to the OnlineBookstore.API project directory. Run the following commands to apply migrations and seed the database:
Step 4: Build the Solution
In the terminal or command prompt, run the following command to build the solution:dotnet build
tep 5: Run the Application
Run the application using the following command:

**Running Unit Tests**
Unit tests are located in the OnlineBookstore.Tests project. To run the tests, use the following command:dotnet test

**Thought Process**
**Architecture**
The application follows a layered architecture with the following layers:

**API Layer:** Contains the controllers and handles HTTP requests. It uses dependency injection to interact with the service layer.
**Service Layer:** Contains business logic and interacts with the data access layer. It abstracts the data layer from the API layer.
**Data Access Layer:** Contains the EF Core DbContext and handles database operations.

**Entity Framework Core**
EF Core is used for database operations. The BookstoreContext class is the DbContext that manages the database connection and entity configurations. Migrations are used to manage database schema changes.

**Logging**
A custom ILoggerManager interface is used for logging. This interface is implemented by a concrete logger class, and dependency injection is used to inject the logger into controllers and services.

**Pagination**
Pagination is implemented using the XPagedList library, which simplifies the process of splitting data into pages and allows easy integration with ASP.NET Core.

**Unit Testing**
Unit tests are written using the xUnit framework and Moq library. Tests cover various scenarios for the API endpoints to ensure they behave as expected.

**High-Level Design**
The following diagram provides a high-level overview of the system architecture:

OnlineBookstore
│
├── OnlineBookstore.API
│   └── OnlineBookstore.API.csproj
│
├── OnlineBookstore.Application
│   └── OnlineBookstore.Application.csproj
│
├── OnlineBookstore.Infrastructure
│   └── OnlineBookstore.Infrastructure.csproj
│
└── OnlineBookstore.Tests
    └── OnlineBookstore.Tests.csproj

**Components**
**Controllers:** Handle HTTP requests and return appropriate responses.
**Services:** Contain business logic and perform operations on data.
**Repositories:** Abstract database operations and interact with DbContext.
**Models:** Define the data structure and entities.
**DTOs:** Used for data transfer between layers and to/from clients.

**API Endpoints**
**Books**
GET /api/books: Get all books with pagination.
GET /api/books/{id}: Get a book by ID.
POST /api/books: Add a new book.
PUT /api/books/{id}: Update an existing book.
DELETE /api/books/{id}: Delete a book.
**Shopping Cart**
GET /api/shoppingcart: Get all items in the cart with pagination.
POST /api/shoppingcart: Add an item to the cart.
DELETE /api/shoppingcart/{id}: Remove an item from the cart.
**Purchases**
POST /api/purchases: Create a new purchase.
POST /api/purchases/{id}/complete: Complete a purchase with a specified payment method.

**Conclusion**
This Online Bookstore application is designed to demonstrate a structured approach to building a web application using ASP.NET Core. It includes essential features like book management, shopping cart operations, and purchase processing, along with a clean and maintainable architecture. Feel free to extend and customize the application as needed.

For any issues or contributions, please open an issue or submit a pull request on the GitHub repository.
    
