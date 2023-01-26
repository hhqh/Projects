import './css/Note.css';
import delIcon from './assets/delete.png';

function Note({id, text, date, handleDeleteClick}) {
    return (
        <div className="note-body">
            {text} 
            <div className="note-footer"> 
                {date}
                <span>                   
                    <input type="image" src={delIcon} alt="Submit" onClick={() => handleDeleteClick(id)} align="right"></input>
                </span>
            </div>           
        </div>
    );
}
    
export default Note;