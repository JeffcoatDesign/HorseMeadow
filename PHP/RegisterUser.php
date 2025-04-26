<?php

require 'ConnectionSettings.php';

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM users WHERE username = '" . $loginUser . "'"; //select username and level from our USERS table

$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    //Tell user that that name is already taken
    echo "Username is already taken.";

}

else 
{
  echo "Create user...";

  //Insert the user and password into database
  $sql2 = "INSERT INTO users (username, password, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "',250)";

  if ($conn->query($sql2) === TRUE) 
  {
    echo "New record created successfully";
  }
  else {
    echo "Error: " . $sql2 . "<br>" . $conn->error;
  }
}
$conn->close();

?>