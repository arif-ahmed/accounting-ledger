
import React, { useState, useEffect } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { getTrialBalance } from '../services/api';
import './Dashboard.css';

const Dashboard = () => {
  const [trialBalanceData, setTrialBalanceData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchTrialBalance = async () => {
      try {
        const response = await getTrialBalance();
        // Prepare data for recharts
        const chartData = response.data.map(account => ({
          name: account.accountName,
          debit: account.debit,
          credit: account.credit,
        }));
        setTrialBalanceData(chartData);
      } catch (err) {
        setError('Failed to load trial balance data.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchTrialBalance();
  }, []);

  if (loading) {
    return <div>Loading dashboard...</div>;
  }

  if (error) {
    return <div style={{ color: 'red' }}>{error}</div>;
  }

  return (
    <div>
      <h1>Dashboard</h1>
      <div className="dashboard">
        <div className="card">
          <h2>Trial Balance Overview</h2>
          <ResponsiveContainer width="100%" height={300}>
            <BarChart
              data={trialBalanceData}
              margin={{
                top: 20, right: 30, left: 20, bottom: 5,
              }}
            >
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="debit" fill="#8884d8" />
              <Bar dataKey="credit" fill="#82ca9d" />
            </BarChart>
          </ResponsiveContainer>
        </div>
        <div className="card">
          <h2>Recent Transactions</h2>
          {/* Add recent transactions component here */}
        </div>
        <div className="card">
          <h2>Account Balances</h2>
          {/* Add account balances component here */}
        </div>
        <div className="card">
          <h2>Journal Entries</h2>
          {/* Add recent journal entries component here */}
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
