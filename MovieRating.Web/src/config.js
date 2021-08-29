const baseUrl = 'http://localhost:5000/';

const config = {
    apiUrl: baseUrl + 'api/',
    token: '',
    currentUser: {
        id: 0,
        name: '',
        username: ''
    },
    attemptRoute: '',
    errorMessage: 'We have problems communicating with database. Please try again in 2 minutes.'
};

export default config;
