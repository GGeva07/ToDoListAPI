//metodo agregar tarea

document.addEventListener("DOMContentLoaded", function () {
    // Crear el modal dinámicamente
    const modal = document.createElement("div");
    modal.innerHTML = `
        <div class="modal fade" id="tareaModal" tabindex="-1" aria-labelledby="tareaModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="tareaModalLabel">Agregar Tarea</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form id="formTarea">
                            <div class="mb-3">
                                <input type="text" class="form-control" id="tituloTarea" placeholder="Título" required>
                            </div>
                            <div class="mb-3">
                                <textarea class="form-control" id="contenidoTarea" placeholder="Descripción" required></textarea>
                            </div>
                            <div class="mb-3">
                                <label>Estado</label>
                                <div class="contenedor-radios">
                                    <input type="radio" id="completada" value="completada" name="state">
                                    <label for="completada">Completada</label>
                                    <input type="radio" id="en_proceso" value="en proceso" name="state">
                                    <label for="en_proceso">En proceso</label>
                                    <input type="radio" id="pendiente" value="pendiente" name="state">
                                    <label for="pendiente">Pendiente</label>
                                    <input type="radio" id="cancelada" value="cancelada" name="state">
                                    <label for="cancelada">Cancelada</label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                <button type="submit" class="btn btn-primary" id="guardarCambios">Guardar</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    `;

    // Agregar el modal al body
    document.body.appendChild(modal);

    // Obtener el SVG y agregarle el evento para abrir el modal
    const svgAbrirModal = document.querySelector(".bi-plus-square-fill");
    if (svgAbrirModal) {
        svgAbrirModal.addEventListener("click", function () {
            const modalElement = new bootstrap.Modal(document.getElementById("tareaModal"));
            modalElement.show();
        });
    }

    // Agregar evento al formulario para enviar la tarea con fetch
    document.getElementById("formTarea").addEventListener("submit", async function (event) {
        event.preventDefault();

        try {
            const idUsuario = localStorage.getItem("idUsuario"); 
            if (!idUsuario) {
                alert("Debes iniciar sesión para agregar una tarea");
                return;
            }

            const nombre = document.getElementById("tituloTarea").value.trim();
            const contenido = document.getElementById("contenidoTarea").value.trim();
            const estadoSeleccionado = document.querySelector('input[name="state"]:checked');

            if (!nombre || !contenido) { 
                alert("Por favor completa todos los campos");
                return;
            }

            let estado = estadoSeleccionado ? estadoSeleccionado.value : null;
            if (!estado) {
                alert("Debe seleccionar un estado para la tarea");
                return;
            }

            const nuevaTarea = {
                nombre: nombre, 
                contenido: contenido, 
                estado: estado,
                idUsuario: parseInt(idUsuario)
            };

            console.log("Enviando tarea:", nuevaTarea); 

            const response = await fetch("https://localhost:5005/api/Tarea/Post-Tarea", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(nuevaTarea),
            });

            if (!response.ok) {
                throw new Error("Error en la solicitud: " + response.status);
            }

            const data = await response.json();
            console.log("Tarea agregada:", data);
            alert("Tarea agregada correctamente");

            const modalBootstrap = bootstrap.Modal.getInstance(document.getElementById("tareaModal"));
            modalBootstrap.hide();

            obtenerTareas(idUsuario);

        } catch (error) {
            console.error("Error en el fetch:", error);
            alert("Error en la solicitud: " + error.message);
        }
    });
    obtenerTareas(idUsuario);
});


//metodo para buscar una tarea 
const formBarraBusqueda = document.getElementById("formbarra");

if (formBarraBusqueda) {
    formBarraBusqueda.addEventListener("submit", async function (event) {
        event.preventDefault();

        try {
            const name = document.getElementById("Barra-Busqueda");

            let nombre = name.value;
            console.log("Nombre de la tarea:" , nombre);
            if (nombre.value) {
                console.error("Tareas no encontradas");
                alert("Tarea no encontrada");
                return;
            }else{
                obtenerTarea(nombre);
            }

        } catch (error) {
            console.error("Error en el fetch:", error);
            alert("Error en la solicitud: " + error.message);
        }
    });
}

//obtenemos las tareas en la api por medio del titulo para filtrarlas

