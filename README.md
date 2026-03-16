# POLICY CLAIM API - COMPLETE DOCUMENTATION

---

## 1. PROJECT OVERVIEW

**Policy Claim API** is a backend application that manages insurance claims. It stores, organizes, and processes claim data with full CRUD operations.

**Key Features:**
- Create, view, update, and delete claims
- Track claim status (Submitted → Under Review → Approved/Rejected → Paid)
- Add notes to claims
- Get statistics (totals, averages, breakdowns)

---

## 2. TECHNOLOGIES USED

| Technology | Version |
|------------|---------|
| .NET | 10.0 |
| C# | 13 |
| ASP.NET Core | 10.0 |
| Entity Framework Core | 10.0 |
| SQL Server | 2019 |
| AutoMapper | 12.0.1 |
| Swagger | 6.5.0 |

---

## 3. INSTALLATION & RUNNING

### Prerequisites
- .NET 10.0 SDK: https://dotnet.microsoft.com/download/dotnet/10.0

### Quick Start
```bash
# Clone the repository
git clone https://github.com/VisionDevs/PolicyClaimAPI.git
cd PolicyClaimAPI

# Run the API
dotnet run
```

**API will be available at:** http://localhost:5294

---

## 4. API ENDPOINTS

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/claims` | Get all claims |
| GET | `/api/claims/{id}` | Get claim by ID |
| GET | `/api/claims/number/{claimNumber}` | Get by claim number |
| POST | `/api/claims` | Create new claim |
| PUT | `/api/claims/{id}` | Update claim |
| PATCH | `/api/claims/{id}/status` | Update status |
| DELETE | `/api/claims/{id}` | Delete claim |
| GET | `/api/claims/stats/summary` | Get statistics |

### Sample Create Claim
```json
{
  "policyNumber": "POL-AUTO-12345",
  "claimantName": "John Doe",
  "claimantEmail": "john@email.com",
  "claimType": "Auto",
  "claimAmount": 5000.00,
  "dateOfLoss": "2026-03-15T00:00:00Z",
  "description": "Rear-end collision"
}
```

---

## 5. INTERACTIVE DEMO

The **interactive-demo.html** file provides a visual interface to use the API without coding.

### How to Use:
1. Keep API running (`dotnet run`)
2. Double-click `interactive-demo.html` in project folder
3. Use the form to add claims
4. View all claims in the grid
5. Update status or delete claims

**Demo Features:**
- Stats dashboard (total claims, amount, average)
- Create claim form
- Claims grid with cards
- One-click status updates
- Delete with confirmation

---

## 6. SAMPLE DATA

The system comes with 2 pre-loaded claims:

| Claim # | Claimant | Type | Amount | Status |
|---------|----------|------|--------|--------|
| CLM-2025-001 | John Doe | Auto | $5,000 | Approved |
| CLM-2025-002 | Jane Smith | Home | $12,500 | Under Review |

---

## 7. PROJECT STRUCTURE

```
PolicyClaimAPI/
│
├── Controllers/     # Handles requests (ClaimsController.cs)
├── Data/            # Database setup (ApplicationDbContext.cs)
├── Models/          # Data structures (Claim.cs, ClaimStatus.cs)
├── DTOs/            # Data transfer objects
├── Mappings/        # AutoMapper configurations
├── Program.cs       # Application entry point
├── appsettings.json # Configuration
└── interactive-demo.html # Visual interface
```

---

## 8. TROUBLESHOOTING

| Issue | Solution |
|-------|----------|
| "Couldn't find project" | Navigate to folder with .csproj file |
| API won't start | Install .NET 10.0 SDK |
| Demo can't connect | Check port in demo (line ~380): `const API_URL = 'http://localhost:5294/api/claims'` |
| Database errors | Install SQL Server LocalDB |
| GitHub push rejected | `git pull origin main --allow-unrelated-histories` then push |

### Quick Commands
```bash
# Restart API
Ctrl+C then dotnet run

# Clean rebuild
rm -rf bin obj
dotnet restore
dotnet run
```

---

## 9. GITHUB REPOSITORY

**URL:** https://github.com/VisionDevs/PolicyClaimAPI

```bash
# Clone command
git clone https://github.com/VisionDevs/PolicyClaimAPI.git
```

**Please ⭐ star the repository if you find it useful!**

---

## 10. ABOUT THE DEVELOPER

### Rotondwa Vision Mavhungu
**Junior Software Developer at Hollard Insurance**

### Connect With Me
- **GitHub:** https://github.com/VisionDevs
- **LinkedIn:** https://www.linkedin.com/in/vision-mavhungu-050023319
- **Portfolio:** https://visiondevs.github.io/Rotondwa-Portfolio/
- **Email:** vision.mavhungu@icloud.com

### Skills
C# | .NET | Python | JavaScript | SQL | Git | Azure

---

## 11. QUICK REFERENCE

```bash
# Start API
cd PolicyClaimAPI
dotnet run

# Open demo (new window)
start interactive-demo.html

# Test API
http://localhost:5294/api/claims

# Swagger docs
http://localhost:5294/swagger

# GitHub
https://github.com/VisionDevs/PolicyClaimAPI
```

---

**Created with ❤️ by Rotondwa Vision Mavhungu**  
**© 2026 All Rights Reserved**
