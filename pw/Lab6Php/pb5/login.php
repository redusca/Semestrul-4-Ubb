<?php

session_start();

if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $username = $_POST["username"];
    $password = $_POST["password"];

    $connection = new mysqli('localhost','root', '', 'phpDB');
    if ($connection->connect_error) {
        die("Connection failed: " . $connection->connect_error);
    }

    $stmt = $connection->prepare("SELECT id, password FROM users WHERE user = ?");
    $stmt->bind_param("s", $username);
    $stmt->execute();

    $result = $stmt->get_result();

    if ($result->num_rows === 1) {
        $user = $result->fetch_assoc();
        if (password_verify($password, $user["password"])) {
            $_SESSION["userId"] = $user["id"];
            $_SESSION["user"] = $username;
            $_SESSION["csrfToken"] = md5(uniqid(mt_rand(), true));
            header("Location: feed.php");
            exit();
        } else {
            $error = "Parolă și/sau utilizator incorect!";
        }
    } else {
        $error = "Parolă și/sau utilizator incorect!";
    }
}
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="style.css">
    <title> Login </title>
</head>

<body>
    <h1>Login</h1>
    <form method="POST">
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" required>
        <label for="password">Parolă</label>
        <input type="password" id="password" name="password" required>
        <button type="submit">Login</button>
    </form>
    <?php if (isset($error)): ?>
        <p><?php echo htmlspecialchars($error) ?></p>
    <?php endif; ?>
</body>

</html>