import React, { useState, useEffect } from 'react';
import CreateAccount from '../components/accounts/CreateAccount';
import AccountList from '../components/accounts/AccountList';
import { getAccounts } from '../services/api';

const Accounts = () => {
  const [accounts, setAccounts] = useState([]);

  const fetchAccounts = async () => {
    try {
      const response = await getAccounts();
      setAccounts(response.data);
    } catch (error) {
      console.error('Error fetching accounts:', error);
    }
  };

  useEffect(() => {
    fetchAccounts();
  }, []);

  return (
    <div>
      <h1>Accounts</h1>
      <CreateAccount onAccountCreated={fetchAccounts} />
      <AccountList accounts={accounts} />
    </div>
  );
};

export default Accounts;