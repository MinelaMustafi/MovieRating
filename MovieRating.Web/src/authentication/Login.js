import React, { useState, useEffect, useCallback } from "react";
import { Form, Input, Button, Row, Col, Switch, message } from 'antd';
import { Link } from 'react-router-dom';
import config from "../config";
import service from "../services/apiService";
import { getToken, setToken } from "../services/authService";


const styles = {
    layout: {
        labelCol: { span: 8 },
        wrapperCol: { span: 16 }
    },
    formItem: {
        textAlign: 'left',
        margin: '6px 0'
    }
};


const Login = ({ history }) => {
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(true);
    const [form] = Form.useForm();


    async function fetch (credentials) {
        const result = await service.auth(credentials);
        return result;
    }

    const setCurrent = useCallback((result, remember) => {
        config.currentUser = {
            id: result.id,
            name: result.name,
            username: result.username
        };
        config.token = result.token;
        if (remember)
        {
            setToken(result.token);
        }
        if (config.attemptRoute !== '')
        {
            history.push(config.attemptRoute);
            config.attemptRoute = '';
        } else
        {
            history.push('/home');
        };
    }, [history]);


    useEffect(() => {
        async function checkToken () {
            try
            {
                const token = await getToken();
                if (token && token !== "undefined")
                {
                    const credentials = {
                        username: "token",
                        password: token,
                        remember: true,
                    };
                    let result = await fetch(credentials);
                    if (result)
                    {
                        setCurrent(result);
                    } else
                    {
                        setLoading(false);
                    }
                } else
                {
                    setLoading(false);
                }
            } catch (error)
            {
                message.error(config.errorMessage);
            }
        }
        checkToken();
    }, [setCurrent]);


    const handleSubmit = async (values) => {
        setLoading(true);
        let result = await fetch({ ...values });
        if (result)
        {
            setCurrent(result, values.remember);
        } else
        {
            setLoading(false);
            setError("Invalid credentials! Try again!");
        };
    };


    const onFinishFailed = (errorInfo) => {
        console.log("Failed:", errorInfo);
    };


    if (loading) return <div>Loading...</div>
    return (
        <div>
            <div className='login-form'>
                <div style={ { padding: '4px' } }>
                    <Form form={ form } { ...styles.layout } onFinish={ handleSubmit } onFinishFailed={ onFinishFailed }>
                        <div>{ error && <h4 style={ styles.error }>{ error }</h4> }</div>
                        <Form.Item label='Username' name='username' style={ styles.formItem }><Input autoFocus /></Form.Item>
                        <Form.Item label='Password' name='password' style={ styles.formItem }><Input.Password /></Form.Item>
                        <Form.Item label='Remember me' name="remember" valuePropName="checked" size="small" style={ styles.formItem }>
                            <Switch checkedChildren="YES" unCheckedChildren="NO" defaultChecked />
                        </Form.Item>
                        <hr />
                        <Row>
                            <Col span={ 8 } style={ { marginTop: '8px', textAlign: 'left' } }>
                                <Link to='/reset'>Reset Password</Link>
                            </Col>
                            <Col span={ 11 } align='center'>
                                <Button type='primary' htmlType='submit'>Sign In</Button>
                            </Col>
                            <Col span={ 5 } style={ { marginTop: '8px', textAlign: 'right' } }>
                                <Link to='/register'>Sign Up</Link>
                            </Col>
                        </Row>
                    </Form>
                </div>
            </div>
        </div>
    );
}

export default Login;
