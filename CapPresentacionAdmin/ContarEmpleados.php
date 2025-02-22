<?php
// Datos de conexión
$serverName = '172.22.192.1';
$database = 'comedor';
$username = 'sa';
$password = 'Uriel2004.!23';

// Conectar a SQL Server usando ODBC
$connectionInfo = array("Database"=>$database, "UID"=>$username, "PWD"=>$password);
$conn = sqlsrv_connect($serverName, $connectionInfo);

// Verificar conexión
if ($conn === false) {
    die(print_r(sqlsrv_errors(), true));
}

// Contar el total de empleados
$sql = "SELECT COUNT(*) AS totalEmpleados FROM empleados";
$stmt = sqlsrv_query($conn, $sql);

if ($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}

$row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
$totalEmpleados = $row['totalEmpleados'];

// Imprimir el total de empleados
echo $totalEmpleados;

// Cerrar la conexión
sqlsrv_close($conn);
?>
