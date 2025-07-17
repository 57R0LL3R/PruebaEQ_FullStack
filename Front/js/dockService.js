const tabla = document.getElementById("tablaDocKeys");

/**
 * Carga todas las claves desde la API y las muestra en la tabla.
 */
async function cargarClaves() {
    // Mensaje temporal mientras carga
    tabla.innerHTML = `
        <tr>
            <td colspan="4" class="text-center text-secondary">Cargando...</td>
        </tr>
    `;

    await validToken();
    const token = localStorage.getItem("Token");

    const res = await fetch("https://localhost:7209/api/dockeys", {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    const claves = await res.json();

    await new Promise(resolve => setTimeout(resolve, 300)); // retardo visual

    tabla.innerHTML = ""; // limpiar la tabla

    claves.forEach(clave => {
        tabla.innerHTML += `
            <tr>
                <td>${clave.id}</td>
                <td>${clave.key}</td>
                <td>${clave.docName}</td>
                <td>
                    <a class="btn btn-warning btn-sm" href="dockey-form.html?id=${clave.id}">Editar</a>
                    <button class="btn btn-danger btn-sm" onclick="eliminarClave(${clave.id})">Eliminar</button>
                </td>
            </tr>
        `;
    });
}

/**
 * Elimina una clave por su ID
 * @param {number} id - ID del elemento a eliminar
 */
async function eliminarClave(id) {
    await validToken();
    const token = localStorage.getItem("Token");

    await fetch(`https://localhost:7209/api/dockeys/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    mostrarToast("Clave eliminada", "success");
    cargarClaves(); // recargar tabla
}

// Ejecutar carga inicial
cargarClaves();
