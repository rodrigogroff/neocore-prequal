export default class {
  static getHtml(list, size) {
    var str = '<table>'
    for (let i = 0; i < list.length; i = i + 1) {
      var obj = list[i]
      var id = obj.id
      var label = obj.label

      if (label == null || label == undefined || label == "") {
        str += `<tr>
                <td>
                  <div class="form-row no-padding">
                    <input type='checkbox' style='height:22px' id='${id}'/>
                  </div>
                </td>
              </tr>`
      }
      else
        str += `<tr>
                <td width='${size}px'><label for='${id}'>${label}</label></td>
                <td>
                  <div class="form-row no-padding">
                    <input type='checkbox' style='height:22px' id='${id}'/>
                  </div>
                </td>
              </tr>`
    }
    str += '</table>'
    return str
  }

  static getInvHtml(list, size) {
    var str = '<table>'
    for (let i = 0; i < list.length; i = i + 1) {
      var obj = list[i]
      var id = obj.id
      var label = obj.label

      if (label == null || label == undefined || label == "") {
        str += `<tr>
                <td>
                  <div class="form-row no-padding">
                    <input type='checkbox' style='height:22px' id='${id}'/>
                  </div>
                </td>
              </tr>`
      }
      else
        str += `<tr>                
                <td>
                  <div class="form-row no-padding">
                    <input type='checkbox' style='height:22px' id='${id}'/>
                  </div>
                </td>
                <td width='8px'></td>
                <td>${label}</td>
              </tr>`
    }
    str += '</table>'
    return str
  }
}