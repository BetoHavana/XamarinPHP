<?php 
	include_once("db.php");
	if ((isset($_GET["username"]) && !empty($_GET["username"]) && 
		isset($_GET["password"]) && !empty($_GET["password"]))){
		Login($_GET["username"], $_GET["password"]);
	}
	if (isset($_GET["placa"]) && !empty($_GET["placa"]) && 
		isset($_GET["modelo"]) && !empty($_GET["modelo"])){
			bringAutos($_GET["placa"], $_GET["modelo"]);
		
		}
	function Login($username, $password){
		GLOBAL $con;
		$sql = "SELECT id,username FROM users WHERE username=? AND password=?";
		$st=$con->prepare($sql);
		$st->execute(array($username, $password));//encrypt password
		$listEmpleados = array();
		$all=$st->fetchAll();
		if (count($all) > 0 ){
			for($i=0; $i<count($all) ; $i++){
				$listEmpleados[$i]["id"] = $all[$i]["id"];
				$listEmpleados[$i]["username"] = $all[$i]["username"];
			}
			echo json_encode($listEmpleados);
			exit();
		}
		//if username or password are empty strings
		echo "SERVER: error, invalid username or password";
		exit();
	}

	function bringAutos($placa, $modelo){
		GLOBAL $con;
		$sql = "SELECT id,placa FROM autos WHERE placa=? AND modelo=?";
		$st=$con->prepare($sql);
		$st->execute(array($placa, $modelo));//encrypt password
		$listEmpleados = array();
		$all=$st->fetchAll();
		if (count($all) > 0 ){
			for($i=0; $i<count($all) ; $i++){
				$listEmpleados[$i]["id"] = $all[$i]["id"];
				$listEmpleados[$i]["placa"] = $all[$i]["placa"];
			}
			echo json_encode($listEmpleados);
			exit();
		}
		//if username or password are empty strings
		echo "SERVER: error, invalid username or password";
		exit();
	}
	//if username or password is null (not set)
	echo "SERVER: error, enter a valid username & password";
	//exit():  means end server connection (don't execute the rest)
?>