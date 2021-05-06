-- Table structure for table `users`
DROP TABLE IF EXISTS users;
CREATE TABLE users (
  Id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  username varchar(255) NOT NULL,
  password varchar(255) NOT NULL
);

--For secure passwords use sha1
-- Dumping data for table `users`
/*INSERT INTO users (username, password) VALUES
( 'johndoe', sha1('johndoe')),
( 'johnsmith', sha1('johnsmith') ),
( 'admin ', sha1('test'));*/

INSERT INTO users (username, password) VALUES
( 'johndoe', 'johndoe'),
( 'johnsmith', 'johnsmith'),
( 'admin ', 'test');

DROP TABLE IF EXISTS autos;
CREATE TABLE autos (
  Id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  placa varchar(255) NOT NULL,
  modelo varchar(255) NOT NULL
);

INSERT INTO autos (placa, modelo) VALUES
( '2KUM3T', 'VW VENTO'),
( 'QWE4FV', 'NISSAN SENTRA'),
( 'OLKIP4 ', 'TESLA');