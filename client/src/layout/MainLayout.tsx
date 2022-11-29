import { Button } from '@mui/material';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useUser } from '../components/UserContext';
import './layout.scss';

type Props = {
  children: React.ReactNode;
};

const MainLayout = ({ children }: Props) => {
  const [expanded, setExpanded] = useState(false);

  const userSuit = useUser();
  console.log(userSuit?.user?.token);

  useEffect(() => {
    window.addEventListener('resize', onResize);
    return () => {
      window.removeEventListener('resize', onResize);
    };
  });

  const onResize = () => {
    if (window.innerWidth <= 600) {
    }

    setExpanded(() => false);
  };

  const hangleKeyPress = (event: any) => {
    if (event.keyCode === 13) {
      setExpanded((curr) => !curr);
    }
  };

  const signOut = () => {
    userSuit?.singOut();
  };

  return (
    <div className="app">
      <header className="header">
        <nav className="header__links">
          <Link className="header__link" to="/" tabIndex={0}>
            Home
          </Link>
          <Link className="header__link" to="/sign-up" tabIndex={0}>
            Register
          </Link>
          {!userSuit?.user ? (
            <Link className="header__link" to="/login" tabIndex={0}>
              Login
            </Link>
          ) : (
            <Button
              color="secondary"
              className="header__link"
              onClick={signOut}
            >
              Sign out
            </Button>
          )}
        </nav>
        <div
          className={`hamburger${expanded ? ' hamburger--expanded' : ''}`}
          role="button"
          tabIndex={0}
          onClick={() => setExpanded((curr) => !curr)}
          onKeyDown={hangleKeyPress}
          aria-label="open the menu"
        >
          <span className="hamburger__top"></span>
          <span className="hamburger__middle"></span>
          <span className="hamburger__bottom"></span>
        </div>
      </header>
      <div className={`sidebar${expanded ? ' sidebar--expanded' : ''}`}>
        <div className="sidebar__bar">
          <nav className="sidebar__nav">
            <Link className="sidebar__link" to="/" tabIndex={0}>
              Home
            </Link>
            <Link className="sidebar__link" to="/sign-up" tabIndex={0}>
              Register
            </Link>
            <Link className="sidebar__link" to="/login" tabIndex={0}>
              Login
            </Link>
          </nav>
        </div>
      </div>
      <main className="main">{children}</main>
      <footer></footer>
    </div>
  );
};

export default MainLayout;
