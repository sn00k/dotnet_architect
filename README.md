## Disclaimer
Found this lying around laughing at its non-existent version control.

Over the course of 24 hours I built this as an exercise in software design and architecture with little to no focus on the UX.

Uploading this for the world to see but also as a reminder to myself of when, why and how to implement different design patterns and strategies when building the architecture.

#### Prerequisites
* SQL Server/Express
* .NET MVC 5
* .NET 4

#### Instructions
1. Run `Create_Companies_Table.sql`.
2. Run `Create_Stores_Table.sql`.
3. Edit connection string in `CompanyStores.Web/CompanyStores.Web/Global.asax.cs:26`.
4. Add a Company by clicking on **Create New** in the list of Companies.
5. Add a Store by clicking on **Edit** on a Company in the list and clicking **Create New Store**.
6. Fetch coordinates by clicking on **Edit** on a Store and click on **Fetch**.