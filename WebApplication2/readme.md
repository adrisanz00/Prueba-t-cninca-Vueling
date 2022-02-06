BASE DE DATOS
- En el fichero appsettings.json tenemos especifica la conexion a la base de datos, y para realizar la migracion desde la consola de paquetes ejecutamos los comandos
"add-migration" y después "update-database" asi ya tendremos nuestra base de datos con las tablas y relaciones definidas.

DISTRIBUCIÓN PROYECTO	
- Esta organizado por controladores, Migraciones, modelos, el data context, el manejo de errores, herramientas útiles y por último  el log donde se registran.

EJECUCIÓN
- Al ejecutar el proyecto se nos abrirá el swagger para poder realizar desde ahí todas las peticiones necesarias y ver si están funcionando