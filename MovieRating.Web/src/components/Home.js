import React, { useState, useEffect } from 'react';
import service from '../services/apiService';
import { Layout, Tabs, message, Button } from 'antd';

import config from '../config';
import MovieList from '../components/MovieList';

const { Content } = Layout;
const { TabPane } = Tabs;

const Home = () => {
    const [movies, setMovies] = useState([]);
    const [page, setPage] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [url, setUrl] = useState("movies");


    const loadMore = () => {
        setPage((page) => page + 1);
    };


    const tabSwitch = (key) => {
        if (key == 1) setUrl(`movies/topmovies?pageNumber=${page}&pageSize=10`);
        if (key == 2) setUrl(`movies/topshows?pageNumber=${page}&pageSize=10`);
    }


    useEffect(() => {
        const loadMovies = async () => {
            try
            {
                setIsLoading(true);
                const result = await service.read(url);
                // setMovies((movies) => [...movies, ...result]);
                setMovies([...movies, ...result]);
            }
            catch (error)
            {
                message.error(config.errorMessage);
                setIsLoading(false);
            }
        };

        loadMovies();
    }, [page, url]);


    if (isLoading) return <div>Loading...</div>
    return (
        <Layout>
            <Content>
                <Tabs defaultActiveKey="1" onChange={ tabSwitch }>
                    <TabPane tab="Top Movies" key="1">
                        <MovieList movies={ movies } />
                        <Button onClick={ loadMore }>{ isLoading ? 'Loading...' : 'Load More' }</Button>
                    </TabPane>
                    <TabPane tab="Top Shows" key="2">
                        List of top shows
                    </TabPane>
                </Tabs>
            </Content>
        </Layout>
    );
};

export default Home;
