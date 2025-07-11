using System.Xml;

namespace PlaywrightTesting.Utilities;

public class XMLLocatorsReader
{
    public static string GetLocatorValue(string pageName, string elementName, string locatorType)
    {
        string locatorValue = null;

        // Load the XML file
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(
            $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/Locators.xml");

        // Get the root element
        XmlElement root = xmlDoc.DocumentElement;

        // Define XPATH expressions that can access the specified element based on the given page and locator type
        string xpath = $"/locators/{pageName}/{elementName}[LocatorType='{locatorType}']/LocatorValue";

        // Select the locator value node
        XmlNode locatorValueNode = root.SelectSingleNode(xpath);

        if (locatorValueNode != null)
        {
            locatorValue = locatorValueNode.InnerText;
        }

        return locatorValue;
    }

    static void Main(string[] args)
    {
        Console.WriteLine(GetLocatorValue("LoginPage", "username", "ID"));
        Console.WriteLine(GetLocatorValue("RegistrationPage", "password", "XPATH"));
    }
}