# Steps to run the project

1. Clone the project using command `git clone https://github.com/usamashfque/infosalons-web-api.git`

2. Build the solution to restore nuget packages
 
3. Change connection string in `appsettings.json` file of `Infosalons.Application` project

4. Select `Infosalons.Application` as `Set as startup project`

5. Open `Package Manager Console` and select `Infosalons.Repository` project in dropdown

6. Run `update-database` command, this will create tables neccessary for application to run

7. Start the application from visual studio

8. This will display a Swagger UI, this means api is launched successfully.

9. Now head towards [angular app](https://github.com/usamashfque/infosalons-angular-web.git)

# Steps how to test the project

1. After successfully `Angular` and `Web Api` both project are running.

2. Goto Signin up page and create your user account

3. After account creation, then sign in.

4. After sign in you will see, all invoices table.

5. Click on Add new invoice, then you will create new invoice

6. You can Add, Edit and Delete operation perform against invoices.

## Personal Information

Name: Usama Shafique
Email: usama.shfque@gmail.com
Position: Jr. Full stack Developer