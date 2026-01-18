import "@app/Infra/global.script"
import MyCss from "@app/Components/Style.js"
import Menu from "@app/Components/Menu"
import MasterPage from "@app/Components/Views/MasterPage"

export default class {
  static getHtml() {
    let menu = Menu.getHtml()
    var page = `${MyCss.getStyleHtml()}`
    return MasterPage.getHtml(page, menu)
  }
}