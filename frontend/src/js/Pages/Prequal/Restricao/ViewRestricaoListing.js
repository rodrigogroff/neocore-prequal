import "@app/Infra/global.script"
import MyCss from "@app/Components/Style.js"
import Menu from "@app/Components/Menu"
import ModalEditItem from "./PopupEditItem"
import ModalNewItem from "./PopupNewItem"
import MasterPage from "@app/Components/Views/MasterPage"
import Popup from "@app/Components/Modals/Popup"
import PopupOK from "@app/Components/Modals/PopupOK"

export default class {
  static elements() {
    return {
      formLoader: "formLoader",
      formSearch: "formSearch",
      btnSearch: "btnSearch",
      btnNew: "btnNew",
      // edit item
      popUpSystemConfirm_EditItem: "popUpSystemConfirm_EditItem",
      formEditItem_Id: "formEditItem_Id",
      formEditItem_Descricao: "formEditItem_Descricao",
      formLoaderEditItem: "formLoaderEditItem",
      btnAtualizarItem: "btnAtualizarItem",      
      btnResetSenha: "btnResetSenha",
      // new item
      popUpSystemConfirm_NewItem: "popUpSystemConfirm_NewItem",
      formNewItem_Descricao: "formNewItem_Descricao",
      formLoaderNewItem: "formLoaderNewItem",
      btnCriarItem: "btnCriarItem",
    }
  }
  static getHtml() {
    var elements = this.elements()
    var page = `${MyCss.getStyleHtml()}
${ModalEditItem.getHtml(elements)}
${ModalNewItem.getHtml(elements)}
${Popup.getHtml()}
${PopupOK.getHtml()}
${MyCss.getListTitleAndSubtitle('Listagem de restrições de pessoas na blacklist', '', null)}
${MyCss.getMainFilter(elements.formSearch, "", elements.formLoader, elements.btnSearch, elements.btnNew, '', 'Criar novo item')}
<div align='left' style='font-size:11px'>
  <div id='table_report'></div>
  <br><br><br>
</div>`
    return MasterPage.getHtml(page, Menu.getHtml())
  }
}