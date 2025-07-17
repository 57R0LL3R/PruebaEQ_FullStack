// Obtiene el ID desde los parámetros de la URL
const params = new URLSearchParams(window.location.search);
const id = params.get("id");

// Si existe un ID, se trata de modo edición
if (id) {
    document.getElementById("titulo").textContent = "Edit key";
    getParams(); // Cargar datos existentes
}

/**
 * Obtiene los datos de una clave por su ID para mostrarlos en el formulario
 */
async function getParams() {
    await validToken();
    const token = localStorage.getItem("Token");

    const res = await fetch(`https://localhost:7209/api/dockeys/${id}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    const data = await res.json();

    document.getElementById("clave").value = data.key;
    document.getElementById("docName").value = data.docName;
}

/**
 * Envía el formulario de creación o actualización de una clave
 */
document.getElementById("editForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const key = document.getElementById("clave").value;
    const docName = document.getElementById("docName").value;
    const payload = { key, docName };

    let url = "https://localhost:7209/api/dockeys";
    let methodo = "POST";

    // Si hay ID, es una actualización (PUT)
    if (id) {
        payload.id = parseInt(id);
        methodo = "PUT";
        url += `/${id}`;
    }

    await validToken();
    const token = localStorage.getItem("Token");

    const res = await fetch(url, {
        method: methodo,
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
    });

    if (res.ok) {
        mostrarToast("Guardado correctamente", "success");
        setTimeout(() => window.location.href = "dockey.html", 2000);
    } else {
        mostrarToast("Error al guardar", "error");
    }
});
