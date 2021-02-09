
Adding Migrations
-----------------
From within the EF directory:
dotnet ef --startup-project ../DataTest/ migrations add <name>
dotnet ef --startup-project ../DataTest/ database update