<?php

require 'ConnectionSettings.php';

//User submitted variables
$price = $_POST["price"];
$horseID = $_POST["horseID"];
$userID = $_POST["userID"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//First SQL
$sql = "SELECT coins FROM users WHERE ID = '" . $userID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    while($row = $result->fetch_assoc()) 
    {
        if($row["coins"] >= $price)
        {
            $sql2 = "UPDATE `users` SET `coins` = coins - '" . $price . "' WHERE `id` = '" . $userID . "'";
            $result2 = $conn->query($sql2);
            if($result2) 
            {
                $sql3 = "Insert into `userhorse` (`userID`, `horseID`) VALUES ('" . $userID . "' , '" . $horseID . "')";
                $result3 = $conn->query($sql3);
                if($result3) 
                {
                    echo "Success!";
                } else 
                {
                    echo "Failured";
                }
            }
        }
    }
}
else {
  echo "No money!";
}
$conn->close();

?>