<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully, now we will show the users.". "<br>". "<br>";

$sql = "SELECT username, coins FROM users"; //select username and level from our USERS table

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "username: " . $row["username"]. " - coins: " . $row["coins"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>