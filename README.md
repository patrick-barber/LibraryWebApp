# LibraryWebApp
## Welcome to the MVC Library Web App

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
