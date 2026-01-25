using Master.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Master
{
    public class Startup
    {
        private const string SwaggerCustomStyles = @"
    <link rel='icon' type='image/x-icon' href='/favicon.ico' />
    <link rel='shortcut icon' type='image/x-icon' href='/favicon.ico' />
    <link rel='apple-touch-icon' sizes='180x180' href='/favicon.ico' />
    <style>
        /* Força tema escuro permanente com gradiente */
        body {
            background: linear-gradient(30deg, #0a0a0a 0%, #2a2a2a 50%, #1a1a1a 100%) !important;
            background-attachment: fixed !important;
        }
        
        .swagger-ui {
            background: transparent !important;
        }
        
        /* Hide entire topbar */
        .swagger-ui .topbar {
            display: none !important; 
        }
        
        /* Hide JSON download link */
        .swagger-ui .info .link {
            display: none !important;
        }
        
        .swagger-ui .opblock-tag {
            color: #fff !important;
            border-bottom: 1px solid #3b3b3b !important;
            margin-bottom: 20px !important;
        }
        
        .swagger-ui .opblock-tag-section {
            margin-bottom: 40px !important;
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
            margin-bottom: 60px;
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
        
        /* Fix section headers (Parameters, Request body, Responses) */
        .swagger-ui .opblock-section-header {
            background: transparent !important;
        }
        
        .swagger-ui .opblock-section-header h4,
        .swagger-ui .opblock-section-header label {
            color: #e6e6e6 !important;
        }
        
        .swagger-ui .tab li {
            color: #e6e6e6 !important;
        }
        
        /* Dark modal for authorization */
        .swagger-ui .dialog-ux {
            background: #2b2b2b !important;
            border: 2px solid #ff8c00 !important;
            color: #e6e6e6 !important;
            box-shadow: 0 0 20px rgba(255, 140, 0, 0.5) !important;
        }
        
        .swagger-ui .dialog-ux .modal-ux-header {
            background: #1f1f1f !important;
            border-bottom: 1px solid #ff8c00 !important;
        }
        
        .swagger-ui .dialog-ux .modal-ux-header h3 {
            color: #ffffff !important;
        }
        
        .swagger-ui .dialog-ux .modal-ux-content {
            background: #2b2b2b !important;
            color: #e6e6e6 !important;
        }
        
        .swagger-ui .dialog-ux .modal-ux-content h4,
        .swagger-ui .dialog-ux .modal-ux-content p,
        .swagger-ui .dialog-ux .modal-ux-content code {
            color: #e6e6e6 !important;
        }
        
        .swagger-ui .auth-container {
            background: #2b2b2b !important;
        }
        
        .swagger-ui .auth-container input[type=text],
        .swagger-ui .auth-container input[type=password] {
            background: #1a1a1a !important;
            color: #e6e6e6 !important;
            border: 1px solid #3b3b3b !important;
            width: 570px !important;
        }
        
        /* Darken background 70% when modal is open - NO BORDER */
        .swagger-ui .modal-ux {
            background: rgba(0, 0, 0, 0.7) !important;
            border: none !important;
        }
        
        /* Logo next to title */
        .swagger-ui .info .title::before {
            content: '';
            display: inline-block;
            width: 88px;
            height: 90px;
            background-image: url('/logoWhiteSmall.png');
            background-size: contain;
            background-repeat: no-repeat;
            vertical-align: middle;
            margin-right: 15px;
        }
        
        .swagger-ui .opblock {
            background: rgba(50,50,50,0.4) !important;
            border: 1px solid #3b3b3b !important;
            backdrop-filter: blur(5px) !important;
            transition: all 0.3s ease !important;
            margin-top: 12px;
        }
        
        /* Fix white background on opblock-summary-control when clicked/focused */
        .swagger-ui .opblock-summary-control:focus,
        .swagger-ui .opblock-summary-control:active {
            outline: none !important;
            background: transparent !important;
        }
        
        .swagger-ui .opblock-summary {
            background: transparent !important;
        }
        
        /* Fix white background when opblock is open/expanded */
        .swagger-ui .opblock.is-open {
            background: rgba(50,50,50,0.4) !important;
        }
        
        .swagger-ui .opblock.is-open .opblock-summary {
            border-bottom: 1px solid #3b3b3b !important;
            background: transparent !important;
        }
        
        /* Hover effect - Orange */
        .swagger-ui .opblock:hover {
            background: rgba(255, 140, 0, 0.3) !important;
            transform: translateX(5px) !important;
            box-shadow: 0 4px 15px rgba(255, 140, 0, 0.3) !important;
        }
        
        .swagger-ui .opblock.opblock-post {
            background: rgba(73, 204, 144, 0.15) !important;
        }
        
        .swagger-ui .opblock.opblock-post:hover {
            background: rgba(255, 140, 0, 0.3) !important;
            border-color: #ff8c00 !important;
        }
        
        .swagger-ui .opblock.opblock-get {
            background: rgba(97, 175, 254, 0.15) !important;
        }
        
        .swagger-ui .opblock.opblock-get:hover {
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

            // Obter versão do assembly
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";

            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"NeoCore API -- Bureau Bancário -- {version}",
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
                c.DocumentTitle = "NeoCore-Bureau API";
                c.HeadContent = SwaggerCustomStyles;
            });

            // Servir arquivos estáticos (para o logo)
            app.UseStaticFiles();

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