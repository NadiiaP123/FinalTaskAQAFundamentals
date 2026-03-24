###  FINAL TASK - [Fundamentals] Automated Testing in .NET #8

----------------------------------------------------------------

### TASK DESCRIPTION:

- Site for testing: https://www.saucedemo.com

### 1. Automate following test cases:

## UC-1 Test Login form with only Username provided:
- Enter any username.
- Hit the "Login" buttonEnter password.
- Clear the "Password" field.
- Click the "Login" button.
- Verify that an error message "Password is required" appears.


## UC-2 Test Login form with valid credentials:
- Enter username for a standard user.
- Enter a password from the section “Password for all users”.
- Click “Login” button and verify that main page contains the following elements: 
  burger menu button; 
  label “Swag Labs”; 
  shopping cart icon; 
  dropdown with sorting filters; 
  list of inventory items


## UC-3 Test adding products to shopping cart:

- Login with standard user.
- Open details of any product by clicking on it.
- Add product to cart.
- Verify that the shopping cart icon displays the number of added products.

### 2. Provide:
- possibility to execute tests in parallel;
- add logging to track execution flow;
- use data-driven testing approach.

-----------------------------------------------------------------------

### TASK IMPLEMENTATION:

## 1. Test Configuration.

Test run settings are stored in `Tests/appsettings.json`.

You can configure the following parameters:

- `BaseUrl` - Base address of the application under test;
- Timeouts`ImplicitWait` — WebDriver implicit wait;
- Timeouts`ExplicitWait` — explicit wait timeout;
- Timeouts `PageLoad` — page load timeout;
- Run `Parallel` — enables/disables parallel test execution;
- Run `Threads` — maximum number of parallel tests;
- Browser `Enabled` — include browser in test run;
- Browser `Instances` — number of parallel browser instances;
- Browser `Headless` — run browser in headless mode.

