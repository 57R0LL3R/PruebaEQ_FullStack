function mostrarToast(mensaje, tipo = "success") {
  const colores = {
    success: "text-white bg-success",
    error: "text-white bg-danger",
    warning: "text-dark bg-warning"
  };

  const clase = colores[tipo] || colores.success;

  const id = "toast_" + Date.now();

  const toastHTML = `
    <div id="${id}" class="toast align-items-center ${clase} border-0 mb-2" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="5000">
      <div class="d-flex">
        <div class="toast-body">${mensaje}</div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
      </div>
    </div>
  `;

  const container = document.getElementById("toastContainer");
  container.insertAdjacentHTML("beforeend", toastHTML);

  const toastElement = document.getElementById(id);
  const toast = new bootstrap.Toast(toastElement);
  toast.show();
}
