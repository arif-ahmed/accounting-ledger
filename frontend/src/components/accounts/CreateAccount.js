
import React, { useState } from 'react';
import { createAccount } from '../../services/api';
import '../Form.css';

const CreateAccount = ({ onAccountCreated }) => {
  const [accountName, setAccountName] = useState('');
  const [accountType, setAccountType] = useState('asset');
  const [nameError, setNameError] = useState('');
  const [apiError, setApiError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Reset errors
    setNameError('');
    setApiError('');

    // Client-side validation
    if (!accountName.trim()) {
      setNameError('Account Name cannot be empty.');
      return;
    }

    setLoading(true);
    try {
      await createAccount({ name: accountName, type: accountType });
      onAccountCreated();
      setAccountName('');
      setAccountType('asset');
    } catch (error) {
      console.error('Error creating account:', error);
      setApiError(error.response?.data?.message || 'Failed to create account. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="form-container">
      <h3>Create New Account</h3>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Account Name:</label>
          <input
            type="text"
            value={accountName}
            onChange={(e) => {
              setAccountName(e.target.value);
              setNameError(''); // Clear error on change
            }}
          />
          {nameError && <p style={{ color: 'red' }}>{nameError}</p>}
        </div>
        <div>
          <label>Account Type:</label>
          <select value={accountType} onChange={(e) => setAccountType(e.target.value)}>
            <option value="asset">Asset</option>
            <option value="liability">Liability</option>
            <option value="equity">Equity</option>
            <option value="revenue">Revenue</option>
            <option value="expense">Expense</option>
          </select>
        </div>
        {apiError && <p style={{ color: 'red' }}>{apiError}</p>}
        <button type="submit" disabled={loading}>
          {loading ? 'Creating...' : 'Create Account'}
        </button>
      </form>
    </div>
  );
};

export default CreateAccount;
