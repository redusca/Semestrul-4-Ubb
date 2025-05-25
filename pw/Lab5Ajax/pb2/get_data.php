<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$page = isset($_GET["page"]) ? $_GET["page"] : 1;
$pagesize = 3;
$startindex = ($page -1) * $pagesize;

$result = mysqli_query($con, "SELECT nume,prenume,telefon,email FROM date LIMIT $startindex, $pagesize");

$data = array();
while($row = mysqli_fetch_assoc($result)) {
    $data[] = $row;
}

$count_result = mysqli_query($con, "SELECT COUNT(*) as total FROM date");
$count_row = mysqli_fetch_assoc($count_result);
$total_count = $count_row['total'];

echo json_encode(array(
    "data" => $data,
    "total" => $total_count
));

mysqli_close($con);
?>