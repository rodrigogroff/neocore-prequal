
export function logout() {
  localStorage.removeItem("token");
  localStorage.removeItem("email");
  localStorage.removeItem("name");
  localStorage.removeItem("maxTime");
}

export function isAdmin() {
  return localStorage.getItem("admin") == "true";
}

export function isManager() {
  return localStorage.getItem("manager") == "true";
}

export function saveCompanyData(company) {
  localStorage.setItem("stColorBG", company.stColorBG);
  localStorage.setItem("stColorBG2", company.stColorBG2);
  localStorage.setItem("stColorBG3", company.stColorBG3);
  localStorage.setItem("stColorText", company.stColorText);
  localStorage.setItem("stColorTitle", company.stColorTitle);
  localStorage.setItem("stColorSubtitle", company.stColorSubtitle);
  localStorage.setItem("nuCountUsers", company.nuCountUsers);
}

export function loginOk(resp) {

  localStorage.setItem("token", resp.access_token);
  localStorage.setItem("userName", resp.userName);
  localStorage.setItem("userEmail", resp.userEmail);

  let dataNow = new Date()
  let minutes = 60 * 1
  let tokenObsoleteDate = new Date(dataNow.getTime() + minutes * 60000)
  let maxTime = JSON.stringify(tokenObsoleteDate)

  localStorage.setItem("maxTime", maxTime)
}

export function isAuthenticated() {
  var ret = localStorage.getItem("token");
  if (ret == null || ret == undefined) {
    location.href = "/";
    return null;
  }
  var strMaxTime = localStorage.getItem("maxTime");
  if (strMaxTime == null || strMaxTime == undefined) {
    location.href = "/";
    return null;
  }
  var maxTime = new Date(JSON.parse(strMaxTime));
  var dataNow = new Date();
  if (dataNow > maxTime) {
    location.href = "/";
    return null;
  }
}

export function getUserLogged() {
  var ret = localStorage.getItem("token");
  if (ret == null || ret == undefined) {
    return null;
  }
  return {
    email: localStorage.getItem("email"),
    nome: localStorage.getItem("name"),
  };
}
