# ChatApp Frontend

This repository contains the frontend code for **ChatApp**, a web-based chat application built using React and TypeScript. The app provides a user-friendly interface with authentication and prepares for real-time messaging features.

## Features

- **Login Functionality**:
  - Secure login form with validation for username and password.
  - Displays error messages for invalid credentials.
  - Redirects to the dashboard on successful login.

- **State Management**:
  - Uses React’s `useState` hook to manage form inputs and error messages dynamically.

- **API Integration**:
  - Communicates with the backend using REST APIs for user authentication.
  - Sends login data via a POST request.

- **Responsive Design**:
  - Clean and responsive layout styled with custom CSS.
  - The login page includes a form and an engaging welcome image.

- **Dashboard**:
  - Two sections (Work in Progress): a user list and a chat area.
  - Ready for backend API integration to fetch and display user data.

## Technologies
- React with TypeScript
- CSS for styling
- React Router for navigation
- Fetch API for backend integration

## Roadmap
- Dynamic User List: Fetch and display users in the chat area using backend APIs.
- Registration Page: Add a page for user sign-up.
- Real-Time Messaging: Implement SignalR for real-time chat features.
- State Management: Transition to useContext or Redux for better state handling.
- Enhanced Error Handling: Add error boundaries and loading states for a smoother user experience.
