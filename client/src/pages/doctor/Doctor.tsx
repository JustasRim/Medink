import {
  Avatar,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Typography,
} from '@mui/material';
import { useQuery } from '@tanstack/react-query';
import { useParams } from 'react-router-dom';
import { IEntity } from '../../Interfaces/IEntity';
import ax from '../../utilities/Axios';

const Doctor = () => {
  const { id } = useParams();
  const { isLoading, isSuccess, data } = useQuery<IEntity, Error>(
    ['doctor'],
    async () => {
      const doctor = await ax.get(`medic/${id}`);
      return doctor.data;
    }
  );

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
          {`Dr. ${data.name} ${data.lastName}`}
        </Typography>
      </section>
      <section>
        <Typography sx={{ mt: 10 }} gutterBottom variant="h5" component="div">
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
  );
};

export default Doctor;
