using WebApplication3.Commands;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Db;
using WebApplication3.Mappers;
using WebApplication3.Mappers.CreateMapper;
using WebApplication3.Mappers.EditMapper;
using WebApplication3.Repositories;
using WebApplication3.Validators;

namespace WebApplication3
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<DbSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddTransient<DbConnection>();
            services.AddTransient(provider =>
                provider.GetRequiredService<DbConnection>().CreateConnection());

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<ICreateEmployeeCommand, CreateEmployeeCommand>();
            services.AddScoped<IEditEmployeeCommand, EditEmployeeCommand>();
            services.AddScoped<IGetEmployeesByCompanyCommand, GetEmployeesByCompanyCommand>();

            services.AddScoped<IDbCreateEmployeeMapper, DbCreateEmployeeMapper>();
            services.AddScoped<IDbEditEmployeeMapper, DbEditEmployeeMapper>();

            services.AddScoped<ICreateEmployeeValidator, CreateEmployeeRequestValidator>();

            services.AddScoped<IDtoEmployeeMapper, DtoEmployeeMapper>();
        }

        public void ConfigureServices(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}