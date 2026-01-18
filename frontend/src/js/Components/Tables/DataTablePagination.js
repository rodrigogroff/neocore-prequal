import { getFromSession, setToSession } from "@app/Infra/Util"

export default class {
  static getHtml(totItens, itensPerPage, currentPage, totalPages) {
    // Ensure inputs are integers
    var injectTotItens = parseInt(totItens, 10);
    var itensPerPageInt = parseInt(itensPerPage, 10);
    var currentPageInt = parseInt(currentPage, 10);
    var totalPagesInt = parseInt(totalPages, 10);

    var initPage = currentPageInt - 5;
    if (initPage < 1) initPage = 1;

    var startRes = (currentPageInt - 1) * itensPerPageInt + 1;
    var endRes = startRes + itensPerPageInt - 1;

    if (endRes > injectTotItens) endRes = injectTotItens;

    var previous = currentPageInt - 1;
    if (previous < 1) previous = 1;

    var buttonHtml = "<span style='margin-right:12px' class='button gray'>";
    var buttonSelHtml = "<span style='margin-right:12px' class='button red'>";
    var term = "</span>";
    var injectButtons =
        buttonHtml.replace("class=", "id='firstPage' class=") +
        " Primeiro " +
        term +
        buttonHtml.replace("class=", "id='previousPage' class=") +
        " Anterior " +
        term;

    var maxDisplayItens = 10;

    while (initPage <= totalPagesInt) {
        if (initPage == currentPageInt)
            injectButtons += buttonSelHtml + initPage + term;
        else
            injectButtons +=
                buttonHtml.replace("class=", "id='gotoPage' _page='" + initPage + "' class=") +
                initPage +
                term;

        initPage++;
        maxDisplayItens--;

        if (maxDisplayItens == -1) break;
    }

    injectButtons +=
        buttonHtml.replace("class=", "id='nextPage' class=") +
        " Próximo " +
        term +
        buttonHtml.replace("class=", "id='lastPage' class=") +
        " Último" +
        term;

    return injectTotItens !== 0
        ? `<div align='left'>
                <table width='900px'>
                    <tr height='52px'>
                        <td valign='middle' width='250px'><span style='font-size:small'>Resultados [${startRes} a ${endRes}] de ${injectTotItens}</span></td>
                        <td valign='middle'>${injectButtons}</td>
                    </tr>
                </table>
            </div>`
        : ``;
}

  static CheckClicks(e) {
    switch ($(e.target).attr("id")) {      
      case "firstPage":
        setToSession('pageNumber', '1')
        return true        
      case "lastPage":
        setToSession('pageNumber', getFromSession('totalPages'))
        return true
      case "gotoPage":
        setToSession('pageNumber', $(e.target).attr("_page"))
        return true
      case "previousPage": {
          let current = Number(getFromSession('pageNumber'))
          if (current > 1) {
            current = current - 1;
            setToSession('pageNumber', current.toString())
            return true
          }
        }          
        break
      case "nextPage": {  
      let current = Number(getFromSession('pageNumber'))
        if (current < Number(getFromSession('totalPages'))) {
          current = current + 1;
          setToSession('pageNumber', current.toString())
          return true
        }          
      }
      break
    }
  }
}
