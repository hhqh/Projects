require('dotenv').config()

const PORT = process.env.PORT
const MONGODB_URI = process.env.MONGODB_URI
const TESTMONGODB_URI = process.env.TESTMONGODB_URI

module.exports = {
    PORT, MONGODB_URI, TESTMONGODB_URI
}