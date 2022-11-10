import {useState} from 'react';
import saveIcon from './assets/save.png';
import './css/Note.css';

function Modal({handleAddNote, open, onClose}){
    const characterLimit = 400;
    const [content, setContent] = useState('');

    const handleCreate = (event) => {
        if (content.length < characterLimit){
            setContent(event.target.value);
        }     
    };

    const handleCreateClick = () => {
        if (content.length > 0){
            handleAddNote(content);
            setContent(''); 
        }
    }

    if (!open) {
        return null;
    }

    return (
        <div className='overlay'>
            <div className='modal-body'>
                <label onClick={onClose}>X</label>
                <textarea type="text" placeholder="Content" value={content} onChange={handleCreate}/>
                <div className='note-footer'>
                    {characterLimit - content.length} Remaining
                    <input type="image" src={saveIcon} alt="Submit" onClick={handleCreateClick} align="right"/>
                </div>
            </div>
        </div>
    );
}

export default Modal;