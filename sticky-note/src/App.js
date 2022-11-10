import './App.css';
import Note from './components/Note';
import NewNote from './components/NewNote';
import Modal from './components/NoteModal';
import Search from './components/NoteSearch';
import {useState} from 'react';
import {nanoid} from 'nanoid';

function App() {
  const localDate = new Date();
  const [isShownModal, showModal] = useState(false);
  const [notes, setNotes] = useState([]);
  const [content, searchNote] = useState('');

  const addNote = (text) => {
    const newNote = {
      id: nanoid(),
      text: text,
      date: localDate.toLocaleDateString()
    };
    const newNotes = [...notes, newNote];
    setNotes(newNotes);
  }

  const deleteNote = (id) => {
    const newNotes = notes.filter((note) => note.id !== id);
    setNotes(newNotes);
  }

  return (
    <div>
      <NewNote modal={showModal}/>
      <Search handleSearch={searchNote}/>
      <div className='notes-grid'>      
        { notes.filter((note) => note.text.toLowerCase().includes(content.toLowerCase())).map((note) => (
            <Note
                id={note.id}
                text={note.text}
                date={note.date}
                modal={showModal}
                handleDeleteClick={deleteNote}
            /> ))}
        <Modal handleAddNote={addNote} open={isShownModal} onClose={() => showModal(false)}/>           
      </div>     
    </div>
  ); 
}

export default App;
