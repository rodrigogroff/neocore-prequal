
import { fadeIn, fadeOut } from "@app/Infra/ScreenFX";

export function CheckPopUpCloseClick(e) {
  if ($(e.target.parentElement)[0].id.startsWith("popupClose")) {
    fadeOut($("#popUpSystem")[0], 120);
    return true;
  }
  if ($(e.target.parentElement)[0].id.startsWith("popupCloseOK")) {
    fadeOut($("#popUpSystemOK")[0], 120);
    return true;
  }
  return false;
}

export function displaySystemPopup(_text) {
  fadeIn($("#popUpSystem")[0], 60);
  $("#popUpSystemText").text(_text);
}

export function displaySystemPopupOK(_text) {
  if (_text == null || _text == "" || _text == undefined)
    _text = "Sistema atualizado!"
  fadeIn($("#popUpSystemOK")[0], 60);
  $("#popUpSystemTextOK").text(_text);
}
