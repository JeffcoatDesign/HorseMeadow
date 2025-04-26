<?php

require 'ConnectionSettings.php';

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//$sql = "SELECT password, id FROM users WHERE username = '" . $loginUser . "'"; //select username and level from our USERS table

/* PREPARED STATEMENT*/
$sql = "SELECT password, id FROM users WHERE username = ?";
$statement = $conn->prepare($sql);
$statement->bind_param("s", $loginUser);

$statement->execute();
$result = $statement->get_result();

//$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass)
    {
        echo $row["id"];
        //get user's data here

        //get player info

        //get inventory

        //modifay player data

        //update inventory
    }
    else
    {
        echo "Wrong Credentials";
    }
  }
} else {
  echo "Username Does Not Exist";
}
$conn->close();

?>