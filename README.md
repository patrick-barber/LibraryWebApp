# LibraryWebApp
Welcome to the MVC Library Web App
Setup Instructions:
Clone repo
Nuget restore (if not done automatically)
Open Package Manager Console (Tools > Nuget Package Manager > Package Manager Console )
type command "add migration InitialMigration"
type command "update-database"
Start application (https)

Usage Instructions:
Upon first startup you will need to navigate to the upper right corner nav item "Register" and create an account
On the register page you will have the option to choose from two roles: Librarian and Customer
Librarian = Admin which will give you access to administrative functions

After registration you will be redirected to the "Home Page" which shows you a random selection of featured books. 
Books data is generated with random data using Bogus for .Net

Navigation menu items:
"Home" will direct you to the Featured Books page
"Library" will redirect you to all books page
Admin Only:
"Manage > Users" will redirect you to the manage users page
"Manage > Roles" will redirect you to the manage roles page
Logout > logs you out

General Features: 
Details > selecting the "Details" button on any book will direct you to the Details page where you will see more in depth information about the book including who checked it out and when it is due for unavailable books.
from this page you are also able to Check out a book, Edit (Admin), and return back to the library
Check Out > this can be done by both librarians and Customers, this will set a book to unavailable and set the due date to 5 days from now. 
Check In > this can be done by a librarian on a book that is currently checked out. Sets book back to available and resets due date.
Edit > Allows a librarian to edit any detail of a book and save.
Rate > Allows a user to change the rating of a book. They can choose between 1 and 5 and save.
Delete > Allows a librarian to delete a book
Create New > Allows a librarian to add a new book
Filter and Sort > A user can filter by Category, Title, Author, and Availability. Also can sort the title and Author column by clicking on them. 

Administrative Features: 
Admins can add, edit and delete users from the manage users page.
Admins can also add, edit and delete roles from the manage roles page. 
Admins can add or remove users from roles and setup accounts for users.
