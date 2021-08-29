import React from 'react';
import Movie from './Movie';

const MovieList = ({ movies }) => {
    return (
        <div>
            { movies.map((movie) => {
                return <Movie key={ movie.id } movie={ movie } />
            }) }
        </div>
    );
};

export default MovieList;
