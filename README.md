# AI Resume Analyzer

An AI-powered Resume Analysis and Job Matching platform built with .NET 9, Blazor, Azure Functions, Azure OpenAI, Azure Blob Storage, and Azure Table Storage.

The application helps job seekers improve their resumes by providing ATS scoring, skill gap analysis, AI-powered recommendations, and job description matching.

---

## Features

### Resume Upload

* Upload resume in PDF format
* Secure storage using Azure Blob Storage
* Automatic text extraction

### AI Resume Analysis

* ATS Score Calculation
* Resume Summary Generation
* Technical Skill Detection
* Missing Skill Identification
* Resume Improvement Recommendations

### Job Description Matching

* Compare resume against a job posting
* Match Score Calculation
* Matched Skills Identification
* Missing Skills Detection
* Personalized Recommendations

### Cloud Native Architecture

* Blazor Frontend
* Azure Functions Backend
* Azure OpenAI Integration
* Azure Blob Storage
* Azure Table Storage

---

## Architecture

```text
+----------------------+
|      Blazor UI       |
+----------+-----------+
           |
           v
+----------------------+
|   Azure Functions    |
+----------+-----------+
           |
           +------------------+
           |                  |
           v                  v
+----------------+    +----------------+
| Azure OpenAI   |    | Blob Storage   |
+----------------+    +----------------+
                              |
                              v
                     +----------------+
                     | Table Storage  |
                     +----------------+
```

---

## Technology Stack

### Frontend

* Blazor (.NET 9)
* Bootstrap 5
* Bootstrap Icons

### Backend

* Azure Functions
* ASP.NET Core
* Dependency Injection

### AI

* Azure OpenAI
* GPT-based Resume Analysis

### Storage

* Azure Blob Storage
* Azure Table Storage

### Development

* C#
* .NET 9
* Visual Studio 2022

---

## Solution Structure

```text
src/

├── ResumeAnalyzer.Blazor
│   ├── Pages
│   ├── Components
│   └── Services
│
├── ResumeAnalyzer.Functions
│   ├── Functions
│   ├── Services
│   └── Entities
│
├── ResumeAnalyzer.AnalysisService
│   ├── Services
│   ├── Interfaces
│   └── Models
│
└── ResumeAnalyzer.Shared
    └── Models
```

---

## User Flow

1. Upload Resume
2. Resume stored in Azure Blob Storage
3. PDF text extracted
4. Azure OpenAI analyzes content
5. ATS score generated
6. Results stored in Azure Table Storage
7. User reviews analysis
8. User pastes job description
9. Job match score generated
10. Skill gap recommendations displayed

---

## Screenshots

### Home

Upload resume and start analysis.

![Home](docs/images/home.png)

### Resume Analysis

ATS score, strengths, missing skills, and recommendations.

![Analysis](docs/images/analysis.png)

### Job Match

Compare resume against a job description.

![Job Match](docs/images/jobmatch.png)

---

## Local Development

### Prerequisites

* .NET 9 SDK
* Azure Storage Emulator (Azurite)
* Azure OpenAI Resource
* Visual Studio 2022

### Run

```bash
git clone https://github.com/yourusername/AI-Resume-Analyzer.git

cd AI-Resume-Analyzer

dotnet restore

dotnet build

dotnet run
```

---

## Configuration

### appsettings.json

```json
{
  "StorageConnectionString": "UseDevelopmentStorage=true",
  "OpenAI": {
    "Endpoint": "<endpoint>",
    "ApiKey": "<apikey>"
  }
}
```

---

## Future Enhancements

* LinkedIn Profile Analysis
* Multi-Resume Comparison
* Resume Version Tracking
* Cover Letter Generation
* Career Path Recommendations
* Azure AI Search Integration
* Interview Question Generator
* Recruiter Dashboard

---

## Learning Objectives

This project demonstrates:

* Cloud Native Application Design
* Serverless Architecture
* Azure Storage Services
* AI Integration using Azure OpenAI
* Clean Architecture Principles
* Dependency Injection
* Blazor UI Development
* Azure Functions Development

---

## License

MIT License

---

## Author

Built using .NET, Azure and AI technologies to explore modern cloud-native application development.
