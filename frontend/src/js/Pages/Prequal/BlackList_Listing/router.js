import "@app/Infra/global.script.js"
import Controller from "./CtrlBlackListListing"
const router = () => { document.getElementById("myApp").innerHTML = new Controller().getHtml() }
window.addEventListener("popstate", router)
document.addEventListener("DOMContentLoaded", () => { router() })
window.history.pushState(null, "", window.location.href)
window.onpopstate = function () { window.history.pushState(null, "", window.location.href) }