const express = require('express')
const router = express.Router()

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

router.use(express.static(process.env.symlink))
router.use(sindex(process.env.symlink, options))

module.exports = router;