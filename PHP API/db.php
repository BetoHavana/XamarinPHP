<?php   
    /*$host   = "mysql:host=localhost;dbname=unity";
	$user   = "root";
	$pass   = "";
	$option = array(PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8");
	try{
		$con = new PDO($host,$user, $pass, $option);
		$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}catch(PDOException $e){ echo $e.getMessage(); }*/
	class Conexion extends PDO
	{
		private $hostBd = 'localhost';
		private $nombreBd = 'unity';
		private $usuarioBd = 'root';
		private $passwordBd = '';
		
		public function __construct()
		{
			try{
				parent::__construct('mysql:host=' . $this->hostBd . ';dbname=' . $this->nombreBd . ';charset=utf8', $this->usuarioBd, $this->passwordBd, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
				
				} catch(PDOException $e){
				echo 'Error: ' . $e->getMessage();
				exit;
			}
		}
	}

?>