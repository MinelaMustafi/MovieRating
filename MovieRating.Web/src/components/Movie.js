import '../assets/styles/index.css';
import React from 'react';

const Movie = ({ movie }) => {
    return (
        <div>
            <img />
            <div className="right">
                <div>
                    <strong>Title:</strong> { movie.title }
                </div>
                <div>
                    <strong>Description:</strong> { movie.description }
                </div>
                <div>
                    <strong>Released:</strong> { movie.released }
                </div>
                <div>
                    <strong>Rating:</strong> { movie.averageRating }
                </div>
            </div>
        </div>
    );
};

export default Movie;
