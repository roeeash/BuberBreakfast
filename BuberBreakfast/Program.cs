using BuberBreakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    //define how to create the IBreakfast interface
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}