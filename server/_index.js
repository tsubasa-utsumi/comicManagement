const express = require('express')
const app = express()
const PORT = 12100

const sindex = require('serve-index')
const options = {
	icons: true,
	filter: (filename, index, list, path) => {
		return filename !== '@eaDir' 
	}
}
app.use(express.static('/volume1/book/Comic'))
app.use(sindex('/volume1/book/Comic', options))

//app.get('/', (req, res) => res.send('Hello World!'))

app.listen(PORT)
