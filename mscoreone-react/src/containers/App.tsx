import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import Home from "../components/Home";
import ListOfProduct from "../components/products/Product";
import About from "../components/About";
import NavMenu from "./NavMenu";
import Auth from "./Auth";
import ListOfUser from "../components/user/User";
import Category from "../components/category/Category";
import Unauthorized from "../components/Unauthorized";

const App = () => {
  return (
    <BrowserRouter basename={"/"}>
      <NavMenu />
      <div className="container">
        <div className="row">
          <div className="col">
            <Switch>
              <Route path="/authentication" component={Auth} />
              <Route path="/unauthorized" component={Unauthorized} />
              <Route path="/about" component={About} />
              <Route path="/categories" exact component={Category} />
              <Route path="/products" exact component={ListOfProduct} />
              <Route path="/users" exact component={ListOfUser} />
              <Route path="/" component={Home} />
            </Switch>
          </div>
        </div>        
      </div>
    </BrowserRouter>
  );
};

export default App;
