<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

mysqli_query($con, "INSERT INTO tictactoe() VALUES()");

// Get the last inserted id
$result = mysqli_query($con,"SELECT MAX(id) from tictactoe");

$gameId = mysqli_fetch_row($result);
$gameId = $gameId[0];

$rand = rand(0,1);
$symbol = ($rand == 0) ? "X" : "O";

echo json_encode(array(
    "gameId" => $gameId,
    "symbol" => $symbol
));

mysqli_close($con);
?>
