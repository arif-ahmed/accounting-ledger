
import React, { useState, useEffect } from 'react';
import { getTrialBalance } from '../services/api';

const TrialBalance = () => {
  const [accounts, setAccounts] = useState([]);
  const [sortBy, setSortBy] = useState('accountName'); // Default sort by account name
  const [sortOrder, setSortOrder] = useState('asc'); // Default sort order ascending

  useEffect(() => {
    const fetchTrialBalance = async () => {
      try {
        const response = await getTrialBalance({ sortBy, sortOrder });
        setAccounts(response.data);
      } catch (error) {
        console.error('Error fetching trial balance:', error);
      }
    };

    fetchTrialBalance();
  }, [sortBy, sortOrder]);

  const totalDebits = accounts.reduce((sum, account) => sum + account.debit, 0);
  const totalCredits = accounts.reduce((sum, account) => sum + account.credit, 0);

  return (
    <div>
      <h1>Trial Balance</h1>
      <div style={{ marginBottom: '10px' }}>
        <label htmlFor="sortBy">Sort By:</label>
        <select id="sortBy" value={sortBy} onChange={(e) => setSortBy(e.target.value)}>
          <option value="accountName">Account Name</option>
          <option value="debit">Debit</option>
          <option value="credit">Credit</option>
        </select>

        <label htmlFor="sortOrder" style={{ marginLeft: '20px' }}>Sort Order:</label>
        <select id="sortOrder" value={sortOrder} onChange={(e) => setSortOrder(e.target.value)}>
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
      </div>
      <table>
        <thead>
          <tr>
            <th onClick={() => setSortBy('accountName')}>Account {sortBy === 'accountName' ? (sortOrder === 'asc' ? '▲' : '▼') : ''}</th>
            <th onClick={() => setSortBy('debit')}>Debit {sortBy === 'debit' ? (sortOrder === 'asc' ? '▲' : '▼') : ''}</th>
            <th onClick={() => setSortBy('credit')}>Credit {sortBy === 'credit' ? (sortOrder === 'asc' ? '▲' : '▼') : ''}</th>
          </tr>
        </thead>
        <tbody>
          {accounts.map((account) => (
            <tr key={account.accountName}>
              <td>{account.accountName}</td>
              <td>{account.debit}</td>
              <td>{account.credit}</td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <td><strong>Total</strong></td>
            <td><strong>{totalDebits}</strong></td>
            <td><strong>{totalCredits}</strong></td>
          </tr>
        </tfoot>
      </table>
    </div>
  );
};

export default TrialBalance;
