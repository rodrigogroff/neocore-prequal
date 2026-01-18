import IMask from 'imask';

export function displaySafeText(text, opt, flag) {
  if (text == null || text == undefined || text == 'undefined')
    return ''
  else {
    switch (opt) {
      case 'i': return '<i>' + text + '</i>'
      case 'red': return flag == false ? '<span style="color:red">' + text + '</span>' : text
      case 'href': return "<a href='" + text + "' target='_blank'>" + text + "</a>"
    }
    return text
  }
}

export function getParameterByName(name, _default) {
  let url = window.location.href;
  name = name.replace(/[\[\]]/g, "\\$&");
  let regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
    results = regex.exec(url);
  if (!results) return _default;
  if (!results[2]) return _default;
  return decodeURIComponent(results[2].replace(/\+/g, " "));
}

export function getFromStorage(tag) {
  return localStorage.getItem(tag);
}

export function setToStorage(tag, val) {
  localStorage.setItem(tag, val);
}

export function imageChange(id, img) {
  $(id).attr("src", "src/img/" + img);
}

export function addEnterKeyListener(formId, searchFunction) {
  document.getElementById(formId).addEventListener('keydown', function (event) {
    if (event.keyCode === 13) {
      searchFunction();
    }
  });
}

export function hideElement(elementId) {
  document.getElementById(elementId).style.display = 'none';
}

export function clearStyle(elementId) {
  document.getElementById(elementId).style = '';
}

export function showElement(elementId) {
  document.getElementById(elementId).style.display = 'block';
}

export function showPopup(elementId) {
  document.getElementById(elementId).style.display = 'block';
  document.getElementById(elementId).style.opacity = '100';
  document.getElementById(elementId).style.visibility = 'visible';
}

export function processCheckedList(tag, element) {
  var strBas = '';
  var _ba = sessionStorage.getItem(tag);
  if (_ba != undefined) {
    let bas = JSON.parse(_ba);
    bas.forEach(function (el) {
      var _c = document.getElementById(element + el.id);
      if (_c != null) {
        if (_c.checked) {
          strBas += el.id + ",";
        }
      }
    });
  }
  return strBas;
}

export function applyBorderStyle(formName) {
  document.getElementById(formName).style.border = "1px solid red";
}

function buildCheckLine(chk, id, name) {
  return "<table><tr><td><input type='checkbox' id='" + chk + id +
    "' value='2'/></td><td width='10px'></td><td><label for='" + chk + id + "'>" +
    name + "</label></td></tr></table>";
}

export function genHtmlForCheckboxes(results, sessionName, checkboxName) {
  let html = '';
  if (results != null) {
    sessionStorage.setItem(sessionName, JSON.stringify(results));
    results.forEach(function (ar) {
      html += buildCheckLine(checkboxName, ar.id, ar.stName);
    });
  }
  else
    html = ' -Nenhum item encontrado'
  return html;
}

export function tableClickHandler(tableId, list, companyUrl) {
  let table = document.getElementById(tableId);
  if (table != undefined && table != null) {
    let rows = table.getElementsByTagName("tr");
    for (let i = 0; i < rows.length; i++) {
      let currentRow = rows[i];
      let createClickHandler = function (row) {
        return function () {
          let ar = JSON.parse(sessionStorage.getItem(list));
          location.href = companyUrl + "?id=" + ar[row.childNodes[0].id].id;
        };
      };
      currentRow.onclick = createClickHandler(currentRow);
    }
  }
}

function buildSelectOption(text, val) {
  let a = document.createElement("option");
  a.setAttribute("value", val);
  let b = document.createTextNode(text);
  a.appendChild(b);
  return a;
}

export function populateSelectArea(results, tagId, sel) {
  if (sel == undefined) sel = '(Selecione)'
  let selectArea = document.getElementById(tagId);
  if (selectArea !== null) {
    selectArea.innerHTML = '';
    selectArea.appendChild(buildSelectOption(sel, 0));
    results.forEach(function (ar) {
      selectArea.appendChild(buildSelectOption(ar.descricao, ar.id));
    });
  }
}

