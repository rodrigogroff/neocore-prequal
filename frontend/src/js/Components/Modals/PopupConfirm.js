import { BaseLoader } from "@app/Components/Images/BaseLoader"

export default class {
    static getHtml(confirmTitle, confirmMsg) {
        return `<div class="popup-overlay" id="popUpSystemConfirm_">
                <div class="popup-container" style='margin-top:50px;max-width:475px;height:300px' align='center'>
                    <div class="popup-header">
                        <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemConfTitle'>${confirmTitle}</h3>
                        <span id='popupCloseConfirm' class="popup-close" data-dismiss="true">X</span>
                    </div>
                    <div class="popup-content">
                        <br><br>
                        <img src='src/img/error_big.webp' srcset='src/img/error_big.webp 1x' alt='Logo' width='60' height='60'/>
                        <span style="font-size:64px;color:red;padding-top:32px;" id='popupSymbol' class="fa fa-exclamation-circle"></span><br>
                        <br><span id='popUpSystemConfText' style="padding-top:16px;color:black">${confirmMsg}</span><br><br>
                        <br>
                        <br>
                        <div style='width:100px'>${BaseLoader('loaderPopupConfirm')}<p class='button red' id='btnPopoupConfirm'>Confirmar</p></div>
                        <br>
                        <br>
                    </div>
                </div>
            </div>`
    }
}
