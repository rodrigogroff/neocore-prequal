import { BaseLoader } from "@app/Components/Images/BaseLoader"

export default class {
    static getHtml(elements) {
        return `
<div class="popup-overlay" id="popUpSystemConfirm_FileUpload">
  <div class="popup-container" style='margin-top:50px;max-width:575px;z-index:1;height:450px' align='center'>
    <div class="popup-header">
      <h3 style='padding-left:32px;color:black' class="popup-title" id='popUpSystemFileUpload'>
        Processamento de arquivo de BlackList
      </h3>
      <span id='popupCloseFileUpload' class="popup-close" data-dismiss="true">X</span>
    </div>
    <div class="popup-content">
      <br><br><br>
      <div align='center'>
        <span id='${elements.spanMsgUploadProc}' style='display:block'>Realizando upload de arquivo....</span>
        <span id='${elements.spanMsgUploadFail}' style='display:none;color:red'>Falhas identificadas no arquivo</span>
        <span id='${elements.spanMsgUploadOK}' style='display:block'>Arquivo processado com sucesso!</span>
      </div>
      <br>
      <div align='center'>${BaseLoader(elements.formLoaderFileUpload)}</div>
      <br>
      <div id='${elements.pnlUploadMsgs}' align='center' style='height:250px; overflow-y: scroll;margin-right:40px;display:none;'>
        <div align='center' id='${elements.table_report_fileUpload}'></div>
        <br>
      </div>
      <br>
      <br>
    </div>
  </div>
</div>`        
    }
}
