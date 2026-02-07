# DisasterAllevation_Foundation
Disaster Alleviation Foundation
A comprehensive web application for managing disaster relief efforts, built with ASP.NET Core MVC and Entity Framework Core.

🎯 Overview
The Disaster Alleviation Foundation web application provides a complete platform for:

User Management: Secure registration, login, and profile management
Disaster Reporting: Incident reporting and management system
Resource Donations: Money and resource donation tracking
Volunteer Management: Task assignment and volunteer coordination
Admin Dashboard: Comprehensive administrative controls
🚀 Features
User Authentication & Authorization
Secure Registration: Email-based user registration with confirmation
Role-Based Access: Admin and User roles with appropriate permissions
Password Management: Secure password hashing and validation
Profile Management: User profile creation and management
Disaster Incident Reporting
Incident Submission: Users can report disasters with detailed information
Admin Approval: Workflow for admin approval/rejection of reports
Status Tracking: Real-time status updates (Pending/Approved/Rejected)
Report Management: Comprehensive admin interface for report management
Resource Donation System
Monetary Donations: Track financial contributions
Resource Donations: Manage physical resource donations (food, clothing, medical supplies)
Donation Tracking: Complete donation history and analytics
Admin Management: Full donation oversight and reporting
Volunteer Management
Volunteer Registration: Skills-based volunteer registration
Task Assignment: Create and assign volunteer tasks
Task Management: Browse, sign up, and manage volunteer tasks
Skills Tracking: Match volunteers to tasks based on skills
Assignment Tracking: Monitor volunteer contributions
Admin Dashboard
Real-Time Statistics: Live data on users, reports, donations, and volunteers
User Management: Complete user administration
Report Management: Approve/reject disaster reports
Task Management: Create and manage volunteer tasks
Donation Analytics: Comprehensive donation reporting
🛠️ Technology Stack
Framework: ASP.NET Core 8.0 MVC
Database: SQL Server with Entity Framework Core
Authentication: ASP.NET Core Identity
Frontend: Bootstrap 5, HTML5, CSS3, JavaScript
Icons: Bootstrap Icons
Version Control: Git with Azure DevOps
CI/CD: Azure Pipelines
📋 Prerequisites
.NET 8.0 SDK
SQL Server (LocalDB or full SQL Server)
Visual Studio 2022 or VS Code
Git
🚀 Getting Started
1. Clone the Repository
git clone https://dev.azure.com/GiftOfTheGiversP1/Disaster%20Alleviation%20Foundation/_git/Disaster%20Alleviation%20Foundation
cd DisasterAlleviationFoundation
2. Database Setup
# Update database
dotnet ef database update
3. Run the Application
dotnet run
4. Access the Application
URL: https://localhost:7000
Admin Login: admin@disasterfoundation.com / Admin123!
Default User: Create account through registration
🏗️ Project Structure
DisasterAlleviationFoundation/
├── Controllers/           # MVC Controllers
├── Models/               # Data Models and ViewModels
├── Views/                # Razor Views
├── Data/                 # Entity Framework Context and Migrations
├── Areas/                # Identity Area
├── wwwroot/              # Static files (CSS, JS, Images)
├── Properties/           # Application properties
└── Program.cs            # Application entry point
🔧 Configuration
Database Connection
Update appsettings.json with your database connection string:

{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Connection-String-Here"
  }
}
Email Configuration
Configure email settings in appsettings.json for user registration confirmations.

🧪 Testing
You can run tests locally or via the Azure DevOps pipeline.

Run unit/integration tests locally
# From the repo root
dotnet test DisasterAlleviationFoundation.Tests \
  --configuration Release \
  --collect:"XPlat Code Coverage" \
  --logger:"trx;LogFileName=test-results.trx"
Results: DisasterAlleviationFoundation.Tests/TestResults/ (TRX files)
Coverage: coverage.cobertura.xml inside TestResults subfolder
Optional: open coverage with ReportGenerator (if installed):

reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html
Quick load test (baseline)
PowerShell -ExecutionPolicy Bypass -File .\QuickLoadTest.ps1
Outputs 100 sequential requests with success rate and average latency.

Load & stress test (scenarios)
PowerShell -ExecutionPolicy Bypass -File .\Run-LoadTest.ps1
Adjust users/duration in the script parameters (see the top of the script). Captures totals, percentiles (P50/P90/P95/P99), and errors.

Pipeline tests (CI)
Pushing to main triggers the Azure DevOps pipeline which:

Restores and builds
Runs unit tests and collects coverage
Packages and deploys to Azure App Service
Performs a post-deploy health check
📊 Database Schema
Core Entities
Users: Identity users with custom properties
DisasterReports: Disaster incident reports
Donations: Monetary and resource donations
Volunteers: Volunteer profiles and skills
VolunteerTasks: Available volunteer tasks
VolunteerTaskAssignments: Task assignments with skills
Relationships
One-to-Many: User → DisasterReports, Donations
One-to-One: User → Volunteer
One-to-Many: VolunteerTask → VolunteerTaskAssignments
Many-to-Many: Volunteer ↔ VolunteerTask (through assignments)
🚀 Deployment
Azure App Service
Create Azure App Service
Configure connection string
Deploy using Azure Pipelines
Run database migrations
Local Deployment
Update connection string
Run dotnet ef database update
Run dotnet run
🔒 Security Features
Password Hashing: Secure password storage
Role-Based Authorization: Admin/User access control
Input Validation: Comprehensive data validation
SQL Injection Protection: Entity Framework parameterized queries
XSS Protection: Razor view encoding
📈 Performance Features
Entity Framework Optimization: Efficient database queries
Bootstrap Responsive Design: Mobile-friendly interface
Lazy Loading: Optimized data loading
Caching: Strategic data caching
🤝 Contributing
Branching Strategy
This project follows Gitflow branching model:

main: Production-ready code
develop: Integration branch
feature/*: Feature development
hotfix/*: Critical fixes
Commit Convention
feat: New features
fix: Bug fixes
docs: Documentation
style: Code style
refactor: Code refactoring
test: Tests
chore: Maintenance
📝 License
This project is developed for educational purposes as part of POE Part 2.

👥 Team
Developer: Sijongokuhle Jikijela
Student ID: ST10374043
Institution: Rosebank College
📞 Support
For technical support or questions, please contact the development team.

Built with ❤️ for Disaster Relief Management
