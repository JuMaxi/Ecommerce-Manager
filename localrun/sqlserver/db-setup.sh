#!/bin/bash
echo "Waiting for MS SQL SERVER..."
sleep 15

while ! /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $SA_PASSWORD -Q "SELECT GETDATE()"; do
    sleep 1
done

echo "SQL Server started"

echo "Creating $DB_NAME database..."
/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $SA_PASSWORD -Q "CREATE DATABASE $DB_NAME"

sleep 5 # Wait for the database to be ready

echo "Running scripts..."

for script in $(ls /docker-entrypoint-initdb.d/*.sql); do
  echo "executing $script on $DB_NAME"
  /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -d $DB_NAME -U sa -P $SA_PASSWORD -i $script
done

echo "All scripts updated."