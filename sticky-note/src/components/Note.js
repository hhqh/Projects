import './css/Note.css';
import delIcon from './assets/delete.png';
import editIcon from './assets/edit.png';

function Note({id, text, date, handleDeleteClick, handleUpdateClick}) {
    return (
        <div className="note-body">
            {text} 
            <div className="note-footer"> 
                {date}
                <span>
                    <input className="del-icon" type="image" src={delIcon} alt="Submit" onClick={() => handleDeleteClick(id)} align="right"></input>
                </span>
            </div>           
        </div>
    );
}
    
export default Note;