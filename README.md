# Medical Appointment System

### Tech Stack:
Angular CLI v16.1.0  
ASP.NET Core 8.0 Web API   
MS SQL Server  
Bootstrap v5.3.0  
 
### Note:

For security, I did not include my email's SMTP information in appsettings.json. If SMTP information is added, email sending will work fine.

### Installation:

Download the project's zip from Github, extract it, and follow these instructions:

1. Install Node.js:

Download and install Node.js v18 from https://nodejs.org/en/about/previous-releases

2. Install Angular CLI v16.1.0:

Open command line and Navigate to MedicalAppointmentSystem-master/ClientApp. Run:

npm install -g @angular/cli@16.1.0

3. Install Project Dependencies by running:

npm install

4. Make sure angular.json includes under build> Options:

"styles": [  
  "node_modules/bootstrap/dist/css/bootstrap.min.css",  
  "node_modules/bootstrap-icons/font/bootstrap-icons.css",  
  "node_modules/ngx-toastr/toastr.css",   
  "src/styles.css"  
]

5. Click MedicalAppointmentSystem.sln and open Visual Studio. From top menues, select: Tools> Nuget Package Manager> package Manager Console. In the console, select "API" as default project and run:
update-database

