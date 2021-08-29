import Cookies from 'universal-cookie'
import config from '../config';


const cookies = new Cookies();

const isLogged = () => {
    let id = parseInt(config.currentUser.id);
    return id !== 0;
}

const setToken = token => {
    let expires = new Date()
    expires.setDate(expires.getDate() + 7)
    cookies.set('trackerToken', token, { expires: expires })
}

const getToken = () => cookies.get('trackerToken')

const clearToken = () => cookies.remove('trackerToken', { path: '/' })


export { isLogged, getToken, setToken, clearToken }
