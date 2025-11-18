# 📚 School Management System – Backend API

A **backend API** for managing **students, teachers, departments, courses, classes, attendance, assignments, and submissions**, built with **.NET 9 Web API** using **Clean Architecture** principles.

---

## 🚀 Features

- **Clean Architecture:** Domain → Application → Infrastructure → API  
- **Role-Based Access:** Admin / Teacher / Student  
- **Entities & Relationships:**  
  - Departments, Courses, Classes (Batches)  
  - Student enrollment and mapping  
  - Attendance tracking  
  - Assignments & submissions  
  - Grading system  
- **Authentication & Authorization:** JWT-based  
- **Validation:** FluentValidation for input rules  
- **Automapper** for DTO mappings  
- **Logging:** Serilog (console, debug, file)  
- **Async/await** database operations  
- **Soft delete support** for entities  
- **Optional caching** for Departments & Courses  
- **Pagination & filtering** for Classes, Students, and Assignments

---

## 🏗 Project Structure

### API Layer (`School.API`)
- [Controllers](School.API/Controllers): AdminController, AuthController, StudentController, TeacherController  
- [Program.cs](School.API/Program.cs)  
- [Configuration](School.API/appsettings.json)  
- Static files/uploads for assignment submissions: [wwwroot/uploads](School.API/wwwroot/uploads)

### Application Layer (`School.Application`)
- [DTOs](School.Application/DTOs): Identity, Department, Course, Class, Assignment, Attendance, StudentClass, Submission  
- [Mappings](School.Application/Mappings/MappingConfig.cs)  
- [Services](School.Application/Services): Interfaces & Implementations  
- [Validators](School.Application/Validators): Authentication, Department  

### Domain Layer (`School.Domain`)
- [Entities](School.Domain/Entities): Core entities (Department, Course, Class, StudentClass, Attendance, Assignment, Submission) and Identity (ApplicationUser, RefreshToken)  
- Enums: [School.Domain/Enums](School.Domain/Enums)  
- Interfaces: Specification interfaces per module: [School.Domain/Interfaces](School.Domain/Interfaces)  

### Infrastructure Layer (`School.Infrastructure`)
- Data: [AppDbContext](School.Infrastructure/Data/AppDbContext.cs), [AppDbContextFactory](School.Infrastructure/Data/AppDbContextFactory.cs)  
- Repositories: Generic + specific per entity: [School.Infrastructure/Repositories](School.Infrastructure/Repositories)  
- Services: [SerilogLoggerAdapter.cs](School.Infrastructure/Services/SerilogLoggerAdapter.cs)  
- Migrations: [School.Infrastructure/Migrations](School.Infrastructure/Migrations)  
- Dependency Injection: [ServiceContainer.cs](School.Infrastructure/DependancyInjection/ServiceContainer.cs)

---

## 🔧 Setup Instructions

### 1️⃣ Clone the repository
Clone the repo and enter the project folder:

```bash
git clone https://github.com/nassar07/SchoolManagementSystem.git
cd SchoolManagementSystem
2️⃣ Configure connection string
Set your SQL Server connection in School.API/appsettings.json:

bash
Copy code
"ConnectionStrings": { 
  "DefaultConnection": "YOUR_SQL_CONNECTION_STRING" 
}
Also update AppDbContextFactory.cs in School.Infrastructure/Data:

bash
Copy code
optionsBuilder.UseSqlServer(
    "YOUR_SQL_CONNECTION_STRING",
    sql => sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
);
3️⃣ Run database migrations
Add initial migration and update the database:

bash
Copy code
dotnet ef migrations add InitialCreate -p School.Infrastructure -s School.API
dotnet ef database update -p School.Infrastructure -s School.API
🔐 API Endpoints
Authentication
Endpoints for registering, logging in, and refreshing JWT tokens:

bash
Copy code
POST   /api/auth/register        # Register new user (Admin / Teacher / Student)
POST   /api/auth/login           # Login and receive JWT
POST   /api/auth/refresh-token   # Refresh JWT token
Admin APIs
Manage departments and courses (Admin only):

bash
Copy code
POST   /api/admin/departments/create       # Create department
GET    /api/admin/departments/GetById/{id} # Get department by ID
GET    /api/admin/departments/GetAll       # Get all departments
PUT    /api/admin/departments/Update/{id}  # Update department
DELETE /api/admin/departments/delete/{id}  # Delete department

POST   /api/admin/courses/create           # Create course
GET    /api/admin/courses/GetById/{id}    # Get course by ID
GET    /api/admin/courses/GetAll          # Get all courses
PUT    /api/admin/courses/Update/{id}     # Update course
DELETE /api/admin/courses/delete/{id}     # Delete course
Teacher APIs
Manage classes, attendance, assignments, and grading (Teacher only):

bash
Copy code
POST   /api/teacher/classes/Create                   # Create class
GET    /api/teacher/classes/GetById/{id}            # Get class by ID
GET    /api/teacher/classes/GetAll                  # Get all classes
PUT    /api/teacher/classes/Update/{id}             # Update class
PUT    /api/teacher/classes/Deactivate/{id}         # Deactivate class
DELETE /api/teacher/classes/delete/{id}             # Delete class
POST   /api/teacher/classes/AssignStudentToClass    # Assign student to class

POST   /api/teacher/attendance                      # Mark attendance
GET    /api/teacher/attendance/{classId}           # Get attendance by class

POST   /api/teacher/assignments                     # Create assignment
GET    /api/teacher/assignments/{classId}          # Get assignments by class
POST   /api/teacher/assignments/{assignmentId}/{StudentId}/grade # Grade student submission
Student APIs
View classes, attendance, grades, and submit assignments (Student only):

bash
Copy code
GET    /api/student/classes/{studentId}     # Get student enrolled classes
GET    /api/student/attendance/{studentId}  # Get student attendance
GET    /api/student/grades/{studentId}      # Get student grades
POST   /api/student/assignments/submit      # Submit assignment
📝 Validation Rules
Email format & password strength validation

Prevent duplicate student enrollment

Assignment due date cannot be in the past

Only assigned teacher can mark attendance/grade

Admin-only endpoints cannot be accessed by other roles

🖥 Run the API
bash
Copy code
dotnet build
dotnet run --project School.API
Swagger available at:

bash
Copy code
https://localhost:{PORT}/swagger
🎥 Demo Video
Add demo video link here: https://drive.google.com/file/d/10ejNfJ8ngQRwI8jdDiGCYnweTRtGtQLs/view?usp=drive_link
