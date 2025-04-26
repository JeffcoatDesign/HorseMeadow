<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "We will now show the colors.". "<br>". "<br>";

$sql = "SELECT color, price FROM colors"; //select username and level from our USERS table

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "color: " . $row["color"]. " - price: " . $row["price"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>