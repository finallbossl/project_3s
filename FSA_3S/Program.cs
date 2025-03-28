using FSA_3S.Models;
using FSA_3S.Repositories;
using FSA_3S.Repositories.Repository;
using FSA_3S.Services.Service;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FSA_3S.Repositories.Interface;
using FSA_3S.Services;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


// --- 1. Cấu hình kết nối database ---
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- 2.1 Resign Service ---
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IPersonalInformationService, PersonalInformationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMappingUserAppointmentRepository, MappingUserAppointmentRepository>();

// --- 2.2 Resign Repository
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddScoped<IMappingUserAppointmentService, MappingUserAppointmentService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();


// --- 3. Cấu hình JWT Authentication ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var account = new Account(
        config["Cloudinary:CloudName"],
        config["Cloudinary:ApiKey"],
        config["Cloudinary:ApiSecret"]
    );
    return new Cloudinary(account);
});

// --- 4. Cấu hình Swagger và Controllers ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();


builder.Services.AddSwaggerGen(options =>
{
    // Thêm phần cấu hình cho Bearer Token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập 'Bearer' [space] và token vào đây. Ví dụ: Bearer abc123"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddControllers();

var app = builder.Build();

// --- 5. Chạy migration (nếu có) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// --- 6. Cấu hình pipeline HTTP ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseRouting();

app.UseAuthentication(); // ✅ Kích hoạt Authentication
app.UseAuthorization(); // ✅ Kích hoạt Authorization

app.MapControllers();

app.Run();
