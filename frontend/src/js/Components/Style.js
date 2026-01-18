import { BaseLoaderMin } from "@app/Components/Images/BaseLoader"
import StyleHtml from "./Style.html"

export default class {

  static getTitleAndSubtitle(btnList, title, subtitle) {
    let colorTitle = localStorage.getItem('stColorTitle')
    let colorSubtitle = localStorage.getItem('stColorSubtitle')
    return `<div align='left'>
  <div style='margin-left:32px;margin-right:32px;width:960px'>
    <table>
      <tr>
        <td valign='top'>
          <p class='button green' style='float:right;margin-top:24px' title='Voltar para a listagem' id='${btnList}'>Lista</p>
        </td>
        <td width='16px'></td>
        <td valign='top'>
          <table>
            <tr>
              <td><h2 style='color:${colorTitle}'>${title}</h2></td>
              <td><h2 style='color:${colorTitle}'> / </h2></td>
              <td><h2 style='color:#E50006' id='LblItem'></h2></td>
            </tr>
          </table>
          <h3 style='color:${colorSubtitle}'>${subtitle}</h2>            
        </td>
      </tr>
    </table>`
  }

  static getListTitleAndSubtitle(title, subtitle, btnOrganization) {
    let colorTitle = localStorage.getItem('stColorTitle')
    let colorSubtitle = localStorage.getItem('stColorSubtitle')
    return `<div align='left'>
  <div style='margin-left:32px;margin-right:32px;width:95%'>
    <table>
      <tr>
        ${btnOrganization != undefined ? `
        <td valign='top'><br><div id="${btnOrganization}" style='margin-top:6px' class="button block red">Organização</div></td>
        <td width='20px'></td>
        ` : ``}
        <td valign='top'>
          <h2 style='color:${colorTitle}'>${title}</h2>
          <h3 style='color:${colorSubtitle}'>${subtitle}</h2>            
        </td>
      </tr>
    </table>`
  }

  static getMainDialog() {
    return `<div id='_mainDialog' style="width:960px;min-height:400px;background-color:#cacaca">`
  }

  static getSubDialog() {
    return `<div id='_subDialog' style="width:960px;min-height:400px;background-color:white">`
  }

  static getMainFilter(formSearch, btnAdv, formLoader, btnSearch, btnNew, placeholder, tipNew, injectFilter = '') {
    if (btnNew == undefined) btnNew = ''
    if (placeholder == undefined) placeholder = ''
    if (tipNew == undefined) tipNew = ''
    return `<br>
    <table width='800px'>
      <tr>
        <td width='150px'><span id='lblFilter'>Filtro geral</span></td>
        <td width='200px'>
          ${btnAdv == "" ?
        `<input id="${formSearch}" placeholder="${placeholder}" >` :
        `<table><tr><td><input id="${formSearch}" placeholder="${placeholder}"></td>
          <td>
            <img title='Filtro avançado' id="${btnAdv}" style='max-height:48px;box-shadow: 0 6px 10px 0 rgba(0,0,0,.1);cursor:pointer' src='src/img/filter.png' />
          </td></tr></table>
          `}
        </td>
        <td width='20px'></td>        
        ${injectFilter == "" ? '' : `<td width='300px'>${injectFilter}</td><td width='20px'></td>` }
        <td width='200px'>
          ${BaseLoaderMin(formLoader)}
          <div align='center' id="${btnSearch}" style='display:none' class="button block green">Pesquisar</div>
        </td>
        <td width='20px'>
          ${btnNew == "" ? '' : `<div align='center' id="${btnNew}" title='${tipNew}' class="button block blue">+</div>`}
        </td>
      </tr>
    </table>
    <br>
    `
  }

  static getMainFilterMobile(formSearch, formLoader, btnSearch) {
    return `<br>
    <table width='0px'>
      <tr>
        <td width='140px'><input id="${formSearch}"></td>
        <td width='12px'></td>
        <td width='140px'>
          ${BaseLoaderMin(formLoader)}
          <div align='center' id="${btnSearch}" style='display:none' class="button block green">Pesquisar</div>
        </td>
      </tr>
    </table>
    <br>
    `
  }

  static getStyleHtml(injectStyle) 
  {
    let str = StyleHtml;
    if (injectStyle !== undefined) str += `<style>${injectStyle}</style>`;
    return str;
  }
}
