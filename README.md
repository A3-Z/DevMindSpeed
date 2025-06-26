# Dev Mind Speed – Game API

Solution for the CIRCA Backend Developer Task : Dev Mind Speed Game API 

A backend-only game that tests a player's ability to solve math problems quickly using HTTP API requests. Built with ASP.NET Core Web API and Entity Framework Core.

---

## Features

- Start a new game session
- Answer math questions through API
- Track response time and accuracy
- End the game and view a detailed summary
- Game difficulty levels: Easy (1) → Hard (4)

---

##Tech Stack

- **Backend**: ASP.NET Core Web API (.NET 8)
- **Database**: MySQL (EF Core)
- **ORM**: Entity Framework Core
- **Mapping**: Mapster

---

## Setup Instructions

1. **Clone the repository**

2. **Update connection string**
   In appsettings.json, update your MySQL connection string:
   "ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=yourpassword;database=DevMindSpeed;"
  }
3. **Apply Migrations**
4. **Update Database**

##  Sample Postman Requests

### Start A Game 

**Endpoint:** 
`POST /game/start`
**Request Body:**
```json
{
  "playerName": "Abdulraheem",
  "difficulty": 2
}
```
### Submit an Answer

**Endpoint:**  
`POST /game/{gameId}/submit`

**Request Body:**
```json
{
  "questionId": "your-question-guid-here(from the response of the start game request)",
  "playerAnswer": 42.0
}
```
### End the Game

**Endpoint:**  
`GET /game/{gameId}/end`

**Request Body:**
```json
{
}
```

## Project Structure
DevMindSpeed/
│
├── DevMindSpeed.API/              # Main API entry point
├── DevMindSpeed.BusinessLayer/   # Core game logic and services
├── DevMindSpeed.DataAccessLayer/ # EF Core repositories and DBContext
├── DevMindSpeed.Entity/          # Entity definitions (Game, Question)
└── DevMindSpeed.Common/          # Shared models, helpers

## Task Requirements Checklist

 1.**Start Game API**
 2.**Submit Answer API**
 3.**End Game API**
 4.**MySQL integration**
 5.**Tracks best score**
 6.**Provides full history**
 7.**Sample Postman usage**
 8.**README with setup instructions**
