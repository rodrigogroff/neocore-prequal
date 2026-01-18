import { BaseLoader } from "@app/Components/Images/BaseLoader"
import CheckboxList from "@app/Components/Fields/CheckBoxList"
import SelectField from "@app/Components/Fields/Select"

export default class {
    static getHtml(elements) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_NewItem">
<div class="popup-container" style='margin-top:50px;max-width:875px;z-index:1;height:600px' align='center'>
  <div class="popup-header">
    <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemNewItem'>
      Novo item para black list
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
            ${CheckboxList.getHtml([ { id: elements.formNewItem_Ativo }], '160')}
          </td>
          <td width='20px'></td>
          <td width='120px'></td>
          <td width='190px'>
          </td>
        </tr>
        <tr>
          <td>Tipo pessoa</td>
          <td>
            ${SelectField.getHtml(elements.formNewItem_TipoPessoa)}
          </td>
          <td></td>
          <td>Documento</td>
          <td>
            <input id="${elements.formNewItem_Documento}">
          </td>
        </tr>
        <tr>
          <td>Tipo restrição</td>
          <td>
            ${SelectField.getHtml(elements.formNewItem_TipoRestricao)}
          </td>
          <td></td>
          <td>Nivel severidade</td>
          <td>
            ${SelectField.getHtml(elements.formNewItem_NivelSeveridade)}
          </td>
        </tr>
        <tr>
          <td>Obs. Restrição</td>
          <td colspan='4'>
            <input id="${elements.formNewItem_ObsRestricao}">
          </td>          
        </tr>
        <tr>
          <td>Data inicio</td>
          <td>
            <input id="${elements.formNewItem_DataInicio}">
          </td>
          <td></td>
          <td>Data fim</td>
          <td>
            <input id="${elements.formNewItem_DataFim}">
          </td>
        </tr>
        <tr>
          <td>Fonte</td>
          <td>
            <input id="${elements.formNewItem_Fonte}">
          </td>
          <td></td>
          <td>Identificador</td>
          <td>
            <input id="${elements.formNewItem_Identificador}">
          </td>
        </tr>
        <tr>
          <td>Link da fonte</td>
          <td colspan='4'>
            <input id="${elements.formNewItem_Link}">
          </td>          
        </tr>
      </table>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderNewItem)}</div>
      <br><br><br>
      <div align='center'>
        <a id="${elements.btnCriarItem}" class="button red">Criar Item</a>
      </div>
      <br><br>      
    </div>
  </div>
</div>
</div>`        
    }
}
