# Autenticación usando JWT (JSON Web Token) con ASP.NET Core
API que solicita autenticación mediante JWT. Contiene como método de prueba la conversión de dólares (USD) a soles (PEN).
Para probar esta API debes seguir los siguientes pasos:
1. Ejecuta el método "GeneracionToken", el cual genera dinámicamente el JWT. Copia este valor.
2. Ejecuta el método de prueba "ConversionMoneda". Para realizar esto de manera exitosa, debes estar primero autorizado, caso contrario el método retornará el código de error 401 (Unauthorized). Para estar autorizado, puedes hacerlo de dos formas:
- Si estas consumiedo la API desde un navegador, presiona el botón "Authorize" de SWAGGER y pega el JWT.
- Si estas consumiendo la API desde POSTMAN, ve a la pestaña "Authorization", escoge el Type "Bearer Token" y en la caja de texto "Token" pega el JWT.
