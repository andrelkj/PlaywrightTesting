using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class TabsAndPopups
{
    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync(
            "https://sso.teachable.com/secure/673/identity/sign_up/otp?wizard_id=Ik1KtMfP5uT_IQBqSjquhZ1lUm_yjVRZF9_M-F5M7ifxu7qIMCE0QwunSTSLbk7opOIzBZb2Lei1vm-Fkj4jDg");

        // Defining pop handler
        var popupTask = page.WaitForPopupAsync();

        // Actions
        var firstPrivacyLink = page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Privacy" }).First;
        await firstPrivacyLink.ClickAsync(new LocatorClickOptions
            { Modifiers = new[] { KeyboardModifier.ControlOrMeta } });

        var popup = await popupTask;
        await popup.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = " Sign Up " }).ClickAsync();

        await Task.Delay(2000);

        // Close the browser
        await page.CloseAsync();
    }
}