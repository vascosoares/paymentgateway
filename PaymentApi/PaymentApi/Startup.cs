using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.OpenApi.Models;
using PaymentApi.Data.Database;
using PaymentApi.Data.Repository.v1;
using PaymentApi.Domain;
//using PaymentApi.Messaging.Receive.Options.v1;
//using PaymentApi.Messaging.Receive.Receiver.v1;
using PaymentApi.Models.v1;
using PaymentApi.Service.v1.Command;
using PaymentApi.Service.v1.Query;
//using PaymentApi.Service.v1.Services;
//using PaymentApi.Validators.v1;

namespace PaymentApi
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

            // (TODO VS) Add configuration for Messaging Queues

            services.AddDbContext<PaymentContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version = "v1",
                    Title = "PaymentApi",
                    Description = "A simple API to create a payment request to an external provider",
                    Contact = new OpenApiContact
                    {
                        Name = "Vasco Soares",
                        Email = "vascosoares.online@gmail.com",
                        Url = new Uri("https://github.com/vascosoares/")
                    }
                });

                /*var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);*/
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

            //services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICustomerNameUpdateService).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            //services.AddTransient<IValidator<PaymentModel>, PaymentModelValidator>();

            services.AddTransient<IRequestHandler<GetPaymentByIdQuery, Payment>, GetPaymentByIdQueryHandler>();

            //services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

            /*if (serviceClientSettings.Enabled)
            {
                services.AddHostedService<CustomerFullNameUpdateReceiver>();
            }*/
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentApi v1");
                //c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
