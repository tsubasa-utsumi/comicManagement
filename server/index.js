const express = require('express')
const basicAuth = require('express-basic-auth')
const app = express()
const PORT = 53994

const directory = "./symbolic"
require('dotenv').config()
if (!(process.env.u && process.env.p)) {
    console.log("invalid env")
    process.exit(1)
}

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
    showUp: false,
	filter: (filename, index, list, path, isDirectory) => {
		return isDirectory || filename !== '@eaDir' && filename.endsWith(".zip")
	},
	template: __dirname + '/assets/template.html'
}

app.use(express.static(directory))
app.use(sindex(directory, options))
app.listen(PORT)
