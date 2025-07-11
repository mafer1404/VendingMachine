# Manual de Uso

## Instalaciones necesarias para ejecución en un ambiente local

## Frontend Vue.js con Node.js

1. Asegurarse de estar en la carpeta adecuada para la ejecución del proyecto

```bash
cd CI-0126\VendingMachine\frontend
```

2. Instalar las dependencias necesarias para correr el proyecto:

```bash
npm install
```

```bash
npm install -g @vue/cli
```

```bash
npm install vue-router@4
```

```bash
npm install axios
```

3. Levantar el servidor de desarrollo

```bash
npm run serve
```

La dirección por defecto del frontend es `http://localhost:8080`.

## Backend .NET Web API

1. Asegurarse de estar en la carpeta adecuada para la ejecución del proyecto

```bash
cd CI-0126\VendingMachine\backend
```

2. Ejecutar manualmente el proyecto desde consola:

```bash
dotnet restore 
```

```bash
dotnet build 
```

```bash
dotnet run 
```

3. Para más facilidad puede abrir y ejecutar el proyecto desde `Visual Studio`, abriendo la solución `backend.sln` ubicada en la ruta:

```bash
CI-0126\VendingMachine\backend\backend.sln
```

La dirección del backend es `https://localhost:7295/`.
