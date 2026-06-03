const API_URL = "https://localhost:44302/api/usuarios";

let usuarioEditandoId = null;
//Obtener Usuarios

async function obtenerUsuarios()
{
    try
    {
        const response = await fetch(API_URL);
        
        const usuarios = await response.json();
        
        const tbody = document.getElementById("usuariosBody");

        tbody.innerHTML = "";

        usuarios.forEach(usuario =>
            {
                tbody.innerHTML += `
                    <tr>
                        <td>${usuario.id}</td>
                        <td>${usuario.nombre}</td>
                        <td>${usuario.email}</td>
                        <td>
                            <button onclick="editarUsuario(${usuario.id})">
                                Editar
                            </button>
                             <button onclick="eliminarUsuario(${usuario.id})">
                                Eliminar
                            </button>

                        </td>
                    </tr>
                `;
            });
    }
    catch(error){
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

        const usuario = {
            nombre: nombre,
            email: email,
            password: password
        };
        
        try {
            let url = API_URL;
            let metodo = "POST";

            if(usuarioEditandoId !== null){
                url = `${API_URL}/${usuarioEditandoId}`;
                metodo = "PUT";
            }

            const response = await fetch(url,{
                method: metodo,
                headers:
                {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(usuario)
            });

            if (response.ok){

                if(metodo === "POST")
                {
                    alert("Usuario creado correctamente");
                }
                else
                {
                    alert("Usuario actualizado correctamente");
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

    if(!confirmar){
        return;
    }

    try{
        const response = 
            await fetch(
                `${API_URL}/${id}`,
                {
                    method: "DELETE"
                }
            );
        if(response.ok){
            alert("usuario eliminado");

            obtenerUsuarios();
        }
    }
    catch(error){
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