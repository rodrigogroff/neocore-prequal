const express = require('express');
const path = require('path');
const router = express.Router();

router.get("/", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_intranet_login.html")) })

router.get("/main", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_main.html")) })
router.get("/prequal_blacklist_listing", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_prequal_blacklist_listing.html")) })
router.get("/prequal_restricao_listing", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_prequal_restricao_listing.html")) })
router.get("/prequal_severidade_listing", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_prequal_severidade_listing.html")) })
router.get("/usuarios_listing", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_usuarios_listing.html")) })

module.exports = router;
