# 🎓 College Management System (Windows Forms Application)

A desktop-based College Management System built with **C# (.NET WinForms)**. This system helps manage student records, departments, fee payments, teachers, and administrative dashboards.

---

## 🛠️ Technologies Used

- **C#** – Windows Forms (WinForms)
- **.NET Framework**
- **SQL Server** – For data storage (`SQLQuery1.sql`)
- **Visual Studio** – Project and Designer files

---

## 📁 Project Structure

- `Dashboard.cs`, `Login.cs`, `Form1.cs` – Main forms and interface logic
- `frmStudents.cs`, `frmDepartments.cs`, `frmFeesDeposite.cs`, `Teacher.cs` – Entity-specific forms
- `CollegeDataSet.xsd` – Typed dataset and database schema
- `App.config` – Connection strings and configuration
- `SQLQuery1.sql` – SQL script for database structure
- `bin/`, `obj/`, `Resources/`, `Properties/` – Auto-generated folders by Visual Studio

---

## 🚀 Features

- Admin dashboard with form navigation
- Manage students, departments, and teachers
- Add and update fee deposits
- Login system (basic)
- Visual Studio Designer integration

---

## 🧪 How to Run

1. Open the solution (`.csproj`) file in **Visual Studio**.
2. Restore any required NuGet packages if prompted.
3. Ensure your SQL Server is running and import `SQLQuery1.sql` into your database.
4. Update the **connection string** in `App.config`.
5. Run the application.

---

## 📄 License

This project is for educational purposes. Feel free to use, fork, or extend it.

## 👨‍💻 Author

Hadi Mcheimech– Built as part of a .NET WinForms learning or academic project.
