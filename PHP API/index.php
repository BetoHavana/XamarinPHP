<?php 
	/*include_once("db.php");
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
	//exit():  means end server connection (don't execute the rest)*/

	include 'db.php';
	
	$pdo = new Conexion();
	
	//Listar registros y consultar registro
	if($_SERVER['REQUEST_METHOD'] == 'GET'){
		if(isset($_GET["placa"]) && !empty($_GET["placa"]) || 
			isset($_GET["modelo"]) && !empty($_GET["modelo"]))
		{
			$sql = $pdo->prepare("SELECT id,placa,modelo FROM autos WHERE placa=:placa AND modelo=:modelo");
			$sql->bindValue(':placa', $_GET["placa"]);
			$sql->bindValue(':modelo',$_GET["modelo"]);
			$sql->execute();
			$sql->setFetchMode(PDO::FETCH_ASSOC);
			header("HTTP/1.1 200 hay datos");
			echo json_encode($sql->fetchAll());
			exit;	
			} else {
			$sql = $pdo->prepare("SELECT * FROM autos");
			$sql->execute();
			$sql->setFetchMode(PDO::FETCH_ASSOC);
			header("HTTP/1.1 200 hay datos");
			echo json_encode($sql->fetchAll());
			exit;		
		}
	}
	
	//Insertar registro
	if($_SERVER['REQUEST_METHOD'] == 'POST')
	{
		$jsonPost=file_get_contents("php://input");
		$valuesPost= json_decode($jsonPost, true);
		if (isset($valuesPost["placa"]) && isset($valuesPost["modelo"])) {
			$sql = "INSERT INTO autos (placa,modelo) VALUES(:placa,:modelo)";
			$stmt  = $pdo->prepare($sql);
			$stmt ->bindValue(':placa', $valuesPost["placa"]);
			$stmt ->bindValue(':modelo', $valuesPost["modelo"]);
			$stmt ->execute();
			$idPost = $pdo->lastInsertId(); 
			$sql = $pdo->prepare("SELECT id,placa,modelo FROM autos WHERE id=$idPost");
			$sql->execute();
			$sql->setFetchMode(PDO::FETCH_ASSOC);
			header("HTTP/1.1 200 okay");
			echo json_encode($sql->fetchAll());
			exit;
		}else{
			echo "";
		}
		
	}
	
	//Actualizar registro
	if($_SERVER['REQUEST_METHOD'] == 'PUT')
	{		
		$jsonPost=file_get_contents("php://input");
		$valuesPut= json_decode($jsonPost, true);
		if (isset($valuesPut["id"])) {
			$sql = "UPDATE autos SET placa=:placa, modelo=:modelo WHERE id=:id";
			$sql = $pdo->prepare($sql);
			$sql->bindValue(':placa', $valuesPut['placa']);
			$sql->bindValue(':modelo', $valuesPut['modelo']);
			$sql->bindValue(':id', $valuesPut['id']);
			$sql->execute();
			$sql = $pdo->prepare("SELECT id,placa,modelo FROM autos WHERE id=:id");
			$sql->bindValue(':id', $valuesPut['id']);
			$sql->execute();
			$sql->setFetchMode(PDO::FETCH_ASSOC);
			header("HTTP/1.1 200 okay");
			echo json_encode($sql->fetchAll());
			exit;
		}else{
			echo "";
		}
	}
	
	//Eliminar registro
	if($_SERVER['REQUEST_METHOD'] == 'DELETE')
	{
		$sql = "DELETE FROM autos WHERE id=:id";
		$sql = $pdo->prepare($sql);
		$sql->bindValue(':id', $_GET['id']);
		$sql->execute();
		header("HTTP/1.1 200 Ok");
		$listEmpleados = array();
		$listEmpleados[0]["placa"] = "empty";
		$listEmpleados[0]["modelo"] = "empty";
		echo json_encode($listEmpleados);
		exit;
	}
	
	//Si no corresponde a ninguna opción anterior
	header("HTTP/1.1 400 Bad Request");
?>