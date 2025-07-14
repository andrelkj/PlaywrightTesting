using System.Text;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;

namespace PlaywrightTesting.testcases;

internal class GeminiAITest
{
    private static readonly string
        GEMINI_API_KEY = "AIzaSyBZBYjENds2IZdAypkBT20ASLTHZPyJWO8"; // Replace with your actual API Key

    public static async Task<string> GetAIEnhancedXPath(string elementDescription)
    {
        using (var client = new HttpClient())
        {
            var prompt = "Generate an optimized XPath for the following web element: " + elementDescription +
                         ". Provide only the raw XPath without any explanation or formatting.";

            var json = new JObject
            {
                ["contents"] = new JArray
                {
                    new JObject
                    {
                        ["parts"] = new JArray
                        {
                            new JObject { ["text"] = prompt }
                        }
                    }
                }
            };

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                $"https://generativelanguage.googleapis.com/v1/models/gemini-pro:generateContent?key={GEMINI_API_KEY}",
                content);

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Gemini API Response: " + responseData); // Debugging output

            var jsonResponse = JObject.Parse(responseData);
            if (!jsonResponse.ContainsKey("candidates"))
                throw new Exception("Invalid Gemini API response: 'candidates' key missing");

            // Extract the raw XPath
            var rawXPath = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString().Trim();

            // Clean up the response (remove ```xpath ... ``` formatting)
            if (rawXPath != null) rawXPath = rawXPath.Replace("```xpath", "").Replace("```", "").Trim().TrimEnd('.');

            return rawXPath;
        }
    }

    private static async Task Main(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();
        await page.SetViewportSizeAsync(1920, 1080);
        await page.GotoAsync("https://gmail.com");

        // AI generates an XPath
        var aiGeneratedXPath = await GetAIEnhancedXPath("Learn more about using Guest mode link");
        Console.WriteLine("AI Generated XPath: " + aiGeneratedXPath);

        await page.Locator("#identifierId").FillAsync("hi");
        await page.Locator(aiGeneratedXPath).ClickAsync();

        await Task.Delay(5000);
    }
}