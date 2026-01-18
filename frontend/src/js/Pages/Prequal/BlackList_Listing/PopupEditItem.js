import { BaseLoader } from "@app/Components/Images/BaseLoader"
import CheckboxList from "@app/Components/Fields/CheckBoxList"

export default class {
    static getHtml(elements) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_EditItem">
<div class="popup-container" style='margin-top:50px;max-width:875px;z-index:1;height:600px' align='center'>
  <div class="popup-header">
    <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemEditItem'>
      Editar item da black list
    </h3>
    <span id='popupCloseEditItem' class="popup-close" data-dismiss="true">X</span>
  </div>
  <div class="popup-content">
    <div style='margin-left:30px;margin-right:30px' align='left'><br><br>
    <br>
      <table width='700px' align='center' style='font-weight:bold'>
        <tr>
          <td width='120px'>Ativo</td>
          <td width='190px'>
            ${CheckboxList.getHtml([ { id: elements.formEditItem_Ativo }], '160')}
          </td>
          <td width='20px'></td>
          <td width='120px'></td>
          <td width='190px'>
          </td>
        </tr>
        <tr>
          <td>Tipo pessoa</td>
          <td>
            <input id="${elements.formEditItem_TipoPessoa}">
          </td>
          <td></td>
          <td>Documento</td>
          <td>
            <input id="${elements.formEditItem_Documento}">
          </td>
        </tr>
        <tr>
          <td>Tipo restrição</td>
          <td>
            <input id="${elements.formEditItem_TipoRestricao}">
          </td>
          <td></td>
          <td>Nivel severidade</td>
          <td>
            <input id="${elements.formEditItem_NivelSeveridade}">
          </td>
        </tr>
        <tr>
          <td>Obs. Restrição</td>
          <td colspan='4'>
            <input id="${elements.formEditItem_ObsRestricao}">
          </td>          
        </tr>
        <tr>
          <td>Data inicio</td>
          <td>
            <input id="${elements.formEditItem_DataInicio}">
          </td>
          <td></td>
          <td>Data fim</td>
          <td>
            <input id="${elements.formEditItem_DataFim}">
          </td>
        </tr>
        <tr>
          <td>Fonte</td>
          <td>
            <input id="${elements.formEditItem_Fonte}">
          </td>
          <td></td>
          <td>Identificador</td>
          <td>
            <input id="${elements.formEditItem_Identificador}">
          </td>
        </tr>
        <tr>
          <td>Link da fonte</td>
          <td colspan='4'>
            <input id="${elements.formEditItem_Link}">
          </td>          
        </tr>
      </table>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderEditItem)}</div>
      <br><br><br>
      <div align='center'>
        <table align='center'>
          <tr>
            <td><a id="${elements.btnAtualizarItem}" class="button red">Atualizar Item</a></td>
            <td width='20px'></td>
            <td><a id="${elements.btnVisualizarHist}" class="button green">Visualizar Histórico</a></td>
          </tr>
        </table>        
      </div>
      <br><br>      
    </div>
  </div>
</div>
</div>`        
    }
}
