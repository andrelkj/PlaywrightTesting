using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace PlaywrightTesting.Exercises;

public class SecondAssignment
{
    /*
     * Exercise 1: Checking pre-selected checkboxes
     * 1. Navigate to `http://www.tizag.com/htmlT/htmlcheckboxes.php`
     * 2. Look for the "HTML Pre-Selected Checkboxes" section
     * 3. Check all the checkboxes (already selected ones should not change)
     *
     * Exercise 2: Randomly check 2 checkboxes
     * 1. Navigate to `http://www.tizag.com/htmlT/htmlcheckboxes.php`
     * 2. Look for the "HTML Checkbox Form" section
     * 3. Build a method that randomly selects two of the available checkboxes
     *
     * Exercise 3: Printing the name of the available checkboxes
     * 1. Navigate to `http://www.tizag.com/htmlT/htmlcheckboxes.php`
     * 2. Look for the "HTML Checkbox Form" section
     * 3. Print the name of each checkbox field
     */

    /// Primary method: Extracts sport name using Regular Expression pattern matching
    /// This method analyzes the HTML structure to find the text label before each checkbox
    public static async Task<string> GetSportName(ILocator checkbox)
    {
        // Step 1: Get the checkbox's value attribute (e.g., "soccer", "football")
        var value = await checkbox.GetAttributeAsync("value");

        // Step 2: Navigate to the parent element that contains both the text and the checkbox
        var parent = checkbox.Locator("xpath=..");

        // Step 3: Get the complete HTML content (including tags) from the parent element
        // We use InnerHTML instead of InnerText because we need to see the HTML structure
        var fullHtml = await parent.InnerHTMLAsync();

        // Step 4: Create a Regular Expression pattern to find the sport name
        // Pattern breakdown:
        // - ([A-Za-z]+): Captures one or more letters (the sport name)
        // - :\s*: Matches a colon followed by optional whitespace
        // - <input[^>]*: Matches the opening of an input tag with any attributes
        // - value=[""']: Matches the start of a value attribute (with quote)
        // - [value]: The actual checkbox value we're looking for
        // - [""']: Matches the closing quote of the value attribute
        var pattern = @"([A-Za-z]+):\s*<input[^>]*value=[""']" + Regex.Escape(value) + @"[""']";

        // Step 5: Apply the pattern to the HTML content
        var match = Regex.Match(fullHtml, pattern);

        // Step 6: Return the captured sport name if found, otherwise use fallback
        if (match.Success)
        {
            return match.Groups[1].Value; // Return the first captured group (sport name)
        }

        // Fallback: If regex fails, capitalize the first letter of the value
        return char.ToUpper(value[0]) + value.Substring(1);
    }


    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("http://www.tizag.com/htmlT/htmlcheckboxes.php");

        // Exercise 1
        /*
         var preSelectedCheckboxes =
            page.Locator("h2.specialT:has-text('HTML Pre-Selected Checkboxes:') + div.display input[type='checkbox']");

        for (int i = 0; i < await preSelectedCheckboxes.CountAsync(); i++)
        {
            var checkbox = preSelectedCheckboxes.Nth(index: i);
            await checkbox.CheckAsync();

            Console.WriteLine($"Checkbox {i + 1} is checked: {await checkbox.IsCheckedAsync()}");
            await Task.Delay(1000);
        }

        Console.WriteLine("All checkboxes are checked.");
        */

        // Exercise 2
        /*
         * // Define variables
        var checkboxForm =
            page.Locator("h2.specialT:has-text('HTML Checkbox Form:') + div.display input[type='checkbox']");

        // Initialize randomizer and a hashset
        var random = new Random();
        var selectedIndexes = new HashSet<int>(); // store only unique values

        // Generate unique random indexes
        while (selectedIndexes.Count < 2)
        {
            var randomIndex = random.Next(0, await checkboxForm.CountAsync());
            selectedIndexes.Add(randomIndex); // add the random indexes generated to the hashset
        }

        // Randomly select two checkboxes
        foreach (var index in selectedIndexes) // loop over the hashset with unique randomized indexes
        {
            await checkboxForm.Nth(index).CheckAsync();

            Console.WriteLine($"Checkbox {index + 1} is checked: {await checkboxForm.Nth(index).IsCheckedAsync()}");
            await Task.Delay(1000);
        }

        Console.WriteLine("Two random checkboxes were checked.");
        */

        // Exercise 3
        // Identify all the checkbox elements from one specific container based on the sibling header text value
        var checkboxForm =
            page.Locator("h2.specialT:has-text('HTML Checkbox Form:') + div.display input[type='checkbox']");

        // Actions
        Console.WriteLine($"Total number of checkboxes found: {await checkboxForm.CountAsync()}");

        Console.WriteLine("---------------------------------------------------------------------");

        // Iterate over all the checkboxes
        for (int i = 0; i < await checkboxForm.CountAsync(); i++)
        {
            var checkbox = checkboxForm.Nth(i);
            var sportName = await GetSportName(checkbox);

            Console.WriteLine($"Sport: {sportName}");

            await Task.Delay(1000);
        }

        await Task.Delay(2000);

        // Close the browser
        await browser.CloseAsync();
    }
}