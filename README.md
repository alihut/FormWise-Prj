# FormWise – Reimbursement Submission App

This project demonstrates a minimal web application allowing university employees to submit receipts for reimbursement. It includes:

- A backend API built with ASP.NET Core Web API  
- A frontend web interface built with ASP.NET Core MVC  
- SQLite as the data store

---

## 🎯 Purpose

This project was built as part of a technical assessment. The goal was to demonstrate core skills in building a clean, minimal feature to allow users to submit reimbursement requests with basic validations.

---

## 💻 Tech Stack & Design Choices

### Backend (`FormWise.WebApi`)
- **Framework**: ASP.NET Core Web API  
- **ORM**: Entity Framework Core  
- **Database**: SQLite

> I chose ASP.NET Core Web API with C# because it’s a combination I’m highly experienced and comfortable working with.
> Since this is a simple feature, I avoided overengineering with extra layers like clean architecture and handled all logic in one project.

### Frontend (`FormWise.WebApp`)
- **Framework**: ASP.NET Core MVC

> I used ASP.NET Core MVC as it’s the frontend tech I’m most experienced with.  
> Although I’ve worked on Angular/TypeScript in pet projects and would love to deepen that skillset, I focused here on what I could deliver quickly and reliably.

---

## 🔐 Authentication

Although not explicitly required, I added **JWT-based authentication**.

> **Reason**: Reimbursements should be linked to a specific employee.  
> So, authentication ensures `UserId` is available to associate with each reimbursement.

---

## 🧾 Reimbursement Entity

Each reimbursement record includes the following fields:

- **UserId** (Guid): The user who submitted the request  
- **Date** (DateTime): The date of the purchase  
- **Amount** (decimal): The amount to be reimbursed  
- **Description** (string): A short description of the purchase  
- **ReceiptFilePath** (string): Path to the uploaded receipt file  

---

## ✅ Features Implemented

- 🔐 Secure login page (with JWT)
- 📄 Add reimbursement form with:
  - Amount
  - Date
  - Description
  - Receipt upload (.pdf, .jpg, .png)
- 💾 File storage saved to `wwwroot/receipts`(simulating an external blob storage service like Azure Blob Storage for real-world use cases)
- 🧪 Validation rules:
  - Amount must be greater than 0
  - Date must not be in the future
  - Description must be between 5 and 250 characters

---

## 👤 Default User

Only one user is seeded into the database:

- **Email**: admin@uiowa.edu  
- **Password**: admin123

---

## 🚀 How to Run the Project

1. Clone the repository  
2. Open the solution in Visual Studio  
3. Apply the database migrations by running:

    ```
    dotnet ef database update --project FormWise.WebApi
    ```

4. Start both projects:  
   - `FormWise.WebApi`  
   - `FormWise.WebApp`

---

## ⏱️ Time Estimate

| Estimate | Actual   |
|----------|----------|
| 5 hours  | 3.5 hours |

Initially, I considered implementing features like “View Previous Reimbursements,” “Logout,” or “Remember Me.”  
After revisiting the task description, I realized those would go beyond the intended scope.

---

## 🙏 Final Note

Thanks for taking the time to review this solution.  
I enjoyed working on it, and I’d be happy to explain any part of the code or thought process in an interview.
