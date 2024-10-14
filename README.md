# NullamWebApp

## Hosted using Aspire (can run locally)
To run this app locally choose __NullamWebApp.AppHost__ and run it in "__https__"

## App consists of 2 components
### API that is connected to the DB (stored on local SQL Server and created on initial load).
### WebApp using Blazor and connected to the API (All data is stored on SQL server and accessed via API)

# Database Diagram

![image](https://github.com/user-attachments/assets/d441c675-333a-4f81-a198-8af349b807f5)


# Ideas for improvement
## API
### Split API into more layers
#### Add Validation and Command layer - ServiceLayer should only handle the general.
### Add Unit Tests
### Improve "API Response Message" to be easier to use and introduce static error messages

## UI
### Add Selenium Tests
### Add Authorization
### Add A way to completely remove participants from DB
### Add A custom calendar component for better start time handling
### Generalize CSS to make future development easier to both manage and keep unified.
### Add translation file and language choice option 
