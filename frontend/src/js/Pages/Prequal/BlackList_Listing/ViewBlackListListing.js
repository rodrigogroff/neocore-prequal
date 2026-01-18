import "@app/Infra/global.script"
import CheckboxList from "@app/Components/Fields/CheckBoxList"
import MyCss from "@app/Components/Style.js"
import Menu from "@app/Components/Menu"
import MasterPage from "@app/Components/Views/MasterPage"
import ModalEditItem from "./PopupEditItem"
import ModalEditItemHistorico from "./PopupEditItemHistorico"
import ModalNewItem from "./PopupNewItem"
import ModalFileUpload from "./PopupFileUpload"
import Popup from "@app/Components/Modals/Popup"
import PopupOK from "@app/Components/Modals/PopupOK"

export default class {
  static elements() {
    return {
      formLoader: "formLoader",
      formSearch: "formSearch",
      formSearch_FilterVigentes: "formSearch_FilterVigentes",
      btnSearch: "btnSearch",
      btnNew: "btnNew",
      btnExportExcel: "btnExportExcel",
      btnUploadFile: "btnUploadFile",
      formFileUpload: "formFileUpload",      
      // edit item
      popUpSystemConfirm_EditItem: "popUpSystemConfirm_EditItem",
      formEditItem_Ativo: "formEditItem_Ativo",
      formEditItem_TipoPessoa: "formEditItem_TipoPessoa",
      formEditItem_Documento: "formEditItem_Documento",
      formEditItem_TipoRestricao: "formEditItem_TipoRestricao",
      formEditItem_ObsRestricao: "formEditItem_ObsRestricao",
      formEditItem_NivelSeveridade: "formEditItem_NivelSeveridade",
      formEditItem_DataInicio: "formEditItem_DataInicio",
      formEditItem_DataFim: "formEditItem_DataFim",
      formEditItem_Fonte: "formEditItem_Fonte",
      formEditItem_Identificador: "formEditItem_Identificador",
      formEditItem_Link: "formEditItem_Link",
      formLoaderEditItem: "formLoaderEditItem",
      btnAtualizarItem: "btnAtualizarItem",
      btnVisualizarHist: "btnVisualizarHist",
      // new item
      popUpSystemConfirm_NewItem: "popUpSystemConfirm_NewItem",
      formNewItem_Ativo: "formNewItem_Ativo",
      formNewItem_TipoPessoa: "formNewItem_TipoPessoa",
      formNewItem_Documento: "formNewItem_Documento",
      formNewItem_TipoRestricao: "formNewItem_TipoRestricao",
      formNewItem_ObsRestricao: "formNewItem_ObsRestricao",
      formNewItem_NivelSeveridade: "formNewItem_NivelSeveridade",
      formNewItem_DataInicio: "formNewItem_DataInicio",
      formNewItem_DataFim: "formNewItem_DataFim",
      formNewItem_Fonte: "formNewItem_Fonte",
      formNewItem_Identificador: "formNewItem_Identificador",
      formNewItem_Link: "formNewItem_Link",
      formLoaderNewItem: "formLoaderNewItem",
      btnCriarItem: "btnCriarItem",
      // file upload
      popUpSystemConfirm_FileUpload: "popUpSystemConfirm_FileUpload",
      formLoaderFileUpload: "formLoaderFileUpload",
      table_report_fileUpload: "table_report_fileUpload",
      spanMsgUploadProc: "spanMsgUploadProc",
      spanMsgUploadFail: "spanMsgUploadFail",
      spanMsgUploadOK: "spanMsgUploadOK",
      pnlUploadMsgs: "pnlUploadMsgs",
    }
  }
  static getHtml() {
    var elements = this.elements()
    var injectFilter = CheckboxList.getHtml([ { id: elements.formSearch_FilterVigentes, label: 'Itens Vigentes' }], '120')
    var page = `${MyCss.getStyleHtml()}
${ModalEditItem.getHtml(elements)}
${ModalEditItemHistorico.getHtml(elements)}
${ModalNewItem.getHtml(elements)}
${ModalFileUpload.getHtml(elements)}
${Popup.getHtml()}
${PopupOK.getHtml()}
${MyCss.getListTitleAndSubtitle('BlackList restritiva CPF/CNPJ', '', null)}
${MyCss.getMainFilter(elements.formSearch, "", elements.formLoader, elements.btnSearch, elements.btnNew, '', 'Criar novo item', injectFilter)}
<div align='left' style='font-size:11px'>
  <div id='table_report'></div>
  <br><br><br>
</div>`
    return MasterPage.getHtml(page, Menu.getHtml())
  }
}
