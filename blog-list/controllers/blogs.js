const blogRouter = require('express').Router()
const Blog = require('../models/blog')

blogRouter.get('/', (request, response) => {
    Blog
      .find({})
      .then(blogs => {
        response.json(blogs)
      })
})

blogRouter.delete('/:id', async (request, response) => {
  Blog.findByIdAndRemove(request.params.id)
    .then(result => {
      response.status(204).end()
    })
})

blogRouter.put('/:id', async (request, response) => {
  const {title, author, url, likes} = request.body

  Blog.findByIdAndUpdate(
    request.params.id, 
    {title, author, url, likes}, {new: true})
    .then(updateBlog => {
      response.json(updateBlog)
    })
})
  
blogRouter.post('/', async (request, response) => {
    const body = request.body

    if (!body.title){
      return response.status(400).end()
    }

    if(!body.url){
      return response.status(400).end()
    }

    const blog = new Blog({
      title: body.title,
      author: body.author,
      url: body.url,
      likes: body.likes || 0
    })


    const savedBlog = await blog.save()
    response.status(201).json(savedBlog)
})

module.exports = blogRouter
