using OpenCvSharp;

namespace PlaywrightTesting.testcases;

internal class AImageElementFinder
{
    public static Point? FindElementByImage(string screenshotPath, string templatePath)
    {
        using var screenshot = new Mat(screenshotPath);
        using var template = new Mat(templatePath);

        var result = new Mat();
        Cv2.MatchTemplate(screenshot, template, result, TemplateMatchModes.CCoeffNormed);

        Cv2.MinMaxLoc(result, out _, out var maxVal, out _, out var maxLoc);

        // Adjust this threshold as needed
        const double threshold = 0.8;
        return maxVal >= threshold ? maxLoc : null;
    }
}