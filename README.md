# POS + Accounting System

This is a Windows Forms application that combines a Point of Sale (POS) system with accounting features.

## Project Structure

Create these folders and files in your project:

```
POSAccountantApp/
├── Forms/
│   ├── LoginForm.cs
│   ├── DashboardForm.cs
│   ├── POSForm.cs
│   ├── InventoryForm.cs
│   └── ReportsForm.cs
├── Models/
│   ├── User.cs
│   ├── Product.cs
│   └── Sale.cs
├── Services/
│   ├── AuthService.cs
│   ├── InventoryService.cs
│   ├── SaleService.cs
│   └── ReportService.cs
├── Database/
│   └── SqlConnectionHelper.cs
├── Utilities/
│   └── PrintHelper.cs
├── Program.cs
└── POSAccountantApp.csproj
```

## How to Set Up

1. Install Visual Studio 2022 Community Edition
2. Create a new Windows Forms App (.NET 6.0) project named "POSAccountantApp"
3. Create the folders: Forms, Models, Services, Database, and Utilities
4. Copy each file's content from below into the corresponding files in your project

## File Contents

### POSAccountantApp.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net6.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <EnableWindowsTargeting>true</EnableWindowsTargeting>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="System.Data.SqlClient" Version="4.8.5" />
  </ItemGroup>
</Project>
```

### Program.cs
```csharp
using System;
using System.Windows.Forms;

namespace POSAccountantApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("A critical error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```

[Continue copying all the other files' content from our previous work...]

## Running the Application

1. Open the solution in Visual Studio 2022
2. Build the solution (Ctrl + Shift + B)
3. Run the application (F5)
4. Login with:
   - Username: admin
   - Password: admin

## Features

- User authentication with role-based access
- Point of Sale system with barcode support
- Inventory management
- Financial reporting
- Receipt printing
