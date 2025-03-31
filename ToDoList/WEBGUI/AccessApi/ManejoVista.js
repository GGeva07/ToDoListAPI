function redirigir(){
    document.getElementById("Mensaje").innerText = "Redirigiendo";
    setTimeout(() =>{
        window.location.href = "LoginUser.html";
    }, 800);
}

function redirigir2(){
    setTimeout(() =>{
        window.location.href = "../../HtmlLayout/LoginUser.html";
    }, 800);
}