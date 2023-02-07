﻿using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("LuckySpinDbMac");

/* Install Services using the builder.Services methods
 */

//Enable MVC and DIJ Services for this application
builder.Services.AddMvc();
builder.Services.AddTransient<LuckySpin.Services.TextTransform>();
builder.Services.AddDbContext<LuckySpin.Models.LuckySpinDbc>(options => options.UseSqlite(connection));
//builder.Services.AddSingleton<LuckySpin.Services.Repository>(); //TODO: remove this line
//TODO: Initialize the DatabaseContext for DIJ using a Connection String as shown in the slides



var app = builder.Build();


/* Middleware in the HTTP Request Pipeline
 */
app.UseStaticFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Spinner/Error");
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id}",
    defaults: new
    {
        controller = "Spinner",
        action = "Index",
        id = 0
    });

app.Run();