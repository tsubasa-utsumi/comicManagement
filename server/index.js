const express = require('express')
const app = express()
const PORT = 12100

const sindex = require('serve-index')
const options = {
	icons: true,
	filter: (filename, index, list, path) => {
		return filename !== '@eaDir' 
	},
	template: __dirname + '/public/template.html'
}
app.use('/',express.static('./comics'), sindex('./comics', options))
app.listen(PORT)
