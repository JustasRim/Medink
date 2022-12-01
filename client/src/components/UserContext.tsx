import { createContext, useContext, useState, useEffect } from 'react';
import IUser from '../Interfaces/IUser';
import { getCookie } from '../utilities/CookieManager';

const userSuitContext = createContext<UserSuit | undefined>(undefined);

type Props = {
  children: React.ReactNode;
};

type UserSuit = {
  user?: IUser;
  login: Function;
  singOut: Function;
};

export const useUser = () => {
  return useContext(userSuitContext);
};

const login = (user: IUser, setUserSuit: Function) => {
  setUserSuit({
    user: user,
    login: login,
    singOut: signOut,
  });

  if (!user.expires) {
    return;
  }

  const expiration = new Date(user.expires).toUTCString();
  document.cookie = `token=${user.token};expires=${expiration}`;
  document.cookie = `role=${user.role};expires=${expiration}`;
};

const signOut = (setUserSuit: Function) => {
  document.cookie = 'token=;expires=Thu, 01 Jan 1970 00:00:00 UTC;';
  document.cookie = 'role=;expires=Thu, 01 Jan 1970 00:00:00 UTC;';
  setUserSuit({
    login: login,
    singOut: signOut,
    user: undefined,
  });
};

export const UserContextProvider = ({ children }: Props) => {
  const [userSuit, setUserSuit] = useState<UserSuit>({
    login: (usr: IUser) => login(usr, setUserSuit),
    singOut: () => signOut(setUserSuit),
    user: undefined,
  });

  useEffect(() => {
    if (userSuit.user) {
      return;
    }

    const token = getCookie('token');
    const role = getCookie('role');
    if (!token) {
      return;
    }

    setUserSuit({
      login: login,
      singOut: () => signOut(setUserSuit),
      user: {
        token: token,
        role: role,
      },
    });
  }, [userSuit]);

  return (
    <userSuitContext.Provider value={userSuit}>
      {children}
    </userSuitContext.Provider>
  );
};