function obtenerTarea(nombre) {
    fetch(`https://localhost:5005/api/Tarea/Get-TareaByTitle/${nombre}`)
        .then(response => response.json())
        .then(tareas => {
            console.log("Tareas obtenidas:", tareas);
            mostrarTareas(tareas);

        })
        .catch(error => console.error("Error al obtener las tareas:", error));
}

//metodo para mostrar tareas 
const idUsuario = localStorage.getItem("idUsuario"); 
     console.log("ID de Usuario obtenido:", idUsuario); 

if (idUsuario) {
    obtenerTareas(idUsuario);
} else {
    console.error("No se encontró el idUsuario en la URL");
}

//obtenemos las tareas en la api por medio del id usuario
function obtenerTareas(idUsuario) {
    fetch(`https://localhost:5005/api/Tarea/Get-Tareas-by-Usuario/${idUsuario}`)
        .then(response => response.json())
        .then(tareas => {
            console.log("Tareas obtenidas:", tareas);
            mostrarTareas(tareas);
            mostrarTareasPorEstado(tareas);
        })
        .catch(error => console.error("Error al obtener las tareas:", error));
}

//mostramos las tareas en la lista de tareas
function mostrarTareas(tareas) {
    const listaTareas = document.querySelector(".list-group");
    listaTareas.innerHTML = "";

    tareas.forEach(tarea => {
        const tareaHTML = `
            <a href="#" class="list-group-item list-group-item-action">
                <div class="d-flex w-100 justify-content-between">
                    <h5 class="mb-1">${tarea.nombre}</h5>
                    <small>
                        <svg onclick="mostrarModalEditar('${tarea.id}')" xmlns="http://www.w3.org/2000/svg" width="20" height="20" "fill="currentColor" class="bi bi-pencil-square " viewBox="0 0 16 16">
                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293z"/>
                            <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                        </svg>
                        <svg onclick="eliminarTarea('${tarea.id}', '${tarea.idUsuario}')" xmlns="http://www.w3.org/2000/svg" width="20" height="20" "fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1z"/>
                        </svg>
                    </small>
                </div>
                <p class="mb-1">${tarea.contenido}</p>
                <small>Estado: ${tarea.estado}</small>
            </a>
        `;
        listaTareas.innerHTML += tareaHTML;
    });
}


//metodo para agregar tareas a la lista de tareas
//separando las tareas por estado

function mostrarTareasPorEstado(tareas) {
    const listaTareas = document.querySelector(".Lista-Tareas-lateral");
    
    listaTareas.querySelectorAll(".tareas-estado").forEach(el => el.innerHTML = "");

    const estados = ["completada", "en proceso", "pendiente", "cancelada"];
    
    estados.forEach(estado => {
        const estadoElemento = listaTareas.querySelector(`h6[value='${estado}']`);
        if (!estadoElemento) return;

        const contenedorTareas = document.createElement("div");
        contenedorTareas.classList.add("tareas-estado");

        const tareasFiltradas = tareas.filter(tarea => tarea.estado.toLowerCase() === estado);
        tareasFiltradas.forEach(tarea => {
            const tareaHTML = `
                <a href="#" class="list-group-item list-group-item-action">
                    <div class="d-flex w-auto  justify-content-between text-aling-center">
                        <h7 class="mb-1 titulos-tareas">${tarea.nombre}</h7>
                        <small>
                            <svg onclick="eliminarTarea('${tarea.id}', '${tarea.idUsuario}')" xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"  class="bi bi-trash-fill" viewBox="0 0 16 16">
                                <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1z"/>
                            </svg>
                        </small>
                    </div>
                </a>
            `;
            contenedorTareas.innerHTML += tareaHTML;
        });
        estadoElemento.insertAdjacentElement("afterend", contenedorTareas);
    });
}

