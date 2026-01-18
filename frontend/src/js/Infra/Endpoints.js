export function Endpoints() {
  return {
    // auth / users
    authenticate: "backoffice/token",
    bo_user_listing: "usuarios/obter-usuarios",
    bo_user_update: "usuarios/atualizar-usuario",
    bo_user_new: "usuarios/incluir-usuario",
    bo_reset_pass: "usuarios/reset-senha",
    // prequal blacklist
    prequal_blacklist_listing: "blacklist/obter",
    prequal_blacklist_update: "blacklist/gerenciar",
    prequal_blacklist_upload: "blacklist/upload",
    // prequal enums (obter / atualizar)
    prequal_enums_tipoPessoa: "enums/tipo-pessoa",
    prequal_enums_tipoRestricao: "enums/tipo-restricao",
    prequal_enums_nivelSeveridade: "enums/nivel-severidade",
  }
}
