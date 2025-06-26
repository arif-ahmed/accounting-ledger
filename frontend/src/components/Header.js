
import React from 'react';
import { Link } from 'react-router-dom';
import './Header.css';

const Header = () => {
  return (
    <header className="header">
      <h1>Accounting Ledger</h1>
      <nav>
        <ul>
          <li>
            <Link to="/">Dashboard</Link>
          </li>
          
          <li>
            <Link to="/accounts">Accounts</Link>
          </li>
          <li>
            <Link to="/journal">Journal</Link>
          </li>
          <li>
            <Link to="/trial-balance">Trial Balance</Link>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;
