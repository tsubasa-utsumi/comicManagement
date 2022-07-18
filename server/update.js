const express = require('express')
const router = express.Router()
const path = require('path')
const fs = require('fs')

const limit = process.env.update_files

router.get("/api/generate", (req, res) => {
  const data = fs.readFileSync(path.join(__dirname, process.env.symlink, process.env.update_file)).toString().trim()
  const lines = data.split("\r\n")

  var perDay = {}
  lines.forEach(el => {
    if (el.indexOf("CopyInfo") > 0) {
      const day = el.substring(1, 11).replaceAll("/", "")
      var isEnd = el.indexOf("連載中") < 0
      const t = el.split("\t")[1].split("\\")
      perDay[day] = perDay[day] ? perDay[day] : []
      perDay[day].push({
        isEnd: isEnd,
        folder: t[0],
        name: t[1]
      })
    }
  })

  var updates = []
  const syno = process.env.synology === "true" ? "updates/" : ""
  Object.keys(perDay).forEach(ar => {
    var html = fs.readFileSync(path.join(__dirname, "assets", "updates", "updates_template.html")).toString()
    var files = []
    updates.push(`<li><a href="${syno}${ar}" booktitle="${ar}">${ar}</a></li>`)
    perDay[ar].forEach(el => {
      const p = encodeURI(path.join(el.isEnd ? "連載終了" : "連載中", el.folder, el.name))
      const file = `<li><a href="${p}" booktitle="${el.name}">${el.name}</a></li>`
      files.push(file)
    })
    html = html.replace("{files}", files.join("\n"))
    fs.writeFileSync(path.join(__dirname, "assets", "updates", ar + ".html"), html)
  })

  var update = fs.readFileSync(path.join(__dirname, "assets", "updates", "updates_template.html")).toString()
  update = update.replace("{files}", updates.join("\n"))
  fs.writeFileSync(path.join(__dirname, "assets", "update.html"), update)

  res.sendStatus(200)
})

router.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "assets", "update.html"))
})

router.get("/:day", (req, res) => {
  if (req.params.day == "null") return
  res.sendFile(path.join(__dirname, "assets", "updates", req.params.day + ".html"))
})


router.use(express.static(process.env.symlink))
router.use(express.static("assets"))

module.exports = router;
