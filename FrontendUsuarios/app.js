const API_URL = "https://localhost:44302/api/usuarios";

let usuarioEditandoId = null;
//Obtener Usuarios

async function obtenerUsuarios() {
    try {
        const response = await fetch(API_URL);

        const usuarios = await response.json();

        const tbody = document.getElementById("usuariosBody");

        tbody.innerHTML = "";

        usuarios.forEach(usuario => {
            tbody.innerHTML += `
                    <tr>
                        <td>${usuario.id}</td>
                        <td>${usuario.nombre}</td>
                        <td>${usuario.email}</td>
                        <td>
                        <button 
                            class="btn btn-warning btn-sm"
                            onclick="editarUsuario(${usuario.id})">

                            Editar

                            </button>
                            
                            <button
                                class="btn btn-danger btn-sm"
                                onclick="eliminarUsuario(${usuario.id})">

                                Eliminar

                            </button>

                        </td>
                    </tr>
                `;
        });
    }
    catch (error) {
        console.error(error);
    }
}

//Crear Usuario

const formulario = document.getElementById("usuarioForm");

formulario.addEventListener("submit",
    async function (event) {
        event.preventDefault();

        const nombre = document.getElementById("nombre").value;
        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;
        
        if(!nombre.trim() || !email.trim() || !password.trim())
        {
            alert("Todos los campos son obligatorios");
            return;
        }
        
        const usuario = {
            nombre: nombre,
            email: email,
            password: password
        };

        try {
            let url = API_URL;
            let metodo = "POST";

            if (usuarioEditandoId !== null) {
                url = `${API_URL}/${usuarioEditandoId}`;
                metodo = "PUT";
            }

            const response = await fetch(url, {
                method: metodo,
                headers:
                {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(usuario)
            });

            if (response.ok) {

                if (metodo === "POST") {
                    mostrarMensaje(
                        "Usuario creado correctamente",
                        "success"
                    );
                }
                else {
                    mostrarMensaje(
                        "Usuario actualizado correctamente",
                        "warning"
                    );
                }

                formulario.reset();
                usuarioEditandoId = null;
                document.getElementById("btnGuardar").textContent = "Guardar Usuario";
                obtenerUsuarios();
            }
        }
        catch (error) {
            console.error(error);
        }
    }
);

obtenerUsuarios();

//Delete
async function eliminarUsuario(id) {
    const confirmar = confirm("¿Desea eliminar este usuario?");

    if (!confirmar) {
        return;
    }

    try {
        const response =
            await fetch(
                `${API_URL}/${id}`,
                {
                    method: "DELETE"
                }
            );
        if (response.ok) {
            mostrarMensaje(
                "Usuario eliminado correctamente",
                "danger"
            );

            obtenerUsuarios();
        }
    }
    catch (error) {
        console.error(error);
    }
}

//UPDATE
async function editarUsuario(id) {
    try {
        const response = await fetch(`${API_URL}/${id}`);

        const usuario = await response.json();

        document.getElementById("nombre").value = usuario.nombre;
        document.getElementById("email").value = usuario.email;
        document.getElementById("password").value = usuario.password ?? "";

        usuarioEditandoId = id;
        document.getElementById("btnGuardar").textContent = "Actualizar Usuario";
    } catch (error) {
        console.error(error);
    }
}

function mostrarMensaje(texto, tipo){
    const mensaje = document.getElementById("mensaje");
    mensaje.innerHTML = `<div class = "alert alert-${tipo}">
        ${texto}
    </div>`;

    setTimeout(()=>{
        mensaje.innerHTML = "";
    },3000);
}