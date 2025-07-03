using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class DragAndDrop
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://jqueryui.com/droppable/");

        // Actions
        var frame = page.Locator(".demo-frame").ContentFrame;
        ;
        var draggable = frame.Locator("#draggable");
        var droppable = frame.Locator("#droppable");

        // Get bounding boxes
        var draggableBoundingBox = await draggable.BoundingBoxAsync();
        var droppableBoundingBox = await droppable.BoundingBoxAsync();

        /*
         // Using a built-in playwright method to drag and drop
         await draggable
            .DragToAsync(droppable); // drop the draggable element right in the middle of the droppable element
            */

        // Move the cursor to the center of the draggable element
        await page.Mouse.MoveAsync(draggableBoundingBox.X + draggableBoundingBox.Width / 2,
            draggableBoundingBox.Y + draggableBoundingBox.Height / 2);

        // Press mouse down key
        await page.Mouse.DownAsync();

        // Move the cursor to the center of the droppable element
        await page.Mouse.MoveAsync(droppableBoundingBox.X + droppableBoundingBox.Width / 2,
            droppableBoundingBox.Y + droppableBoundingBox.Height / 2);

        // Release mouse button
        await page.Mouse.UpAsync();

        await Task.Delay(2000);

        // Close the browser
        await browser.CloseAsync();
    }
}