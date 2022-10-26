type Props = {
  children: React.ReactNode;
};

const MainLayout = ({ children }: Props) => {
  return <div className="App">{children}</div>;
};

export default MainLayout;
