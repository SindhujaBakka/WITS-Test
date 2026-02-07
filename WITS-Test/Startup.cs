using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Contracts;
using Persistance.Implementations;
using Services.Contracts;
using Services.Implementations;
using Services.Profiles;
using WITS_Test.Middlewares;

namespace WITS_Test
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //DataContext
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IAccountUsersRepository, AccountUsersRepository>();
            services.AddScoped<IAccountUsersService, AccountUsersService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
