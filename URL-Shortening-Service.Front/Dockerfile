# Etapa de construcción: Usar una imagen LTS de Node.js
FROM node:20 as build-stage

# Establecer el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copiar los archivos de dependencias (package.json y package-lock.json)
COPY package*.json ./

# Instalar las dependencias del proyecto
RUN npm install

# Copiar el resto de los archivos del proyecto
COPY . .

# Construir la aplicación Angular en modo producción
RUN npm run build -- --configuration production

# Etapa de producción: Usar una imagen ligera de Nginx
FROM nginx:alpine

# Copiar los archivos construidos desde la etapa de construcción
COPY --from=build-stage /app/dist/url-shortening-service.front/browser /usr/share/nginx/html

# Copiar la configuración personalizada de Nginx (opcional)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# Exponer el puerto 80
EXPOSE 80

# Comando para iniciar Nginx
CMD ["nginx", "-g", "daemon off;"]
