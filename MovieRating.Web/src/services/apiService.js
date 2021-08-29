import axios from 'axios';
import config from '../config';


const headers = () => {
    return {
        'content-type': 'application/json; charset=utf8',
        'pragma': 'no-cache, no-store, must-revalidate',
        'cache-control': 'no-cache',
        'expires': '0',
        'accept': 'application/json',
        'authorization': `Bearer ${config.token}`,
        'headerModel': JSON.stringify(config.headers)
    };
};


const auth = async function (credentials) {
    return await request('post', `${config.apiUrl}login`, credentials, headers());
};


const list = async function (dataSet) {
    let url = `${config.apiUrl}${dataSet}`
    if (config.filter) url += `?filter=${config.filter}`;
    return await request('get', url, {}, headers());
};


const read = async function (dataSet, id) {
    return await request(
        'get',
        `${config.apiUrl}${dataSet}/${id}`,
        {},
        headers()
    );
};


const insert = async function (dataSet, data) {
    return await request('post', `${config.apiUrl}${dataSet}`, data, headers());
};


const update = async function (dataSet, data, id) {
    return await request(
        'put',
        `${config.apiUrl}${dataSet}/${id}`,
        data,
        headers()
    );
};


const remove = async function (dataSet, id) {
    return await request(
        'delete',
        `${config.apiUrl}${dataSet}/${id}`,
        {},
        headers()
    );
};


const request = async function (method, url, data, headers) {
    try
    {
        const res = await axios({
            method: method,
            url: url,
            data: data,
            headers: headers
        });
        let hdr = res.headers.headermodel;
        if (hdr !== undefined)
        {
            config.headers = JSON.parse(hdr);
        }
        if (res.status === 207 || res.status === 204)
        {
            return { status: 200, message: res.data };
        } else
        {
            return res.data;
        }
    } catch (err)
    {
        console.log(err);
        return false;
    }
};


export default { auth, list, read, insert, update, remove, headers };
