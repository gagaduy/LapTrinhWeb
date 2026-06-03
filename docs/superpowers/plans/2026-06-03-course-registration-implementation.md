# Course Registration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build the midterm ASP.NET Core MVC course registration application with EF Core, Identity, SQL Server via Docker, course browsing, admin CRUD, enrollment, search, and Google login wiring.

**Architecture:** The app keeps a standard MVC structure with `ApplicationDbContext` extending `IdentityDbContext<ApplicationUser>`. Public course browsing lives in MVC controllers/views, admin CRUD is isolated under `/admin/courses`, and Identity handles local and Google authentication with seeded roles and starter data.

**Tech Stack:** ASP.NET Core MVC (.NET 8), Entity Framework Core, ASP.NET Core Identity, SQL Server, Docker Compose, xUnit

---

### Task 1: Foundation And Data Model

**Files:**
- Create: `Data/ApplicationDbContext.cs`
- Create: `Data/DbInitializer.cs`
- Create: `Models/ApplicationUser.cs`
- Create: `Models/Category.cs`
- Create: `Models/Course.cs`
- Create: `Models/Enrollment.cs`
- Modify: `KiemTraGiuaKy.csproj`
- Modify: `Program.cs`
- Modify: `appsettings.json`
- Create: `docker-compose.yml`
- Create: `.env.example`

- [ ] Add EF Core, SQL Server, Identity, and test package references.
- [ ] Configure SQL Server and Identity services in `Program.cs`.
- [ ] Define domain models and relationships in `ApplicationDbContext`.
- [ ] Seed roles, an admin account, categories, and sample courses.
- [ ] Add Docker Compose SQL Server service and app connection string defaults.

### Task 2: Testable Query And Enrollment Logic

**Files:**
- Create: `Services/CourseQueryService.cs`
- Create: `Services/EnrollmentService.cs`
- Create: `Services/PagedResult.cs`
- Create: `tests/KiemTraGiuaKy.Tests/KiemTraGiuaKy.Tests.csproj`
- Create: `tests/KiemTraGiuaKy.Tests/CourseQueryServiceTests.cs`
- Create: `tests/KiemTraGiuaKy.Tests/EnrollmentServiceTests.cs`

- [ ] Write failing tests for pagination, search, duplicate-enrollment prevention, and unenroll behavior.
- [ ] Implement the minimal services to satisfy the tests.
- [ ] Re-run targeted tests until green.

### Task 3: MVC Features And Auth Flows

**Files:**
- Create: `Controllers/CoursesController.cs`
- Create: `Controllers/EnrollmentsController.cs`
- Create: `Controllers/AccountController.cs`
- Create: `ViewModels/...`
- Create: `Views/Courses/...`
- Create: `Views/Enrollments/...`
- Create: `Views/Account/...`
- Modify: `Controllers/HomeController.cs`
- Modify: `Views/Home/Index.cshtml`
- Modify: `Views/Shared/_Layout.cshtml`

- [ ] Add public course listing and reuse it for `Home/Index`.
- [ ] Implement register/login/logout with default `Student` role assignment and Google challenge/callback wiring.
- [ ] Implement enroll, unenroll, and My Courses with role guards.
- [ ] Update navigation and responsive views.

### Task 4: Admin CRUD, Migrations, And Verification

**Files:**
- Create: `Areas/Admin/Controllers/CoursesController.cs`
- Create: `Areas/Admin/Views/Courses/...`
- Create: `Migrations/...`
- Modify: `Program.cs`

- [ ] Add failing coverage where practical for admin/form validation helpers.
- [ ] Implement admin CRUD under `/admin/courses` with `[Authorize(Roles = "Admin")]`.
- [ ] Generate EF Core migration.
- [ ] Run tests, `dotnet build`, and `dotnet ef database update` against Docker SQL Server.
