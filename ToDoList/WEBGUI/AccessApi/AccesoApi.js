// Login de usuarios
document.addEventListener("DOMContentLoaded", function () {
    const formAccesoUsuarios = document.getElementById("FormularioAccesoUsuarios");

    if (formAccesoUsuarios) {
        formAccesoUsuarios.addEventListener("submit", async function (event) {
            event.preventDefault();

            const correo = document.getElementById("email").value;
            const contrasenia = document.getElementById("password").value;

            try {
                const response = await fetch("https://localhost:5005/Login", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ correo, contrasenia })
                })
                .then(response => response.json())
                .then(data => {
                    console.log("Respuesta completa de la API:", data); 
                
                    if (data.usuario && typeof data.usuario.id !== "undefined") { 
                        localStorage.setItem("idUsuario", data.usuario.id);
                        console.log("ID de usuario guardado:", data.usuario.id);
                    } else {
                        console.error("No se recibió un ID de usuario. Estructura incorrecta:", data);
                    }
                })
                .catch(error => console.error("Error en el fetch:", error));
                
                alert("Login exitoso!");

                setTimeout(() => {
                    window.location.href = "../HtmlLayout/PrincipalLayout/PaginaPrincipalUser.html";
                }, 800);

            } catch (error) {
                console.error("Error en el fetch:", error);
                alert("Error en la solicitud: " + error.message);
            }
        });
    }

    // Registro de usuarios (Sign-In)
    const formLogearUsuario = document.getElementById("LogearUsuario");
    if (formLogearUsuario) {
        formLogearUsuario.addEventListener("submit", async function (event) {
            event.preventDefault();

            const usuarioNombre = document.getElementById("inputNombre").value;
            const correo = document.getElementById("inputCorreo").value;
            const contrasenia = document.getElementById("inputPass").value;

            try {
                const response = await fetch("https://localhost:5005/Sing-In", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ usuarioNombre, correo, contrasenia })
                });

                if (!response.ok) {
                    const errorData = await response.json();
                    alert("Error: " + errorData.message);
                    return;
                }

                
                alert("Registro exitoso!");
                setTimeout(() => {
                    window.location.href = "../HtmlLayout/LayoutInicioSesion.html";
                }, 800);
                
                const data = await response.json();

                if (data.Usuario && data.Usuario.id) {
                    localStorage.setItem("idUsuario", data.Usuario.id);
                    console.log("ID de usuario guardado:", data.Usuario.id);
                } else {
                    console.error("No se recibio un ID de usuario");
                    return;
                }


            } catch (error) {
                console.error("Error en el fetch:", error);
                alert("Error en la solicitud: " + error.message);
            }
        });
    }
});
