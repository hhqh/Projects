import './css/Note.css';
import addIcon from './assets/add.png';

function NewNote({setShown}) {
    const handleNewClick = () => {
        setShown(current => !current);
    };

    return (           
        <div className="note-body">
            <div>
                <input className="add-btn" type="image" src={addIcon} alt="Submit" onClick={handleNewClick}></input>
                <p align="center">Add Note </p>
            </div>
        </div>
    )
}

export default NewNote;