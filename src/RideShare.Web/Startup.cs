using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Applyze.Exceptions.AspNetCore;
using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation.AspNetCore;
using MarkdownDocumenting.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RideShare.Web.Helpers.Documentation;
using RideShare.Web.Helpers.Extensions;
using RideShare.Web.Models;

namespace RideShare.Web
{
    public class Startup
    {
        const string CorsPolicyName = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            #region MVC and Json Options & Validation Register
            services.AddControllers();
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                    JsonConvert.DefaultSettings = () => opts.SerializerSettings;
                })
                .AddDataAnnotationsLocalization()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            #endregion

            #region Model State Filter
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region DbContext Options
            services.AddScoped<Settings>(sp =>
            {
                Settings settings = new Settings()
                {
                    ConnectionString = Configuration.GetConnectionString("Host"),
                    Database = Configuration.GetValue<string>("ConnectionStrings:Database")
                };
                return settings;
            });
            #endregion

            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            #endregion

            #region Api Versioning
            // This is for versionning API with route like: /v1/values - /v2/values etc.
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer();
            #endregion

            #region Documentation
            services.AddDocumentation();
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{this.GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? "Ride Share API - Deprecated" : "Ride Share API",
                        License = new OpenApiLicense
                        {
                            Name = "Apache 2.0",
                            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html"),
                        },
                    });
                }
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();

                //c.ExampleFilters();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Jwt Berarer token from Ride Share Identity Service"
                });

                c.DocumentFilter<LowercaseDocumentFilter>();
                c.OperationFilter<AuthorizationOperationFilter>();
                c.OperationFilter<DefaultValuesOperationFilter>();

                var docFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, docFile);

                if (File.Exists((filePath)))
                {
                    c.IncludeXmlComments(filePath);
                }
            });
            services.AddMvcCore()
            .AddApiExplorer();
            #endregion

            #region Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                Configuration.Bind(nameof(JwtBearerOptions), options);
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecurityKey")));
            });
            #endregion

            #region Object Mapping
            services.AddAutoMapper(GetType().Assembly);
            #endregion

            #region WebService Standards

            // This adds IHttpContextAccessor to DI Container. And it provides to reach current HttpContext.
            services.AddHttpContextAccessor();

            // This adds IMemoryCache to DI Container and it provides to managing api for Memroy Cache.
            services.AddMemoryCache();

            // This allows usage of [ResponseCache] over actions.
            services.AddResponseCaching();
            #endregion

            #region RateLimitation
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion

            services.AddRepositories();
            services.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IServiceProvider serviceProvider, IApiVersionDescriptionProvider versionningProvider)
        {
            app.UseApplyzeExceptionHandler(logger);

            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRequestLocalization(serviceProvider.GetService<IOptions<RequestLocalizationOptions>>()?.Value ?? throw new Exception("RequestLocalizationOptions couldn't be resolved from services."));

            app.UseDocumentation(opts => this.Configuration.Bind("DocumentationOptions", opts));

            app.UseSwagger();

            app.UseReDoc(options =>
            {
                options.SpecUrl = "/swagger/1.0/swagger.json";
                options.RoutePrefix = "api-docs-redoc";
            });

            app.UseSwaggerUI(options =>
            {
                options.EnableDeepLinking();
                options.ShowExtensions();
                options.DisplayRequestDuration();
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.RoutePrefix = "api-docs";

                foreach (var description in versionningProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
