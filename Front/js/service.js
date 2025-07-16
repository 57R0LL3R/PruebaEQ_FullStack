    document.getElementById("uploadForm").addEventListener("submit", async function(e) {
    e.preventDefault();
    const file = document.getElementById("pdfFile").files[0];

    if (!file) {
        mostrarToast("Debe seleccionar un archivo", "warning");
        return;
    }

    if (file.type !== "application/pdf") {
        mostrarToast("Solo se permiten archivos PDF", "error");
        return;
    }

    if (file.size > 10 * 1024 * 1024) {
        mostrarToast("El archivo excede los 10 MB", "error");
        return;
    }
    
    const formData = new FormData();
    formData.append("file", file);
    const response = await fetch(`https://localhost:7209/api/pdf`,{
        method :"POST",
        body : formData
    })
    
    if (response.ok) {
        mostrarToast("Archivo subido correctamente", "success");
    } else {
        mostrarToast("Error al guardar el archivo", "error");
    }
    });