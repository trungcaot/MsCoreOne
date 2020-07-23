import React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

import LoginMenu from "../components/LoginMenu";
import { selectIsAuthenticated, selectUser } from "../store/auth-slice";

const NavMenu = () => {
  const isAuthenticated = useSelector(selectIsAuthenticated);
  const userName = useSelector(selectUser)?.name;

  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-light border-bottom box-shadow mb-3">
        <Link to="/home" className="navbar-brand">
            MsCoreOne-React
        </Link>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarColor01">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active">
              <Link className="nav-link" to="/categories">Categories</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/products">Products</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/users">Users</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/about">About</Link>
            </li>
          </ul>
        </div>
        <div>
          <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse" id="navbarNavDropdown">
            <LoginMenu isAuthenticated={isAuthenticated} userName={userName} />
          </div>
        </div>
      </nav>
    </header>
  );
};

export default NavMenu;
