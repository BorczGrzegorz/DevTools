using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevTools.Application;
using Extensions.Binders;
using Extensions.Infrastructure.Models;
using DevTools.Infrastructure.Models;
using DevTools.Application.Abstract;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.OpenApi.Models;
using DevTools.Converters;
using DevTools.Configuration;
using DevTools.DataAccess;
using DevTools.JiraApi;
using System.Net.Http;
using DevTools.JiraApi.Abstract;
using DevTools.JiraApi.Mock;
using Newtonsoft.Json.Converters;
using DevTools.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using DevTools.Application.Models.Dto;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using DevTools.Exceptions;

namespace DevTools
{
    public class Startup
    {
        private IServiceCollection _services;
        private IWebHostEnvironment _env;
        private readonly Settings _settings;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _settings = configuration.Get<Settings>();
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddCors();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.ModelBinderProviders.Insert(0, new IdBinderProvider());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.AddConverter(new MachineIdConverter());
                options.AddConverter(new ProductIdConverter());
                options.AddConverter(new ProjectIdConverter());
                options.AddConverter(new AddressIdConverter());
            });
            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dev Tools", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
                c.AddCustomIdMap<MachineId>();
                c.AddCustomIdMap<ProductId>();
                c.AddCustomIdMap<ProjectId>();
                c.AddCustomIdMap<AddressId>();
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "client-app/build";
            });

            RegisterServices(services);
            SetUp(services.BuildServiceProvider());
        }

        private void SetUp(ServiceProvider serviceProvider)
        {
            string config = File.ReadAllText("appsettings.json");
            JObject configToken = JsonConvert.DeserializeObject<JObject>(config);

            if (configToken["Data"] == null)
            {
                return;
            }

            var context = serviceProvider.GetRequiredService<MongoDataContext>();
            context.Database.Client.DropDatabase(_settings.Mongo.DatabaseName);

            foreach (JToken productToken in configToken["Data"]["Products"])
            {
                var productService = serviceProvider.GetRequiredService<IProductService>();
                var product = productService.Add(productToken["Name"].Value<string>());
                var machinesToken = productToken["Machines"];

                foreach (var item in machinesToken)
                {
                    var property = item.ToObject<JProperty>();
                    productService.AddMachines(product.Id, new[] { new NewMachineDto { Name = property.Name, Address = property.Value.Value<string>() } });
                }

                foreach (JToken projectToken in productToken["Projects"])
                {
                    var projects = productService.AddProjects(product.Id, new string[] { projectToken["Name"].Value<string>() });
                    var project = projects.Single();

                    foreach (JToken addressToken in projectToken["Addresses"])
                    {
                        productService.AddAddresses(project.Id, new[] { new NewAddressDto
                        {
                            IsSingleUrl = addressToken["IsSingleUrl"].Value<bool>(),
                            Name = addressToken["Name"].Value<string>(),
                            Path = addressToken["Path"].Value<string>()
                        } });
                    }
                }
            }

        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<RequestHandler>();
            string jiraEnviromentAddress = Environment.GetEnvironmentVariable("JIRA_ADDRESS", EnvironmentVariableTarget.Machine);
            string jiraAddress = jiraEnviromentAddress ?? _settings.JiraAddress;

            if (!string.IsNullOrWhiteSpace(jiraAddress))
            {
                services.AddHttpClient<IJiraWebClient, JiraWebClient>(client =>
                {
                    client.BaseAddress = new Uri(jiraAddress);
                })
                .AddHttpMessageHandler<RequestHandler>();
            }
            else
            {
                services.AddSingleton<IJiraWebClient, JiraMockWebClient>();
            }

            services.AddSingleton<IMachineLockService, MachineLockService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(p => new MongoDataContext(_settings.Mongo.ConnectionString, _settings.Mongo.DatabaseName));
            services.AddScoped<MongoProductRepository>();
            services.AddScoped<IProductRepository>(p => p.GetRequiredService<MongoProductRepository>());
            services.AddScoped<IProductQuery>(p => p.GetRequiredService<MongoProductRepository>());
            services.AddScoped<IAddressesQuery>(p => p.GetRequiredService<MongoProductRepository>());
            services.AddScoped<IProjectQuery>(p => p.GetRequiredService<MongoProductRepository>());
            services.AddScoped<IMachineQuery>(p => p.GetRequiredService<MongoProductRepository>());
            services.AddScoped<WorklogQuery>(x =>
            {
                // TODO add parameter validation
                var accessor = x.GetRequiredService<IHttpContextAccessor>();
                string boardId = null;
                StringValues boardHeader = accessor.HttpContext.Request.Headers["x-board"];
                if (boardHeader == StringValues.Empty
                    && accessor.HttpContext.Request.RouteValues.TryGetValue("boardId", out object value))
                {
                    boardId = (string)value;
                }
                else if (boardHeader.Count > 1)
                {
                    throw new ArgumentException("Only one boardId is allowed");
                }
                else
                {
                    boardId = boardHeader.Single();
                }

                if (string.IsNullOrWhiteSpace(boardId))
                {
                    throw new BoardIdNotFoundException();
                }

                return new WorklogQuery(x.GetRequiredService<IJiraWebClient>(), boardId);
            });
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IWorkLogQuery>(x => x.GetRequiredService<WorklogQuery>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.WithOrigins("*").AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dev Tools V1");
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