export function cleanStyle(element) {
  document.getElementById(element).style.border = ""
}

export function cleanValue(element) {
  document.getElementById(element).value = ""
}

export function disable(element) {
  document.getElementById(element).disabled = true
}

export function enable(element) {
  document.getElementById(element).disabled = false
}

export function getStrCheckedItens(sessionName, checkboxName) {
  var str = ''
  var results = getFromSession(sessionName)
  if (results != undefined) {
    let res = JSON.parse(results)
    res.forEach(function (el) {
      var _c = document.getElementById(checkboxName + el.id)
      if (_c != null)
        if (_c.checked == 1)
          str += el.id + ","
    })
  }
  return str;
}

export function get(element) {
  return document.getElementById(element).value
}

export function getCheck(element) {
  return document.getElementById(element).checked
}

export function set(element, myValue) {
  document.getElementById(element).value = myValue
}

export function setPicture(element, myValue) {
  document.getElementById(element).src = myValue
}

export function reset(element) {
  document.getElementById(element).selectedIndex = 0; 
}

export function setZero(element, myValue) {
  document.getElementById(element).value = myValue > 0 ? myValue : 0
}

export function text(element, text) {
  document.getElementById(element).textContent = text
}

export function nullOrEmpty(element) {
  return (element == null || element == "")
}

export function check(element, myValue) {
  if (myValue == true)
    document.getElementById(element).checked = myValue
}

export function disableButton(btn) {
  $(btn).prop("disabled", true);
}

export function enableButton(btn) {
  $(btn).prop("disabled", false);
}

export function getSelectedTextAndId(select) {
  var s = document.getElementById(select);
  var id = s.value;
  var stName = ''
  for (let i = 0; i < s.options.length; i = i + 1) {
    let item = s.options[i]
    if (item.value == id) {
      if (item.text != '(Selecione)')
      {
        stName = item.text
        break
      }
    }
  }
  return {
    id,
    descricao: stName
  }
}

export function getCnpjMask() {
  return { mask: [{ mask: '00.000.000/0000-00' }] } 
}

export function getCepMask() {
  return { mask: [{ mask: '00000-000' }] } 
}

export function getDateMask() { 
  return {
    mask: Date,
    pattern: 'd/`m/`Y',
    blocks: {
      d: {
        mask: IMask.MaskedRange,
        from: 1,
        to: 31,
        maxLength: 2,
      },
      m: {
        mask: IMask.MaskedRange,
        from: 1,
        to: 12,
        maxLength: 2,
      },
      Y: {
        mask: IMask.MaskedRange,
        from: 1900,
        to: 2999,
        maxLength: 4,
      },      
    },
    format: (date) => {
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const year = date.getFullYear();
      return `${day}/${month}/${year}`;
    },
    parse: (str) => {
      const [day, month, year] = str.split('/');
      return new Date(year, month - 1, day);
    },
  };
}

export function getDateMinMask() {
  return {
    mask: Date,
    pattern: 'd/`m/`Y H:`M:`S',
    blocks: {
      d: {
        mask: IMask.MaskedRange,
        from: 1,
        to: 31,
        maxLength: 2,
      },
      m: {
        mask: IMask.MaskedRange,
        from: 1,
        to: 12,
        maxLength: 2,
      },
      Y: {
        mask: IMask.MaskedRange,
        from: 1900,
        to: 2999,
        maxLength: 4,
      },
      H: {
        mask: IMask.MaskedRange,
        from: 0,
        to: 23,
        maxLength: 2,
      },
      M: {
        mask: IMask.MaskedRange,
        from: 0,
        to: 59,
        maxLength: 2,
      },
      S: {
        mask: IMask.MaskedRange,
        from: 0,
        to: 59,
        maxLength: 2,
      },
    },
    format: (date) => {
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const year = date.getFullYear();
      const hours = String(date.getHours()).padStart(2, '0');
      const minutes = String(date.getMinutes()).padStart(2, '0');
      const seconds = String(date.getSeconds()).padStart(2, '0');
      return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
    },
    parse: (str) => {
      const [datePart, timePart] = str.split(' ');
      const [day, month, year] = datePart.split('/');
      const [hours, minutes, seconds] = timePart.split(':');
      return new Date(year, month - 1, day, hours, minutes, seconds);
    },
  };
}

