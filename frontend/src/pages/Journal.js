
import React, { useState, useEffect } from 'react';
import CreateJournalEntry from '../components/journal/CreateJournalEntry';
import JournalEntryList from '../components/journal/JournalEntryList';
import { getJournalEntries } from '../services/api';

const Journal = () => {
  const [entries, setEntries] = useState([]);

  const fetchJournalEntries = async () => {
    try {
      const response = await getJournalEntries();
      setEntries(response.data);
    } catch (error) {
      console.error('Error fetching journal entries:', error);
    }
  };

  useEffect(() => {
    fetchJournalEntries();
  }, []);

  return (
    <div>
      <h1>Journal</h1>
      <CreateJournalEntry onJournalEntryCreated={fetchJournalEntries} />
      <JournalEntryList entries={entries} />
    </div>
  );
};

export default Journal;
