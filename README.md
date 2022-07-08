# Steps to run the project

1. Clone the project using command `git clone https://github.com/usamashfque/infosalons-web-api.git`
2. Build the solution to restore nuget packages
3. 
4. Change connection string in `appsettings.json` file of `Infosalons.Application` project
5. Select `Infosalons.Application` as `Set as startup project`
6. Open `Package Manager Console` and select `Infosalons.Repository` project in dropdown
7. Run `update-database` command, this will create tables neccessary for application to run
8. Start the application from visual studio
9. This will display a Swagger UI, this means api is launched successfully.
10. Now head towards [angular app](https://github.com/usamashfque/infosalons-angular-web.git)