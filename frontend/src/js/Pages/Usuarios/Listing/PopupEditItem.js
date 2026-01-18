import { BaseLoader } from "@app/Components/Images/BaseLoader"
import CheckboxList from "@app/Components/Fields/CheckBoxList"

export default class {
    static getHtml(elements) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_EditItem">
<div class="popup-container" style='margin-top:50px;max-width:875px;z-index:1;height:430px' align='center'>
  <div class="popup-header">
    <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemEditItem'>
      Editar credenciais do usuário
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
          <td>Email do usuário</td>
          <td colspan='4'>
            <input id="${elements.formEditItem_Email}">
          </td>          
        </tr>
        <tr>
          <td>Nome do usuário</td>
          <td colspan='4'>
            <input id="${elements.formEditItem_Nome}">
          </td>          
        </tr>        
        <tr>
          <td>Área do usuário</td>
          <td colspan='4'>
            <input id="${elements.formEditItem_Area}">
          </td>          
        </tr>        
      </table>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderEditItem)}</div>
      <br><br><br>
      <div align='center'>
        <table width='400px' align='center'>
          <tr>
            <td>
              <a id="${elements.btnAtualizarItem}" class="button green">Atualizar Usuário</a>
            </td>
            <td width='20px'></td>
            <td>
              <a id="${elements.btnResetSenha}" class="button red">Reset senha</a>
            </td>
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
