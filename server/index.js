const express = require('express')
const app = express()
const PORT = 12100

const directory = "./symbolic"

const sindex = require('serve-index')
const options = {
	icons: true,
	filter: (filename, index, list, path) => {
		return filename !== '@eaDir' 
	},
	template: __dirname + '/asset/template.html'
}

// app.use(express.static(directory))
app.use(sindex(directory, options))
app.listen(PORT)
