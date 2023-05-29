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

const errorHandling = (error, request, response, next) => {
  console.log(error.message)
  if (error.name === 'CastError'){
    return response.status(400).send({error: 'malformatted id'})
  }else if (error.name === 'ValidationError'){
    return response.status(400).json({error: error.message})
  }
  next(error)
}

app.use(errorHandling)

const generateId = () => {
  return Math.random(Number. MAX_VALUE)
}

app.get('/info', (request, response) => {
    response.send(`<p> Phonebook has info for ${Person.length} people </p> <p> ${new Date()} </p>`)
})

app.get('/api/persons', (request, response, next) => {
  Person.find({}).then(persons => {
    response.json(persons)
  })
  .catch(error => next(error))
})

app.get('/api/persons/:id', (request, response, next) => {
  Person.findById(request.params.id)
    .then(person => {
      if (person){
        response.json(person)
      }else{
        response.status(404).end()
      }
    })
    .catch(error => next(error))
})

app.delete('/api/persons/:id', (request, response, next) => {
  Person.findByIdAndRemove(request.params.id)
    .then(result => {
      response.status(204).end()
    })
    .catch(error => next(error))
})

app.put('/api/persons/:id', (request, response, next) => {
  const {name, number} = request.body

  Person.findByIdAndUpdate(
    request.params.id, 
    {name, number}, 
    {new: true, runValidators: true, context: 'query'})
    .then(updatePerson => {
      response.json(updatePerson)
    })
    .catch(error => next(error))
})

app.post('/api/persons', (request, response, next) => {
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

  const person = new Person({
    id: generateId(),
    name: body.name,
    number: body.number || ""
  })
  person.save().then(savedPerson => {
    response.json(savedPerson)
  }) 
  .catch(error => next(error))
})

const PORT = process.env.PORT
app.listen(PORT)