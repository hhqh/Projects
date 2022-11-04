import {useState} from 'react';
import saveIcon from './assets/save.png';

function EditNote({handleAddNote, setShown}){
    const characterLimit = 400;
    const [content, setContent] = useState('');

    const handleCreate = (event) => {
        if (content.length < characterLimit){
            setContent(event.target.value);
        }     
    };

    const handleCreateClick = () => {
        if (content.length > 0){
            setShown(current => !current);
            handleAddNote(content);
            setContent(''); 
        }
    }
    return (
        <div className="note-body">
            <textarea 
                placeholder="Type something..."
                value={content}
                onChange={handleCreate}>
            </textarea>
            <div className="note-footer">
                {characterLimit - content.length} Remaining
                <input type="image" src={saveIcon} alt="Submit" onClick={handleCreateClick} align="right"></input>
            </div> 
        </div>
    )
}

export default EditNote;