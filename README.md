# URL Shortening Service

Un servicio de acortamiento de URL.

Más información en: [roadmap.sh/projects/url-shortening-service](https://roadmap.sh/projects/url-shortening-service)



## Angular

### URLShorteningServiceFront

El archivo de configuración de entorno para Angular se encuentra en `environment.ts`.

**Nota:**

- Durante el desarrollo local (usando Docker), el archivo puede dejarse tal cual.
- En producción, se debe actualizar el valor de `apiUrl` con el dominio correspondiente.

```typescript
export const environment = {
  production: true,
  apiUrl: 'http://localhost:5000'
};
```




## .NET

### Producción

Para la conexión a la base de datos en producción, se deben definir las variables de entorno en un archivo `.env` ubicado en la raíz del proyecto.

**Ejemplo de `.env` para una red de contenedores Docker:**

```
DB_SERVER=sqlserver,1433
DB_USER_ID=sa
DB_PASSWORD=sa.Bd.123
SA_PASSWORD=sa.Bd.123
```


> **Nota:**  
> Si se utiliza una base de datos externa a la red Docker, la variable `SA_PASSWORD` podría no ser necesaria.

### Local (Development)

Durante el desarrollo local, las credenciales de la base de datos se deben modificar en dos lugares:
- **/Context/ConnectionStringProvider:**  
  Aquí se construye la cadena de conexión utilizada por la aplicación.
- **/Properties/launchSettings.json:**  
  Este archivo se utiliza para definir variables de entorno cuando se ejecuta la aplicación en Visual Studio.

> **Nota:**  
> Actualmente esta configuración se encuentra duplicada; se podría mejorar para centralizar la configuración.

### Variables en Tiempo de Ejecución

- **En Desarrollo:**  
  Las variables se configuran en `Properties/launchSettings.json`.  
  **Ejemplo de configuración en `launchSettings.json`:**

  ```json
  {
    "profiles": {
      "https": {
        "commandName": "Project",
        "launchBrowser": true,
        "launchUrl": "swagger",
        "environmentVariables": {
          "ASPNETCORE_ENVIRONMENT": "Development",
          "DB_SERVER": "LOCALHOST,1533",
          "DB_DATABASE_NAME": "URLSHORT",
          "DB_USER_ID": "sa",
          "DB_PASSWORD": "sa.Bd.123"
        },
        "dotnetRunMessages": true,
        "applicationUrl": "https://localhost:7134;http://localhost:5201"
      }
    }
  }
    ```
- **En Producción**
Las variables se definen en el archivo `.env` en la raíz del proyecto.

### Variables en tiempo de diseño

Al ejecutar comandos como `update-database` de Entity Framework, se requiere disponer de una cadena de conexión completa.  
Si la cadena de conexión en los archivos de configuración (por ejemplo, `appsettings.json`) está incompleta, se implementa la interfaz `IDesignTimeDbContextFactory<TContext>` para construirla correctamente.  
Por ejemplo, se puede definir la clase `ApplicationContextFactory.cs` para obtener la cadena de conexión de los archivos de configuración y/o variables de entorno.

## Docker

Si se ejecuta el proyecto con Docker en local, se debe crear un archivo `.env` en la raíz del proyecto (siguiendo el ejemplo de producción mencionado anteriormente) y luego ejecutar el siguiente comando en la raíz del proyecto:

```cmd
docker compose up --build -d
```
En el archivo `docker-compose.yml`, se definen los siguientes servicios:

- **sqlserver:**  
  Servicio para SQL Server que utiliza la imagen oficial, mapea el puerto 1433 (en el contenedor) al 1533 del host y configura las variables de entorno necesarias.
- **backend:**  
  Servicio para la aplicación .NET. Depende de sqlserver y utiliza las variables de entorno para la conexión a la base de datos.
- **frontend:**  
  Servicio para la aplicación Angular.  


**Ejemplo de docker-compose.yml**

```yml


services:
  sqlserver:
    build:
      context: ./URL-Shortening-Service.BD
      dockerfile: Dockerfile
    environment:
      - SA_PASSWORD=${SA_PASSWORD}
    ports:
      - "1533:1433"
  
  migrations:
    depends_on:
      - sqlserver
    build:
      context: ./URL-Shortening-Service
      dockerfile: Dockerfile.migrations
    environment:
      - DB_USER_ID=${DB_USER_ID}
      - DB_PASSWORD=${DB_PASSWORD}
      - DB_SERVER=${DB_SERVER}
    
    entrypoint: ["dotnet", "ef", "database", "update", "--project", ".", "--startup-project", "."]

  backend:
    depends_on:
      - sqlserver
      - migrations
    image: url-shortening-service:latest
    build:
      context: ./URL-Shortening-Service
      dockerfile: Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - DB_USER_ID=${DB_USER_ID}
      - DB_PASSWORD=${DB_PASSWORD}
      - DB_SERVER=${DB_SERVER}
    ports:
      - "5000:8080"

  frontend:
    image: url-shortening-service-front:latest
    build:
      context: ./URL-Shortening-Service.Front
      dockerfile: Dockerfile
    ports:
      - "4200:80"
  
 
```

En este ejemplo, se utiliza un servicio adicional `migrations` para ejecutar las migraciones (usando un Dockerfile específico, `Dockerfile.migrations`), y luego el servicio backend se inicia. Esto permite mantener separada la lógica de migración de la ejecución de la aplicación.

