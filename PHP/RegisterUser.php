<?php
require 'ConnectionSettings.php';

//Variables submitted by username
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

if($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM users WHERE username = '" . $loginUser ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
	//Tell the user that that name is alreay taken
	echo "Username is already taken";

} else {
	echo "Creating user...";
	//insert the user and password into the database
	$sql2 = "INSERT INTO `users`(`username`, `password`, `level`, `coins`) VALUES ('". $loginUser ."','". $loginPass ."',1,0)";
	if ($conn->query($sql2) === TRUE) {
		echo "New record created successfully";
	} else {
		echo "Error: " . $sql2 . "<br>" . $conn->error;
	}
}

$conn->close();
?>