# 🌤️ Weather Forecast API

A RESTful Web API built with ASP.NET Core (.NET 8) that fetches and stores weather forecast data using [Open-Meteo](https://open-meteo.com) based on latitude and longitude inputs. 

This project follows **Microservices Architecture** principles and is structured using **Clean Architecture** for separation of concerns and testability.

---

## 🚀 Features

- Add and store latitude/longitude locations
- Fetch current weather forecasts from Open-Meteo
- Store structured forecast data in SQL Server
- List all tracked locations
- Retrieve latest forecasts for stored locations
- Delete forecasts
- Swagger UI for easy testing
- JSON input/output support
- Basic test/spec coverage

---

## 🧱 Tech Stack

- **.NET 8 Web API**
- **Entity Framework Core** (SQL Server)
- **HttpClient** for Open-Meteo integration
- **Swagger / Swashbuckle** for API docs
- **xUnit / Moq** for testing

---

## 📦 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) (optional but recommended)

### ⚙️ Setup

1. Clone the repo:
git clone https://github.com/mounika-snkr/weather-api.git
cd weather-api

2. Run the app:
dotnet run
