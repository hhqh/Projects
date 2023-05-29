import { useState, useEffect } from 'react'
import personService from './services/persons.js'
import message from './css/message.css'

const Persons = ({persons, onClick}) => {
  return (
    <div>
      {persons.map(person => 
        <Person person={person} onClick={onClick}/>
      )}
    </div>
  )
}

const Person = ({person, onClick}) => {
  return (
    <p key={person.name}>
      {person.name} {person.number}
      <button onClick={() => onClick(person.id)}>
        Delete
      </button>
    </p>
  )
}

const PersonForm = ({onSubmit, nameValue, nameOnChange, numValue, numOnChange}) => {
  return(
   <form onSubmit={onSubmit}>
    <div>
      name: 
      <input 
          value={nameValue}
          onChange={nameOnChange}
      />
    </div>
    <div>
      number: 
      <input 
          value={numValue}
          onChange={numOnChange}
      />
    </div>
    <div>
      <button type="submit">add</button>
    </div>
  </form>
  )
}

const Filter = ({value, onChange}) => {
  return (
    <div>
        Filter Persons
        <input
          value={value}
          onChange={onChange}
        />
    </div>
  )
}

const Message = ({message, error}) => {
  if (message === null){
    return null
  }

  let style = 'successMessage'

  if (error){ 
    style = 'errorMessage'
  }

  return (
    <div className={style}>
      {message}
    </div>
  )
}

const App = () => {
  const [persons, setPersons] = useState([]) 
  const [newName, setNewName] = useState('')
  const [newNum, setNewNum] = useState('')
  const [newFilter, setFilter] = useState('')
  const [message, setMessage] = useState(null)
  const [error, setError] = useState(false)

  useEffect(() => {
    personService.
      getAll().
      then(initialPersons => {
        setPersons(initialPersons)
    })
  }, [])

  const addPerson = (event) => {
    event.preventDefault()

    if (persons.map(person => person.name).includes(newName)){
      if (window.confirm(`${newName} is already added to the phonebook, replace old number with new one?`)){
        const p = persons.find(person => person.name === newName)
        const changedPerson = {...p, number: newNum}

        personService
          .update(p.id, changedPerson)
          .then(returnedPersons => {
            setPersons(persons.map(person => person.name !== newName ? person : changedPerson))
            setError(false)
            setMessage(`${newName} is updated to the phonebook`)
          })
          .catch(error => {
            setPersons(persons.filter(person => person.id !== p.id))
            setError(true)
            setMessage(`${newName} is already removed from phonebook`)
          })    
        setTimeout(() => {
          setMessage(null)
        }, 3000)
      }
      setNewName('')
      setNewNum('')
    }else{
      const personObj = {
        name: newName,
        number: newNum,
        id: newName
      }

      personService
        .create(personObj)
        .then(returnedPerson => {       
          setPersons(persons.concat(personObj))
          setError(false)
          setMessage(`${newName} is added to the phonebook`)
          setTimeout(() => {
            setMessage(null)
          }, 3000)
          setNewName('')
          setNewNum('')
        })
        .catch(error => {
          setError(true)
          setMessage(`Person validation failed: Length is too short`)
        })
        setTimeout(() => {
          setMessage(null)
        }, 3000)
    }
  }

  const handleNameChange = (event) => {
    setNewName(event.target.value)
  }

  const handleNumChange = (event) => {
    setNewNum(event.target.value)
  }

  const handleFilterChange =(event) => {
    setFilter(event.target.value)
  }

  const deleteP = (id) => {
    const deletedPerson = persons.filter(person => person.id === id)

    if (window.confirm(`Do you want to delete ${deletedPerson[0].name}?`)){
      personService
        .deletePerson(deletedPerson[0].id)
      setPersons(persons.filter(person => person.id !== id))
    }
    
  }

  const personFilter = persons.filter(person => person.name.toLowerCase().includes(newFilter.toLowerCase()))

  return (
    <div>
      <h2>Phonebook</h2>
      <Filter value={newFilter} onChange={handleFilterChange}/>
      <h2>Add New</h2>
      <Message message={message} error={error}/>
      <PersonForm 
        onSubmit={addPerson} 
        nameValue={newName}
        nameOnChange={handleNameChange}
        numValue={newNum}
        numOnChange={handleNumChange}/>
      <h2>Numbers</h2>
      <Persons persons={personFilter} onClick={deleteP}/>
    </div>
  )
}

export default App
