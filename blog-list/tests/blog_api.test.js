const mongoose = require('mongoose')
const supertest = require('supertest')
const app = require('../app')
const helper = require('./list_helper.test')

const api = supertest(app)

const Blog = require('../models/blog')

beforeEach(async () => {
  await Blog.deleteMany({})
  await Blog.insertMany(helper.blogs)
})

describe('get blogs api', () => {
    test('blogs are returned as json', async () => {
        await api
        .get('/api/blogs')
        .expect(200)
        .expect('Content-Type', /application\/json/)
    })

    test('all blogs are returned', async () => {
        const response = await api.get('/api/blogs')
    
        expect(response.body).toHaveLength(helper.blogs.length)
    })
    
    test('a specific blog is within the blogs', async () => {
        const response = await api.get('/api/blogs')
    
        const blogTitle = response.body.map(r => r.title)
        expect(blogTitle).toContain(
            'TDD harms architecture'
        )
    })

    test('unique identifier of blog is named id', async() => {
        const blogs = await Blog.find({})
        expect(blogs[0]._id).toBeDefined()
    })

})

describe('post blogs api', () => {
    test('a valid blog can be added', async () => {
        const newBlog = {
            title: "On Types",
            author: "Robert C. Martin",
            url: "http://blog.cleancoder.com/uncle-bob/2021/06/25/OnTypes.html",
            likes: 2
        }
      
        await api
          .post('/api/blogs')
          .send(newBlog)
          .expect(201)
          .expect('Content-Type', /application\/json/)
      
        const response = await api.get('/api/blogs')
      
        const contents = response.body.map(r => r.title)
      
        expect(response.body).toHaveLength(helper.blogs.length + 1)
        expect(contents).toContain(
          'On Types'
        )
      })

      test('a blog without likes is added a blog with zero likes', async () => {
        const newBlog = {
            title: "Roots",
            author: "Robert C. Martin",
            url: "http://blog.cleancoder.com/uncle-bob/2021/09/25/roots.html",
        }
      
        await api
          .post('/api/blogs')
          .send(newBlog)
          .expect(201)
          .expect('Content-Type', /application\/json/)
      
        const response = await api.get('/api/blogs')
      
        const contents = response.body.map(r => r.likes)
      
        expect(response.body).toHaveLength(helper.blogs.length + 1)
        expect(contents).toContain(0)
      })

      test('blog with missing title is not added', async () => {
        const newBlog = {
            author: "Robert C. Martin",
            url: "http://blog.cleancoder.com/uncle-bob/2021/09/25/roots.html",
        }
      
        await api
          .post('/api/blogs')
          .send(newBlog)
          .expect(400)
      
        const response = await api.get('/api/blogs')
      
        expect(response.body).toHaveLength(helper.blogs.length)
      })

      test('blog with missing url is not added', async () => {
        const newBlog = {
            title: "Roots",
            author: "Robert C. Martin",
        }
      
        await api
          .post('/api/blogs')
          .send(newBlog)
          .expect(400)
      
        const response = await api.get('/api/blogs')
      
        expect(response.body).toHaveLength(helper.blogs.length)
      })

})

describe('delete blogs api', () => {
  test('successfully deleted a valid blog', async () => {
    const blogToDelete = helper.blogs[0]

    await api
      .delete(`/api/blogs/${blogToDelete._id}`)
      .expect(204)

    const response = await api.get('/api/blogs')

    expect(response.body).toHaveLength(helper.blogs.length - 1)

    const contents = response.body.map(r => r.title)

    expect(contents).not.toContain(blogToDelete.title)
    
  })
}) 


afterAll(async() => {
    await mongoose.connection.close()
})