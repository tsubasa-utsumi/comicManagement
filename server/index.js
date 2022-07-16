const path = require('path')
const express = require('express')
const basicAuth = require('express-basic-auth')
const app = express()
const PORT = 53994

const directory = "./symbolic"
require('dotenv').config()

// basic認証
app.use(basicAuth({
    challenge: true,
    unauthorizeResponse: () => {
        return "fuck you"
    },
    authorizer: (username, password) => {
        const userMatch = basicAuth.safeCompare(username, process.env.u)
        const passMatch = basicAuth.safeCompare(password, process.env.p)
    
        return userMatch & passMatch
    }
}))

// serve-indexの設定
const sindex = require('serve-index')
const options = {
	icons: true,
    stylesheet: path.join(__dirname, 'assets', 'style.css'),
	filter: (filename, index, list, path) => {
		return filename !== '@eaDir' 
	},
	template: __dirname + '/assets/template.html'
}

app.use(express.static(directory))
app.use(sindex(directory, options))
app.listen(PORT)
