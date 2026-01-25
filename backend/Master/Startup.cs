using Master.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Master
{
    public class Startup
    {
        private const string SwaggerCustomStyles = @"
            <style>
                /* Força tema escuro permanente com gradiente */
                body {
                    background: linear-gradient(30deg, #0a0a0a 0%, #2a2a2a 50%, #1a1a1a 100%) !important;
                    background-attachment: fixed !important;
                }
                
                .swagger-ui {
                    background: transparent !important;
                }
                
                .swagger-ui .topbar {
                    background: linear-gradient(30deg, #1f1f1f 0%, #3a3a3a 100%) !important;
                    box-shadow: 0 2px 10px rgba(0,0,0,0.5) !important;
                }
                
                .swagger-ui .opblock-tag {
                    color: #fff !important;
                    border-bottom: 1px solid #3b3b3b !important;
                    margin-bottom: 20px !important;
                }
                
                .swagger-ui .opblock-tag-section {
                    margin-bottom: 20px !important;
                }
                
                .swagger-ui .opblock .opblock-summary-description,
                .swagger-ui .opblock .opblock-summary-operation-id,
                .swagger-ui .opblock .opblock-summary-path,
                .swagger-ui .opblock .opblock-summary-path__deprecated {
                    color: #e6e6e6 !important;
                }
                
                .swagger-ui .scheme-container {
                    background: rgba(43, 43, 43, 0.8) !important;
                    backdrop-filter: blur(10px) !important;
                }
                
                .swagger-ui .info .title,
                .swagger-ui .info p,
                .swagger-ui .info li,
                .swagger-ui label,
                .swagger-ui .parameter__name,
                .swagger-ui .parameter__type,
                .swagger-ui .response-col_status {
                    color: #e6e6e6 !important;
                }
                
                .swagger-ui .opblock {
                    background: rgba(50,50,50,0.4) !important;
                    border: 1px solid #3b3b3b !important;
                    backdrop-filter: blur(5px) !important;
                    transition: all 0.3s ease !important;
                    margin-top: 12px !important;
                }
                
                /* Hover effect - Orange */
                .swagger-ui .opblock:hover {
                    background: rgba(255, 140, 0, 0.3) !important;
                    border-color: #ff8c00 !important;
                    transform: translateX(5px) !important;
                    box-shadow: 0 4px 15px rgba(255, 140, 0, 0.3) !important;
                }
                
                .swagger-ui .opblock.opblock-post {
                    background: rgba(73, 204, 144, 0.15) !important;
                    border-color: #49cc90 !important;
                }
                
                .swagger-ui .opblock.opblock-post:hover {
                    background: rgba(255, 140, 0, 0.3) !important;
                    border-color: #ff8c00 !important;
                }
                
                .swagger-ui .opblock.opblock-get {
                    background: rgba(97, 175, 254, 0.15) !important;
                    border-color: #61affe !important;
                }
                
                .swagger-ui .opblock.opblock-get:hover {
                    background: rgba(255, 140, 0, 0.3) !important;
                    border-color: #ff8c00 !important;
                }
                
                .swagger-ui .opblock.opblock-put {
                    background: rgba(252, 161, 48, 0.15) !important;
                    border-color: #fca130 !important;
                }
                
                .swagger-ui .opblock.opblock-put:hover {
                    background: rgba(255, 140, 0, 0.3) !important;
                    border-color: #ff8c00 !important;
                }
                
                .swagger-ui .opblock.opblock-delete {
                    background: rgba(249, 62, 62, 0.15) !important;
                    border-color: #f93e3e !important;
                }
                
                .swagger-ui .opblock.opblock-delete:hover {
                    background: rgba(255, 140, 0, 0.3) !important;
                    border-color: #ff8c00 !important;
                }
                
                .swagger-ui .opblock-body,
                .swagger-ui .parameters,
                .swagger-ui .responses-inner {
                    background: rgba(43, 43, 43, 0.8) !important;
                }
                
                .swagger-ui input,
                .swagger-ui textarea,
                .swagger-ui select {
                    background: rgba(26, 26, 26, 0.9) !important;
                    color: #e6e6e6 !important;
                    border: 1px solid #3b3b3b !important;
                }
                
                .swagger-ui .btn {
                    background: #3b3b3b !important;
                    color: #e6e6e6 !important;
                    border-color: #5b5b5b !important;
                }
                
                .swagger-ui .btn.authorize {
                    border-color: #49cc90 !important;
                    color: #49cc90 !important;
                }
                
                .swagger-ui .btn.execute {
                    background: #4990e2 !important;
                    border-color: #4990e2 !important;
                    color: #fff !important;
                }
                
                .swagger-ui table thead tr td,
                .swagger-ui table thead tr th {
                    color: #e6e6e6 !important;
                    border-bottom: 1px solid #3b3b3b !important;
                }
                
                .swagger-ui .model-box,
                .swagger-ui .model {
                    background: rgba(43, 43, 43, 0.8) !important;
                }
                
                .swagger-ui .model-title {
                    color: #e6e6e6 !important;
                }
                
                .swagger-ui section.models {
                    border: 1px solid #3b3b3b !important;
                }
                
                /* Fix SVG icons - arrows, locks, etc */
                .swagger-ui svg,
                .swagger-ui svg path,
                .swagger-ui svg use {
                    fill: #e6e6e6 !important;
                }
                
                .swagger-ui .arrow svg,
                .swagger-ui .arrow svg path {
                    fill: #e6e6e6 !important;
                }
                
                .swagger-ui button svg {
                    fill: currentColor !important;
                }

                /* CSS customizado original para espaço no final */
                .swagger-ui .info {
                    margin: 35px 0 0 0 !important;
                }
                .swagger-ui .block.col-12.block-desktop.col-12-desktop {
                    padding-bottom: 100px !important;
                }
            </style>
        ";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();

            services.Configure<LocalNetwork>(Configuration.GetSection("localNetwork"));

            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "NeoCore API -- Bureau Bancário -- 01.01.03",
                    Version = "v1",
                    Description = "Endpoints para serviços bancários de crédito"
                });

                // Configuração para autenticação JWT no Swagger
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            // Configuração da autenticação JWT
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Em produção, mude para true
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = LocalNetwork.Issuer,
                    ValidateAudience = true,
                    ValidAudience = LocalNetwork.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {

                        return System.Threading.Tasks.Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {

                        return System.Threading.Tasks.Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {

                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Habilitar Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Master API V1");
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
                c.HeadContent = SwaggerCustomStyles;
            });

            app.UseRouting();

            // CORS deve vir antes de Authentication/Authorization
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // A ordem é importante: Authentication antes de Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
