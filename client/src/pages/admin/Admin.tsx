import { Button, Typography } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { useQuery } from '@tanstack/react-query';
import { useState } from 'react';
import { useUser } from '../../components/UserContext';
import { IEntity } from '../../Interfaces/IEntity';
import ax from '../../utilities/Axios';
import { Role } from '../../utilities/Enums';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', flex: 0.3 },
  { field: 'name', headerName: 'First name', flex: 1 },
  { field: 'lastName', headerName: 'Last name', flex: 1 },
  {
    field: 'email',
    headerName: 'Email',
    flex: 3,
  },
];

const Admin = () => {
  const {
    refetch: refetchMedics,
    isLoading: isMedicLoading,
    isSuccess: isMedicSuccess,
    data: medics,
  } = useQuery<IEntity[], Error>(['adminDoctors'], async () => {
    const doctors = await ax.get(`medic`);
    return doctors.data;
  });

  const {
    refetch: refetchPatients,
    isLoading: isPatientLoading,
    isSuccess: isPatientSuccess,
    data: patients,
  } = useQuery<IEntity[], Error>(['adminPatients'], async () => {
    const patients = await ax.get(`patient`);
    return patients.data;
  });

  const [selectedMedicsIds, setSelectedMedicsIds] = useState<number[]>([]);
  const [selectedPatientsIds, setSelectedPatientsIds] = useState<number[]>([]);
  const userSuit = useUser();

  if (userSuit?.user?.role !== Role.Admin) {
    window.location.href = '/login';
  }

  if (isMedicLoading || isPatientLoading) {
    return <div>Loading...</div>;
  }

  const deleteMedic = async () => {
    if (selectedMedicsIds.length !== 1) {
      return;
    }

    const response = await ax.delete(`medic/${selectedMedicsIds[0]}`);
    if (response.status === 200) {
      refetchMedics();
    }
  };

  const deletePatient = async () => {
    if (selectedPatientsIds.length !== 1) {
      return;
    }

    const response = await ax.delete(`medic/${selectedPatientsIds[0]}`);
    if (response.status === 200) {
      refetchPatients();
    }
  };

  const updateMedic = () => {
    if (selectedMedicsIds.length !== 1) {
      return;
    }

    window.location.href = `doctor/${selectedMedicsIds[0]}`;
  };

  const updatePatient = () => {
    if (selectedPatientsIds.length !== 1) {
      return;
    }

    window.location.href = `doctor/${selectedPatientsIds[0]}`;
  };

  return (
    <>
      <Typography sx={{ mt: 5, mb: 3 }} variant="h1">
        Admin panel
      </Typography>
      {isMedicSuccess && (
        <section>
          <Typography sx={{ mt: 5, mb: 3 }} variant="h2">
            Medics:
          </Typography>
          <div style={{ height: 475, width: '100%' }}>
            <DataGrid
              rows={medics}
              columns={columns}
              pageSize={7}
              rowsPerPageOptions={[7]}
              onSelectionModelChange={(ids) => {
                setSelectedMedicsIds(ids as number[]);
              }}
            />
          </div>
          <Button
            sx={{ mt: 2, mr: 2, mb: 2 }}
            variant="contained"
            onClick={deleteMedic}
          >
            Delete
          </Button>
          <Button sx={{ m: 2 }} variant="contained" onClick={updateMedic}>
            Update
          </Button>
        </section>
      )}
      {isPatientSuccess && (
        <section>
          <Typography sx={{ mt: 5, mb: 3 }} variant="h2">
            Patients:
          </Typography>
          <div style={{ height: 475, width: '100%' }}>
            <DataGrid
              rows={patients}
              columns={columns}
              pageSize={7}
              rowsPerPageOptions={[7]}
              onSelectionModelChange={(ids) => {
                setSelectedPatientsIds(ids as number[]);
              }}
            />
          </div>
          <Button
            sx={{ mt: 2, mr: 2, mb: 2 }}
            variant="contained"
            onClick={deletePatient}
          >
            Delete
          </Button>
          <Button sx={{ m: 2 }} variant="contained" onClick={updatePatient}>
            Update
          </Button>
        </section>
      )}
    </>
  );
};

export default Admin;
