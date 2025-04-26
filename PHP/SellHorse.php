<?php

require 'ConnectionSettings.php';

//User submitted variables
$ID = $_POST["ID"];
$horseID = $_POST["horseID"];
$userID = $_POST["userID"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sqlCheck = "SELECT * FROM userhorse WHERE ID = '" . $ID . "'";
$result = $conn->query($sqlCheck);

if($result->num_rows == 0){
    echo "hey hey";
    //$conn->close();
}

//First SQL
$sql = "SELECT price FROM horse WHERE ID = '" . $horseID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {

  // Store Item Price
  $horsePrice = $result->fetch_assoc()["price"];

  //Second Sql (delete item)
  $sql2 = "DELETE FROM userhorse WHERE ID = '" . $ID . "'";

  $result2 = $conn->query($sql2);
  if($result2)
  {
	  //If deleted successfully
	  $sql3 = "UPDATE `users` SET `coins` = coins + '" . $horsePrice . "' WHERE `id` = '" . $userID . "'";
	  $result3 = $conn->query($sql3);
	  if ($result3) {
		  echo "Success!";
	  }
  }
  else{
	  echo "error: could not delete item";
  }

} else {
  echo "0";
}
$conn->close();

?>