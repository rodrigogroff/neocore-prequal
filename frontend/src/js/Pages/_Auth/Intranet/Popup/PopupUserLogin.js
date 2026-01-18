import { BaseLoader } from "@app/Components/Images/BaseLoader"

export default class {
    static getHtml(elements) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_UserLogin">
<div class="popup-container" style='margin-top:50px;max-width:675px;z-index:1;height:350px' align='center'>
  <div class="popup-header">
    <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemUserLogin'>
      Login
    </h3>
    <span id='popupCloseLogin' class="popup-close" data-dismiss="true">X</span>
  </div>
  <div class="popup-content">
    <div style='margin-left:30px;margin-right:30px' align='left'><br><br>
      <table width='300px' align='center'>
        <tr>
          <td width='80px'>Email</td>
          <td width='190px'>
            <input id="${elements.formEmailLogin}" placeholder="Digite aqui seu email">
          </td>
        </tr>
        <tr>
          <td width='80px'>Senha</td>
          <td width='190px'>
            <input id="${elements.formPassLogin}" placeholder="Digite aqui sua senha" type="password">
          </td>
        </tr>
      </table>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderLoginWithPass)}</div>
      <div align='center'>
        <a id="${elements.btnLoginWithPass}" class="button red">Efetuar login</a>
      </div>
      <br><br>      
    </div>
  </div>
</div>
</div>`        
    }
}
