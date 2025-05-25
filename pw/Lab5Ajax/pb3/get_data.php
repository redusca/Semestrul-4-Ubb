<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$result = mysqli_query($con,"SELECT * FROM video_games");

$data = array();
while($row = mysqli_fetch_array($result)) {
    $data[] = $row;
}

echo json_encode($data);

mysqli_close($con);

?>