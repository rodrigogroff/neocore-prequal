
export function updateHTML(idElement, html) {
  $("#" + idElement).html(html);
}

export function buildCheckLine(chk, id, name) {
  return "<table><tr><td><input type='checkbox' id='" + chk + id +
    "' value='2'/></td><td width='10px'></td><td><label for='" + chk + id + "'>" +
    name + "</label></td></tr></table>";
}

export function removeOptions(selectElement) {
  let i, L = selectElement.options.length - 1;
  for (i = L; i >= 0; i--) {
    selectElement.remove(i);
  }
}

export function buildSelectOption(text, val) {
  let a = document.createElement("option");
  a.setAttribute("value", val);
  let b = document.createTextNode(text);
  a.appendChild(b);
  return a;
}

export function buildTableSimple(tableobj) {
  let lineData = "";
  if (tableobj.data.length > 0) {
    let size = tableobj.header.length;
    lineData = "<table class='table' id='" + tableobj.id + "'><thead><tr>";
    for (let h = 0; h < size; ++h)
      lineData +=
        "<th align='left' width='" +
        tableobj.sizes[h] +
        "'>" +
        tableobj.header[h] +
        "</th > ";
    lineData += "</tr></thead><tbody>";
    for (let d = 0; d < tableobj.data.length; ++d) {
      let ar = tableobj.data[d];
      lineData += "<tr>";
      for (let h = 0; h < size; ++h)
        lineData +=
          "<td  id='" +
          d +
          "' _par_table='" +
          tableobj.id +
          "' _par_table_item_id='" + d + "' valign='top'>" +
          ar[h] +
          "</td>";
      lineData += "</tr>";
    }
    lineData += "</tbody></table>";
  }
  else
    lineData = "<br><b>Nenhum registro encontrado</b><br>";
  return lineData;
}
