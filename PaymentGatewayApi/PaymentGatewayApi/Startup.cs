using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using PaymentGatewayApi.Messaging.Send.Options.v1;
using PaymentGatewayApi.Messaging.Send.Sender.v1;
using PaymentGatewayApi.Domain;
using PaymentGatewayApi.Service.v1.Command;
using PaymentGatewayApi.Service.v1.Query;
using PaymentGatewayApi.Models.v1;
using PaymentGatewayApi.Validators.v1;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace PaymentGatewayApi
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
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            //OAuth configuration
            string authority = $"https://{Configuration["Auth0:Domain"]}/";
            string audience = Configuration["Auth0:Audience"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PaymentGatewayApi",
                    Description = "A simple gateway API to redirect a payment request to the internal backend",
                    Contact = new OpenApiContact
                    {
                        Name = "Vasco Soares",
                        Email = "vascosoares.online@gmail.com",
                        Url = new Uri("https://github.com/vascosoares/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IValidator<CreatePaymentModel>, CreatePaymentModelValidator>();

            services.AddSingleton<IPaymentCreateSender, PaymentCreateSender>();

            services.AddTransient<IRequestHandler<CreatePaymentCommand, Payment>, CreatePaymentCommandHandler>();
            services.AddTransient<IRequestHandler<GetPaymentByIdQuery, Payment>, GetPaymentByIdQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGatewayApi v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
