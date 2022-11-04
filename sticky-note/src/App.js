import './App.css';
import Note from './components/Note';
import NewNote from './components/NewNote';
import EditNote from './components/EditNote';
import {useState} from 'react';
import {nanoid} from 'nanoid';

function App() {
  const localDate = new Date();
  const [isShown, setIsShown] = useState(false);
  const [canUpdate, setUpdateNote] = useState(false);
  const [notes, setNotes] = useState([]);

  const addNote = (text) => {
    const newNote = {
      id: nanoid(),
      text: text,
      date: localDate.toLocaleDateString()
    };
    const newNotes = [...notes, newNote];
    setNotes(newNotes);
  }

  const updateNote = (id, text) => {
    const filteredNotes = notes.filter(note => note.id !== id);
    const updatedNote = notes.filter(note => note.id == id)[0];
    updatedNote.text = text;
    setNotes([...filteredNotes, updatedNote]);
  }

  const deleteNote = (id) => {
    const newNotes = notes.filter((note) => note.id !== id);
    setNotes(newNotes);
  }

  return (
    <div>
      <div className='notes-grid'>
        { !isShown && (<NewNote setShown={setIsShown}/>) }
        { isShown && (<EditNote 
                        handleAddNote={addNote} 
                        setShown={setIsShown}/>)}
        { notes.map((note) => (
          <Note
              id={note.id}
              text={note.text}
              date={note.date}
              handleDeleteClick={deleteNote}
              handleUpdateClick={updateNote}
          /> ))}      
      </div>
    </div>
  ); 
}

export default App;