export function getDigitsMask() { 
  return { mask: [{ mask: '00000000' }] } 
}

export function getCpfMask() { 
  return { mask: [{ mask: '000.000.000-00' }] } 
}

export function getFourDecimal() {
  return {
    mask: Number, // Máscara do tipo número
    scale: 4, // Quantidade de casas decimais
    signed: false, // Sem valores negativos
    thousandsSeparator: '', // Sem separador de milhar
    padFractionalZeros: false, // Não preencher com zeros
    normalizeZeros: false, // Remove zeros à direita
    radix: ',', // Define a vírgula como separador decimal
  }
}

export function getPhoneMask() { 
  return { mask: [[{ mask: '(00) 00000-0000', maxLength: 15 }, { mask: '+00 (00) 000000000' }]] } 
}

export function getFromSession(tag) {
  return sessionStorage.getItem(tag);
}

export function setToSession(tag, val) {
  sessionStorage.setItem(tag, val);
}

export function validarNome(name) {
  if (name.length < 2)
    return false;  
  if (!/^[\p{L}\s]+$/u.test(name)) 
    return false;  
  if (/\s{2,}/.test(name))
    return false;
  return true;
}

export function validarEmail(email) {
  let emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
  if (!emailRegex.test(email))
    return false;
  return true;
}

export function opcoesSexo() {
  return [{ id: 1, stName: 'Masculino' }, { id: 2, stName: 'Feminino' }]
}

export function opcoesMes() {
  return [
    { id: 1, stName: 'Janeiro' },
    { id: 2, stName: 'Fevereiro' },
    { id: 3, stName: 'Março' },
    { id: 4, stName: 'Abril' },
    { id: 5, stName: 'Maio' },
    { id: 6, stName: 'Junho' },
    { id: 7, stName: 'Julho' },
    { id: 8, stName: 'Agosto' },
    { id: 9, stName: 'Setembro' },
    { id: 10, stName: 'Outubro' },
    { id: 11, stName: 'Novembro' },
    { id: 12, stName: 'Dezembro' },
  ]
}


export function opcoesTipoContratacao() {
  return [
    { id: 1, stName: 'Contrato por Prazo Determinado' },
    { id: 2, stName: 'Contrato por Prazo Indeterminado' },
    { id: 3, stName: 'Contrato de Experiência' },
    { id: 4, stName: 'Contrato de Trabalho Temporário' },
    { id: 5, stName: 'Contrato de Estágio' },
    { id: 6, stName: 'Contrato de Trabalho Intermitente' },
    { id: 7, stName: 'Contrato de Trabalho em Regime Parcial' },
    { id: 8, stName: 'Contrato de Trabalho em Regime de Tempo Integral' },
    { id: 9, stName: 'Contrato de Trabalho Autônomo' },
    { id: 10, stName: 'Contrato de Trabalho em Home Office (Teletrabalho)' },
  ]
}

export function filterById(results, id) {
  var ret = []
  for (let i = 0; i < results.length; i = i + 1)
    if (results[i].id != id)
      ret.push(results[i])
  return ret;
}


export function optionsAllocationType() {
  return [
    { id: 1, stName: 'CLT' },
    { id: 2, stName: 'PJ' },
  ]
}

