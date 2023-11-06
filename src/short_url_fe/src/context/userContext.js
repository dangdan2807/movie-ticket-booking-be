import React, { useState } from 'react';

const UserContext = React.createContext({ name: '', phone: '', auth: false });

const UserProvider = ({ children }) => {
  const [user, setUser] = useState({ name: '', phone: '', auth: false });
  const [currentPage, setCurrentPage] = useState('home');
  const [activeItemVerticalMenu, setActiveItemVerticalMenu] = useState('home');

  const loginContext = (name, phone) => {
    setUser({
      name,
      phone,
      auth: true,
    });
  };

  const logout = () => {
    localStorage.removeItem('token');
    setUser({
      name: '',
      phone: '',
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
