<?php

require 'ConnectionSettings.php';

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT coins FROM users WHERE username = '" . $loginUser . "'"; //select username and level from our USERS table

$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    while($row = $result->fetch_assoc()) {
    echo $row["coins"];
  }
}

else 
{
  echo "Coins not found!";
}
$conn->close();

?>