
        const tabla = document.getElementById("tablaDocKeys");

        async function cargarClaves() {
        tabla.innerHTML = "";
        const res = await fetch("https://localhost:7209/api/dockeys");
        const claves = await res.json();

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

        async function eliminarClave(id) {
        await fetch(`https://localhost:7209/api/dockeys/${id}`, { method: "DELETE" });
        mostrarToast("Clave eliminada", "success");
        cargarClaves();
        }

        cargarClaves();