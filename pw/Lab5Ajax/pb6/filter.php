<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$publisher = $_GET['publisher'];
$platform = $_GET['platform'];
$genre = $_GET['genre'];
$stock = $_GET['stock'];
$price = $_GET['price'];

$sql = "Select * from video_games where 1";

if(!empty($publisher)) {
    $sql .= " and publisher = '$publisher'";
}

if(!empty($platform)) {
    $sql .= " and platform = '$platform'";
}

if(!empty($genre)) {
    $sql .= " and genre = '$genre'";
}


    if($stock == 0)
        $sql .= " and stock = 0";   
    else if($stock == 1)
        $sql .= " and stock > 0";


    if($price == 0)
        $sql .= " and price <= 10";
    else if($price == 1)
        $sql .= " and price >= 10 and price <= 20";
    else if($price == 2)
        $sql .= " and price >= 20 and price <= 40";
    else if($price == 3)
        $sql .= " and price >= 40";


$result = mysqli_query($con, $sql);

$data = array();
while($row = mysqli_fetch_assoc($result)) {
    $data[] = $row;
}

echo json_encode($data);

mysqli_close($con);
?>