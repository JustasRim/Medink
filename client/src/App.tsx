import { Route, Routes } from "react-router-dom";
import Registration from "./pages/Registration";
import Login from "./pages/Login";
import MainLayout from "./layout/MainLayout";
import Home from "./pages/Home";

function App() {
  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/sign-up" element={<Registration />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </MainLayout>
  );
}

export default App;
