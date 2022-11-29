import { createContext, useContext, useState, useEffect } from 'react';
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

const login = (user: IUser, setUser: Function) => {
  setUser({
    user: user,
    login: login,
    singOut: signOut,
  });

  if (!user.expires) {
    return;
  }

  const expiration = new Date(user.expires).toUTCString();
  document.cookie = `token=${user.token};expires=${expiration}`;
};

const signOut = (setUser: Function) => {
  document.cookie = 'token=;expires=Thu, 01 Jan 1970 00:00:00 UTC;';
  setUser({
    login: login,
    singOut: signOut,
    user: undefined,
  });
};

export const UserContextProvider = ({ children }: Props) => {
  const [user, setUser] = useState<UserSuit>({
    login: (usr: IUser) => login(usr, setUser),
    singOut: () => signOut(setUser),
    user: undefined,
  });

  useEffect(() => {
    if (user.user) {
      return;
    }

    const token =
      document.cookie.match('(^|;)\\s*token\\s*=\\s*([^;]+)')?.pop() || '';

    if (!token) {
      if (!window.location.pathname.includes('/login')) {
        window.location.href = '/login';
      }

      return;
    }

    setUser({
      login: login,
      singOut: () => signOut(setUser),
      user: {
        token: token,
      },
    });
  }, [user]);

  return <userContext.Provider value={user}>{children}</userContext.Provider>;
};
