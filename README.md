# 📚 School Management System – Backend API

A **backend API** for managing **students, teachers, departments, courses, classes, attendance, assignments, submissions, and notifications**, built with **.NET 9 Web API** using **Clean Architecture** principles.

---

## 🚀 Features
- **Clean Architecture:** Domain → Application → Infrastructure → API.  
- **Role-Based Access:** Admin / Teacher / Student.  
- **Entities & Relationships:**  
  - Departments, Courses, Classes (Batches)  
  - Student enrollment and mapping  
  - Attendance tracking  
  - Assignments & submissions  
  - Grading system  
  - Notifications  
- **Authentication & Authorization:** JWT-based.  
- **Validation:** FluentValidation for input rules.  
- **Automapper** for DTO mappings.  
- **Logging:** Serilog (console, debug, file).  
- **Async/await** database operations.  
- **Soft delete support** for users and courses.  
- **Optional caching** for Departments & Courses.  
- **Pagination & filtering** for Classes, Students, and Assignments.

---

## 🏗 Solution Structure
SchoolManagementSystem.sln
│
├── School.API
│ ├── Controllers
│ │ ├── AdminController.cs
│ │ ├── AuthController.cs
│ │ ├── StudentController.cs
│ │ └── TeacherController.cs
│ ├── Program.cs
│ ├── appsettings.json
│ ├── appsettings.Development.json
│ └── wwwroot/uploads (assignment files)
│
├── School.Application
│ ├── DTOs
│ │ ├── Identity
│ │ ├── Department
│ │ ├── Course
│ │ ├── Class
│ │ ├── Assignment
│ │ ├── Attendance
│ │ ├── StudentClass
│ │ └── Submission
│ ├── Mappings
│ │ └── MappingConfig.cs
│ ├── Services
│ │ ├── Implementation
│ │ └── Interfaces
│ └── Validators
│ ├── Authentication
│ └── Department
│
├── School.Domain
│ ├── Entities
│ │ ├── Identity
│ │ └── Core Entities (Department, Course, Class, StudentClass, Attendance, Assignment, Submission)
│ ├── Enums
│ └── Interfaces
│ ├── AssignmentSpecifies
│ ├── AttendanceSpecifies
│ ├── Authentication
│ ├── ClassSpecifies
│ ├── CourseSpecifies
│ ├── DepartmentSpecifies
│ ├── StudentClassSpecifies
│ └── SubmissionSpecifies
│
└── School.Infrastructure
├── Data (AppDbContext, AppDbContextFactory)
├── Repositories
│ ├── GenericRepository.cs
│ ├── AssignmentSpecifies
│ ├── AttendanceSpecifies
│ ├── Authentication
│ ├── ClassSpecifies
│ ├── CourseSpecifies
│ ├── DepartmentSpecifies
│ ├── StudentClassSpecifies
│ └── SubmissionSpecifies
├── Services (SerilogLoggerAdapter.cs)
├── Migrations
└── DependancyInjection (ServiceContainer.cs)


---

## 🔧 Setup Instructions

### 1️⃣ Clone the repository

git clone https://github.com/YourUsername/SchoolManagementSystem.git
cd SchoolManagementSystem

2️⃣ Configure connection string

In School.API/appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "YOUR_SQL_CONNECTION_STRING"
}


Also, update AppDbContextFactory.cs in School.Infrastructure/Data:

optionsBuilder.UseSqlServer(
    "YOUR_SQL_CONNECTION_STRING",
    sql => sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
);

3️⃣ Run database migrations
dotnet ef migrations add InitialCreate -p School.Infrastructure -s School.API
dotnet ef database update -p School.Infrastructure -s School.API

🔐 Authentication Endpoints
Method	Endpoint	Description
POST	/api/auth/register	Register new user (Admin / Teacher / Student)
POST	/api/auth/login	Login and receive JWT
POST	/api/auth/refresh-token	Refresh JWT token
👨‍💼 Admin APIs
Method	Endpoint	Description
POST	/api/admin/departments/create	Create department
GET	/api/admin/departments/GetById/{id}	Get department by ID
GET	/api/admin/departments/GetAll	Get all departments
PUT	/api/admin/departments/Update/{id}	Update department
DELETE	/api/admin/departments/delete/{id}	Delete department
POST	/api/admin/courses/create	Create course
GET	/api/admin/courses/GetById/{id}	Get course by ID
GET	/api/admin/courses/GetAll	Get all courses
PUT	/api/admin/courses/Update/{id}	Update course
DELETE	/api/admin/courses/delete/{id}	Delete course
👩‍🏫 Teacher APIs
Method	Endpoint	Description
POST	/api/teacher/classes/Create	Create class
GET	/api/teacher/classes/GetById/{id}	Get class by ID
GET	/api/teacher/classes/GetAll	Get all classes
PUT	/api/teacher/classes/Update/{id}	Update class
PUT	/api/teacher/classes/Deactivate/{id}	Deactivate class
DELETE	/api/teacher/classes/delete/{id}	Delete class
POST	/api/teacher/classes/AssignStudentToClass	Assign student to class
POST	/api/teacher/attendance	Mark attendance
GET	/api/teacher/attendance/{classId}	Get attendance by class
POST	/api/teacher/assignments	Create assignment
GET	/api/teacher/assignments/{classId}	Get assignments by class
POST	/api/teacher/assignments/{assignmentId}/{StudentId}/grade	Grade student submission
🎓 Student APIs
Method	Endpoint	Description
GET	/api/student/classes/{studentId}	Get student enrolled classes
GET	/api/student/attendance/{studentId}	Get student attendance
GET	/api/student/grades/{studentId}	Get student grades
POST	/api/student/assignments/submit	Submit assignment
📝 Validation Rules

Email format & password strength validation

Prevent duplicate student enrollment

Assignment due date cannot be in the past

Only assigned teacher can mark attendance/grade

Admin-only endpoints cannot be accessed by other roles

🖥 Run the API
dotnet build
dotnet run --project School.API


Swagger available at:
https://localhost:{PORT}/swagger

🎥 Demo Video

Add demo video link here:
https://youtube.com/...
