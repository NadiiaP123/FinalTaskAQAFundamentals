
##  FINAL TASK - [Fundamentals] Automated Testing in .NET #8

This project contains UI automation tests based on three user cases. 
Tests are implemented using Selenium WebDriver, NUnit and C#.
The framework supports parallel execution and multiple browsers.

----------------------------------------------------------------

## TASK DESCRIPTION:

- Site for testing: https://www.saucedemo.com

## 1. Automate following test cases:

### UC-1 Test Login form with only Username provided:

- Enter any username.
- Hit the "Login" button
- Enter password.
- Clear the "Password" field.
- Click the "Login" button.
- Verify that an error message "Password is required" appears.

### UC-2 Test Login form with valid credentials:

- Enter username for a standard user.
- Enter a password from the section “Password for all users”.
- Click “Login” button and verify that main page contains the following elements: 
  burger menu button; 
  label “Swag Labs”; 
  shopping cart icon; 
  dropdown with sorting filters; 
  list of inventory items

### UC-3 Test adding products to shopping cart:

- Login with standard user.
- Open details of any product by clicking on it.
- Add product to cart.
- Verify that the shopping cart icon displays the number of added products.

### 2. Provide:

- possibility to execute tests in parallel;
- add logging to track execution flow;
- use data-driven testing approach.


-----------------------------------------------------------------------
## RUN TESTS:

### 1. Configure test parameters using 'Tests/appsettings.json".
 - Set timeouts for Webdriver operations.
 - Make browser enabled to include it in test run.
 - Set headless mode if needed.

 Default configuration is the following: 

  - Chrome and Edge are included in the test run in headless mode.

  "Timeouts": {"ImplicitWait": 2, "ExplicitWait": 5, "PageLoad": 10},
  "Browsers": {
    "Chrome": {"Enabled": true, "Headless": true},
    "Edge": {"Enabled": true, "Headless": true}, 
    "Firefox": {"Enabled": false, "Headless": true}
   }


### 2. Apply the desired execution mode and run tests:
 
 - Run all tests in sequential mode:
  
 ```dotnet test Tests.csproj --settings Sequential.runsettings```
 
  - Run all tests in parallel mode:
  
 ```dotnet test Tests.csproj --settings Parallel.runsettings```

 
 @ Note: In Visual Studio use "Test" -> "Configure Run Settings" -> "Select Solution Wide Run Settings File" 
 and choose the desired .runsettings file.


 # 3. Run specific test:
 
 - Overview of available tests:
 
 ```dotnet test Tests.csproj --list-tests```

 - Run specific test by name:
 
 ```dotnet test Tests.csproj --filter "MainPageContains_BurgerMenuButton" --settings Sequential.runsettings```

 - Run tests with specific category:
 
 ```dotnet test Tests.csproj --filter "Category=UC1" --settings Sequential.runsettings```


 ### 4.  View log file:

 Check Log records in: FinalTaskAQAFundamentals/bin/Debug/net10.0/logs/test-log.txt

-----------------------------------------------------------------------

## TASK IMPLEMENTATION:

### Tech Stack:

- C#
- .NET 10
- Selenium WebDriver
- WebDriverManager
- NUnit
- Log4Net

### Supported Browsers

- Chrome
- Firefox
- Edge

### Project Structure:

```text
FinalTaskAQAFundamentals
│
├── Core                        # Framework core logic and shared infrastructure
│   ├── Config                    # Configuration loading and settings models
│   │   ├── BrowserSettings.cs      # Browser configuration model
│   │   ├── BrowserType.cs          # Supported browser enum
│   │   ├── ConfigLoader.cs         # Loads configuration from appsettings.json
│   │   ├── TestSettings.cs         # Test-related settings
│   │   └── TimeoutSettings.cs      # Timeout configuration
│   │
│   ├── Utilities                 # Shared helper utilities
│   │   ├── RunMode.cs              # Sequential / Parallel run mode enum
│   │   └── WaitHelper.cs           # Explicit wait helpers
│   │
│   └── WebDriver                 # WebDriver initialization and management
│       └── WebDriverFactory.cs     # Driver creation for different browsers
│
├── Pages                       # Page Object Model classes
│   ├── BasePage.cs               # Base class for all pages
│   ├── CartPage.cs               # Cart page actions and locators
│   ├── InventoryItemPage.cs      # Product details page
│   ├── InventoryPage.cs          # Products list page
│   └── LoginPage.cs              # Login page
│
└── Tests                       # Test project
    ├── Data                      # Test data and loaders
    │   ├── testdata.json           # Test data file
    │   ├── TestDataLoader.cs       # Loads test data from JSON
    │   └── TestDataModel.cs        # Test data model
    │
    ├── Tests                     # Test classes
    │   ├── BaseTest.cs             # Base test setup and teardown
    │   ├── GlobalSetup.cs          # Global test configuration
    │   ├── UC1.cs                  # Tests for user case UC1
    │   ├── UC2.cs                  # Tests for user case UC2
    │   └── UC3.cs                  # Tests for user case UC3
    │
    ├── appsettings.json          # Framework configuration
    ├── log4net.config            # Logging configuration
    ├── Parallel.runsettings      # Parallel execution settings
    ├── Sequential.runsettings    # Sequential execution settings
    └── README.md                 # Project documentation
...
```

 ### Logging

Logging is implemented using Log4Net.
By default, every new test run rewrites the existing log file.
You can change this by modifying the "Tests/Tests/GlobalSetup.cs".

Log file is stored in:

/bin/Debug/net10.0/logs/test-log.txt

----------------------------------------------------------------