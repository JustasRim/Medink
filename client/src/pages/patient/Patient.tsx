import { yupResolver } from '@hookform/resolvers/yup';
import {
  Avatar,
  Button,
  FormControl,
  InputLabel,
  List,
  ListItem,
  ListItemAvatar,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { useQuery } from '@tanstack/react-query';
import { useForm } from 'react-hook-form';
import { useParams } from 'react-router-dom';
import { useUser } from '../../components/UserContext';
import { IEntity, IPatient } from '../../Interfaces/IEntity';
import ax from '../../utilities/Axios';
import { Role } from '../../utilities/Enums';
import * as yup from 'yup';
import { useState } from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

const Patient = () => {
  const { id } = useParams();
  const { isLoading, isSuccess, data } = useQuery<IPatient, Error>(
    ['patient'],
    async () => {
      const patient = await ax.get(`patient/${id}`);
      return patient.data;
    }
  );

  const { data: doctorsData } = useQuery(['doctors'], async () => {
    const medics = await ax.get('medic');
    return medics.data;
  });

  const { refetch, data: symptomData } = useQuery(['symptoms'], async () => {
    const symptom = await ax.get('symptom');
    const filtered = symptom.data.filter(
      (q: any) => q.patientId === Number(id)
    );
    return filtered;
  });

  const [selectedPatientsIds, setSelectedPatientsIds] = useState<number[]>([]);
  const [symptomName, setSymptomName] = useState<string>();
  const [description, setDescription] = useState<string>();

  const userSuit = useUser();

  const schema = yup.object().shape({
    email: yup.string().email().required(),
    phone: yup.string().optional(),
    medicId: yup.number().optional(),
  });

  type inputs = {
    name: string;
    lastName: string;
    email: string;
    number: string;
    medicId: number;
    id: number;
  };

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<inputs>({
    resolver: yupResolver(schema),
  });

  const onSubmit = async (data: inputs) => {
    const response = await ax.put('/patient', data);
    if (response.status === 200) {
    }
  };

  const handleSymptom = async () => {
    await ax.post('symptom', {
      name: symptomName,
      description: description,
      patientId: id,
    });

    refetch();
  };

  const deleteSymptom = async () => {
    if (selectedPatientsIds.length !== 1) {
      return;
    }

    const response = await ax.delete(`symptom/${selectedPatientsIds[0]}`);
    if (response.status === 200) {
      refetch();
    }
  };

  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', flex: 0.3 },
    { field: 'name', headerName: 'Name', flex: 1 },
    { field: 'description', headerName: 'Description', flex: 1 },
  ];

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!isSuccess) {
    return <div>Error...</div>;
  }

  if (
    !(
      userSuit?.user?.role === Role.Admin ||
      userSuit?.user?.role === Role.Patient
    )
  ) {
    return <div>To see content login...</div>;
  }

  return (
    <>
      <section>
        <Typography sx={{ mt: 10 }} variant="h1" component="div">
          {`${data.name} ${data.lastName}`}
        </Typography>
      </section>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Typography sx={{ mt: 10 }} gutterBottom variant="h5" component="div">
          Ameniniai duomenys:
        </Typography>
        <List sx={{ width: '100%', maxWidth: 360 }}>
          <ListItem>
            <ListItemAvatar>
              <Avatar></Avatar>
            </ListItemAvatar>
            <TextField
              defaultValue={data.email}
              variant="outlined"
              {...register('email')}
              sx={{ width: '100%' }}
              error={!!errors.email}
            />
          </ListItem>
          <ListItem>
            <ListItemAvatar>
              <Avatar></Avatar>
            </ListItemAvatar>
            <TextField
              defaultValue={data.number}
              variant="outlined"
              {...register('number')}
              sx={{ width: '100%' }}
              error={!!errors.number}
            />
          </ListItem>
          <ListItem>
            <FormControl fullWidth>
              <InputLabel id="select-label">Medikas</InputLabel>
              <Select
                labelId="select-label"
                id="select"
                defaultValue={data.medicId}
                label="Age"
                {...register('medicId')}
              >
                {doctorsData?.map((doctor: IEntity) => {
                  return (
                    <MenuItem key={doctor.id} value={doctor.id}>
                      {doctor.name}
                    </MenuItem>
                  );
                })}
              </Select>
            </FormControl>
          </ListItem>
        </List>
        <TextField value={data.name} type="hidden" {...register('name')} />
        <TextField
          value={data.lastName}
          type="hidden"
          {...register('lastName')}
        />
        <TextField value={data.id} type="hidden" {...register('id')} />
        <Button type="submit" variant="contained">
          Submit
        </Button>
      </form>
      <section>
        <Typography sx={{ mt: 10, mb: 5 }} variant="h5" component="div">
          Symptoms
        </Typography>
        <InputLabel id="symptom">Symptom you are feeling now</InputLabel>
        <TextField
          variant="outlined"
          value={symptomName}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            setSymptomName(e.target.value)
          }
          sx={{ width: '100%', mb: 2 }}
        />
        <InputLabel id="symptom">Description</InputLabel>
        <TextField
          variant="outlined"
          value={description}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            setDescription(e.target.value)
          }
          sx={{ width: '100%', mb: 2 }}
        />
        <Button variant="contained" sx={{ mb: 2 }} onClick={handleSymptom}>
          Submit Symptom
        </Button>
      </section>
      <section>
        <div style={{ height: 475, width: '100%' }}>
          <DataGrid
            rows={symptomData}
            columns={columns}
            pageSize={7}
            rowsPerPageOptions={[7]}
            onSelectionModelChange={(ids) => {
              setSelectedPatientsIds(ids as number[]);
            }}
          />
        </div>
        <Button
          variant="contained"
          sx={{ mb: 2, mt: 2 }}
          onClick={deleteSymptom}
        >
          Delete Symptom
        </Button>
      </section>
    </>
  );
};

export default Patient;
