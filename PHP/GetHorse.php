<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
$horseID = $_POST["horseID"];

$sql = "SELECT colorID, markingID, price FROM horse WHERE id = '" . $horseID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
  //after the whole array is created
  echo json_encode($rows);
} else {
  echo "0";
}
$conn->close();

?>