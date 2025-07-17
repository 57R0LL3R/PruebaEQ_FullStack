# PruebaEQ - Sistema de carga y procesamiento de archivos PDF

Este proyecto permite subir archivos PDF, asociarlos a claves personalizadas (DocKeys) y visualizar los registros de procesamiento, todo protegido con autenticación mediante JWT.

##  Características principales

-  Inicio de sesión con autenticación JWT.
-  Carga de archivos PDF con validaciones.
-  Asociación de claves (DocKeys) a documentos.
-  Visualización de logs con historial de archivos procesados.
-  Interfaz web moderna con Bootstrap.
-  Verificación automática del token y cierre de sesión.


## Seguridad

- El token JWT se guarda en `localStorage`.
- Todas las peticiones a la API se envían con el header `Authorization: Bearer <token>`.
- Si el token expira, se redirige al login.

## 📄 Requisitos

- Servidor backend .NET API con rutas:
  - `/api/auth/login`
  - `/api/dockeys`
  - `/api/pdf`
  - `/api/logprocces`

##  Pendientes o mejoras

- Validación de roles o permisos.
- Carga múltiple de archivos.
- Interfaz más accesible para dispositivos móviles.

##  Objetivo

Este proyecto fue desarrollado como una solución práctica para un flujo de carga y procesamiento de documentos, ideal para entornos internos, administrativos o educativos.

