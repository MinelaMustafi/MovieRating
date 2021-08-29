import React, { useEffect } from 'react';

import config from '../config';
import { clearToken } from '../services/authService';

export default function Logout ({ history }) {

    useEffect(() => {
        let isCancelled = false;
        const eraseToken = () => {
            try
            {
                if (!isCancelled)
                {
                    config.currentUser = { id: 0, name: '', role: '', landing: '' };
                    config.token = '';
                    clearToken();
                    history.push('/login');
                }
            } catch (error)
            {
                if (!isCancelled)
                {
                    throw error;
                }
            }
        }
        eraseToken();
    }, [history]);

    return <div />
}
