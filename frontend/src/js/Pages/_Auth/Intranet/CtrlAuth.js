import { Endpoints } from "@app/Infra/Endpoints"
import { postBOPublicLogin } from "@app/Infra/Api"
import { get, hideElement, showElement, showPopup } from "@app/Infra/Util"
import { applyBorderStyle, cleanStyle, cleanValue } from "@app/Infra/Util"
import { loginOk } from "@app/Infra/Auth"
import { displaySystemPopup, CheckPopUpCloseClick } from "@app/Infra/PopupControl"
import MasterPageSingle from "@app/Components/Views/MasterPageSingle"
import MyForm from "./ViewAuth"

window.addEventListener("resize", (e) => {
  MasterPageSingle.resize()
})

export default class {

    getHtml() {
        return MyForm.getHtml()
    }

    constructor() {

        $(document).ready(function() {

            MasterPageSingle.resize()
            localStorage.clear()
            
            // ---------------------
            // login
            // ---------------------

            function openLogin() {
                let elements = MyForm.elements()
                showPopup('popUpSystemConfirm_UserLogin')
                document.getElementById(elements.formEmailLogin).focus()
                cleanValue(elements.formEmailLogin)
                cleanValue(elements.formPassLogin)
            }

            function ulrRedirection(resp) {
                loginOk(resp.payload)   
                window.location.href = '/main'
            }

            // ------------------------
            // page clicks
            // ------------------------

            document.body.addEventListener("click", (e) => {
                if (CheckPopUpCloseClick(e))
                    return
                let elements = MyForm.elements()
                switch ($(e.target).attr("id")) {
                    
                    case elements.btnLogin:
                        return openLogin()

                    case elements.btnLoginWithPass:
                        cleanStyle(elements.formPassLogin)
                        var email = get(elements.formPassLogin)
                        if (email.length < 1) {
                            applyBorderStyle(elements.formPassLogin)
                        } else {
                            hideElement(elements.btnLoginWithPass)
                            showElement(elements.formLoaderLoginWithPass)
                            postBOPublicLogin(Endpoints().authenticate, {
                                    email: get(elements.formEmailLogin),
                                    senha: get(elements.formPassLogin),
                                })
                                .then((resp) => {
                                    if (resp.ok) {
                                        ulrRedirection(resp)
                                    } else {
                                        showElement(elements.btnLoginWithPass)
                                        hideElement(elements.formLoaderLoginWithPass)
                                        displaySystemPopup("Usuário ou senha incorreta")
                                    }
                                })
                        }
                        break
                }
            })
        })
    }
}
