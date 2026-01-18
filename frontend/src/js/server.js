var http = require('http')
const compression = require('compression')
const express = require("express")
const path = require("path")
const app = express()
const fs = require('fs');
const routes = require('./routes');

const shouldCompress = (req, res) => {
  if (req.headers['x-no-compression']) return false
  return compression.filter(req, res)
}

app.use(compression({ filter: shouldCompress, threshold: 0 }))
app.use("/src", express.static(path.resolve(__dirname, "src")))
app.use(routes);

const ports = [10961]

const servers = ports.map((port) => {
  const server = http.createServer(app)
  server.listen(port, () => { console.log(`Server is running on port ${port}`) })
  return server
})
