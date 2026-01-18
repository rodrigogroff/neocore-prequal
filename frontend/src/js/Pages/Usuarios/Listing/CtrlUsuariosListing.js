import { isAuthenticated } from "@app/Infra/Auth"
import { Endpoints } from "@app/Infra/Endpoints"
import { getBOTokenPortal, postBOTokenPortal } from "@app/Infra/Api"
import { displaySystemPopup, displaySystemPopupOK, CheckPopUpCloseClick } from "@app/Infra/PopupControl"
import { buildTableSimple, updateHTML } from "@app/Infra/Builder"
import { get, getCheck, set, setToSession, displaySafeText, hideElement, showElement } from "@app/Infra/Util"
import { addEnterKeyListener, showPopup, check, disable } from "@app/Infra/Util"
import MasterPage from "@app/Components/Views/MasterPage"
import MyForm from "./ViewUsuariosListing"

export default class {
  getHtml() { return MyForm.getHtml() }
  constructor() {
    $(document).ready(function () {
      isAuthenticated()      
      MasterPage.applyTheme()
      var elements = MyForm.elements()
      addEnterKeyListener(elements.formSearch, search)
      search()      
    })

    function search() {
      var elements = MyForm.elements()
      hideElement(elements.btnSearch)
      showElement(elements.formLoader)
      getBOTokenPortal(Endpoints().bo_user_listing, "")
        .then((resp) => {
          showElement(elements.btnSearch)
          hideElement(elements.formLoader)
          const termoPesquisa = get(elements.formSearch).toLowerCase().trim();
          let myTable = {
            id: 'table_report',
            header: ['Ativo', 'Email', 'Nome', 'Area'],
            sizes: [ 70, 200, 150, 150 ],
            data: [],
          }
          var totItens = 0
          resp.payload.conteudo.forEach(function (ar) {
            const linha = [
              (ar.ativo == true ? 'Sim' : 'Não'),
              displaySafeText(ar.email), 
              displaySafeText(ar.nome),               
              displaySafeText(ar.area),               
            ];
            if (!termoPesquisa || linha.some(campo => 
              campo.toString().toLowerCase().includes(termoPesquisa)
            )) {
              totItens++
              myTable.data.push([
                linha[0], linha[1], linha[2], linha[3], 
              ]);
            }
          })
          setToSession('list', JSON.stringify(resp.payload.conteudo))
          updateHTML('table_report', "<br><br>" + totItens + " itens encontrados<br><br><br>" + buildTableSimple(myTable))
        })
    }

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return
      var elements = MyForm.elements()
      var clickObj = e.target
      if (clickObj.hasAttribute("_par_table")) {
        if (clickObj.getAttribute("_par_table") == "table_report") {
          var itemId = clickObj.getAttribute("_par_table_item_id")
          var ar = JSON.parse(sessionStorage.getItem('list'))
          var editItem = ar[itemId]
          setToSession('list_item', JSON.stringify(editItem))
          check(elements.formEditItem_Ativo, editItem.ativo)
          set(elements.formEditItem_Email, editItem.email)
          set(elements.formEditItem_Nome, editItem.nome)
          set(elements.formResetSenha_Email, editItem.email)
          set(elements.formEditItem_Area, editItem.area)
          disable(elements.formEditItem_Email)
          showPopup(elements.popUpSystemConfirm_EditItem)
          return
        }
      }

      switch ($(e.target).attr("id")) {
      
        case elements.btnSearch:
          search()
          break

        case elements.btnAtualizarItem:
          postBOTokenPortal(Endpoints().bo_user_update, {
            ativo: getCheck(elements.formEditItem_Ativo),
            email: get(elements.formEditItem_Email),
            nome: get(elements.formEditItem_Nome),
            area: get(elements.formEditItem_Area),              
          })
          .then((resp) => {
            if (!resp.ok) {
              displaySystemPopup(resp.payload.mensagem)
            }
            else {
              hideElement(elements.popUpSystemConfirm_EditItem)
              search()
              displaySystemPopupOK("Usuário atualizado com sucesso!")              
            }
          })
          break

        case elements.btnNew:
          check(elements.formNewItem_Ativo, true)
          set(elements.formNewItem_Email, '')
          set(elements.formNewItem_Nome, '')          
          set(elements.formNewItem_Area, '')
          showPopup(elements.popUpSystemConfirm_NewItem)
          break

        case elements.btnCriarItem:
          
          postBOTokenPortal(Endpoints().bo_user_new, {
            email: get(elements.formNewItem_Email),
            nome: get(elements.formNewItem_Nome),
            area: get(elements.formNewItem_Area),              
          })
          .then((resp) => {
            if (!resp.ok) {
              displaySystemPopup(resp.payload.mensagem)
            }
            else {
              displaySystemPopupOK("Usuário criado com sucesso!")
              hideElement(elements.popUpSystemConfirm_NewItem)
              search()            
            }
          })
          break
          
        case elements.btnResetSenha:
          hideElement(elements.popUpSystemConfirm_EditItem)
          set(elements.formResetSenha_NovaSenha, '')
          showPopup(elements.popUpSystemConfirm_ResetSenha)
          break

        case elements.btnAtualizarSenha:
          showElement(elements.formLoaderResetSenha)
          postBOTokenPortal(Endpoints().bo_reset_pass, {
            email: get(elements.formResetSenha_Email),
            novaSenha: get(elements.formResetSenha_NovaSenha),              
          })
          .then((resp) => {
            hideElement(elements.formLoaderResetSenha)
            if (!resp.ok) {
              displaySystemPopup(resp.payload.mensagem)
            }
            else {
              displaySystemPopupOK("Senha do usuário alterada com sucesso!")
              hideElement(elements.popUpSystemConfirm_ResetSenha)
            }
          })
          break
      }
    })
  }
}
