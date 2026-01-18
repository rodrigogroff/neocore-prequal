export default class {
  static getHtml(id, style, injectAttributes) {
    if (style) style = "style='" + style + "'"
    return `<select class='custom-select' id='${id}' ${style} ${injectAttributes} />`
  }
}
