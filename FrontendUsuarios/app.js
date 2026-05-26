const API_URL = "https://localhost:44302/api/usuarios";

async function obtenerUsuarios()
{
    try
    {
        const reponse = await fetch(API_URL);
        
        const usuarios = await reponse.json();
        
        const tbody = document.getElementById("usuariosBody");

        tbody.innerHTML = "";

        usuarios.forEach(usuario =>
            {
                tbody.innerHTML += `
                    <tr>
                        <td>${usuario.id}</td>
                        <td>${usuario.nombre}</td>
                        <td>${usuario.email}</td>
                    </tr>
                `;
            });
    }
    catch(error){
        console.error(error);
    }
}

obtenerUsuarios();