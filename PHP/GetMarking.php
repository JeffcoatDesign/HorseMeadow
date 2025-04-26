<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "We will now show the markings.". "<br>". "<br>";

$sql = "SELECT marking, price FROM markings"; //select username and level from our USERS table

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "marking: " . $row["marking"]. " - price: " . $row["price"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>