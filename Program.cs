using System.Diagnostics;

namespace LiturgieBord;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var url = builder.Configuration.GetValue<string>("LiturgieBord:Url");
        builder.WebHost.UseUrls(url);

        builder.Services.AddRazorPages();
        builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // when the app has started, fire off the browser pointing at your generated HTML
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStarted.Register(() =>
        {
            // give Kestrel a moment to start listening
            Task.Delay(500).ContinueWith(_ =>
            {
                // === Edge ===
                var psi = new ProcessStartInfo
                {
                    FileName = "msedge",
                    Arguments = $"--kiosk \"{url}/Content/liturgie_bord_generated.html\" --edge-kiosk-type=fullscreen --no-first-run",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();

        app.Run();
    }
}
