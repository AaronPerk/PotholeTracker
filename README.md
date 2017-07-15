# PotholeTracker
PotholeTracker is an ASP.NET MVC Web App built using the Google Maps API to report potholes and track the progress of their repair. Role-based authentication allows different permission levels. For example: only a city employee can access the employee portal.

## Database Folder

The database folder contains two files: `schema.sql` and `data.sql`.

- `schema.sql` should contain all of your `CREATE` statements should the database ever need to be rebuilt.
- `data.sql` should contain all of your `INSERT` seed data that is necessary to initially using of the database.

## NuGet Packages Installed
 
- **Capstone.Web**
    - Ninject
    - Ninject.MVC5
    - jQuery
    - jQuery.Validation
    - jQuery.Validation.Unobtrusive
    - Bootstrap

- **Capstone.Web.Tests**
    - Moq

- **Capstone.UITests**
    - Selenium.WebDriver
    - Selenium.WebDriver.ChromeDriver
    - Selenium.Support
    - SpecRun.SpecFlow
