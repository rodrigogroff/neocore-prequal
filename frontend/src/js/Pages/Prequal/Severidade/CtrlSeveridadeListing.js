import { isAuthenticated } from "@app/Infra/Auth"
import { Endpoints } from "@app/Infra/Endpoints"
import { getPrequalTokenPortal, postPrequalTokenPortal } from "@app/Infra/Api"
import { displaySystemPopup, displaySystemPopupOK, CheckPopUpCloseClick } from "@app/Infra/PopupControl"
import { buildTableSimple, updateHTML } from "@app/Infra/Builder"
import { get, set, setToSession, displaySafeText, hideElement, showElement } from "@app/Infra/Util"
import { addEnterKeyListener, showPopup, disable } from "@app/Infra/Util"
import MasterPage from "@app/Components/Views/MasterPage"
import MyForm from "./ViewSeveridadeListing"

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
      getPrequalTokenPortal(Endpoints().prequal_enums_nivelSeveridade, "")
        .then((resp) => {
          showElement(elements.btnSearch)
          hideElement(elements.formLoader)
          const termoPesquisa = get(elements.formSearch).toLowerCase().trim();
          let myTable = {
            id: 'table_report',
            header: ['Id','Tipo de restrição'],
            sizes: [ 70, 200 ],
            data: [],
          }
          var totItens = 0
          resp.payload.conteudo.forEach(function (ar) {
            const linha = [
              displaySafeText(ar.id),
              displaySafeText(ar.descricao),
            ];
            if (!termoPesquisa || linha.some(campo => 
              campo.toString().toLowerCase().includes(termoPesquisa)
            )) {
              totItens++
              myTable.data.push([ linha[0], linha[1] ]);
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
          set(elements.formEditItem_Id, editItem.id)
          set(elements.formEditItem_Descricao, editItem.descricao)
          disable(elements.formEditItem_Id)
          showPopup(elements.popUpSystemConfirm_EditItem)
          return
        }
      }

      switch ($(e.target).attr("id")) {
      
        case elements.btnSearch:
          search()
          break

        case elements.btnAtualizarItem:
          postPrequalTokenPortal(Endpoints().prequal_enums_nivelSeveridade, {
            id: get(elements.formEditItem_Id),
            descricao: get(elements.formEditItem_Descricao),            
          })
          .then((resp) => {
            if (!resp.ok) {
              displaySystemPopup(resp.payload.erros[0].mensagem)
            }
            else {
              hideElement(elements.popUpSystemConfirm_EditItem)
              search()
              displaySystemPopupOK("Nivel de severidade atualizado com sucesso!")              
            }
          })
          break

        case elements.btnNew:
          set(elements.formNewItem_Descricao, '')
          showPopup(elements.popUpSystemConfirm_NewItem)
          break

        case elements.btnCriarItem:
          postPrequalTokenPortal(Endpoints().prequal_enums_nivelSeveridade, {
            id: 0,
            descricao: get(elements.formNewItem_Descricao),            
          })
          .then((resp) => {
            if (!resp.ok) {
              displaySystemPopup(resp.payload.erros[0].mensagem)
            }
            else {
              displaySystemPopupOK("Nivel de severidade criado com sucesso!")
              hideElement(elements.popUpSystemConfirm_NewItem)
              search()            
            }
          })
          break

      }
    })
  }
}
