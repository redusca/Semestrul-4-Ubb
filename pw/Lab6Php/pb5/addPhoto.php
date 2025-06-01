<?php

session_start();

if (!isset($_POST['csrfToken']) || $_POST['csrfToken'] !== $_SESSION['csrfToken']) {
    die('Invalid CSRF token - possible security attack!');
}

if ($_POST['userId'] != $_SESSION['userId']) {
    die('User ID mismatch!');
}

if (!isset($_FILES["photo"]) || !isset($_POST["userId"])) {
    die("Photo missing or invalid request.");
}

if ($_FILES["photo"]["size"] > 10 * 1000000) {
    die("Photo too large. Maximum size is 10MB.");
}

$extension = strtolower(pathinfo($_FILES["photo"]["name"])["extension"]);

if ($extension !== "png" && $extension !== "jpg" && $extension !== "jpeg") {
    die("Invalid file type. Only PNG, JPG, and JPEG are allowed.");
}

$conn = new mysqli("localhost","root","","phpDB");

if ($conn->connect_error) {
    http_response_code(500);
    exit;
}

$saveStmt = $conn->prepare("INSERT INTO userImages (userId) VALUES (?)");
$saveStmt->bind_param("s", $_POST["userId"]);
$saveStmt->execute();

$photosDir = "photos/";

$lastid = $conn->query("Select LAST_INSERT_ID() as id");
$imageId = $lastid->fetch_assoc()["id"];

$savePath = $photosDir . $imageId . ".$extension";

if (!move_uploaded_file($_FILES["photo"]["tmp_name"], $savePath)) {
    $deleteStmt = $conn->prepare("DELETE FROM userImages WHERE id = ?");
    $deleteStmt->bind_param("i", $imageId);
    $deleteStmt->execute();

    http_response_code(500);
    exit;
}

$updateStmt = $conn->prepare("UPDATE userImages SET path = ? WHERE id = ?");
$updateStmt->bind_param("si", $savePath, $imageId);
$updateStmt->execute();

$_SESSION["csrfToken"] = md5(uniqid(mt_rand(), true));

header("Location: profil.php?user=" . $_SESSION["user"]);
exit();
?>