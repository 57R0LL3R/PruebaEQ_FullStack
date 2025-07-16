    async function cargarLogs() {
      const tabla = document.getElementById("tablaLogs");
      tabla.innerHTML = `
            <tr>
            <td colspan="4" class="text-center text-secondary">Cargando...</td>
            </tr>
        `;



      const res = await fetch("https://localhost:7209/api/logprocces");
      const logs = await res.json();

      await new Promise(resolve => setTimeout(resolve, 300)); 

      tabla.innerHTML = "";
        console.log(logs)
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
    cargarLogs();