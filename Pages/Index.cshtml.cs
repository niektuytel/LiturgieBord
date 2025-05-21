using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;
using System.Threading.Tasks;
using System;

namespace LiturgieBord.Pages
{
    public class IndexModel : PageModel
    {
        // JSON injected into the page.
        public string InitialDataJson { get; set; } = "[]";
        // Local file for persistence.
        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "sections.json");

        public async Task OnGetAsync()
        {
            if (System.IO.File.Exists(filePath))
            {
                InitialDataJson = await System.IO.File.ReadAllTextAsync(filePath);
            }
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostSaveAsync([FromBody] string data)
        {
            try
            {
                //var json = data.GetRawText();
                //await System.IO.File.WriteAllTextAsync(filePath, json);
                //return new JsonResult(new { success = true });
            }
            catch
            {
            }
                return new JsonResult(new { success = false });
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostGenerateHtmlAsync([FromBody] string data)
        {
            //if (data.TryGetProperty("generatedHtml", out var htmlElem)
            //    && htmlElem.ValueKind == JsonValueKind.String)
            //{
            //    string generatedHtml = htmlElem.GetString()!;

            //    // Compute Content folder path
            //    string basePath = AppContext.BaseDirectory;
            //    string liturgieFilePath = Path.GetFullPath(
            //        Path.Combine(basePath, "..", "..", "..", "Content", "liturgie_bord.html"));
            //    string outputFilePath = Path.Combine(
            //        Path.GetDirectoryName(liturgieFilePath)!,
            //        "liturgie_bord_generated.html");

            //    if (System.IO.File.Exists(liturgieFilePath))
            //    {
            //        string fileContent = await System.IO.File.ReadAllTextAsync(liturgieFilePath);
            //        string updatedContent = fileContent.Replace("<p>{BORD}</p>", generatedHtml);
            //        await System.IO.File.WriteAllTextAsync(outputFilePath, updatedContent);
            //    }

            //    return new JsonResult(new { success = true });
            //}

            return new JsonResult(new { success = false });
        }
    }
}