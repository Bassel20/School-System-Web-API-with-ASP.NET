# School System Web API

A small ASP.NET Core 5.0 Web API for managing school data (students, courses,
academic years, and grades), built with Entity Framework Core and SQLite.

## What it does

- Exposes a REST API at `api/School` with full CRUD for **Students**:
  - `GET api/School` – list all students
  - `GET api/School/{id}` – get a single student
  - `POST api/School` – create a student
  - `PUT api/School` – update a student
  - `DELETE api/School/{id}` – delete a student
- Persists data via EF Core (`SchoolDbContext`) to a SQLite database.
- Uses the repository pattern (`iStudentRepository` / `StudentRepository`) to
  separate data access from the controller.
- Ships with Swagger/OpenAPI UI in development (`/swagger`).

## Project structure

```
WebApplication2/
├── Controllers/
│   └── SchoolController.cs   # Student CRUD endpoints
├── Models/
│   ├── Student.cs             # Student, AcademicYear, Course, Grade entities
│   └── SchoolDbContext.cs     # EF Core DbContext
├── Repositories/
│   ├── iStudentRepository.cs  # Repository interface
│   └── StudentRepository.cs   # EF Core implementation
├── Program.cs
└── Startup.cs
```

## Data model

- **Student**: has an Id, a name, and a many-to-many relationship with Courses.
- **Course**: has an Id, a name, and a many-to-many relationship with Students.
- **AcademicYear**: has an Id, a name, and a collection of Students.
- **Grade**: has an Id, a value, and a collection of Students.

Only `Student` is currently wired up to the database and exposed through the API.

## Running the project

```bash
cd WebApplication2
dotnet run
```

Then open `https://localhost:5001/swagger` (or the URL shown in the console)
to explore the API.

## What's currently missing / known issues

- **No SQLite connection string**: `Startup.ConfigureServices` calls
  `o.UseSqlite()` with no connection string, so the app will throw at
  startup when it tries to create the database. A connection string needs to
  be added to `appsettings.json` and passed to `UseSqlite(...)`.
- **`SchoolDbContext` has incorrect `DbSet` types**: `Courses` and `Grades`
  are both declared as `DbSet<Student>` instead of `DbSet<Course>` and
  `DbSet<Grade>`, so those entities aren't actually queryable/persisted.
- **No relationships configured**: the many-to-many (Student↔Course) and
  one-to-many (AcademicYear↔Student, Grade↔Student) relationships exist on
  the model classes but aren't configured via Fluent API or migrations.
- **No EF Core migrations**: the database is created with
  `Database.EnsureCreated()`, which doesn't support evolving the schema over
  time.
- **`PUT` route is missing the `{id}` route parameter**: `PutStudents` reads
  `id` from the route, but the action is mapped as `[HttpPut]` with no
  `"{id}"` segment, so the route won't match as written.
- **Only Students have an API**: there are no controllers/repositories for
  Courses, AcademicYears, or Grades, even though the models exist.
- **No DTOs/validation**: controllers return EF entities directly, which can
  cause serialization issues with circular navigation properties (e.g.
  Student ↔ Course) and exposes the full entity shape to clients.
- **No authentication or authorization**.
- **No automated tests**.
- **`.NET 5` is end-of-life** (out of support since May 2022) — consider
  upgrading to a current LTS (e.g. .NET 8).
- **Build artifacts are committed to git**: `bin/`, `obj/`, `.vs/`, and
  `*.csproj.user` are tracked; a `.gitignore` should be added and these
  removed from source control.
