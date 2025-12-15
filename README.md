# LibraryWebApp

An ASP.NET MVC web application simulating an online library system. Features user authentication with role-based authorization (Librarian/Admin vs. Customer), book management (checkout/check-in, ratings, CRUD), filtering/sorting, and fake data generation with Bogus.

**Live Demo**: [GitHub Link](https://github.com/patrick-barber/LibraryWebApp) (Run locally)

## Screenshots

### Home Page (Featured Books)
![Home Page with Featured Books]
<img width="1886" height="639" alt="image" src="https://github.com/user-attachments/assets/8280bb79-5094-407c-b891-3e389e91d812" />

### All Books Library View (with Filter/Sort)
![Library Books List - Filtering and Sorting]
<img width="1891" height="905" alt="image" src="https://github.com/user-attachments/assets/7b085cc5-e5af-4761-94a1-b30e2cae0440" />

### Book Details Page (Available Book)
![Book Details - Checkout and Rating Options]
<img width="1883" height="680" alt="image" src="https://github.com/user-attachments/assets/e089211d-7454-4394-a777-b394be7c6e94" />

### Book Details Page (Checked Out Book)
![Book Details - Due Date and Check-In (Admin)]
<img width="1880" height="720" alt="image" src="https://github.com/user-attachments/assets/8b7d6f35-f3e7-41aa-8060-b85656ac1edb" />

### Rate Book Page
<img width="1881" height="469" alt="image" src="https://github.com/user-attachments/assets/30fb9ba3-c9e8-412a-8888-e12f932921eb" />

### Delete Book Page
<img width="1876" height="649" alt="image" src="https://github.com/user-attachments/assets/1698acbd-6ee1-4874-b096-819a31a010f6" />

### Registration and Login
![Register Page with Role Selection]
<img width="1887" height="652" alt="image" src="https://github.com/user-attachments/assets/8280d47e-2901-488c-89be-507057ad3afe" />

![Login Page]
<img width="1894" height="534" alt="image" src="https://github.com/user-attachments/assets/6f264ceb-2411-4bf5-a181-475724003150" />

### Checkout Process
![Checking Out a Book]
<img width="1889" height="716" alt="image" src="https://github.com/user-attachments/assets/ddf016ae-ab7e-4bbb-964b-8108c69f1a67" />

### Admin Features
![Manage Users (Admin Only)]
<img width="1886" height="623" alt="image" src="https://github.com/user-attachments/assets/ca3413f4-0d12-4f89-9875-d3205e5d1fd0" />

![Manage Roles (Admin Only)]
<img width="1881" height="698" alt="image" src="https://github.com/user-attachments/assets/dc3ff83c-f1e8-4fc5-ab5f-35a184245d97" />

![Editing a Book (Admin)]
<img width="1883" height="899" alt="image" src="https://github.com/user-attachments/assets/5c03f3f0-06de-4827-9474-1e7ef64ec269" />

---

### **Setup Instructions:**
- Clone the repository
- Perform NuGet restore (if not done automatically)
- Open Package Manager Console:  
  `Tools > NuGet Package Manager > Package Manager Console`
- Run the following commands:
  - `add migration InitialMigration`
  - `update-database`
- Start the application (https)

---

### **Usage Instructions:**
- Upon first startup, navigate to the upper right corner nav item **"Register"** to create an account.
- On the register page, choose from two roles:
  - **Librarian** (Admin) – Access to administrative functions.
- After registration, you will be redirected to the **"Home Page,"** showcasing a random selection of featured books.
- Books data is generated with random data using **Bogus** for .NET.

**Navigation menu items:**
- **"Home"**: Directs you to the Featured Books page.
- **"Library"**: Redirects you to the All Books page.

---

### **Admin Only:**
- **"Manage > Users"**: Redirects you to the Manage Users page.
- **"Manage > Roles"**: Redirects you to the Manage Roles page.
- **Logout**: Logs you out.

---

### **General Features:**

- **Details**: Selecting the **"Details"** button on any book directs you to the Details page with in-depth information, including who checked it out and when it is due for unavailable books.
  - From this page, you can also:
    - **Check out** a book
    - **Edit** (Admin)
    - Return to the library

- **Check Out**: Available to both librarians and customers; this sets a book to unavailable and assigns a due date 5 days from now.

- **Check In**: Librarians can check in books currently checked out, setting the book back to available and resetting the due date.

- **Edit**: Allows librarians to edit any detail of a book and save the changes.

- **Rate**: Users can change a book's rating, choosing between 1 and 5 and saving it.

- **Delete**: Allows librarians to delete a book.

- **Create New**: Enables librarians to add a new book.

- **Filter and Sort**: Users can filter by Category, Title, Author, and Availability. You can also sort the Title and Author columns by clicking on them.

---

### **Administrative Features:**

- Admins can add, edit, and delete users from the Manage Users page.
- Admins can also add, edit, and delete roles from the Manage Roles page.
- Admins can add or remove users from roles and set up accounts for users.
