using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTesting.Testcases;

public class APITesting
{
    private static async Task Main(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();

        // initialize the request context
        var request = await playwright.APIRequest.NewContextAsync();

        // POST request
        Console.WriteLine("Post request: Creates a new user");
        var postResponse = await request.PostAsync("https://petstore.swagger.io/v2/user", new APIRequestContextOptions
        {
            DataObject = new
            {
                Id = 1,
                Username = "jackson",
                FirstName = "Jack",
                LastName = "Sparrow",
                Email = "test@email.com",
                Password = "123",
                Phone = "1234567890",
                UserStatus = 0
            }
        });
        Console.WriteLine($"Status code: {postResponse.Status}");
        Console.WriteLine($"Response body: {await postResponse.TextAsync()}");

        await Task.Delay(2000);

        Console.WriteLine("---------------------------------------------------------------");

        // PUT request
        Console.WriteLine("Put request: Updates a user by user name");
        var putResponse = await request.PutAsync("https://petstore.swagger.io/v2/user/jackson",
            new APIRequestContextOptions
            {
                DataObject = new
                {
                    Id = 1,
                    Username = "jackson",
                    FirstName = "Jack",
                    LastName = "Sparrow",
                    Email = "test@email.com",
                    Password = "123",
                    Phone = "1234567890",
                    UserStatus = 1 // updated user status
                }
            });
        Console.WriteLine($"Status code: {putResponse.Status}");
        Console.WriteLine($"Response body: {await putResponse.TextAsync()}");

        await Task.Delay(2000);

        Console.WriteLine("---------------------------------------------------------------");

        // GET request
        Console.WriteLine("Get request: Gets a user by user name");
        var getResponse = await request.GetAsync("https://petstore.swagger.io/v2/user/jackson");
        Console.WriteLine($"Status code: {getResponse.Status}");
        Console.WriteLine($"Response body: {await getResponse.TextAsync()}");

        var jsonResponse = await getResponse.JsonAsync();
        var lastName = jsonResponse?.GetProperty("lastName").ToString();
        Console.WriteLine(lastName);
        Assert.That("Sparrow", Is.EqualTo(lastName));

        await Task.Delay(2000);

        Console.WriteLine("---------------------------------------------------------------");

        // DELETE request
        Console.WriteLine("Delete request: Deletes a user by user name");
        var deleteResponse = await request.DeleteAsync("https://petstore.swagger.io/v2/user/jackson");
        Console.WriteLine($"Status code: {deleteResponse.Status}");
        Console.WriteLine($"Response body: {await deleteResponse.TextAsync()}");
    }
}