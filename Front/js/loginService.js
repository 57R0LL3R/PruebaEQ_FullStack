/**
 * Maneja el evento de envío del formulario de login.
 * Envía usuario y contraseña a la API y guarda el token si es válido.
 */
document.getElementById("loginForm").addEventListener("submit", async function (e) {
  e.preventDefault();

  const usuario = document.getElementById("usuario").value;
  const contrasena = document.getElementById("contrasena").value;

  const payload = { usuario, contrasena };

  try {
    const res = await fetch("https://localhost:7209/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    if (res.ok) {
      const data = await res.json();
      localStorage.setItem("Token", data.token);
      mostrarToast("Inicio de sesión exitoso", "success");
      setTimeout(() => window.location.href = "index.html", 1500);
    } else {
      mostrarToast("Usuario o contraseña incorrectos", "error");
    }
  } catch (error) {
    mostrarToast("Error de conexión con el servidor", "error");
    console.error(error);
  }
});

/**
 * Rellena automáticamente el formulario con el usuario y contraseña por defecto.
 * Útil para pruebas rápidas.
 */
document.getElementById("singin").addEventListener("click", function (e) {
  e.preventDefault();

  const usuario = document.getElementById("usuario");
  const contrasena = document.getElementById("contrasena");
  usuario.value = "admin@email.com";
  contrasena.value = "Administrador123";
});
