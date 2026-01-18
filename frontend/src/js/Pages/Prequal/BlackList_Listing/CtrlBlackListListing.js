import { isAuthenticated } from "@app/Infra/Auth"
import { Endpoints } from "@app/Infra/Endpoints"
import { getPrequalTokenPortal, postPrequalTokenPortal, postPrequalTokenPortalFile } from "@app/Infra/Api"
import { displaySystemPopup, displaySystemPopupOK, CheckPopUpCloseClick } from "@app/Infra/PopupControl"
import { buildTableSimple, updateHTML } from "@app/Infra/Builder"
import { get, getSelectedTextAndId, getCheck, set, setToSession, displaySafeText, hideElement, showElement, clearStyle } from "@app/Infra/Util"
import { addEnterKeyListener, showPopup, check, disable, getDateMinMask, populateSelectArea, setZero } from "@app/Infra/Util"
import * as XLSX from 'xlsx';
import IMask from 'imask';
import MasterPage from "@app/Components/Views/MasterPage"
import MyForm from "./ViewBlackListListing"

export default class {
  getHtml() { return MyForm.getHtml() }
  constructor() {
    $(document).ready(function () {
      isAuthenticated()      
      MasterPage.applyTheme()
      var elements = MyForm.elements()
      IMask(document.getElementById(elements.formEditItem_DataInicio), getDateMinMask())
      IMask(document.getElementById(elements.formEditItem_DataFim), getDateMinMask())
      IMask(document.getElementById(elements.formNewItem_DataInicio), getDateMinMask())
      IMask(document.getElementById(elements.formNewItem_DataFim), getDateMinMask())
      check(elements.formSearch_FilterVigentes, true )
      addEnterKeyListener(elements.formSearch, search)
      search()
      var respTipoPessoa = getPrequalTokenPortal(Endpoints().prequal_enums_tipoPessoa, "")
      var respTipoRestricao = getPrequalTokenPortal(Endpoints().prequal_enums_tipoRestricao, "")
      let respNivelSeveridade = getPrequalTokenPortal(Endpoints().prequal_enums_nivelSeveridade, "")
      Promise.all([respTipoPessoa, respTipoRestricao, respNivelSeveridade]).then(myPromisses => {
        respTipoPessoa = myPromisses[0]
        respTipoRestricao = myPromisses[1]
        respNivelSeveridade = myPromisses[2]
        populateSelectArea(respTipoPessoa.payload.conteudo, elements.formNewItem_TipoPessoa)
        populateSelectArea(respTipoRestricao.payload.conteudo, elements.formNewItem_TipoRestricao)
        populateSelectArea(respNivelSeveridade.payload.conteudo, elements.formNewItem_NivelSeveridade)        
      })
    })

    function formatarData(dataStr) {
      const dia = dataStr.substring(0, 2);
      const mes = dataStr.substring(2, 4);
      const ano = dataStr.substring(4, 8);
      const hora = dataStr.substring(8, 10);
      const minuto = dataStr.substring(10, 12);
      const segundo = dataStr.substring(12, 14);
      return `${dia}/${mes}/${ano} ${hora}:${minuto}:${segundo}`;
    }

    function search() {
      var elements = MyForm.elements()
      hideElement(elements.btnSearch)
      showElement(elements.formLoader)
      getPrequalTokenPortal(Endpoints().prequal_blacklist_listing, "vigente=" + getCheck(elements.formSearch_FilterVigentes))
        .then((resp) => {
          showElement(elements.btnSearch)
          hideElement(elements.formLoader)
          const termoPesquisa = get(elements.formSearch).toLowerCase().trim();
          let myTable = {
            id: 'table_report',
            header: ['Ativo', 'Tipo Pessoa', 'Documento', 'Tipo Restricao', 'Obs. Restrição', 'Nivel Severidade', 'Data Inicio', 'Data Fim', 'Fonte', 'Identificador', 'Link'],
            sizes: [ 70, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90 ],
            data: [],
          }
          var totItens = 0
          resp.payload.conteudo.forEach(function (ar) {
            const dataInicio = ar.dataInicio ? formatarData(ar.dataInicio) : '';
            const dataFim = ar.dataFim ? formatarData(ar.dataFim) : '';
            const li = [
              (ar.ativo == true ? 'Sim' : 'Não'),
              displaySafeText(ar.codigoTipoPessoa), 
              displaySafeText(ar.documento), 
              displaySafeText(ar.tipoRestricao), 
              displaySafeText(ar.obsRestricao), 
              displaySafeText(ar.nivelSeveridade), 
              displaySafeText(dataInicio), 
              displaySafeText(dataFim), 
              displaySafeText(ar.fonte), 
              displaySafeText(ar.fonteIdentificador), 
              displaySafeText(ar.fonteLink), 
            ];
            if (!termoPesquisa || li.some(campo => campo.toString().toLowerCase().includes(termoPesquisa))) 
            {
              totItens++
              myTable.data.push([ li[0], li[1], li[2], li[3], li[4], li[5], li[6], li[7], li[8], li[9], li[10] ]);
            }
          })
          var strExport = 
           `<br><table><tr>` + 
           `<td><div align='center' id="${elements.btnExportExcel}" style="width:140px" class="button green">Exportar excel</div></td>` + 
           `<td width='20px'></td>` + 
           `<td width='200px'><input id="${elements.formFileUpload}" style="box-shadow:none; width:300px; height:64px" type="file" accept=".xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" /></td>` +
           `<td width='20px'></td>` + 
           `<td><div align='center' id="${elements.btnUploadFile}" style="width:140px" class="button red">Importar arquivo</div></td>` + 
           `</tr></table><br><br><br>${totItens} registro(s) encontrado(s).<br><br>`;
          if (resp.payload.conteudo.length == 0) strExport = ''
          setToSession('list', JSON.stringify(resp.payload.conteudo))
          updateHTML('table_report', strExport + buildTableSimple(myTable))
        })
    }

    function exportXLS() {
      var elements = MyForm.elements()
      hideElement(elements.btnSearch)
      showElement(elements.formLoader)
      getPrequalTokenPortal(Endpoints().prequal_blacklist_listing, "vigente=" + getCheck(elements.formSearch_FilterVigentes))
        .then((resp) => {
          showElement(elements.btnSearch)
          hideElement(elements.formLoader)
          var excel_table = []
          excel_table.push([
            'Ativo', 'Tipo Pessoa', 'Documento', 'Tipo Restricao', 'Obs. Restrição', 'Nivel Severidade', 'Data Inicio', 'Data Fim', 'Fonte', 'Identificador', 'Link']);
          resp.payload.conteudo.forEach(function (ar) {
            const dataInicio = ar.dataInicio ? formatarData(ar.dataInicio) : '';
            const dataFim = ar.dataFim ? formatarData(ar.dataFim) : '';
            excel_table.push([
              (ar.ativo == true ? 'Sim' : 'Não'),
              displaySafeText(ar.codigoTipoPessoa), displaySafeText(ar.documento), 
              displaySafeText(ar.tipoRestricao), displaySafeText(ar.obsRestricao), 
              displaySafeText(ar.nivelSeveridade), displaySafeText(dataInicio), 
              displaySafeText(dataFim), displaySafeText(ar.fonte), 
              displaySafeText(ar.fonteIdentificador), displaySafeText(ar.fonteLink), 
            ])
          })
          const workbook = XLSX.utils.book_new();
          const worksheet = XLSX.utils.aoa_to_sheet(excel_table);
          XLSX.utils.book_append_sheet(workbook, worksheet, "Usuários");
          XLSX.writeFile(workbook, "export.xlsx");
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
          // marcar campos vindos do payload
          check(elements.formEditItem_Ativo, editItem.ativo)
          set(elements.formEditItem_TipoPessoa, editItem.codigoTipoPessoa)
          set(elements.formEditItem_Documento, editItem.documento)
          set(elements.formEditItem_TipoRestricao, editItem.tipoRestricao)
          set(elements.formEditItem_ObsRestricao, editItem.obsRestricao)
          set(elements.formEditItem_NivelSeveridade, editItem.nivelSeveridade)
          set(elements.formEditItem_DataInicio, formatarData(editItem.dataInicio))
          set(elements.formEditItem_DataFim, formatarData(editItem.dataFim))
          set(elements.formEditItem_Fonte, editItem.fonte)
          set(elements.formEditItem_Identificador, editItem.fonteIdentificador)
          set(elements.formEditItem_Link, editItem.fonteLink)          
          // idempotencia     
          disable(elements.formEditItem_TipoPessoa)
          disable(elements.formEditItem_Documento)
          disable(elements.formEditItem_TipoRestricao)
          disable(elements.formEditItem_NivelSeveridade)
          disable(elements.formEditItem_Fonte)
          disable(elements.formEditItem_Identificador)          
          // display
          showPopup(elements.popUpSystemConfirm_EditItem)
          return
        }
      }

      switch ($(e.target).attr("id")) {
      
        case elements.btnSearch:
          search()
          break

        case elements.btnExportExcel:
          exportXLS()
          break

        case elements.btnNew:
          // limpeza dos campos
          check(elements.formNewItem_Ativo, true)
          setZero(elements.formNewItem_TipoPessoa)
          setZero(elements.formNewItem_TipoRestricao)
          setZero(elements.formNewItem_NivelSeveridade)
          set(elements.formNewItem_Documento, '')
          set(elements.formNewItem_ObsRestricao, '')          
          set(elements.formNewItem_DataInicio, '')
          set(elements.formNewItem_DataFim, '')
          set(elements.formNewItem_Fonte, '')
          set(elements.formNewItem_Identificador, '')
          set(elements.formNewItem_Link, '')          
          // display
          showPopup(elements.popUpSystemConfirm_NewItem)
          break

        case elements.btnAtualizarItem:
          // verificar campos de data (todo)
          postPrequalTokenPortal(Endpoints().prequal_blacklist_update, {
            Conteudo: [
              {
                codigoTipoPessoa: get(elements.formEditItem_TipoPessoa),
                documento: get(elements.formEditItem_Documento),
                tipoRestricao: get(elements.formEditItem_TipoRestricao),
                obsRestricao: get(elements.formEditItem_ObsRestricao),
                nivelSeveridade: get(elements.formEditItem_NivelSeveridade),
                dataInicio: get(elements.formEditItem_DataInicio).replace(/\D/g, ''),
                dataFim: get(elements.formEditItem_DataFim).replace(/\D/g, ''),
                fonte: get(elements.formEditItem_Fonte),
                fonteIdentificador: get(elements.formEditItem_Identificador),
                fonteLink: get(elements.formEditItem_Link),
                ativo: getCheck(elements.formEditItem_Ativo),
              }
            ]
          })
          .then((resp) => {
            if (!resp.ok) {
              displaySystemPopup(resp.erros[0].mensagem)
            }
            else {
              hideElement(elements.popUpSystemConfirm_EditItem)
              displaySystemPopupOK("Item atualizado com sucesso!")
              search()            
            }
          })
          break

        case elements.btnCriarItem:
          postPrequalTokenPortal(Endpoints().prequal_blacklist_update, {
            Conteudo: [
              {
                codigoTipoPessoa: getSelectedTextAndId(elements.formNewItem_TipoPessoa).descricao,
                documento: get(elements.formNewItem_Documento),
                tipoRestricao: getSelectedTextAndId(elements.formNewItem_TipoRestricao).descricao,
                obsRestricao: get(elements.formNewItem_ObsRestricao),
                nivelSeveridade: getSelectedTextAndId(elements.formNewItem_NivelSeveridade).descricao,
                dataInicio: get(elements.formNewItem_DataInicio).replace(/\D/g, ''),
                dataFim: get(elements.formNewItem_DataFim).replace(/\D/g, ''),
                fonte: get(elements.formNewItem_Fonte),
                fonteIdentificador: get(elements.formNewItem_Identificador),
                fonteLink: get(elements.formNewItem_Link),
                ativo: getCheck(elements.formNewItem_Ativo),
              }
            ]
          })
          .then((resp) => {
            if (!resp.ok) {
              displaySystemPopup(resp.erros[0].mensagem)
            }
            else {
              displaySystemPopupOK("Item criado com sucesso!")
              hideElement(elements.popUpSystemConfirm_NewItem)
              search()            
            }
          })
          break

        case elements.btnUploadFile:
          let inputFileUp = document.getElementById(elements.formFileUpload)  
          let files = inputFileUp.files
          if (files.length > 0) {
            let formDataFile = new FormData()
            formDataFile.append('arquivo', files[0])            
            hideElement(elements.spanMsgUploadFail)
            hideElement(elements.spanMsgUploadOK)
            hideElement(elements.pnlUploadMsgs)
            showElement(elements.spanMsgUploadProc)
            showElement(elements.formLoaderFileUpload)
            showPopup(elements.popUpSystemConfirm_FileUpload)
            postPrequalTokenPortalFile(Endpoints().prequal_blacklist_upload, formDataFile)
              .then((resp) => {
                hideElement(elements.formLoaderFileUpload)
                hideElement(elements.spanMsgUploadProc)                
                if (!resp.ok) {
                  let myTable_fileUpload = {
                    id: 'table_report',
                    header: ['Codigo', 'Mensagem'],
                    sizes: [ 90, 290 ],
                    data: [],
                  }
                  resp.erros.forEach(function (ar) {
                    myTable_fileUpload.data.push([
                      ar.codigo,
                      ar.mensagem                
                    ])
                  })
                  updateHTML(elements.table_report_fileUpload, buildTableSimple(myTable_fileUpload))
                  showElement(elements.spanMsgUploadFail)
                  showElement(elements.pnlUploadMsgs)                  
                }
                else {
                  let myTable_fileUpload = {
                    id: 'table_report',
                    header: [ 'Mensagem'],
                    sizes: [ 290 ],
                    data: [],
                  }                  
                  myTable_fileUpload.data.push(['Registros Atualizados: ' + resp.payload.registrosAtualizados ])
                  myTable_fileUpload.data.push(['Registros Novos: ' + resp.payload.registrosNovos ])                                  
                  updateHTML(elements.table_report_fileUpload, buildTableSimple(myTable_fileUpload))
                  showElement(elements.spanMsgUploadOK)
                  showElement(elements.pnlUploadMsgs)                   
                }
              })
          }
          break

        case 'popupCloseFileUpload':
          location.reload()
          break
      }
    })
  }
}
