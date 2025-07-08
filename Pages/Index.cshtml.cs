using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace LiturgieBord.Pages;

public class IndexModel : PageModel
{
    public string InitialDataJson { get; set; } = "[]";
    private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "sections.json");

    public async Task OnGetAsync()
    {
        if (System.IO.File.Exists(filePath))
        {
            InitialDataJson = await System.IO.File.ReadAllTextAsync(filePath);
        }
    }

    public async Task<IActionResult> OnPostSaveAsync([FromBody] JsonElement content)
    {
        var sections = content.GetProperty("sections").ToString();
        await System.IO.File.WriteAllTextAsync(filePath, sections);

        var generatedHtml = content.GetProperty("generated_html");
        return await GenerateHtmlAsync(generatedHtml);
    }

    private static async Task<IActionResult> GenerateHtmlAsync(JsonElement generatedHtml)
    {
        string liturgieFilePath = Path.Combine(Environment.CurrentDirectory, "wwwroot/Content", "liturgie_bord_template.html");

        if (!System.IO.File.Exists(liturgieFilePath))
        {
            return new JsonResult(new { success = false, message = "Liturgie template file not found." });
        }

        string fileContent = await System.IO.File.ReadAllTextAsync(liturgieFilePath);
        string updatedContent = fileContent.Replace("{BORD}", generatedHtml.ToString());

        string outputFilePath = Path.Combine(Path.GetDirectoryName(liturgieFilePath)!, "liturgie_bord_generated.html");
        await System.IO.File.WriteAllTextAsync(outputFilePath, updatedContent);

        return new JsonResult(new { success = true });

    }
}
