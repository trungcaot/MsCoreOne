import React from "react";

const Unauthorized = () => {
  return (
    <>
      <div className="row justify-content-md-center">
        <div className="col align-self-center">
          <div className="alert alert-secondary" role="alert">
            <h5 className="mb-4">401 - You are not unauthorized for selected action.</h5>
            <p>
              Go to login <a href="/authentication/login">page</a>
            </p>
          </div>
        </div>
      </div>
    </>
    );
};

export default Unauthorized;
