using BeforeTheScholarship.Web;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;

services.AddAuthorizationCore();
services.AddBlazoredLocalStorage();
services.AddMudServices();

services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
services.AddScoped<IConfigurationService, ConfigurationService>();
services.RegisterClientServices();

await builder.Build().RunAsync();
