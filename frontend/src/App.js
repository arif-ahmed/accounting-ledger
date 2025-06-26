import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import Dashboard from './pages/Dashboard';

import Accounts from './pages/Accounts';
import Journal from './pages/Journal';
import TrialBalance from './pages/TrialBalance';

function App() {
  return (
    <Router>
      <Header />
      <div className="container">
        <Routes>
          <Route path="/" element={<Dashboard />} />
          
          <Route path="/accounts" element={<Accounts />} />
          <Route path="/journal" element={<Journal />} />
          <Route path="/trial-balance" element={<TrialBalance />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
