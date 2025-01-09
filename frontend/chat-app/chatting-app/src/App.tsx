import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { CSSTransition, TransitionGroup } from 'react-transition-group';
import Login from './pages/Login/login'; 
import Register from './pages/Register/Register';
import Dashboard  from './pages/Dashboard/Dashboard';

const App: React.FC = () => {
  return (
    <Router>
      <AppRoutes />
    </Router>
  );
};

const AppRoutes: React.FC = () => {
  return (
    <TransitionGroup>
      <Routes>
        <Route
          path="/"
          element={
            <CSSTransition timeout={500} classNames="fade">
              <Login />
            </CSSTransition>
          }
        />
        <Route
          path="/register"
          element={
            <CSSTransition timeout={500} classNames="fade">
              <Register />
            </CSSTransition>
          }
        />
        <Route
          path="/dashboard"
          element={
            <CSSTransition timeout={500} classNames="fade">
              <Dashboard />
            </CSSTransition>
          }
        />
      </Routes>
    </TransitionGroup>
  );
};

export default App;
