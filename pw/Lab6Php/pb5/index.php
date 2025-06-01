<?php

session_start();

if (isset($_SESSION["user"])) {
    $conn = new mysqli('localhost','root', '', 'phpDB');
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    $stmt = $conn->prepare("SELECT user FROM users WHERE user != ?");
    $stmt->bind_param("s", $_SESSION["user"]);
    $stmt->execute();

    $result = $stmt->get_result();
}
else {
    header("Location: login.php");
    exit;
}
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="style.css">
    <title> Main </title>
</head>

<body>
    <?php if (isset($_SESSION["user"])): ?>
        <h1>User: <?= htmlspecialchars($_SESSION["user"]) ?></h1>
        <a href="profil.php?user=<?= $_SESSION["user"] ?>">My Profile</a>
        <a href="feed.php">FEED</a>
        <a href="logout.php">Logout</a>
        <h3>All users:</h3>
        <ul>
            <?php while ($row = $result->fetch_assoc()): ?>
                <li>
                    <a href="profil.php?user=<?php echo $row["user"] ?>"><?= htmlspecialchars($row["user"]) ?></a>
                </li>
            <?php endwhile; ?>
        </ul>
    <?php else: ?>
        <h1>Poze</h1>
        <a href="login.php">Login</a>
    <?php endif; ?>
</body>

</html>