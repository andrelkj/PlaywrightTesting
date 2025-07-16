# Playwright Testing Project

A comprehensive C# test automation project demonstrating key concepts and techniques using Microsoft Playwright for web
automation testing.

## Overview

This project showcases various web automation testing concepts using Playwright with C#. It includes examples of basic
browser automation, advanced locator strategies, mouse interactions, keyword-driven testing, practical exercises, test parallelization, retry mechanisms, and comprehensive reporting.

## Technologies Used

- **Framework**: .NET 9.0
- **Language**: C# 13.0
- **Testing Library**: Microsoft Playwright
- **Configuration**: Microsoft.Extensions.Configuration
- **Test Runner**: NUnit 3x-4x with VsTest

## Key Concepts Demonstrated

### 1. Basic Playwright Setup and Navigation

- **Playwright Instance Creation**: Creating playwright instances to access browser automation methods
- **Browser Management**: Launching different browser engines (Chromium, Firefox, WebKit)
- **Page Navigation**: Basic navigation and page interactions
- **Browser Context**: Custom viewport configuration and browser context management

### 2. Locator Strategies

The project demonstrates multiple approaches to element identification:

- **CSS Selectors**: Traditional CSS-based element selection
- **XPath**: XML path language for precise element targeting
- **Playwright Methods**: Built-in methods for accessible element identification
    - `GetByRole()` - ARIA role-based selection
    - `GetByText()` - Text-content-based selection
    - `GetByLabel()` - Label-based selection
    - `GetByPlaceholder()` - Placeholder text-based selection
- **Filter Locators**: Combining locators with filtering conditions
- **Chaining Locators**: Building complex locator chains for precise targeting

### 3. Mouse Interactions and Advanced Actions

- **Hover Actions**: Mouse hover functionality for dropdown menus
- **Click Actions**: Various click operations on different elements
- **Form Interactions**: Filling forms and input fields
- **Keyboard Actions**: Key press simulations

### 4. Element Manipulation and Validation

- **Element Counting**: Counting collections of elements
- **Text Extraction**: Retrieving text content from elements
- **Attribute Access**: Getting element attributes
- **State Validation**: Checking element states (visible, checked, etc.)
- **Assertions**: Page title and content validation

### 5. Configuration-Driven Testing

- **External Configuration**: Using `appsettings.json` for test configuration
- **Browser Selection**: Dynamic browser selection based on configuration
- **Environment Management**: Configurable URLs and test data

### 6. Keyword-Driven Testing Framework

- **Action Methods**: Reusable action methods (`Click`, `Fill`)
- **XML Locator Management**: External locator storage and retrieval
- **Page Object Pattern**: Structured approach to page element management

### 7. Regular Expression Pattern Matching

- **Text Parsing**: Using RegEx for extracting specific information from HTML
- **Dynamic Content Handling**: Parsing complex HTML structures to extract meaningful data

### 8. Test Parallelization

- **Parallel Test Execution**: Running tests concurrently for faster feedback
- **Thread Safety**: Ensuring tests can run in parallel without interference
- **Resource Management**: Optimizing browser instance allocation in parallel scenarios
- **Execution Control**: Managing dependency between tests with execution attributes

### 9. Retry Mechanisms

- **Automatic Retries**: Configuration for automatic retry of flaky tests
- **Conditional Retry**: Custom logic to determine when tests should be retried
- **Exponential Backoff**: Smart waiting strategy between retries
- **Failure Analysis**: Capturing and analyzing different failure attempts

### 10. Test Reporting

- **ExtentReports Integration**: Rich HTML report generation with ExtentReports
- **Screenshot Capture**: Automatic screenshot capture on test failure
- **Video Recording**: Test execution video recording for visual verification
- **Customized Logging**: Detailed step-by-step test execution logging
- **Report Customization**: Branded and categorized test reports

## Project Structure

PlaywrightTesting/
├── Exercises/ # Practice exercises and assignments
├── Resources/ # Configuration files and test data
│ ├── appsettings.json # Application configuration
│ └── Locators.xml # Element locators storage
├── Reports/ # Test execution reports and artifacts
│ ├── Screenshots/ # Captured test failure screenshots
│ ├── Videos/ # Test execution recordings
│ └── ExtentReports/ # HTML test reports
├── Testcases/ # Main test scenarios
└── Utilities/ # Helper classes and utilities

## Featured Test Scenarios

### Basic Web Automation

- Simple navigation and page title validation
- Element interaction and form filling

### Advanced Locator Usage

- Multi-strategy element identification
- Complex locator chaining examples

### Mouse and Keyboard Interactions

- Hover-based dropdown navigation
- Form submission and validation

### Link Analysis

- Counting and analyzing page links
- Extracting link properties and URLs

### Checkbox Manipulation

- Pre-selected checkbox validation
- Random checkbox selection algorithms
- Dynamic checkbox name extraction

### Real-World Applications

- Google search result analysis
- Travel website interaction (MakeMyTrip)
- Educational website navigation

### Parallel Test Execution

- Multi-browser parallel testing
- Data-driven parallel scenarios
- Cross-platform concurrent execution

### Test Reporting and Analysis

- ExtentReports integration with custom status labels
- Screenshot and video capture on failures
- Detailed execution logs with timing information
- Categorized test reporting by feature

## Key Learning Outcomes

1. **Asynchronous Programming**: Proper use of `async/await` patterns in test automation
2. **Element Identification**: Multiple strategies for reliable element location
3. **Test Data Management**: External configuration and locator management
4. **Error Handling**: Robust element interaction with timeout handling
5. **Code Reusability**: Keyword-driven and utility-based testing approaches
6. **Cross-Browser Testing**: Multi-browser support and configuration
7. **Dynamic Content Handling**: Working with changing web content using various techniques
8. **Test Scalability**: Implementing parallelization for faster test execution
9. **Test Stability**: Creating robust tests with retry mechanisms for flaky scenarios
10. **Results Communication**: Building comprehensive visual reports for stakeholders

## Best Practices Demonstrated

- **Wait Strategies**: Using appropriate wait conditions for stable tests
- **Locator Reliability**: Choosing robust locator strategies over fragile ones
- **Test Maintainability**: Separating test data from test logic
- **Browser Resource Management**: Proper browser lifecycle management
- **Configuration Management**: Externalized test configuration for flexibility
- **Parallel Execution**: Optimizing test suite execution time through parallelization
- **Flaky Test Handling**: Implementing intelligent retry mechanisms for unstable tests
- **Visual Reporting**: Providing clear, visual test execution reports with screenshots and videos
- **Failure Documentation**: Automatically capturing detailed context on test failures
- **CI/CD Integration**: Structuring tests for seamless integration with continuous integration pipelines

## Getting Started

1. Ensure .NET 9.0 SDK is installed
2. Restore NuGet packages: `dotnet restore`
3. Install Playwright browsers: `pwsh bin/Debug/net9.0/playwright.ps1 install`
4. Run individual test files or use your preferred test runner

This project serves as a comprehensive learning resource for web automation testing concepts using Playwright and C#.
