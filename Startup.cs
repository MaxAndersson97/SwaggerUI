using _NetCore.DTO;
using _NetCore.IdentityServer4.IdentityConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swagger.Services;

namespace _NetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryApiScopes(Scopes.GetApiScopes())
                .AddTestUsers(Users.Get())
                .AddDeveloperSigningCredential();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("*");
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });
            });

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = "inventoryApi";
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;
                });

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "_NetCore", Version = "v1" });
                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                      In = ParameterLocation.Header,
                      Name = "Authorization",
                      Type = SecuritySchemeType.Http,
                      BearerFormat = "oAuth",
                      Scheme = "bearer"
                  });
                  c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },System.Array.Empty<string>()
                    } });
              });
            services.AddScoped<UserService>();
            services.AddScoped<AdjustmentDetailsService>();
            services.AddScoped<AdjustmentService>();
            services.AddScoped<BrandService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ClientService>();
            services.AddScoped<CurrencieService>();
            services.AddScoped<ExpenseCategorieService>();
            services.AddScoped<ExpenseService>();
            services.AddScoped<MigrationService>();
            services.AddScoped<oAuthAccessCodeService>();
            services.AddScoped<oAuthAccessTokenService>();
            services.AddScoped<oAuthClientService>();
            services.AddScoped<oAuthPersonalAccessClientService>();
            services.AddScoped<oAuthRefreshTokenService>();
            services.AddScoped<PasswordResetService>();
            services.AddScoped<PaymentPurchaseReturnService>();
            services.AddScoped<PaymentPurchaseService>();
            services.AddScoped<PaymentSaleReturnService>();
            services.AddScoped<PaymentSaleService>();
            services.AddScoped<PaymentWithCreditCardService>();
            services.AddScoped<PermissionRoleService>();
            services.AddScoped<PermissionService>();
            services.AddScoped<PermissionRoleService>();
            services.AddScoped<PosSettingService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductVariantService>();
            services.AddScoped<ProductWareHouseService>();
            services.AddScoped<ProvidersService>();
            services.AddScoped<PurchaseDetailService>();
            services.AddScoped<PurchaseReturnDetailService>();
            services.AddScoped<PurchaseReturnService>();
            services.AddScoped<PurchaseService>();
            services.AddScoped<QuotationDetailService>();
            services.AddScoped<QuotationService>();
            services.AddScoped<RelationalSchemaService>();
            services.AddScoped<RoleService>();
            services.AddScoped<RoleUserService>();
            services.AddScoped<SaleDetailService>();
            services.AddScoped<SaleReturnDetailService>();
            services.AddScoped<SaleReturnService>();
            services.AddScoped<SaleService>();
            services.AddScoped<SettingService>();
            services.AddScoped<ServerService>();
            services.AddScoped<TransferDetailService>();
            services.AddScoped<TransferService>();
            services.AddScoped<UnitService>();
            services.AddScoped<WareHouseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_NetCore v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("_myAllowSpecificOrigins");
            app.UseAuthorization();

            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
