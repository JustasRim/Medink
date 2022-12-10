import { createContext, useContext, useState, useEffect } from 'react';
import IUser from '../Interfaces/IUser';
import { getCookie } from '../utilities/CookieManager';

const userSuitContext = createContext<UserSuit | undefined>(undefined);

type Props = {
  children: React.ReactNode;
};

type UserSuit = {
  user?: IUser;
  login: (user: IUser) => void;
  singOut: () => void;
};

export const useUser = () => {
  return useContext(userSuitContext);
};

const login = (
  user: IUser,
  setUserSuit: (val: (cur: UserSuit) => void) => void
) => {
  setUserSuit((current: UserSuit) => ({
    login: current.login,
    signOut: current.singOut,
    user: user,
  }));

  if (!user.expires) {
    return;
  }

  const expiration = new Date(user.expires).toUTCString();
  document.cookie = `token=${user.token};expires=${expiration}`;
  document.cookie = `role=${user.role};expires=${expiration}`;
};

const signOut = (setUserSuit: (val: (cur: UserSuit) => void) => void) => {
  document.cookie = 'token=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/;';
  document.cookie = 'role=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/;';
  setUserSuit((current: UserSuit) => ({
    login: current.login,
    signOut: current.singOut,
    user: undefined,
  }));
};

export const UserContextProvider = ({ children }: Props) => {
  const [userSuit, setUserSuit] = useState<UserSuit>({
    login: (usr: IUser) =>
      login(usr, setUserSuit as (val: (cur: UserSuit) => void) => void),
    singOut: () =>
      signOut(setUserSuit as (val: (cur: UserSuit) => void) => void),
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

    setUserSuit((current: UserSuit) => {
      return {
        ...current,
        user: {
          token: token,
          role: role,
        },
      };
    });
  }, [userSuit.user]);

  return (
    <userSuitContext.Provider value={userSuit}>
      {children}
    </userSuitContext.Provider>
  );
};
