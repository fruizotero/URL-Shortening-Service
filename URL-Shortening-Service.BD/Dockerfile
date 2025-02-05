# Usa la imagen oficial de SQL Server 2022
FROM mcr.microsoft.com/mssql/server:2022-latest

# Acepta el EULA y establece la contraseña del usuario 'sa'
ENV ACCEPT_EULA=Y
#ENV SA_PASSWORD=YourStrong!Passw0rd

# Exponer el puerto 1433 (puerto predeterminado de SQL Server)
EXPOSE 1433

# Comando para iniciar SQL Server
CMD ["/opt/mssql/bin/sqlservr"]
