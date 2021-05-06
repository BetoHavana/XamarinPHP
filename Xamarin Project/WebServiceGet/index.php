<?php

if (isset($_GET["IdEmpresa"])) {

    $IdEmpresa=$_GET["IdEmpresa"];

    $NombreEmpresa=$IdEmpresa==1?"HOCOM": "SAHANA";
    
    $listEmpleados=array();
    for ($i=0; $i < 5; $i++) { 
        $listEmpleados[$i]["IdEmpleado"]=$i;
        $listEmpleados[$i]["EmpNombre"]="Empleado ".$i." de la empresa ".$NombreEmpresa;
        $listEmpleados[$i]["EmpApellido"]="Apellido de empleado ".$i;
    }

    echo json_encode($listEmpleados);

}else{
    echo "";
}


?>