
export default class {
  static getHtml(page, menu) {
    return `
      <div class="wrapper" align="center">
        ${menu}
        <div class="wrapper-inline img shadow">
          <header id='appHeader'>
            <div class="navi-menu-button">
              <em></em>
              <em></em>
              <em></em>
            </div>
          </header>
          <main>
            <section class="container">
              <br>
              <div id='mainFormApp'>
                ${page}
              </div>
            </section>
          </main>
        </div>
      </div>`
  }

  static applyTheme() {
    document.getElementById('appHeader').style = "background-color: #4a4a4a"
    document.getElementById('companyName').innerHTML = `
      <a href='/main' style='color:white'>
         PORTAL CCP -- Back Office Setup
      </a>`
    document.getElementById('userNameMenu').innerHTML = `<span style='color:white'> > Bem-vindo,</span> <b>${localStorage.getItem('userName')}</b>`    
  }
}
