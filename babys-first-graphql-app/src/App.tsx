import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { ExchangeRates } from "./ExchangeRates";

const App: React.FC = () => {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />

        <ExchangeRates />
      </header>
    </div>
  );
};

export default App;
