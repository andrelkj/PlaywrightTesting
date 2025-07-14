using System.Xml;

namespace PlaywrightTesting.Utilities;

public class XMLLocatorsReader
{
    public static string GetLocatorValue(string pageName, string elementName)
    {
        string locatorValue = null;

        // Load the XML file
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(
            $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/Locators.xml");

        // Get the root element
        var root = xmlDoc.DocumentElement;

        // Define XPATH expressions that can access the specified element based on the given page and locator type
        var xpath = $"/locators/{pageName}/{elementName}";

        // Select the locator value node
        var locatorValueNode = root.SelectSingleNode(xpath);

        if (locatorValueNode != null) locatorValue = locatorValueNode.InnerText;

        return locatorValue;
    }

    private static void Main(string[] args)
    {
        Console.WriteLine(GetLocatorValue("LoginPage", "username"));
        Console.WriteLine(GetLocatorValue("RegistrationPage", "password"));
    }
}