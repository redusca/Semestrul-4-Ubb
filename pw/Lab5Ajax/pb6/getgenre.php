<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$result = mysqli_query($con, "SELECT DISTINCT genre FROM video_games order by genre ASC");

$data = array();
while($row = mysqli_fetch_assoc($result)) {
    $data[] = $row['genre'];
}

echo json_encode($data);

mysqli_close($con);
?>