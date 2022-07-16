const express = require('express')
const router = express.Router()
const directory = "./symbolic"

// serve-indexの設定
const sindex = require('serve-index')
const options = {
	icons: true,
    showUp: false,
	filter: (filename, index, list, path, isDirectory) => {
		return isDirectory || filename !== '@eaDir' && filename.endsWith(".zip")
	},
	template: __dirname + '/../assets/template.html'
}

router.use(express.static(directory))
router.use(sindex(directory, options))

module.exports = router;