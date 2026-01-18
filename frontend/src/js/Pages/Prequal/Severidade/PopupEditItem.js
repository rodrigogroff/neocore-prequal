import { BaseLoader } from "@app/Components/Images/BaseLoader"

export default class {
    static getHtml(elements) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_EditItem">
<div class="popup-container" style='margin-top:50px;max-width:875px;z-index:1;height:300px' align='center'>
  <div class="popup-header">
    <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemEditItem'>
      Editar nivel de severidade
    </h3>
    <span id='popupCloseEditItem' class="popup-close" data-dismiss="true">X</span>
  </div>
  <div class="popup-content">
    <div style='margin-left:30px;margin-right:30px' align='left'><br><br>
    <br>
      <table width='700px' align='center' style='font-weight:bold'>
        <tr>
          <td width='120px'>Id</td>
          <td width='190px'>
            <input id="${elements.formEditItem_Id}" disabled>
          </td>
          <td width='20px'></td>
          <td width='120px'>Descrição</td>
          <td width='190px'>
            <input id="${elements.formEditItem_Descricao}">
          </td>
        </tr>        
      </table>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderEditItem)}</div>
      <br><br><br>
      <div align='center'>
        <a id="${elements.btnAtualizarItem}" class="button red">Atualizar Item</a>
      </div>
      <br><br>      
    </div>
  </div>
</div>
</div>`        
    }
}
