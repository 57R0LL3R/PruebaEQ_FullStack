/**
 * Carga los registros de procesamiento desde la API y los muestra en la tabla.
 * Cada registro incluye: nombre original, estado, nuevo nombre y fecha.
 */
async function cargarLogs() {
  const tabla = document.getElementById("tablaLogs");

  // Mostrar mensaje temporal mientras se cargan los datos
  tabla.innerHTML = `
        <tr>
          <td colspan="4" class="text-center text-secondary">Cargando...</td>
        </tr>
    `;

  await validToken();
  const token = localStorage.getItem("Token");

  const res = await fetch("https://localhost:7209/api/logprocces", {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`
    }
  });

  const logs = await res.json();

  await new Promise(resolve => setTimeout(resolve, 300));

  // Limpiar y rellenar tabla
  tabla.innerHTML = "";
  logs.forEach(log => {
    tabla.innerHTML += `
      <tr>
        <td>${log.originalFileName}</td>
        <td>${log.status}</td>
        <td>${log.newFileName}</td>
        <td>${new Date(log.dateProcces).toLocaleString()}</td>
      </tr>
    `;
  });
}

// Ejecuta carga inicial
cargarLogs();
