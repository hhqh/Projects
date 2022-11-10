import React from 'react';
import './css/Note.css';

function NoteSearch({handleSearch}){
    return (
        <div>           
            <input onChange={(event) => handleSearch(event.target.value)}
            type='text' placeholder="Search..."/>
        </div>
    );
}

export default NoteSearch;