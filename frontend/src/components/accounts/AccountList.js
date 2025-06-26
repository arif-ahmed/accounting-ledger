
import React from 'react';

const AccountList = ({ accounts }) => {

  return (
    <div>
      <h3>Account List</h3>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Type</th>
          </tr>
        </thead>
        <tbody>
          {accounts && accounts.map((account) => (
            <tr key={account.id}>
              <td>{account.id}</td>
              <td>{account.name}</td>
              <td>{account.type}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AccountList;
