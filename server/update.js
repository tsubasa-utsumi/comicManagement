const express = require('express')
const router = express.Router()
const path = require('path')
const fs = require('fs-extra')

router.get("/api/generate", async (req, res) => {
  // 全更新するのでまっさらに
  await deleteDir(path.join(__dirname, "assets", "updates", "files"))
  fs.mkdirSync(path.join(__dirname, "assets", "updates", "files"))

  const limit = new Date()
  limit.setMonth(limit.getMonth() - process.env.update_month)
  const data = fs.readFileSync(path.join(__dirname, process.env.symlink, process.env.update_file)).toString().trim()
  const lines = data.split("\r\n")

  var perDay = {}
  lines.forEach(el => {
    if (el.indexOf("CopyInfo") > 0) {
      // 日付関連の処理(nヶ月前なら切り捨て)
      var day = el.substring(1, 11)
      const paste = new Date(day.replaceAll("/", "-"))
      if (limit > paste) return
      day = day.replaceAll("/", "")

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
  const syno = process.env.synology === "true" ? "update/" : ""
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
    fs.writeFileSync(path.join(__dirname, "assets", "updates", "files", ar + ".html"), html)
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
  res.sendFile(path.join(__dirname, "assets", "updates", "files", req.params.day + ".html"))
})

async function deleteDir(src) {
  try {
    await fs.remove(src)
  } catch (err) {
    console.error(err)
  }
}

router.use(express.static(process.env.symlink))
router.use(express.static("assets"))

module.exports = router;
