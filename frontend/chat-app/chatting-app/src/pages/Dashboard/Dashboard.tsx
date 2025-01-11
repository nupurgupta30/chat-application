import React from 'react';
import { useLocation } from 'react-router-dom';
import './Dashboard.css';

const Dashboard: React.FC = () => {
  const location = useLocation();
  const userData = location.state; // Access the passed user data

  return (
    <div className="dashboard">
      <div className="component1">
        <div className="profiepicture"/>
        <div className="information">
          <h2>Welcome, {userData?.username.toUpperCase( )}!</h2>
          <p>Email: {userData?.email}</p>
        </div>
      </div>

      <div className="component2">
        <h2>Your Dashboard</h2>
        <p>Here you can manage your chats. 
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Quia placeat aliquam dolorum, pariatur facilis modi voluptatum, magnam perferendis in nulla a dignissimos, nostrum odio neque libero cumque fugiat ut alias.
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Perspiciatis officiis reprehenderit, assumenda odit praesentium maiores sed officia velit explicabo nisi odio sapiente a maxime! Vitae sed neque incidunt odit optio?
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sequi aliquam, labore corporis ullam numquam, facere, quisquam tempora earum asperiores illum ipsum voluptate! Tenetur doloremque debitis minus soluta. Quod, voluptas dolorum!
          Lorem ipsum dolor sit, amet consectetur adipisicing elit. Quidem ad enim repellendus animi perferendis nostrum corrupti molestias officiis, quas minus, nobis magnam explicabo dolor odio inventore, recusandae impedit suscipit! Reiciendis.
        </p>
      </div>
    </div>
  );
};

export default Dashboard;
