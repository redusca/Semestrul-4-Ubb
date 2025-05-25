<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$result = mysqli_query( $con," SELECT sosiri FROM trenuri WHERE plecari = '".$_GET["plecare"]."'");

while( $row = mysqli_fetch_array($result) ){
    echo "<option value=" . $row[0] . ">" . $row[0] . "</option>";
}
mysqli_close($con);
?>