const totalLikes = (blogs) => {
    if (blogs.length === 0){
        return 0;
    }
    return blogs.reduce((sum, blog) => sum + blog.likes, 0);
}

const favoriteBlog = (blogs) => {
    if (blogs.length === 0){
        return null;
    }

    var likes = blogs.map(v => v.likes)
    maxIndex = likes.indexOf(Math.max(...likes))
    return blogs[maxIndex]
}

const mostBlogs = (blogs) => {
    if (blogs.length == 0){
        return null;
    }

    var authors = blogs.map(obj => obj.author)

    var authorCounts = {}

    authors.forEach(author => {
        authorCounts[author] = (authorCounts[author] || 0) + 1
    })

    maxBlogs = Math.max(...Object.values(authorCounts))
    maxAuthor = Object.keys(authorCounts).filter(author => authorCounts[author] === maxBlogs).toString()

    return {
        author: maxAuthor,
        blogs: maxBlogs
    }
}

const mostLikes = (blogs) => {
    if (blogs.length == 0){
        return null;
    }

    var authors = blogs.map(obj => ({author: obj.author, likes: obj.likes}))
    var authorLikes = {}

    authors.forEach(obj => {
        authorLikes[obj.author] = (authorLikes[obj.author] || 0) + obj.likes
    })

    maxLikes = Math.max(...Object.values(authorLikes))
    maxAuthor = Object.keys(authorLikes).filter(author => authorLikes[author] === maxLikes).toString()

    return {
        author: maxAuthor,
        likes: maxLikes
    }
}
  
module.exports = {
    totalLikes, favoriteBlog, mostBlogs, mostLikes
}