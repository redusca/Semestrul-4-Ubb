<?php

session_start();

if (!isset($_SESSION["adminId"])) {
    header("Location: login.php");
    exit;
}

$conn = new mysqli('localhost','root','','phpDB');
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$result = $conn->query("SELECT id, author, content FROM comms WHERE approved = 0");
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
        <link rel="stylesheet" href="style.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Comms Check</title>
</head>

<body>
    <h1>Admin - Comms Check</h1>
    <a href="index.php">Pagina articolului</a>
    <h3>Comentariile neaprobate:</h3>
    <?php if ($result->num_rows > 0): ?>
        <ul>
            <?php while ($row = $result->fetch_assoc()): ?>
                <li>
                    <strong><?= htmlspecialchars($row["author"]) ?>:</strong>
                    <p><?= htmlspecialchars($row["content"]) ?></p>
                    <a href="approveComm.php?commentId=<?php echo $row["id"] ?>&csrfToken=<?= $_SESSION["csrfToken"] ?>">Aprobă</a>
                    <a href="denyComm.php?commentId=<?php echo $row["id"] ?>&csrfToken=<?= $_SESSION["csrfToken"] ?>">Respinge</a>
                </li>
            <?php endwhile; ?>
        </ul>
    <?php else: ?>
                <div>
                    <h3> Toate comentariile sunt procesate!</h3>
                </div>
    <?php endif; ?>
</body>

</html>