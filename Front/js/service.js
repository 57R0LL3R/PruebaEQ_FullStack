

    document.getElementById("uploadForm").addEventListener("submit", async function (e) {
    e.preventDefault();
    const file = document.getElementById("pdfFile").files[0];
    const spinner = document.getElementById("spinner");



    const formData = new FormData();
    formData.append("file", file);

    spinner.style.display = "inline-block";
    let Tc = true;
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
    try {
      const res = await fetch("https://localhost:7209/api/pdf", {
        method: "POST",
        body: formData
      });

      if (res.ok) {
        
      } else {
        mostrarToast("Error al guardar el archivo", "error");
      }
    } catch {
      mostrarToast("No se pudo conectar con el servidor", "error");
      Tc = false;
    } finally {
      spinner.style.display = "none";
    }
    if(Tc){
        mostrarToast("Archivo subido correctamente", "success");
        document.getElementById("uploadForm").reset();
    }
  });