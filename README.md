\# Policy Claim API



A RESTful API for managing insurance claims built with C# and .NET.



\## 🚀 Features

\- Full CRUD operations for insurance claims

\- Status tracking (Submitted, Under Review, Approved, Rejected, Paid)

\- Notes and documents support

\- Filtering and pagination

\- Swagger documentation

\- Interactive demo UI



\## 🛠️ Tech Stack

\- C# / .NET 8.0

\- ASP.NET Core Web API

\- Entity Framework Core

\- SQL Server (LocalDB)

\- Swagger / OpenAPI

\- HTML/CSS/JavaScript (Demo UI)



\## 📋 API Endpoints



| Method | Endpoint | Description |

|--------|----------|-------------|

| GET | `/api/claims` | Get all claims (with filters) |

| GET | `/api/claims/{id}` | Get claim by ID |

| GET | `/api/claims/number/{claimNumber}` | Get claim by claim number |

| POST | `/api/claims` | Create new claim |

| PUT | `/api/claims/{id}` | Update claim |

| PATCH | `/api/claims/{id}/status` | Update claim status |

| POST | `/api/claims/{id}/notes` | Add note to claim |

| DELETE | `/api/claims/{id}` | Delete claim |

| GET | `/api/claims/stats/summary` | Get claim statistics |



\## 🏃 How to Run



1\. \*\*Clone the repository\*\*

&#x20;  ```bash

&#x20;  git clone https://github.com/VisionDevs/PolicyClaimAPI.git

&#x20;  cd PolicyClaimAPI

