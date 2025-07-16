
    const params = new URLSearchParams(window.location.search);

    const id = params.get("id");
    if (id) {
      document.getElementById("titulo").textContent = "Editar Palabra Clave";
      fetch(`https://localhost:7209/api/dockeys/${id}`)
        .then(res => res.json())
        .then(data => {
          document.getElementById("clave").value = data.key;
          document.getElementById("docName").value = data.docName;
        });
    }

    document.getElementById("editForm").addEventListener("submit", async function (e) {
      e.preventDefault();
      const key = document.getElementById("clave").value;
      const docName = document.getElementById("docName").value;

      const payload = { key, docName };
      let url = "https://localhost:7209/api/dockeys";
      let method = "POST";

      if (id) {
        payload.id = parseInt(id);
        method = "PUT";
        url += `/${id}`;
      }

      const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });

      if (res.ok) {
        mostrarToast("Guardado correctamente", "success");
        setTimeout(() => window.location.href = "dockey.html", 2000);
      } else {
        mostrarToast("Error al guardar", "error");
      }
    });
  