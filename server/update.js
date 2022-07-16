const express = require('express')
const router = express.Router()
const path = require('path')
const fs = require('fs')

const limit = process.env.update_files

router.get("/", (req, res) => {
  const data = fs.readFileSync(path.join(__dirname, process.env.symlink, process.env.update_file)).toString()
  const lines = data.split("\r\n")
  var targets = []
  var count = 0
  for (var i = lines.length - 1; i >= 0; i--) {
    if (lines[i].indexOf("CopyInfo") > 0) {
      targets.push(lines[i])
      if (++count >= limit) break
    }
  }

  var converted = []
  targets.forEach(el => {
    const t = el.split("\t")
    var which = ""
    if (el.indexOf("連載終了") < 0) {
      which = "連載中"
    } else {
      which = "連載終了"
    }
    const filename = t[1].split("\\")
    const p = encodeURI(path.join("Comic", which, filename[0], filename[1]))
    const url = `<li><a href="${p}" booktitle="${filename[1]}">${filename[1]}</a></li>`
    converted.push(url)
  });

  var html = fs.readFileSync(path.join(__dirname, "assets", "update.html")).toString()
  html = html.replace("{files}", converted.join(""))

  res.send(html)
})


router.use(express.static(process.env.symlink))

module.exports = router;
