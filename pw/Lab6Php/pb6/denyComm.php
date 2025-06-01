<?php

session_start();

if (!isset($_SESSION["adminId"])) {
    header("Location: login.php");
    exit;
}

if (!isset($_GET["commentId"])) {
    die("CommentId nespecificat!");
}

if (!isset($_GET["csrfToken"]) || $_GET["csrfToken"] != $_SESSION["csrfToken"]) {
    http_response_code(403);
    exit;
}

$conn = new mysqli('localhost', 'root', '', 'phpDB');
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$stmt = $conn->prepare("Delete FROM comms WHERE id = ?");
$stmt->bind_param("i", $_GET["commentId"]);
$stmt->execute();

if (!$stmt->affected_rows) {
    http_response_code(500);
    exit;
}

header("Location: comms.php");
exit();
?>