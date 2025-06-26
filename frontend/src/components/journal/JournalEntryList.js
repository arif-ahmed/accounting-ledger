
import React from 'react';

const JournalEntryList = ({ entries }) => {

  return (
    <div>
      <h3>Journal Entries</h3>
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Description</th>
            <th>Account</th>
            <th>Debit</th>
            <th>Credit</th>
          </tr>
        </thead>
        <tbody>
          {entries && entries.map((entry) => (
            <React.Fragment key={entry.id}>
              {entry.lines.map((line, index) => (
                <tr key={`${entry.id}-${index}`}>
                  <td>{index === 0 ? new Date(entry.date).toLocaleDateString('en-GB') : ''}</td>
                  <td>{index === 0 ? entry.description : ''}</td>
                  <td>{line.accountName}</td>
                  <td>{line.debit}</td>
                  <td>{line.credit}</td>
                </tr>
              ))}
            </React.Fragment>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default JournalEntryList;
