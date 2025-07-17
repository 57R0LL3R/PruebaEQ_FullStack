/**
 * Controla el proceso de carga del archivo PDF desde el formulario.
 * Se valida el tipo, el tamaño, y se sube con token de autenticación.
 */
document.getElementById("uploadForm").addEventListener("submit", async function (e) {
  e.preventDefault();

  const file = document.getElementById("pdfFile").files[0];
  const spinner = document.getElementById("spinner");

  // Muestra el spinner mientras se procesa
  spinner.style.display = "inline-block";

  // Validaciones del archivo
  if (!file) {
    mostrarToast("Debe seleccionar un archivo", "warning");
    spinner.style.display = "none";
    return;
  }

  if (file.type !== "application/pdf") {
    mostrarToast("Solo se permiten archivos PDF", "error");
    spinner.style.display = "none";
    return;
  }

  if (file.size > 10 * 1024 * 1024) {
    mostrarToast("El archivo excede los 10 MB", "error");
    spinner.style.display = "none";
    return;
  }

  const formData = new FormData();
  formData.append("file", file);

  let exito = false;

  try {
    await validToken();
    const token = localStorage.getItem("Token");

    const res = await fetch("https://localhost:7209/api/pdf", {
      method: "POST",
      headers: {
        "Authorization": `Bearer ${token}`
      },
      body: formData
    });

    if (res.ok) {
      exito = true;
    } else {
      mostrarToast("Error al guardar el archivo", "error");
    }
  } catch {
    mostrarToast("No se pudo conectar con el servidor", "error");
  } finally {
    spinner.style.display = "none";
  }

  if (exito) {
    mostrarToast("Archivo subido correctamente", "success");
    document.getElementById("uploadForm").reset();
  }
});
