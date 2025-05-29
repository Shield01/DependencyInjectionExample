using Autofac;
using Autofac.Extensions.DependencyInjection;
using DIExampleServiceContracts;
using DIExampleServices;

var builder = WebApplication.CreateBuilder(args);

//Demonstrating the use of Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();

//Demonstraing the use of shorthand AddTransient, AddScoped, AddSingleton

//builder.Services.AddTransient<ICitiesService, CitiesService>();
//builder.Services.AddScoped<ICitiesService, CitiesService>();
//builder.Services.AddSingleton<ICitiesService, CitiesService>();

// Continuation of Autofac demonstration
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency(); // Same as AddTransient<>
    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope(); // Same as AddScoped<>
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance(); // Same as AddSingleton<>

});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
