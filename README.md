# CodeBlog

![Home (2)](https://github.com/asteekgoswami/CodeBlog/assets/96569849/935aad7f-427b-4e4e-99f9-3cdf4bfea5f0)

## Overview
CodeBlog is a robust blog site built using ASP.NET MVC, designed to facilitate seamless blogging with features such as user authentication, admin and super admin roles, comment sections, search functionality, filtering, pagination, and gated content. Only logged-in users can access certain parts of the site, ensuring a secure and personalized user experience.

## Key Features
- **User Roles**: Different roles for users, admins, and super admins.
- **Blog Posting**: Admin and super admin can post blogs with ease.
- **Like and Comments**: Users can like and comment on blog posts.
- **Search and Filter**: Powerful search and filtering options for finding blogs.
- **Pagination**: Efficient pagination to navigate through multiple blog posts.
- **Gated Content**: Only logged-in users can access certain parts of the site.
- **Responsive Design**: Optimized for both desktop and mobile devices.

## Installation

### Prerequisites
- .NET Core SDK
- SQL Server or any other compatible database
- Visual Studio or any other preferred IDE

### Steps
1. **Clone the repository**
    ```bash
    git clone https://github.com/yourusername/CodeBlog.git
    cd CodeBlog
    ```

2. **Set up the database**
    - Update the connection string in `appsettings.json` to point to your SQL Server database.

3. **Run the migrations**
    ```bash
    dotnet ef database update
    ```

4. **Build and run the application**
    ```bash
    dotnet build
    dotnet run
    ```

5. **Open in browser**
    - Navigate to `http://localhost:5000` to see the site in action.

## Usage

### Admin and Super Admin
- **Login**: Admins and super admins can log in using their credentials.
- **Post Blog**: Use the dashboard to create and publish new blog posts.

### Users
- **Register/Login**: Users can register or log in to their accounts.
- **View Blogs**: Browse through the blog posts.
- **Comment**: Engage with the content by leaving comments.
- **Search and Filter**: Use search and filter functionalities to find specific blogs.

## Screenshot

- **All Blogs**
![All Blogs](https://github.com/asteekgoswami/CodeBlog/assets/96569849/644af5fc-2c43-4506-9435-d38121aca926)

- **Filtering and Pagination**
![filtering](https://github.com/asteekgoswami/CodeBlog/assets/96569849/875659bb-0571-4c0d-8cb2-34dff820bb62)


- **Gated Content**
  ![gated](https://github.com/asteekgoswami/CodeBlog/assets/96569849/9b368bec-6d5a-4ed5-9766-856faf019fd9)

- **Super Admin Functionality to add Admin**
![Screenshot (30)](https://github.com/asteekgoswami/CodeBlog/assets/96569849/4c089ab3-1774-480f-bf44-5f6af9065058)


