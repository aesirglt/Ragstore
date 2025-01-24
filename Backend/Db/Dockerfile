# Use a imagem oficial do SQL Server como base
FROM mcr.microsoft.com/mssql/server:latest

# Define a senha para o usuário sa como uma variável de ambiente
ENV SA_PASSWORD=Sup3rS3cr3t
ENV ACCEPT_EULA=Y

# Copiar arquivos de inicialização se necessário (opcional)
# COPY setup.sql /docker-entrypoint-initdb.d/

# Expor a porta padrão do SQL Server
EXPOSE 1433

# Comando de inicialização do SQL Server
CMD ["/opt/mssql/bin/sqlservr"]
