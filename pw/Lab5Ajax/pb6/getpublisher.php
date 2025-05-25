<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$result = mysqli_query($con, "SELECT DISTINCT publisher FROM video_games order by publisher ASC");

$data = array();
while($row = mysqli_fetch_assoc($result)) {
    $data[] = $row['publisher'];
}

echo json_encode($data);

mysqli_close($con);
?>