import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5000/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getAccounts = () => {
  return apiClient.get('/Accounts');
};

export const createAccount = (accountData) => {
  return apiClient.post('/Accounts', accountData);
};

export const getJournalEntries = () => {
  return apiClient.get('/JournalEntries');
};

export const createJournalEntry = (entryData) => {
  return apiClient.post('/JournalEntries', entryData);
};

export const getTrialBalance = (params) => {
  return apiClient.get('/TrialBalance', { params });
};
