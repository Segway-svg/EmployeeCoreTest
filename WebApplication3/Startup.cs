using Microsoft.Data.SqlClient;
using WebApplication3.Commands;
using WebApplication3.Mappers;
using WebApplication3.Repositories;

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

            services.AddTransient(_ =>
                new SqlConnection(
                    Configuration.GetConnectionString("DefaultConnection") ??
                    "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
                )
            );

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICreateEmployeeCommand, CreateEmployeeCommand>();
            services.AddScoped<IDbEmployeeMapper, DbEmployeeMapper>();
            services.AddScoped<ICreateEmployeeCommand, CreateEmployeeCommand>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}