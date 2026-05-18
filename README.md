# IBI SmartHome System - Backend

A robust and scalable backend for a Smart Home Control Center, built with .NET 8 and following a clean three-layer architecture.

## Overview

The IBI SmartHome System backend provides the core logic, data persistence, and real-time communication capabilities for managing smart devices in a household environment. It handles authentication, device management, climate control, lighting, and security monitoring.

## Technology Stack

- **Framework**: .NET 8 (ASP.NET Core Web API)
- **Database**: MS SQL Server (via Pomelo.EntityFrameworkCore.MySql for MySQL/MariaDB compatibility)
- **ORM**: Entity Framework Core (Code-First approach)
- **Authentication**: ASP.NET Core Identity
- **Real-time Communication**: SignalR (WebSockets)
- **API Documentation**: Swagger/OpenAPI
- **Containerization**: Docker

## Architecture

The project follows a **Three-Layer Architecture** to ensure separation of concerns and maintainability:

1.  **IBI SmartHome System.Data**: Handles data persistence, database context, and entity configurations. Includes EF Core Migrations and Seeding.
2.  **IBI SmartHome System.Service**: Contains the business logic, service interfaces, and implementations (e.g., `ClimateService`, `LightingService`, `SecurityService`).
3.  **IBI SmartHome System**: The entry point Web API project, managing controllers, authentication, and SignalR hubs.

### Design Patterns
- **Repository Pattern**: Abstracting data access.
- **Service Layer Pattern**: Decoupling business logic from controllers.
- **Dependency Injection**: Managing service lifetimes and promoting testability.

## Database Structure

The system uses a relational schema with the following core entities:
- **User (ApplicationUser)**: Identity management and authorization.
- **House**: The top-level container for rooms and devices.
- **Room**: Logical grouping of devices within a house.
- **Device**: Base entity for all smart devices (Lamps, Motion Sensors, Cameras, etc.).
- **ClimateSchedule / Temperature**: Specialized entities for climate monitoring and automation.

## Security & Identity

- **Authentication**: Implemented using ASP.NET Core Identity with cookie-based or token-based support.
- **Authorization**: Role-based access control (RBAC).
- **CORS**: Configured to allow communication from authorized frontend origins (configurable via environment variables).
- **HTTPS**: All communication is secured via HTTPS redirections.

## Real-time with SignalR

SignalR is used to provide instant updates to the frontend without page refreshes. Key events include:
- Status changes of devices (on/off).
- Real-time temperature and climate updates.
- Security alerts and motion detection notifications.

## Testing

The project includes comprehensive testing suites:
- **Unit Tests (IBI_SmartHome_System.Tests)**: Testing business logic and services using xUnit.
- **UI Tests (IBI_SmartHome_System.UITests)**: Automated browser testing (Selenium-based) to verify the complete user flow from login to device control.

## Deployment & Setup

### Prerequisites
- .NET 8 SDK
- Docker & Docker Compose
- MySQL/MariaDB Server

### Local Setup
1.  **Clone the repository**.
2.  **Configure appsettings.json**: Update the DefaultConnection string.
3.  **Apply Migrations**:
    ```bash
    dotnet ef database update --project "IBI SmartHome System.Data" --startup-project "IBI SmartHome System"
    ```
4.  **Run the application**:
    ```bash
    dotnet run --project "IBI SmartHome System"
    ```

### Docker
```bash
docker build -t ibi-smarthome-backend .
docker run -p 8080:8080 ibi-smarthome-backend
```