export function validarCNPJ(cnpj) {
  cnpj = cnpj.replace(/[^\d]+/g, '');

  if (cnpj == '') return false;

  if (cnpj.length != 14)
    return false;

  // Elimina CNPJs inválidos conhecidos
  if (cnpj == "00000000000000" ||
    cnpj == "11111111111111" ||
    cnpj == "22222222222222" ||
    cnpj == "33333333333333" ||
    cnpj == "44444444444444" ||
    cnpj == "55555555555555" ||
    cnpj == "66666666666666" ||
    cnpj == "77777777777777" ||
    cnpj == "88888888888888" ||
    cnpj == "99999999999999")
    return false;

  // Valida DVs
  var tamanho = cnpj.length - 2
  var numeros = cnpj.substring(0, tamanho);
  var digitos = cnpj.substring(tamanho);
  var soma = 0;
  var pos = tamanho - 7;
  for (let i = tamanho; i >= 1; i--) {
    soma += numeros.charAt(tamanho - i) * pos--;
    if (pos < 2)
      pos = 9;
  }
  var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
  if (resultado != digitos.charAt(0))
    return false;
  tamanho = tamanho + 1;
  numeros = cnpj.substring(0, tamanho);
  soma = 0;
  pos = tamanho - 7;
  for (let i = tamanho; i >= 1; i--) {
    soma += numeros.charAt(tamanho - i) * pos--;
    if (pos < 2)
      pos = 9;
  }
  resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
  if (resultado != digitos.charAt(1))
    return false;
  return true;
}

export function validarCPF(cpf) {
  cpf = cpf.replace(/[^\d]/g, ''); // Remove caracteres não numéricos

  if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) return false; // Verifica se o CPF tem 11 dígitos e se não é uma sequência de números repetidos

  let soma = 0;
  for (let i = 0; i < 9; i++) {
    soma += parseInt(cpf.charAt(i)) * (10 - i);
  }

  let resto = 11 - (soma % 11);
  if (resto === 10 || resto === 11) resto = 0;

  if (resto !== parseInt(cpf.charAt(9))) return false;

  soma = 0;
  for (let i = 0; i < 10; i++) {
    soma += parseInt(cpf.charAt(i)) * (11 - i);
  }

  resto = 11 - (soma % 11);
  if (resto === 10 || resto === 11) resto = 0;

  if (resto !== parseInt(cpf.charAt(10))) return false;

  return true;
}

export function AddCEPEventListener(formAddressZipCode, formRegion, formCity, formAddress, formCountry) {
  document.getElementById(formAddressZipCode).addEventListener('keydown', function (event) {
    if (event.keyCode === 13) {
      const url = "https://viacep.com.br/ws/" + document.getElementById(formAddressZipCode).value + "/json/";
      fetch(url)
        .then(response => {
          if (response.ok)
            return response.json()
        })
        .then(data => {
          if (data.logradouro != undefined) {
            document.getElementById(formRegion).value = data.uf
            document.getElementById(formCity).value = data.localidade
            document.getElementById(formAddress).value = data.logradouro
            if (formCountry != null && formCountry != undefined)
              document.getElementById(formCountry).value = "Brasil"
          }
        })
    }
  })
}

export function isValidDate(dateString) {
  if (nullOrEmpty(dateString))
    return false;
  const parts = dateString.split('/')
  const day = parseInt(parts[0], 10)
  const month = parseInt(parts[1], 10) - 1
  const year = parseInt(parts[2], 10)
  const date = new Date(year, month, day)
  return (
    date.getDate() === day &&
    date.getMonth() === month &&
    date.getFullYear() === year &&
    !isNaN(date.getTime())
  )
}

export function isSecondDateHigher(dateString1, dateString2) {
  const parts1 = dateString1.split('/')
  const day1 = parseInt(parts1[0], 10)
  const month1 = parseInt(parts1[1], 10) - 1
  const year1 = parseInt(parts1[2], 10)
  const parts2 = dateString2.split('/')
  const day2 = parseInt(parts2[0], 10)
  const month2 = parseInt(parts2[1], 10) - 1
  const year2 = parseInt(parts2[2], 10);
  const date1 = new Date(year1, month1, day1)
  const date2 = new Date(year2, month2, day2)
  return date2 > date1
}
