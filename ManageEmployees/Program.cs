using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Mapper.Attendances;
using ManageEmployees.Mapper.Departments;
using ManageEmployees.Mapper.Employees;
using ManageEmployees.Mapper.LeaveRequest;
using ManageEmployees.Mapper.StatusLeaveRequests;
using ManageEmployees.Repositories;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services;
using ManageEmployees.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//connexion a la bd
builder.Services.AddDbContext<ManageEmployeeDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("ManageEmployeesdb")));

// Configuration d'AutoMapper
builder.Services.AddAutoMapper(typeof(UpdateDepartmentProfile), typeof(CreateDepartmentProfile), typeof(ReadDepartmentProfile));
builder.Services.AddAutoMapper(typeof(UpdateAttendanceProfile), typeof(CreateAttendanceProfile), typeof(ReadAttendanceProfile));
builder.Services.AddAutoMapper(typeof(CreateStatusLeaveRequestProfile) ,typeof(ReadStatusLeaveRequestProfile), typeof(UpdateStatusLeaveRequestProfile));
builder.Services.AddAutoMapper(typeof(CreateLeaveRequestProfile) ,typeof(ReadLeaveRequestProfile), typeof(UpdateLeaveRequestProfile));
builder.Services.AddAutoMapper(typeof(CreateEmployeeProfile) ,typeof(ReadEmployeeProfile), typeof(UpdateEmployeeProfile), typeof(CreateEmployeeWithDepartmentIDProfile));


// Ajout des repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IStatusLeaveRequestRepository, StatusLeaveRequestRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


// Ajout des services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IStatusLeaveRequestService, StatusLeaveRequestService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"AllowBlazorOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7169", "http://localhost:5222").AllowAnyHeader().AllowAnyMethod();
    });
});

//injection des services et repo



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorOrigin");
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
