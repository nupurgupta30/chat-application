import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './login.css';
import groowvyImage from '../images/groowvy.png';

const Login: React.FC = () => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [errorMessage, setErrorMessage] = useState<string>('');
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    if (username === '' || password === '') {
      setErrorMessage('Please fill in both fields.');
      return;
    }

    const loginRequest = { username, password };

    try {
      const response = await fetch('http://localhost:5108/api/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginRequest),
      });

      if (!response.ok) {
        const errorData = await response.json();
        setErrorMessage(errorData.message || 'Login failed');
      } else {
        const userData = await response.json();
        console.log('Login successful:', userData);
        setErrorMessage('');
        navigate('/dashboard', { state: userData }); // Pass userData to Dashboard
      }
    } catch (error) {
      console.error('Error occurred:', error);
      setErrorMessage('Something went wrong. Please try again later.');
    }
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <h2>Login</h2>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
        <form onSubmit={handleSubmit} className="login-form">
          <div className="input-group">
            <label htmlFor="username">Username</label>
            <input
              type="text"
              id="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              placeholder="Enter username"
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="password">Password</label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Enter password"
              required
            />
          </div>

          <button type="submit" className="login-button">
            Login
          </button>
        </form>
        <p className="register-link">
          Not a user? <Link className="link" to="/register">Register here</Link>
        </p>
      </div>
      <div className="image-container">
        <img src={groowvyImage} alt="Welcome to ChatApp" />
      </div>
    </div>
  );
};

export default Login;
