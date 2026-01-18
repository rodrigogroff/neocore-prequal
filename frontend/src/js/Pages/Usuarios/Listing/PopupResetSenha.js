import { BaseLoader } from "@app/Components/Images/BaseLoader"
import CheckboxList from "@app/Components/Fields/CheckBoxList"

export default class {
    static getHtml(elements) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_ResetSenha">
<div class="popup-container" style='margin-top:50px;max-width:515px;z-index:1;height:330px' align='center'>
  <div class="popup-header">
    <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemResetSenha'>
      Limpar senha do usuário
    </h3>
    <span id='popupCloseResetSenha' class="popup-close" data-dismiss="true">X</span>
  </div>
  <div class="popup-content">
    <div style='margin-left:30px;margin-right:30px' align='left'><br><br>
    <br>
      <table width='420px' align='center' style='font-weight:bold'>
        <tr>
          <td width='120px'>Email do usuário</td>
          <td width='230px'>
            <input id="${elements.formResetSenha_Email}" disabled>
          </td>
          <td width='20px'></td>
        </tr>
        <tr>
          <td>Nova senha</td>
          <td>
            <input id="${elements.formResetSenha_NovaSenha}">
          </td>
          <td></td>
        </tr>
      </table>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderResetSenha)}</div>
      <br><br><br>
      <div align='center'>
          <a id="${elements.btnAtualizarSenha}" class="button green">Atualizar senha</a>
      </div>
      <br><br>      
    </div>
  </div>
</div>
</div>`        
    }
}
