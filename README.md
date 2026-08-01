# InfoTrack Solicitor Scraper

A full-stack web application built as part of the InfoTrack development assessment.

The application automates the extraction of solicitor contact details from [Solicitors.com](https://www.solicitors.com/) based on selected locations. Users can manage which locations are enabled for scraping, run the scraper, and view the results in a structured report format.

---

## Features

### Solicitor Data Extraction
- Scrapes solicitor information from:
  - https://www.solicitors.com/conveyancing.html
- Supports the following locations:
  - London
  - Birmingham
  - Leeds
  - Manchester
  - Sheffield
  - Bradford
  - Liverpool
  - Bristol

Extracted information includes:
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

### Location Management
Users can:
- View available search locations
- Enable/disable locations
- Control which locations are included during scraping

Locations are stored separately from solicitor results to allow future expansion and historical tracking.

---

### Reporting Dashboard
The application provides a clean report layout displaying:
- Solicitor results grouped by location
- Ratings and review counts
- Contact information
- Website/email links
- Verification status

---

## Architecture

The solution follows a Clean Architecture approach with separation of responsibilities:

```
InfoTrack.SolicitorScraper
│
├── InfoTrack.SolicitorScraper.Api
│   └── REST API endpoints
│
├── InfoTrack.SolicitorScraper.Application
│   └── Application services and business logic
│
├── InfoTrack.SolicitorScraper.Domain
│   └── Entities and interfaces
│
├── InfoTrack.SolicitorScraper.Infrastructure
│   └── Scraping logic and data storage implementations
│
└── InfoTrack.SolicitorScraper.Web
    └── Vue.js SPA frontend
```

---

## Technology Stack

### Backend
- .NET 10 Web API
- C#
- ASP.NET Core
- Dependency Injection
- REST API
- Clean Architecture principles

### Frontend
- Vue 3
- TypeScript
- Vite
- Tailwind CSS / DaisyUI

### Storage
The application currently uses an in-memory repository implementation.

This keeps the application lightweight while allowing repositories to be replaced with a database implementation in the future.

---

# Running the Application

## Prerequisites

Ensure you have installed:

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

---

# Frontend Setup

Navigate to the Vue application:

```
cd InfoTrack.SolicitorScraper.Web
```

Install dependencies:

```
npm install
```

Run the frontend:

```
npm run dev
```

The application will be available through the Vite development server.

---

# API Endpoints

## Locations

### Get available locations

```
GET /api/Locations
```

Returns the configured scraping locations.

---

## Scraper

### Run scraper

```
POST /api/Scraper/run
```

Runs the scraper against all enabled locations and returns the extracted solicitor information.

---

# Scraping Approach

The scraper was implemented without third-party scraping libraries as requested.

The scraping process is separated into:

### Http Client Layer
Responsible for:
- Requesting HTML pages
- Managing HTTP headers
- Handling responses

### HTML Parser
Responsible for:
- Extracting solicitor result blocks
- Parsing individual fields
- Mapping HTML data into domain entities

### Scraper Service
Responsible for:
- Coordinating scraping operations
- Processing enabled locations
- Saving results

This keeps scraping logic reusable and easier to maintain.

---

# Design Decisions

## Dependency Injection

Services and repositories are registered through ASP.NET Core dependency injection.

This allows implementations to be swapped without changing business logic.

Example:

```
ISolicitorRepository
        |
        |
InMemorySolicitorRepository
```

A database repository could be introduced later without modifying the application layer.

---

## Repository Pattern

Repositories abstract data access from the rest of the application.

Benefits:
- Cleaner separation of concerns
- Easier testing
- Database implementation can be added later

---

## Domain Driven Structure

Business entities and rules are kept separate from infrastructure concerns.

This allows the application to scale with additional features such as:
- Scheduled scraping
- Historical reporting
- Change detection
- Additional scraping sources

---

# Future Improvements

Given more development time, possible improvements would include:

### Database Persistence
Replace in-memory storage with:
- SQL Server Express
- PostgreSQL

Allowing:
- Historical scrape results
- Daily/weekly comparisons
- New solicitor detection

---

### Background Scheduling

Add scheduled scraping using:
- Hosted services
- Azure Functions

---

### Additional Insights

Generate analytics such as:

- Highest rated solicitors
- Most reviewed solicitors
- Location comparisons
- New solicitor alerts

---

# Notes

The application was designed to prioritise:
- Clean architecture
- Maintainability
- Separation of concerns
- Reusable components
- Simple deployment
