import { IEntity } from '../Interfaces/IEntity';
import Card from '@mui/material/Card';
import { CardActions, CardContent, CardMedia, Typography } from '@mui/material';
import { Link } from 'react-router-dom';

const EntityCard = ({ name, lastName, id }: IEntity) => {
  return (
    <Card sx={{ maxWidth: '100%' }}>
      <CardMedia
        component="img"
        height="250"
        image="https://picsum.photos/400/250"
        alt="green iguana"
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {`${name} ${lastName}`}
        </Typography>
      </CardContent>
      <CardActions>
        <Link to={`doctor/${id}`} tabIndex={0}>
          Learn More
        </Link>
      </CardActions>
    </Card>
  );
};

export default EntityCard;
