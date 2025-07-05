using Microsoft.Playwright;

namespace PlaywrightTesting.Exercises;

public class ThirdAssignment
{
    static async Task Main(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        /*
         * Exercise 1: Moving the slider from right to left
         * 1. Navigate to `https://jqueryui.com/resources/demos/slider/default.html`
         * 2. Move the slider to some point
         * 3. Perform an action to move the slider to the left
         */

         await page.GotoAsync("https://jqueryui.com/resources/demos/slider/default.html");

         var slider = page.Locator("#slider > span");
         var boundingBox = await slider.BoundingBoxAsync();

         var startX = boundingBox.X + boundingBox.Width / 2;
         var startY = boundingBox.Y + boundingBox.Height / 2;

         await page.Mouse.MoveAsync(startX + 500, startY);
         await page.Mouse.DownAsync();

         await Task.Delay(2000);

         await page.Mouse.MoveAsync(startX + 200, startY);
         await page.Mouse.DownAsync();

         await Task.Delay(2000);

        /*
         * Exercise 2: Dropping draggable elements outside the draggable box
         * 1. Navigate to `https://jqueryui.com/droppable//`
         * 2. Access frame context
         * 3. Click the draggable element to select it
         * 4. Release the click anywhere outside the droppable box
         */

        await page.GotoAsync("https://jqueryui.com/droppable//");

        var frame = page.Locator(".demo-frame").ContentFrame;
        var draggableElement = frame.Locator("#draggable");

        var draggableBoundingBox = await draggableElement.BoundingBoxAsync();

        await page.Mouse.MoveAsync(draggableBoundingBox.X + draggableBoundingBox.Width / 2,
            draggableBoundingBox.Y + draggableBoundingBox.Height / 2);

        await page.Mouse.DownAsync();

        await page.Mouse.MoveAsync(draggableBoundingBox.X + 320 + draggableBoundingBox.Width / 2,
            draggableBoundingBox.Y + 220 + draggableBoundingBox.Height / 2);

        await page.Mouse.UpAsync();

        await Task.Delay(2000);

        /*
         * Exercise 3: Right-click menu options navigation
         * 1. Navigate to `https://deluxe-menu.com/popup-mode-sample.html`
         * 2. Perform a right-click on the image
         * 3. Select the option Product Info > Installation > How To Setup
         */

        await page.GotoAsync("https://deluxe-menu.com/popup-mode-sample.html");

        var image = page.Locator(".contentTd img").Last;
        await image.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var productInfoOption = page.Locator("#dm2m1i1tdT");
        await productInfoOption.ClickAsync();

        var installationOption = page.Locator("#dm2m2i1tdT");
        await installationOption.ClickAsync();

        var howToSetupOption = page.Locator("#dm2m3i1tdT");
        await howToSetupOption.HoverAsync();

        await Task.Delay(2000);
    }
}