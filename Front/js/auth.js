// Recupera el token almacenado
const token = localStorage.getItem("Token");

// Si no hay token, lo solicita automáticamente con credenciales predeterminadas.
// Si existe, valida si sigue siendo válido.
if (!token) {
    getToken();
} else {
    validToken();
}

/**
 * Solicita un nuevo token JWT desde la API usando credenciales predeterminadas.
 * El token recibido se almacena en localStorage.
 */
async function getToken() {
    const usuario = "Admin@email.com";
    const contrasena = "Administrador123";
    const payload = { usuario, contrasena };

    const res = await fetch("https://localhost:7209/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if (res.ok) {
        const tokenResponse = await res.json();
        localStorage.setItem("Token", tokenResponse.token);
    }
}

/**
 * Valida si el token actual sigue siendo válido.
 * Si el token ha expirado (401), redirige a la pantalla de login.
 */
async function validToken() {
    const token = localStorage.getItem("Token");

    const res = await fetch("https://localhost:7209/api/logprocces", {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    if (res.status === 401) {
        window.location.href = "login.html"; // token expirado, forzar re-login
    }
}

// Al cargar el DOM, agrega funcionalidad de cierre de sesión
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("SingOut").addEventListener("click", function (e) {
        e.preventDefault();
        localStorage.removeItem("Token");
        window.location.href = "login.html";
    });
});
