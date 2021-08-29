import '../assets/styles/menu.css';
import { Menu } from 'antd';
import { withRouter } from 'react-router';
import config from '../config';

const { SubMenu } = Menu;

const Navigator = ({ history }) => {

    const handleClick = e => {
        console.log('click:', e.key);
        history.push(e.key);
    };

    return (
        <Menu onClick={ handleClick } mode='horizontal'>
            <Menu.Item key='/home'>Home</Menu.Item>
            <SubMenu title={ config.currentUser.name } type='default'>
                <Menu.Item key='/logout'>Logout</Menu.Item>
            </SubMenu>
        </Menu>
    );
}

export default withRouter(Navigator);
