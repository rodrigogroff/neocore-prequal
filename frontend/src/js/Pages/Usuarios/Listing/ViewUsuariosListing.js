import "@app/Infra/global.script"
import MyCss from "@app/Components/Style.js"
import Menu from "@app/Components/Menu"
import ModalEditItem from "./PopupEditItem"
import ModalNewItem from "./PopupNewItem"
import ModalResetSenha from "./PopupResetSenha"
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
      formEditItem_Ativo: "formEditItem_Ativo",
      formEditItem_Email: "formEditItem_Email",
      formEditItem_Nome: "formEditItem_Nome",
      formEditItem_Area: "formEditItem_Area",
      formLoaderEditItem: "formLoaderEditItem",
      btnAtualizarItem: "btnAtualizarItem",      
      btnResetSenha: "btnResetSenha",
      // new item
      popUpSystemConfirm_NewItem: "popUpSystemConfirm_NewItem",
      formNewItem_Ativo: "formNewItem_Ativo",
      formNewItem_Email: "formNewItem_Email",
      formNewItem_Nome: "formNewItem_Nome",
      formNewItem_Area: "formNewItem_Area",
      formLoaderNewItem: "formLoaderNewItem",
      btnCriarItem: "btnCriarItem",
      // reset senha
      popUpSystemConfirm_ResetSenha: "popUpSystemConfirm_ResetSenha",
      formResetSenha_Email: "formResetSenha_Email",
      formResetSenha_NovaSenha: "formResetSenha_NovaSenha",
      formLoaderResetSenha: "formLoaderResetSenha",
      btnAtualizarSenha: "btnAtualizarSenha"
    }
  }
  static getHtml() {
    var elements = this.elements()
    var page = `${MyCss.getStyleHtml()}
${ModalEditItem.getHtml(elements)}
${ModalNewItem.getHtml(elements)}
${ModalResetSenha.getHtml(elements)}
${Popup.getHtml()}
${PopupOK.getHtml()}
${MyCss.getListTitleAndSubtitle('Listagem de usuários com acesso', '', null)}
${MyCss.getMainFilter(elements.formSearch, "", elements.formLoader, elements.btnSearch, elements.btnNew, '', 'Criar novo item')}
<div align='left' style='font-size:11px'>
  <div id='table_report'></div>
  <br><br><br>
</div>`
    return MasterPage.getHtml(page, Menu.getHtml())
  }
}