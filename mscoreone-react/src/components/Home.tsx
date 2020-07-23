import React from "react";

const Home = () => {
  return (
    <>
      <div className="row">
        <div className="col align-self-center">
          <div className="alert alert-primary" role="alert">
            Welcome to MsCoreOne-React - {process.env.REACT_APP_ENV_INFO}
            
            <h6 className="mt-4">Administrator</h6>
            <p>Manage your store. Admin email: <strong>admin@mscoreone.com</strong> Admin password: <strong>P@ssw0rd</strong></p>
          </div>
        </div>
      </div>
    </>
    );
};

export default Home;
