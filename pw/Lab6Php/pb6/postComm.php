<?php

if (!isset($_POST["author"]) || !isset($_POST["content"])) {
    die("Autor si/sau continut nesetate!");
}

$author = $_POST["author"];
$content = $_POST["content"];

if (strlen($author) > 50 || strlen($content) > 9999) {
    die("Autor si/sau continut prea mari!");
}

$conn = new mysqli('localhost','root', '', 'phpDB');
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$stmt = $conn->prepare("INSERT INTO comms (author, content) VALUES (?, ?)");
$stmt->bind_param("ss", $author, $content);
$stmt->execute();

if (!$stmt->affected_rows) {
    http_response_code(500);
    exit;
}

header("Location: index.php");
exit();
?>