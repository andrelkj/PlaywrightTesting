using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

/*
 * It is possible to use AI to generate dynamic locators that you can use during the test run based on different conditions:
 * 1. Description - based on the example below, all elements that might have descriptions as labels or text are identified and compared against the expected value
 * 2. Image - it is possible to identify elements based on screenshots using plugins as OpenCvSharp, it will then do a image comparison for the closest possible element
 *
 * It is even possible to integrate AI so it can perform prompts based on API calls and return responses (e.g. return xpath values based on given text values)
 */

internal static class AIElementFinder
{
    public static async Task<IElementHandle> FindByDescriptionAsync(IPage page, string description)
    {
        var elements =
            await page.QuerySelectorAllAsync("a, button, input, [aria-label], [role='button']");

        foreach (var element in elements)
            try
            {
                var innerText = await element.InnerTextAsync();
                var ariaLabel = await element.GetAttributeAsync("aria-label");
                var altText = await element.GetAttributeAsync("alt");

                if ((!string.IsNullOrEmpty(innerText) &&
                     innerText.Contains(description, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(ariaLabel) &&
                     ariaLabel.Contains(description, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(altText) &&
                     altText.Contains(description, StringComparison.OrdinalIgnoreCase)))
                    return element;
            }
            catch (PlaywrightException)
            {
            }

        throw new Exception($"Element with description '{description}' was not found.");
    }
}