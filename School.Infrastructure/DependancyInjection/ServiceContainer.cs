using System.Security.Claims;
using System.Text;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using School.Application.Services.Interfaces;
using School.Domain.Entities.Identity;
using School.Domain.Interfaces;
using School.Domain.Interfaces.AssignmentSpecifies;
using School.Domain.Interfaces.AttendanceSpecifies;
using School.Domain.Interfaces.Authentication;
using School.Domain.Interfaces.CourseSpecifies;
using School.Domain.Interfaces.DepartmentSpecifies;
using School.Domain.Interfaces.StudentClassSpecifies;
using School.Domain.Interfaces.SubmissionSpecifies;
using School.Infrastructure.Data;
using School.Infrastructure.Repositories;
using School.Infrastructure.Repositories.AssignmentSpecifies;
using School.Infrastructure.Repositories.AttendanceSpecifies;
using School.Infrastructure.Repositories.Authentication;
using School.Infrastructure.Repositories.CourseSpecifies;
using School.Infrastructure.Repositories.DepartmentSpecifies;
using School.Infrastructure.Repositories.StudentClassSpecifies;
using School.Infrastructure.Repositories.SubmissionSpecifies;
using School.Infrastructure.Services;

namespace School.Infrastructure.DependancyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(config.GetConnectionString("DefaultConnection"),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();
            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);


            services.AddScoped<IRoleManagement, RoleManagement>();
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<ITokenManagement, TokenManagement>();
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
            services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));
            services.AddScoped<IClass, ClassRepository>();
            services.AddScoped<ICourse, CourseRepository>();
            services.AddScoped<IDepartment, DepartmentRepository>();
            services.AddScoped<IAttendance, AttendanceRepository>();
            services.AddScoped<IAssignment, AssignmentRepository>();
            services.AddScoped<ISubmission, SubmissionRepository>();
            services.AddScoped<IStudentClass, StudentClassRepository>();


            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddRoles<IdentityRole<Guid>>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["JWT:Key"]!)
                        ),
                        RoleClaimType = ClaimTypes.Role
                    };
                });




            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("TeacherOnly", policy => policy.RequireRole("Teacher"));
                options.AddPolicy("StudentOnly", policy => policy.RequireRole("Student"));
            });

            return services;
        }


    }
}
