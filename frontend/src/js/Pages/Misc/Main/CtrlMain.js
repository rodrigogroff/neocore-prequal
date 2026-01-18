import { isAuthenticated } from "@app/Infra/Auth"
import { CheckPopUpCloseClick } from "@app/Infra/PopupControl"
import MasterPage from "@app/Components/Views/MasterPage"
import MyForm from "./ViewMain"

export default class {

  getHtml() { return MyForm.getHtml() }

  constructor() {
    $(document).ready(function () {
      isAuthenticated()
      MasterPage.applyTheme()      
    })

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return      
    })
  }
}