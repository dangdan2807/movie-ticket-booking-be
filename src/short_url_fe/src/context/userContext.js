import React, { useState } from 'react';

const UserContext = React.createContext({ name: '', email: '', auth: false });

const UserProvider = ({ children }) => {
  const [user, setUser] = useState({ name: '', email: '', auth: false });
  const [currentPage, setCurrentPage] = useState('home');
  const [activeItemVerticalMenu, setActiveItemVerticalMenu] = useState('home');

  const loginContext = (name, email) => {
    setUser({
      name,
      email,
      auth: true,
    });
  };

  const logout = () => {
    localStorage.removeItem('token');
    setUser({
      name: '',
      email: '',
      auth: false,
    });
  };

  return (
    <UserContext.Provider
      value={{
        user,
        currentPage,
        setCurrentPage,
        activeItemVerticalMenu,
        setActiveItemVerticalMenu,
        loginContext,
        logout,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export { UserContext, UserProvider };
