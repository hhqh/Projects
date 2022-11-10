import './css/Note.css';
import addIcon from './assets/add.png';

function NewNote({modal}) {
    return (           
        <div className="add-bar">
            <input className="add-btn" type="image"  src={addIcon} alt="Submit" onClick={() => modal(true)}></input>
        </div>
    )
}

export default NewNote;