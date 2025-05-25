<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$data = json_decode(file_get_contents('php://input'), true);

$id = $data['id'];
$title = $data['title'];
$platform = $data['platform'];
$genre = $data['genre'];
$publisher = $data['publisher'];
$release_date = $data['release_date'];
$stock = $data['stock'];
$price = $data['price'];

$sql = "UPDATE video_games SET 
title = '$title', platform = '$platform', genre = '$genre', publisher = '$publisher', release_date = '$release_date', stock = $stock, price = $price WHERE id = $id";

$result = mysqli_query($con,$sql);

if($result) {
    echo json_encode(array("status" => "success", "message" => "Data updated successfully"));
} 

mysqli_close($con);

?>