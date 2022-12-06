import { Route, Routes } from 'react-router-dom';
import Registration from './pages/Registration';
import Login from './pages/Login';
import MainLayout from './layout/MainLayout';
import Home from './pages/home/Home';
import Doctor from './pages/doctor/Doctor';
import Admin from './pages/admin/Admin';
import Patient from './pages/patient/Patient';

function App() {
  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/sign-up" element={<Registration />} />
        <Route path="/login" element={<Login />} />
        <Route path="/doctor/:id" element={<Doctor />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/patient/:id" element={<Patient />} />
      </Routes>
    </MainLayout>
  );
}
export default App;
