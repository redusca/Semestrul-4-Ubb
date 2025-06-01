<?php

session_start();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $username = $_POST["username"];
    $password = $_POST["password"];

    $connection = new mysqli('localhost','root','','phpDB');

    if ($connection->connect_error) {
        die("Connection failed: " . $connection->connect_error);
    }

    $statement = $connection->prepare("SELECT id, password FROM profesori WHERE user = ?");
    $statement->bind_param("s", $username);
    $statement->execute();

    $result = $statement->get_result();

    if ($result->num_rows === 1) {
        $prof = $result->fetch_assoc();
        if (password_verify($password, $prof["password"])) {
            $_SESSION["profesor_id"] = $prof["id"];
            header("Location: profesori.php");
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
    <title>Login Profesor</title>
</head>

<body>
    <h1>Login Profesor</h1>
    <form method="POST">
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" required>
        <label for="password">Parolă</label>
        <input type="password" id="password" name="password" required>
        <button type="submit">Login</button>
    </form>
    <?php if (isset($error)): ?>
        <p><?php echo htmlspecialchars($error) ?></>
    <?php endif; ?>
</body>

</html>