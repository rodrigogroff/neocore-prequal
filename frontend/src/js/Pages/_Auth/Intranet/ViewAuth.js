import Popup from "@app/Components/Modals/Popup"
import ModalUserLogin from "./Popup/PopupUserLogin"
import ViewHtml from "./Main.html"

export default class {
  static elements() {
    return {
      btnLogin: "btnLogin",
      // login
      formEmailLogin: "formEmailLogin",
      formPassLogin: "formPassLogin",
      formLoaderLoginWithPass: "formLoaderLoginWithPass",
      btnLoginWithPass: "btnLoginWithPass",
    }
  }

  static getHtml() {
    var elements = this.elements()
    return `${ViewHtml}${ModalUserLogin.getHtml(elements)}${Popup.getHtml()}`
  }
}
