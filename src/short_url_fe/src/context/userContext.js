import React, { useState } from 'react';

const UserContext = React.createContext({ name: '', email: '', auth: false, isAdmin: false });

const UserProvider = ({ children }) => {
  const [user, setUser] = useState({ name: '', email: '', auth: false, isAdmin: false });
  const [currentPage, setCurrentPage] = useState('home');
  const [activeItemVerticalMenu, setActiveItemVerticalMenu] = useState('home');

  const loginContext = (name, email, isAdmin) => {
    setUser({
      name,
      email,
      auth: true,
      isAdmin: isAdmin ?? false,
    });
  };

  const logout = () => {
    localStorage.removeItem('token');
    setUser({
      name: '',
      email: '',
      auth: false,
      isAdmin: false,
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
