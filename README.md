# InfoTrack Solicitor Scraper

A full-stack web application developed as part of the InfoTrack development assessment.

The application automates the extraction of solicitor contact details from [Solicitors.com](https://www.solicitors.com/) based on selected locations. Users can manage which locations are enabled, run the scraper, and view the scraped solicitor information through a reporting interface.

---

# Features

## Solicitor Data Extraction

The application scrapes solicitor information from:

https://www.solicitors.com/conveyancing.html

Supported locations:

- London
- Birmingham
- Leeds
- Manchester
- Sheffield
- Bradford
- Liverpool
- Bristol

The scraper extracts:

- Solicitor name
- Location
- Verified status
- Star rating
- Review count
- Phone number
- Address
- Description
- Website URL
- Email URL
- Listing URL

---

## Location Management

Users can:

- View available scraping locations
- Add new locations
- Enable or disable locations
- Control which locations are included during scraping

Locations are stored separately from solicitor results, allowing the application to be extended with historical scraping and tracking functionality in the future.

---

## Reporting

The application provides a structured report containing:

- Solicitor details
- Contact information
- Ratings
- Review counts
- Location-based results

The report data is formatted for easy consumption by the frontend application.

---

# Solution Architecture

The solution follows a Clean Architecture approach with separation of responsibilities.

```
InfoTrack.SolicitorScraper
│
├── InfoTrack.SolicitorScraper.Api
│   └── REST API controllers and application entry point
│
├── InfoTrack.SolicitorScraper.Application
│   └── Business logic, services and DTOs
│
├── InfoTrack.SolicitorScraper.Domain
│   └── Domain entities and interfaces
│
├── InfoTrack.SolicitorScraper.Infrastructure
│   └── Scraping logic and repository implementations
│
└── InfoTrack.SolicitorScraper.Web
    └── Vue.js frontend application
```

---

# Technology Stack

## Backend

- C#
- .NET 10 Web API
- ASP.NET Core
- Dependency Injection
- REST API
- Clean Architecture principles

## Frontend

- Vue 3
- TypeScript
- Vite
- Tailwind CSS
- DaisyUI

## Storage

The application uses in-memory repository implementations.

No database configuration is required.

The repository pattern has been used so that a database implementation could be introduced in the future without changing the application logic.

---

# Running the Application

## Prerequisites

Install:

- .NET 10 SDK
- Node.js (v18 or later)
- npm

---

# Backend Setup

Navigate to the API project:

```
cd InfoTrack.SolicitorScraper.Api
```

Restore dependencies:

```
dotnet restore
```

Run the API:

```
dotnet run
```

The API will start on:

```
https://localhost:7159
```

Swagger documentation is available at:

```
https://localhost:7159/swagger
```

---

# Frontend Setup

Navigate to the frontend project:

```
cd InfoTrack.SolicitorScraper.Web
```

Install dependencies:

```
npm install
```

Run the application:

```
npm run dev
```

The Vue application will start using the Vite development server.

---

# API Endpoints

## Locations

### Get all locations

```
GET /api/Locations
```

Returns all available scraping locations and their enabled/disabled status.

---

### Add a new location

```
POST /api/Locations
```

Creates a new scraping location.

Example request:

```json
{
  "name": "Liverpool",
  "urlSlug": "liverpool",
  "isEnabled": true
}
```

---

### Update location status

```
PUT /api/Locations/{id}/status
```

Updates whether a location is enabled for scraping.

Example request:

```json
{
  "isEnabled": false
}
```

---

# Solicitors

### Get all solicitors

```
GET /api/Solicitors
```

Returns all scraped solicitor records.

---

### Get solicitors by location

```
GET /api/Solicitors/location/{locationId}
```

Returns solicitor records filtered by location.

---

# Reports

### Get solicitor report

```
GET /api/Reports
```

Returns formatted solicitor report data used by the frontend dashboard.

---

# Scraping Implementation

The scraper was implemented without third-party scraping libraries as requested.

The scraping process is separated into different responsibilities:

## HTTP Client

Responsible for:

- Sending requests to Solicitors.com
- Handling HTTP responses
- Managing request configuration

## HTML Parser

Responsible for:

- Extracting solicitor result sections
- Parsing HTML content
- Mapping extracted information into application models

## Scraper Service

Responsible for:

- Managing the scraping workflow
- Processing enabled locations
- Saving scraped solicitor results

This separation keeps the scraping logic reusable and easier to maintain.

---

# Design Decisions

## Dependency Injection

ASP.NET Core dependency injection is used throughout the application.

This allows services and repositories to be replaced without changing business logic.

Example:

```
ISolicitorRepository
        |
        |
InMemorySolicitorRepository
```

---

## Repository Pattern

Repositories are used to abstract data access.

Benefits:

- Separation of concerns
- Easier testing
- Ability to replace in-memory storage with a database later

---

## DTO Usage

DTOs are used between the API and application layers to:

- Control the data exposed by the API
- Avoid exposing domain models directly
- Keep API contracts separate from internal models

---

# Future Improvements

Given additional development time, the application could be extended with:

## Database Persistence

Replace the current in-memory storage with:

- SQL Server Express
- PostgreSQL

This would allow:

- Historical scrape results
- Tracking changes between scrapes
- Detecting newly added solicitors

---

## Scheduled Scraping

Add automated scraping using:

- Background services
- Scheduled jobs

This would allow solicitor information to be refreshed automatically.

---

## Additional Scraping Sources

The scraper could be extended to support additional conveyancing websites by introducing additional scraper implementations.

---

# Notes

The application was designed with focus on:

- Clean separation of concerns
- Maintainable code structure
- Reusable components
- Simple setup and deployment
- Extensibility for future requirements