//Este es el codigo para generar el modal que edita las tareas
//se abre al hacer click en el icono de editar
async function mostrarModalEditar(id) {
    const modalHTML = `
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Editar Tarea</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form id="formEditarTarea">
                            <div class="mb-3">
                                <input type="text" class="form-control" id="Titulo-Tarea" placeholder="Titulo" required>
                            </div>
                            <div class="mb-3">
                                <textarea class="form-control" id="Contenido-Tarea" placeholder="Descripción" required></textarea>
                            </div>
                            <div class="mb-3">
                                <label>Estado</label>
                                <div class="contenedor-radios">
                                    <input type="radio" id="completada" value="completada" name="estado">
                                    <label for="completada">Completada</label>
                                    
                                    <input type="radio" id="en_proceso" value="en proceso" name="estado">
                                    <label for="en_proceso">En proceso</label>
                                    
                                    <input type="radio" id="pendiente" value="pendiente" name="estado">
                                    <label for="pendiente">Pendiente</label>
                                    
                                    <input type="radio" id="cancelada" value="cancelada" name="estado">
                                    <label for="cancelada">Cancelada</label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn boton_modal" data-bs-dismiss="modal">Cerrar</button>
                                <button type="submit" class="btn btn-form boton_modal" id="guardarCambios">Guardar</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    `;

    // Insertar el modal en el bodY Y llamar al metodo actualizar tareas 
    document.body.insertAdjacentHTML("beforeend", modalHTML);
    try {
        const response = await fetch(`https://localhost:5005/api/Tarea/Get-TareaById/${id}`);
        if (!response.ok) throw new Error("No se pudo obtener la tarea");

        const tarea = await response.json();

        document.getElementById("Titulo-Tarea").value = tarea.nombre;
        document.getElementById("Contenido-Tarea").value = tarea.contenido;
        document.querySelectorAll('input[name="estado"]').forEach(radio => {
            radio.checked = radio.value === tarea.estado;
        });

        document.getElementById("exampleModal").setAttribute("data-id", id);
        document.getElementById("exampleModal").setAttribute("data-idUsuario", tarea.idUsuario);
    } catch (error) {
        console.error("Error al obtener la tarea:", error);
        alert("Error al obtener la tarea");
    }
    const modal = new bootstrap.Modal(document.getElementById("exampleModal"));
    modal.show();

    document.getElementById("formEditarTarea").addEventListener("submit", actualizarTarea);
}

//este es el metodo que actualiza las tareas
//se llama al hacer click en el boton de guardar cambios del modal
async function actualizarTarea(event) {
    event.preventDefault();

    const modalElement = document.getElementById("exampleModal");
    const id = modalElement.getAttribute("data-id");
    const idUsuario = modalElement.getAttribute("data-idUsuario");

    if (!id || !idUsuario) {
        alert("Error: No se encontró la información de la tarea.");
        return;
    }

    const nombre = document.getElementById("Titulo-Tarea").value;
    const contenido = document.getElementById("Contenido-Tarea").value;
    const estado = document.querySelector('input[name="estado"]:checked')?.value || "Pendiente";

    const tareaActualizada = { id, nombre, contenido, estado };

    console.log("Datos enviados:", tareaActualizada);

    try {
        const response = await fetch(`https://localhost:5005/api/Tarea/Put-Tarea/${id}/${idUsuario}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(tareaActualizada)
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error("Error en la respuesta del servidor:", errorText);
            throw new Error("No se pudo actualizar la tarea");
        }

        alert("Tarea actualizada correctamente");

        const modal = bootstrap.Modal.getInstance(modalElement);
        modal.hide();
        modalElement.remove();
        await obtenerTareas(idUsuario);

    } catch (error) {
        console.error("Error al actualizar la tarea:", error);
        alert("Error al actualizar la tarea: " + error.message);
    }
}

//metodo para eliminar tareas
//se llama al hacer click en el icono de eliminar
async function eliminarTarea(id, idUsuario) {


    console.log("id tarea" , id +" y id usuario ", idUsuario)
    Swal.fire({
        title: "¿Estás seguro?",
        text: "Esta acción no se puede deshacer!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Sí, eliminar!"
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {
                await fetch(`https://localhost:5005/api/Tarea/Delete-Tarea/${id}/${idUsuario}`, {
                    method: "DELETE"
                });
                Swal.fire("Eliminado!", "La tarea ha sido eliminada.", "success");
                obtenerTareas(localStorage.getItem("idUsuario"));
            } catch (error) {
                console.error("Error al eliminar tarea:", error);
                Swal.fire("Error", "No se pudo eliminar la tarea", "error");
            }
        }
    });
}


