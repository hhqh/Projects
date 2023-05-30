const personRouter = require('express').Router()
const Person = require('../models/person')


const generateId = () => {
    return Math.random(Number. MAX_VALUE)
}

personRouter.get('/info', (request, response) => {
    response.send(`<p> Phonebook has info for ${Person.length} people </p> <p> ${new Date()} </p>`)
})

personRouter.get('/', (request, response) => {
  Person.find({}).then(persons => {
    response.json(persons)
  })
})

personRouter.get('/:id', (request, response, next) => {
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

personRouter.delete('/:id', (request, response, next) => {
  Person.findByIdAndRemove(request.params.id)
    .then(result => {
      response.status(204).end()
    })
    .catch(error => next(error))
})

personRouter.put('/:id', (request, response, next) => {
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

personRouter.post('/', (request, response, next) => {
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

module.exports = personRouter