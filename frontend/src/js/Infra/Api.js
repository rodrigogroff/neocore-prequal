
// ---------------------------------------
// Back Office API locations
// ---------------------------------------

export function getBOApiGetLocation() {
  let lstNodes = JSON.parse(process.env.API_BO_GET_NODES)
  let randomIndex = Math.floor(Math.random() * lstNodes.length)
  return lstNodes[randomIndex]
}

export function getBOApiPostPutLocation() {
  let lstNodes = JSON.parse(process.env.API_BO_POST_NODES)
  let randomIndex = Math.floor(Math.random() * lstNodes.length)
  return lstNodes[randomIndex]
}

export function getBOApiFileLocation() {
  let lstNodes = JSON.parse(process.env.API_BO_FILE_NODES)
  let randomIndex = Math.floor(Math.random() * lstNodes.length)
  return lstNodes[randomIndex]
}

// ---------------------------------------
// Prequal API locations
// ---------------------------------------

export function getPrequalApiGetLocation() {
  let lstNodes = JSON.parse(process.env.API_PREQUAL_GET_NODES)
  let randomIndex = Math.floor(Math.random() * lstNodes.length)
  return lstNodes[randomIndex]
}

export function getPrequalApiPostPutLocation() {
  let lstNodes = JSON.parse(process.env.API_PREQUAL_POST_NODES)
  let randomIndex = Math.floor(Math.random() * lstNodes.length)
  return lstNodes[randomIndex]
}

export function getPrequalApiFileLocation() {
  let lstNodes = JSON.parse(process.env.API_PREQUAL_FILE_NODES)
  let randomIndex = Math.floor(Math.random() * lstNodes.length)
  return lstNodes[randomIndex]
}

// ---------------------------------------
// Back Office API functions
// ---------------------------------------

export function getBOTokenPortal(endpoint, parameters) {
  let ApiLocation = getBOApiGetLocation()
  let ret = new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint + "?" + parameters,
      {
        "method": "GET",
        "headers": {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Authorization": "Bearer " + localStorage.getItem("token"),
        },
      }
    )
      .then((res) => {
        if (res.ok === true)
          res.json().then((data) => { resolve({ ok: true, payload: data }) })
        else
          resolve({ ok: false })
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        })
      })
  })
  return ret
}

export function postBOTokenPortalFile(endpoint, formData) {
  return new Promise((resolve, reject) => {
    let ApiLocation = getBOApiFileLocation()
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint,
      {
        'method': 'POST',        
        'headers': 
        { 
          'Access-Control-Allow-Origin': '*',
          "Authorization": "Bearer " + localStorage.getItem("token") 
        },
        "body": formData
      })
      .then((res) => { resolve({ ok: res.ok }) })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        })
      })
  })
}

export function postBOTokenPortal(endpoint, _obj) {
  let ApiLocation = getBOApiPostPutLocation()
  let obj = JSON.stringify(_obj)
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint,
      {
        "method": "POST",
        "headers": {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Authorization": "Bearer " + localStorage.getItem("token"),
        },
        "body": obj,
      }
    )
    .then(async (res) => {
      let data = null
      try {
        const text = await res.text()
        if (text) {
          data = JSON.parse(text)
        }
      } catch (e) {
      }
      resolve({ 
        ok: res.ok,
        status: res.status,
        payload: data
      })
    })
    .catch((errorMsg) => {
      resolve({
        ok: false,
        msg: errorMsg.toString(),
      })
    })
  })
}

export function postBOTokenPortalWithRet(endpoint, _obj) {
  let ApiLocation = getBOApiPostPutLocation()
  let obj = JSON.stringify(_obj)
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint,
      {
        "method": "POST",
        "headers": {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Authorization": "Bearer " + localStorage.getItem("token"),
        },
        "body": obj,
      }
    )
      .then((res) => {
        if (res.ok === true)
          res.json().then((data) => { resolve({ ok: true, payload: data }) })
        else
          resolve({ ok: false })
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        })
      })
  })
}

// custom login!

export function postBOPublicLogin(location, _obj) {
  let ApiLocation = getBOApiPostPutLocation()
  let obj = JSON.stringify(_obj)
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + location,
      {
        "method": "POST",
        "headers": {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Authorization" : process.env.API_CLIENTID_SECRET,
        },
        "body": obj,
      }
    )
      .then((res) => {
        if (res.ok === true)
          res.json().then((data) => { resolve({ ok: true, payload: data }) })
        else
          resolve({ ok: false })
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        })
      })
  })
}

// ---------------------------------------
// Prequal API functions
// ---------------------------------------

export function getPrequalTokenPortal(endpoint, parameters) {
  let ApiLocation = getPrequalApiGetLocation()
  let ret = new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint + "?" + parameters,
      {
        "method": "GET",
        "headers": {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Authorization": "Bearer " + localStorage.getItem("token"),
        },
      }
    )
      .then((res) => {
        if (res.ok === true)
          res.json().then((data) => { resolve({ ok: true, payload: data }) })
        else
          resolve({ ok: false })
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        })
      })
  })
  return ret
}

export function postPrequalTokenPortalFile(endpoint, formData) {
  return new Promise((resolve, reject) => {
    let ApiLocation = getPrequalApiFileLocation()
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint,
      {
        "method": 'POST',
        "headers": 
        { 
          "Authorization": "Bearer " + localStorage.getItem("token") 
        },
        "body": formData
      })
      .then(async (res) => {
        let responseData = null;
        try {
          responseData = await res.json();
        } catch (e) {
          responseData = await res.text();
        }
        resolve({ 
          ok: res.ok,
          status: res.status,
          payload: responseData,
          erros: responseData?.erros || null
        })
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        })
      })
  })
}

export function postPrequalTokenPortal(endpoint, _obj) {
  let ApiLocation = getPrequalApiPostPutLocation()
  let obj = JSON.stringify(_obj)
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/" + endpoint,
      {
        "method": "POST",
        "headers": {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Authorization": "Bearer " + localStorage.getItem("token"),
        },
        "body": obj,
      }
    )
    .then(async (res) => {
      let data = null
      try {
        const text = await res.text()
        if (text) {
          data = JSON.parse(text)
        }
      } catch (e) {
      }
      resolve({ 
        ok: res.ok,
        status: res.status,
        payload: data
      })
    })
    .catch((errorMsg) => {
      resolve({
        ok: false,
        msg: errorMsg.toString(),
      })
    })
  })
}
