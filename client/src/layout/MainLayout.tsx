import { useState } from "react";
import { Link } from "react-router-dom";
import "./layout.scss";

type Props = {
  children: React.ReactNode;
};

const MainLayout = ({ children }: Props) => {
  const [expaned, setExpaned] = useState(false);

  return (
    <div className="app">
      <header className="header">
        <div className="header__links">
          <Link className="header__link" to="/">
            Home
          </Link>
          <Link className="header__link" to="/sign-up">
            Register
          </Link>
          <Link className="header__link" to="/login">
            Login
          </Link>
        </div>
        <div
          className={`hamburger${expaned ? "" : " hamburger--expaned"}`}
          onClick={() => setExpaned((curr) => !curr)}
          aria-label="open the menu"
        >
          <span className="hamburger__top"></span>
          <span className="hamburger__middle"></span>
          <span className="hamburger__bottom"></span>
        </div>
      </header>
      {children}
      <footer></footer>
    </div>
  );
};

export default MainLayout;
