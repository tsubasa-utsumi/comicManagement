const express = require('express')
const basicAuth = require('express-basic-auth')
const app = express()
const PORT = 53994

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

app.use("/", require("./sindex.js"))
app.use("/update", require("./update.js"))

app.listen(PORT)
