import "@app/Infra/global.script.js"
import Controller from "./CtrlMain"
const router = () => { document.getElementById("myApp").innerHTML = new Controller().getHtml() }
window.addEventListener("popstate", router)
document.addEventListener("DOMContentLoaded", () => { router() })
window.history.pushState(null, "", window.location.href)
window.onpopstate = function () { window.history.pushState(null, "", window.location.href) }