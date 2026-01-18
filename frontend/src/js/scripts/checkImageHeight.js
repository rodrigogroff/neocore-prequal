function checkImageHeight(id) {
	var input = document.getElementById(id)
	if (input.files && input.files[0]) {
		var reader = new FileReader()
		reader.onload = function (e) {
			var img = new Image()
			img.src = e.target.result
			img.onload = function () {
				if (img.height > 120) {
					alert("Imagem precisa ter altura máxima em 120 pixels")
					input.value = ""
				}
				if (img.width > 120) {
					alert("Imagem precisa ter largura máxima em 120 pixels")
					input.value = ""
				}
			}
		}
		reader.readAsDataURL(input.files[0])
	}
}