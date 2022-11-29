import { createContext, useContext, useState } from 'react';
import IUser from '../Interfaces/IUser';

const userContext = createContext<UserSuit | undefined>(undefined);

type Props = {
  children: React.ReactNode;
};

type UserSuit = {
  user?: IUser;
  login: Function;
  singOut: Function;
};

export const useUser = () => {
  return useContext(userContext);
};

const UserContextProvider = ({ children }: Props) => {
  const login = (user: IUser) => {
    setUser({
      user: user,
      login: login,
      singOut: signOut,
    });
    const expiration = new Date(user.expires).toUTCString();

    document.cookie = `token=${user.token};expires=${expiration}`;
  };

  const signOut = () => {
    document.cookie = 'token=;expires=Thu, 01 Jan 1970 00:00:00 UTC;';
  };

  const [user, setUser] = useState<UserSuit>({
    login: login,
    singOut: signOut,
  });

  return <userContext.Provider value={user}>{children}</userContext.Provider>;
};

export default UserContextProvider;
