# TodoList Backend

![TodoList Banner](https://github.com/user-attachments/assets/4ce11f9c-fba9-46f3-92d4-cebf38233f48)

---

## Table of Contents

- [About](#about)
- [Screenshots and GIFs](#screenshots-and-gifs)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Project Status](#project-status)
- [Contribution Guidelines](#contribution-guidelines)
- [Contact](#contact)

---

## About

TodoList is an open-source and reverse-engineered version of Jira.
It simplifies task and project management while maintaining essential features.
This backend repository supports the frontend counterpart with a robust API,
leveraging modern development practices and technologies.

---

## Screenshots and GIFs
Here are some visuals showcasing the features:

Login, Register, Logout:
![Login](https://github.com/user-attachments/assets/ce903754-5d8c-413b-ae17-463643d3d8eb)

Drag-and-Drop Columns and Cards:
![D&D](https://github.com/user-attachments/assets/71e32267-ed51-4709-b568-b0f727503561)


CRUD of Columns and Cards:
![CRUD](https://github.com/user-attachments/assets/c3530c91-7c6f-419e-9632-6e7d91369a03)

Add Descriptions and Comments:
![CardDetails](https://github.com/user-attachments/assets/1a3d86ad-76c4-4c59-810e-4e061a56dcd8)

---

## Features

- **CRUD Operations**: Manage Boards, Columns, Cards, and Comments.
- **Drag-and-Drop**: Reorder Columns and Cards to adjust task sequences dynamically.
- **User Authentication**: Secure Login and Registration, with a blocklist of prohibited usernames like `API`, `signin`, `forgot_password`, `admin`, etc.
- **Isolated User APIs**: Prevent unauthorized access to other users' data by enforcing strict `403 Forbidden` errors.
- **Dark Mode Only**: A minimalist design with a focus on dark mode.

---

## Technologies Used

### Backend
- **Framework**: ASP.NET Core based on Clean Architecture principles by [Ardalis](https://github.com/ardalis/CleanArchitecture).
- **Dependency Injection**: Autofac.
- **API Framework**: FastEndpoints.
- **Database**: PostgreSQL using EF Core.
- **Testing**:
    - Functional Tests
    - Integration Tests
    - Unit Tests
    - Libraries: XUnit, NSubstitute, Bogus, FluentAssertions, WebApplicationFactory.
- **Authentication**: JWT tokens for secure user validation.
- **ID Generation**: Ulid for all entities.
- **Other Libraries**: MediatR, RestSharp, Swagger for API documentation.

### Frontend
- **Framework**: Vite with React.
- **State Management**: Redux and Redux Toolkit.
- **UI Framework**: Material UI (MUI).
- **Routing**: React Router.

---

## Setup Instructions

### Prerequisites
Ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download)
- [Node.js](https://nodejs.org/) (for the frontend setup, if applicable)

### Environment Variables
Add the following environment variables to your system:

- `PASSWORD_SALT_SECRET`: Secret value used for hashing passwords.
- `JWT_SECRET`: Secret key for generating and validating JWT tokens.
- `DEFAULT_CONNECTION`: Connection string for your PostgreSQL database.

Example `.env` file:
```env
PASSWORD_SALT_SECRET=your_salt_secret
JWT_SECRET=your_jwt_secret
DEFAULT_CONNECTION=Host=localhost;Database=TodoList;Username=postgres;Password=your_password
```

### Running the Backend

1. Clone the repository
```bash
git clone https://github.com/Mishatopkek/TodoListBackend.git
cd TodoListBackend
```

2. Restore dependencies:
```bash
dotnet restore
Apply migrations to set up the database:
```

3. Apply migrations to set up the database:
```bash
dotnet ef database update
Start the application:
```

4. Start the application:
```bash
dotnet run
```

---

## Project Status
This is an alpha version.
The main features are functional and ready to use, but improvements and additional features are planned.

---

## Contribution Guidelines
Contributions are welcome! If you'd like to contribute:

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a pull request.

---

## Contact
For any inquiries or feedback, feel free to reach out:

- Email: [bezuhlyi.mykhailo@gmail.com](mailto:bezuhlyi.mykhailo@gmail.com)
- LinkedIn: [Mikhailo Bezuhlyi](https://www.linkedin.com/in/mishatopkek/)
