import { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import { Header, List, ListItem } from 'semantic-ui-react';

interface Activity {
  id: number;
  title: string;
}

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios.get('http://localhost:5000/api/activities')
      .then(response => {
        console.log(response);
        setActivities(response.data);
      })
      .catch(error => {
        console.error('There was an error fetching the activities!', error);
      });
  }, []);

  return (
    <div>
      <Header as='h2' icon='users' content='Reactivities' />
      <List>
        {activities.map((activity: Activity) => (
          <ListItem key={activity.id}>{activity.title}</ListItem>
        ))}
      </List>
    </div>
  );
}

export default App;