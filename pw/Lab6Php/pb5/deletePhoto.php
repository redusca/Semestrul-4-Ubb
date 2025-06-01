<?php
session_start();

if (!isset($_GET["photoId"]) || !isset($_GET["csrfToken"])) {
    http_response_code(400);
    exit;
}

if (!isset($_SESSION["userId"])) {
    http_response_code(401);
    exit;
}

if ($_GET["csrfToken"] != $_SESSION["csrfToken"]) {
    http_response_code(403);
    exit;
}

$conn = new mysqli("localhost","root","","phpDB");
if ($conn->connect_error) {
    http_response_code(500);
    exit;
}

$stmt = $conn->prepare("SELECT userId, path FROM userImages WHERE id = ?");
$stmt->bind_param("i", $_GET["photoId"]);
$stmt->execute();

$result = $stmt->get_result();

if (!$result->num_rows) {
    http_response_code(404);
    exit;
}

$image = $result->fetch_assoc();

if ($image["userId"] != $_SESSION["userId"]) {
    http_response_code(403);
    exit;
}

$deleteStmt = $conn->prepare("DELETE FROM userImages WHERE id = ?");
$deleteStmt->bind_param("i", $_GET["photoId"]);
$deleteStmt->execute();

unlink($image["path"]);

$_SESSION["csrfToken"] = md5(uniqid(mt_rand(), true));

header("Location: profil.php?user=" . $_SESSION["user"]);
exit()
?>