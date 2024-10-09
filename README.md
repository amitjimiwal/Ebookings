# EBookings

This project is an event booking website with a .NET Web API backend and an Angular frontend.

## Table of Contents

- [Installation](#installation)
- [Technology Stack](#technology-stack)
- [License](#license)

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Node.js](https://nodejs.org/) (LTS version recommended)
- [Angular CLI](https://angular.io/cli)

### Backend Setup

1. Navigate to the `api` folder:
   ```
   cd api
   ```

2. Restore the required packages:
   ```
   dotnet restore
   ```

3. Run the API:
   ```
   dotnet run
   ```

The API should now be running on `http://localhost:5077`.

### Frontend Setup

1. Navigate to the `frontend` folder:
   ```
   cd frontend
   ```

2. Install the required npm packages:
   ```
   npm install
   ```

3. Start the Angular development server:
   ```
   ng serve
   ```

The frontend application should now be accessible at `http://localhost:4200`.


## Technology Stack

### Backend
- .NET 8.0 Web API
- Entity Framework Core (for database access)
- ASP.NET Core Identity (for authentication and authorization)

### Frontend
- Angular 18
- TypeScript
- RxJS

### Database
- SQL Server 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.