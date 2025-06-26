
import React, { useState, useEffect } from 'react';
import { createJournalEntry, getAccounts } from '../../services/api';
import '../Form.css';

const CreateJournalEntry = ({ onJournalEntryCreated }) => {
  const [entry, setEntry] = useState({
    date: '',
    description: '',
    lines: [
      { accountId: '', amount: '', type: 'debit' },
      { accountId: '', amount: '', type: 'credit' },
    ],
  });
  const [accounts, setAccounts] = useState([]);
  const [formErrors, setFormErrors] = useState({});
  const [apiError, setApiError] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        const response = await getAccounts();
        setAccounts(response.data);
      } catch (error) {
        console.error('Error fetching accounts:', error);
      }
    };

    fetchAccounts();
  }, []);

  const handleChange = (e, index) => {
    const { name, value } = e.target;
    const newLines = [...entry.lines];
    if (name === 'amount') {
      newLines[index].amount = value;
    } else if (name === 'type') {
      newLines[index].type = value;
      // Clear both debit and credit when type changes
      newLines[index].debit = 0;
      newLines[index].credit = 0;
    } else {
      newLines[index][name] = value;
    }
    setEntry({ ...entry, lines: newLines });
    setFormErrors({}); // Clear form errors on change
  };

  const addLine = () => {
    setEntry({ ...entry, lines: [...entry.lines, { accountId: '', amount: '', type: 'debit' }] });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    setFormErrors({});
    setApiError('');

    const errors = {};
    if (!entry.date) {
      errors.date = 'Date is required.';
    }
    if (!entry.description.trim()) {
      errors.description = 'Description is required.';
    }

    entry.lines.forEach((line, index) => {
      if (!line.accountId) {
        errors[`line-${index}-accountId`] = 'Account is required.';
      }
      if (!line.amount || isNaN(parseFloat(line.amount))) {
        errors[`line-${index}-amount`] = 'Amount must be a valid number.';
      }
    });

    if (Object.keys(errors).length > 0) {
      setFormErrors(errors);
      return;
    }

    setLoading(true);
    const journalEntryToSend = {
      ...entry,
      lines: entry.lines.map(line => ({
        accountId: line.accountId,
        debit: line.type === 'debit' ? parseFloat(line.amount) : 0,
        credit: line.type === 'credit' ? parseFloat(line.amount) : 0,
      })),
    };
    try {
      await createJournalEntry(journalEntryToSend);
      onJournalEntryCreated();
      setEntry({
        date: '',
        description: '',
        lines: [
          { accountId: '', amount: '', type: 'debit' },
          { accountId: '', amount: '', type: 'credit' },
        ],
      });
    } catch (error) {
      console.error('Error creating journal entry:', error);
      setApiError(error.response?.data?.message || 'Failed to create journal entry. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="form-container">
      <h3>Create New Journal Entry</h3>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Date:</label>
          <input type="date" name="date" value={entry.date} onChange={(e) => setEntry({ ...entry, date: e.target.value })} />
          {formErrors.date && <p style={{ color: 'red' }}>{formErrors.date}</p>}
        </div>
        <div>
          <label>Description:</label>
          <input type="text" name="description" placeholder="Description" value={entry.description} onChange={(e) => setEntry({ ...entry, description: e.target.value })} />
          {formErrors.description && <p style={{ color: 'red' }}>{formErrors.description}</p>}
        </div>
        {entry.lines.map((line, index) => (
          <div key={index}>
            <select name="accountId" value={line.accountId} onChange={(e) => handleChange(e, index)}>
              <option value="">Select Account</option>
              {accounts.map((account) => (
                <option key={account.id} value={account.id}>
                  {account.name}
                </option>
              ))}
            </select>
            {formErrors[`line-${index}-accountId`] && <p style={{ color: 'red' }}>{formErrors[`line-${index}-accountId`]}</p>}
            <input type="number" name="amount" placeholder="Amount" value={line.amount} onChange={(e) => handleChange(e, index)} />
            {formErrors[`line-${index}-amount`] && <p style={{ color: 'red' }}>{formErrors[`line-${index}-amount`]}</p>}
            <select name="type" value={line.type} onChange={(e) => handleChange(e, index)}>
              <option value="debit">Debit</option>
              <option value="credit">Credit</option>
            </select>
          </div>
        ))}
        <button type="button" onClick={addLine}>Add Line</button>
        {apiError && <p style={{ color: 'red' }}>{apiError}</p>}
        <button type="submit" disabled={loading}>
          {loading ? 'Creating...' : 'Create Entry'}
        </button>
      </form>
    </div>
  );
};

export default CreateJournalEntry;
