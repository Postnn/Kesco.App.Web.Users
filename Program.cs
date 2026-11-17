using Clayzor.Lib.DALC;
using Clayzor.Lib.Entities.MedicalTests;
using Clayzor.Lib.Web.Controls;
using Clayzor.Lib.Web.Controls.Services;
using Clayzor.Lib.Web.Settings;
using Kesco.App.Web.Users.Components;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MudBlazor.Extensions;
using MudBlazor.Extensions.Options;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddWebConfig();
var claySettings = builder.Configuration.BindClaySettings();
builder.Services.AddSingleton(claySettings);
DapperColumnMapper.Initialize();

builder.Services.AddScoped<ClayErrorService>();
builder.Services.AddScoped<ISqlErrorHandler>(sp => sp.GetRequiredService<ClayErrorService>());
builder.Services.AddScoped<DbManager>(sp => new DbManager(claySettings.ConnectionString, sp.GetRequiredService<ISqlErrorHandler>()));
builder.Services.AddScoped<ClayDbReconnectService>();
builder.Services.AddScoped<ClayFormWindowService>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddMudExtensions(cfg => cfg.WithDefaultDialogOptions(d => d.DragMode = MudDialogDragMode.Simple));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
