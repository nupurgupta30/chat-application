import React, { useState, useEffect } from 'react';
import './Dashboard.css';

const Dashboard: React.FC = () => {
  // State to hold the list of users
  const [users, setUsers] = useState<any[]>([]);

  // Call backend API to fetch the users (except the current user)
  useEffect(() => {
    // Assuming the API endpoint is '/api/users'
    const fetchUsers = async () => {
      try {
        const response = await fetch('/api/users');
        const data = await response.json();
        setUsers(data); // Update the users state
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    };
    
    fetchUsers();
  }, []);

  return (
    <div className="dashboard"> {/* The flex container */}
      <div className="component1"> {/* Left panel */}
        <h2>Hello World!</h2>
        <p>This is your dashboard where you can manage your account and more. 
          Lorem, ipsum dolor sit amet consectetur adipisicing elit. Quae autem doloremque reiciendis id modi magnam commodi in, saepe necessitatibus ratione neque harum quos amet dolores similique voluptatibus architecto veniam consectetur.
          Lorem, ipsum dolor sit amet consectetur adipisicing elit. Inventore consequuntur ipsam rem officiis minima culpa facere, facilis fugiat fugit consectetur minus commodi, ipsa ducimus consequatur dignissimos tempora numquam nulla nemo.
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Numquam tenetur voluptatem dolore nisi debitis inventore eveniet voluptatum! Delectus iusto ipsa tenetur vero culpa sapiente modi aperiam consequatur voluptas numquam? Ea.
        </p>
      </div>
      
      <div className="component2"> {/* Right panel */}
        <h2>Chats</h2>
        
        {/* Render the list of users */}
        <div className="user-list">
          {users.length === 0 ? (
            <p>Loading users...</p> // Display loading text while fetching
          ) : (
            users.map((user, index) => (
              <div className="user-item" key={index}>
                <p>{user.username}</p> {/* Assuming each user object has a 'username' property */}
              </div>
            ))
          )}
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
