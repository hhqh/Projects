require('dotenv').config()
const express = require('express')
const app = express()
const morgan = require('morgan')
const cors = require('cors')
const Person = require('./models/person')

app.use(express.json())
app.use(cors())
app.use(express.static('build'))

app.use(morgan(function (tokens, req, res) {
  return [
    tokens.method(req, res),
    tokens.url(req, res),
    tokens.status(req, res),
    tokens.res(req, res, 'content-length'), '-',
    tokens['response-time'](req, res), 'ms'
  ].join(' ')
}))

let persons = [
    { 
      "id": 1,
      "name": "Arto Hellas", 
      "number": "040-123456"
    },
    { 
      "id": 2,
      "name": "Ada Lovelace", 
      "number": "39-44-5323523"
    },
    { 
      "id": 3,
      "name": "Dan Abramov", 
      "number": "12-43-234345"
    },
    { 
      "id": 4,
      "name": "Mary Poppendieck", 
      "number": "39-23-6423122"
    }
]

const generateId = () => {
  return Math.random(Number. MAX_VALUE)
}

app.get('/info', (request, response) => {
    response.send(`<p> Phonebook has info for ${persons.length} people </p> <p> ${new Date()} </p>`)
})

app.get('/api/persons', (request, response) => {
  Person.find({}).then(persons => {
    response.json(persons)
  })
})

app.get('/api/persons/:id', (request, response) => {
  const id = Number(request.params.id)
  const person = persons.find(person => person.id === id)

  if (person){
    response.json(person)
  }else{
    response.status(404).end()
  }
})

app.delete('/api/persons/:id', (request, response) => {
  Person.findByIdAndRemove(request.params.id)
    .then(result => {
      response.status(204).end()
    })
})

app.post('/api/persons', (request, response) => {
  const body = request.body

  if (!body.name){
    return response.status(400).json({
      error: "missing name"
    })
  }

  if (!body.number){
    return response.status(400).json({
      error: "missing number"
    })
  }

  if (persons.find(person => person.name === body.name)){
    return response.status(400).json({
      error: "name must be unique"
    })
  }

  const person = new Person({
    id: generateId(),
    name: body.name,
    number: body.number || ""
  })
  person.save().then(savedPerson => {
    response.json(savedPerson)
  }) 
})

const PORT = process.env.PORT
app.listen(PORT)