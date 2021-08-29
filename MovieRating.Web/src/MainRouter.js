import { Route, Switch } from 'react-router-dom';
import Login from './authentication/Login';
import Home from './components/Home';

const MainRouter = () => {
    return (
        <Switch>
            <Route exact path='/' component={ Login } />
            <Route exact path='/home' component={ Home } />
        </Switch>
    );
}

export default MainRouter;
