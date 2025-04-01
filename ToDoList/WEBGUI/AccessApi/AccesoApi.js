document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("FormularioAccesoUsuarios").addEventListener("submit", async function (event) {
        event.preventDefault();

        const correo = document.getElementById("email").value;
        const contrasenia = document.getElementById("password").value;

        const response = await fetch("https://localhost:5005/Login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ correo, contrasenia })
        });

        const data = await response.json();
        if (response.ok) {

            alert("Login exitoso!");
            console.log("Token:", data.token);

            setTimeout(() =>{
                window.location.href = "../HtmlLayout/PrincipalLayout/PaginaPrincipalUser.html";
            }, 800);
        } else {
            alert("Error: " + data.message);
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("LogearUsuario").addEventListener("submit", async function (event) {
        event.preventDefault();

        const usuarioNombre = document.getElementById("inputNombre").value;
        const correo = document.getElementById("inputCorreo").value;
        const contrasenia = document.getElementById("inputPass").value;

        try {
            const response = await fetch("https://localhost:5005/api/Usuario/Post-Usuario", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ usuarioNombre, correo, contrasenia })
            });

            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            const data = await response.json();
                        alert("Login exitoso!");
            console.log("Token:", data.token);
            console.log("Respuesta de la API:", data);

            alert("Registro exitoso!");
            window.location.href = "../HtmlLayout/LayoutInicioSesion.html";

        } catch (error) {
            console.error("Error en el fetch:", error);
            alert("Error en la solicitud: " + error.message);
        }
    });
});
