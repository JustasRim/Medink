import { useQuery } from '@tanstack/react-query';
import EntityCard from '../../components/EntityCard';
import { IEntity } from '../../Interfaces/IEntity';
import ax from '../../utilities/Axios';
import './home.scss';

const Home = () => {
  const { isLoading, isSuccess, data } = useQuery(['doctors'], () => {
    return ax.get('medic');
  });

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <section className="doctors">
      {isSuccess &&
        data?.data.map((entity: IEntity) => (
          <EntityCard key={entity.id} {...entity} />
        ))}
    </section>
  );
};

export default Home;
