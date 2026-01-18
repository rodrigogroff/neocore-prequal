export default class {
  static getHtml() {

    var menu = `<li class='active' style='font-size:12px'>
        <li class='active'>
          <a style='cursor:pointer;color:white' id='userNameMenu'></a>
          <ul>
            <li><br></li>
          </ul>
        </li>
      <br>`

    menu += `<li>
        <a style='cursor:pointer'><b>Usuários</b></a>
        <ul>            
          <li><a href="/usuarios_listing" style='cursor:pointer'>Colaboradores</a></li>          
          <li><br></li>
        </ul>
      </li>`

    menu += `<li>
        <a style='cursor:pointer'><b>Pré-Qualificação</b></a>
        <ul>
          <li><a href="/prequal_blacklist_listing" style='cursor:pointer'>Blacklist</a></li>
          <li><a href="/prequal_restricao_listing" style='cursor:pointer'>Config: Tipo de restrição</a></li>  
          <li><a href="/prequal_severidade_listing" style='cursor:pointer'>Config: Nivel de severidade</a></li>  
          <li><a href="/d" style='cursor:pointer'>Config: N1 (Proposta Dataprev)</a></li>  
          <li><a href="/e" style='cursor:pointer'>Config: N2 (Dados completos)</a></li>  
          <li><a href="/f" style='cursor:pointer'>Estatisticas</a></li>  
          <li><br></li>
        </ul>
      </li>`

    menu += `<br>
        <li>
          <a style='cursor:pointer'><b>Sair</b></a>
          <ul>
            <li><a href="/" style='cursor:pointer'>Confirma saída?</a></li>
            <li><br></li>            
          </ul>
        </li>
      <li>`

    return `<div align="left" style='font-size:11px'>
              <div class="wrapper-inline img shadow">
                <header id='appHeader'>
                  <div id='menuMobile' class="navi-menu-button"><em></em><em></em><em></em></div>
                </header>                
                <div id='menuMobile' class="nav-menu" align='left'>
                  <nav class="menu">
                    <div class="nav-container" style="background-image: url('src/img/wall_1.jpg');">
                      <img style='margin-top:8px;margin-left:52px' src='src/img/logoMenu.webp' srcset='src/img/logoMenu.webp 1x' alt='LogoMenu' width='210' height='80'/>
                      <ul style='margin-top:8px' class="main-menu">${menu}</ul>
                      <br><br><br>                      
                      <br>
                    </div>
                  </nav>
                </div>
              </div>
            </div>
            <div id='companyName' style='cursor:pointer;z-index:510;color:white;position:absolute;margin-top:16px;margin-left:86px'></div>
            <div id='userName' class='navi-menu-name' style='cursor:pointer;z-index:520;color:white;font-weight:bold;margin-top:16px;margin-right:32px'></div>`
  }
}