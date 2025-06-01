<?php

session_start();

if (!isset($_SESSION["user"])) {
    header("Location: login.php");
    exit;
}

if (!isset($_GET["user"])) {
    die("Username nespecificat!");
}

$profileUser = $_GET["user"];

$conn = new mysqli('localhost','root', '', 'phpDB');
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$userStmt = $conn->prepare("SELECT id FROM users WHERE user = ?");
$userStmt->bind_param("s", $profileUser);
$userStmt->execute();

$userResult = $userStmt->get_result();
if ($userResult->num_rows === 0) {
    http_response_code(404);
    exit;
}

$userId = $userResult->fetch_assoc()["id"];

$imageStmt = $conn->prepare("SELECT id, path FROM userImages WHERE userId = ?");
$imageStmt->bind_param("i", $userId);
$imageStmt->execute();

$imageResult = $imageStmt->get_result();
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
    <title> Profile <?php echo htmlspecialchars($profileUser) ?></title>
    <link rel="stylesheet" href="style.css">
    <style>
        img {
            max-width: 400px;
            height: auto;
        }
    </style>
</head>

<body>
    <h1>Profile <?php echo htmlspecialchars($profileUser) ?></h1>

    <a href="feed.php">Back to Main</a>

    <?php if ($profileUser === $_SESSION["user"]): ?>
        <form action="addPhoto.php" method="POST" enctype="multipart/form-data">
            <h3>Upload Photography</h3>
            <input type="hidden" name="csrfToken" value="<?php echo $_SESSION["csrfToken"] ?>">
            <input type="hidden" name="userId" value="<?php echo $_SESSION["userId"] ?>">
            <label for="photo">Photo</label>
            <input type="file" id="photo" name="photo" accept=".png, .jpg, .jpeg">
            <br><br>
            <button type="submit">Post</button>
        </form>
    <?php endif ?>

    <h3>Images Posted:</h3>
    <ul>
        <?php while ($row = $imageResult->fetch_assoc()): ?>
            <li>
                <img src="<?php echo $row["path"] ?>" alt="<?= "id " . $row["id"] ?>">
                <?php if ($profileUser === $_SESSION["user"]): ?>
                    <a href="deletePhoto.php?photoId=<?php echo $row["id"] ?>&csrfToken=<?php echo $_SESSION["csrfToken"] ?>">Delete Photo</a>
                <?php endif; ?>
            </li>
        <?php endwhile; ?>
    </ul>
</body>

</html>