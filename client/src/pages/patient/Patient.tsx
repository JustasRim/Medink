import { yupResolver } from '@hookform/resolvers/yup';
import {
  Avatar,
  Button,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  TextField,
  Typography,
} from '@mui/material';
import { useQuery } from '@tanstack/react-query';
import { useForm } from 'react-hook-form';
import { useParams } from 'react-router-dom';
import { useUser } from '../../components/UserContext';
import { IEntity } from '../../Interfaces/IEntity';
import ax from '../../utilities/Axios';
import { Role } from '../../utilities/Enums';
import * as yup from 'yup';

const Patient = () => {
  const { id } = useParams();
  const { isLoading, isSuccess, data } = useQuery<IEntity, Error>(
    ['patient'],
    async () => {
      const patient = await ax.get(`patient/${id}`);
      return patient.data;
    }
  );

  const userSuit = useUser();

  const schema = yup.object().shape({
    email: yup.string().email().required(),
    phone: yup.string().optional(),
  });

  type inputs = {
    name: string;
    lastName: string;
    email: string;
    number: string;
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

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!isSuccess) {
    return <div>Error...</div>;
  }

  return (
    <>
      <section>
        <Typography sx={{ mt: 10 }} variant="h1" component="div">
          {`${data.name} ${data.lastName}`}
        </Typography>
      </section>
      {userSuit?.user?.role === Role.Admin ? (
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
      ) : (
        <>
          <section>
            <Typography
              sx={{ mt: 10 }}
              gutterBottom
              variant="h5"
              component="div"
            >
              Ameniniai duomenys:
            </Typography>
            <List sx={{ width: '100%', maxWidth: 360 }}>
              <ListItem>
                <ListItemAvatar>
                  <Avatar></Avatar>
                </ListItemAvatar>
                <ListItemText primary={data.email} />
              </ListItem>
              <ListItem>
                <ListItemAvatar>
                  <Avatar></Avatar>
                </ListItemAvatar>
                <ListItemText primary={data.number} />
              </ListItem>
            </List>
          </section>
        </>
      )}
    </>
  );
};

export default Patient;
