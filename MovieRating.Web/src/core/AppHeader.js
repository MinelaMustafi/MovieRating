import { withRouter } from 'react-router';
import "../assets/styles/index.css";
import "../App.css";
import config from '../config';
import Navigator from './Navigator';

const styles = {
    title: {
        textAlign: 'left',
        fontSize: '1.7em',
        fontWeight: '400',
        marginTop: '8px',
        paddingLeft: '5px'
    }
};

const AppHeader = () => {

    if (!config.currentUser.id)
    {
        return (
            <div>
                <div style={ styles.title }>Movie Rating Engine</div>
            </div>
        );
    }

    return (
        <header>
            <div className='left'>
                <div style={ styles.title }>Movie Rating Engine</div>
            </div>
            <div className='right'>
                <Navigator />
            </div>
        </header>);
}

export default withRouter(AppHeader);
