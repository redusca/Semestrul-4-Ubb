<?php

session_start();

if (!isset($_SESSION['profesor_id'])) {
    header('Location: login.php');
    exit();
}

$connection = new mysqli('localhost','root','','phpDB');

if ($connection->connect_error) {
    die("Connection failed: " . $connection->connect_error);
}


if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $id_student = $_POST["id_student"];
    $id_materie = $_POST["id_materie"];
    $nota = $_POST["nota"];

    $sql = "INSERT INTO note (id_student, id_materie, nota)
            VALUES (?, ?, ?)";

    $statement = $connection->prepare($sql);
    $statement->bind_param("sid", $id_student, $id_materie, $nota);
    $statement->execute();
}

$studenti = $connection->query("SELECT * FROM student");
$materii = $connection->query("SELECT * FROM materii");
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
    <title>Adaugat Note</title>
    <link rel="stylesheet" href="style.css">
</head>

<body>
    <h1>Profesori</h1>
    <a href="note.php">Vezi note</a>
    <a href="logout.php">Logout</a>

    <h2>Notare</h2>
    <form method="POST">
        <label for="id_student">Student:</label>
        <select id="id_student" name="id_student" required>
            <?php while ($student = $studenti->fetch_assoc()): ?>
                <option value="<?php echo $student["nr_matricol"] ?>">
                    <?php echo htmlspecialchars($student["nume"]) ?>
                </option>
            <?php endwhile; ?>
        </select>
        <label for="id_student">Student:</label>
        <select id="id_materie" name="id_materie" required>
            <?php while ($materie = $materii->fetch_assoc()): ?>
                <option value="<?= $materie["id"] ?>">
                    <?php echo htmlspecialchars($materie["nume"]) ?>
                </option>
            <?php endwhile; ?>
        </select>
        <label for="nota">Notă:</label>
        <input type="number" step="0.01" id="nota" name="nota" min="0" max="10" required>
        <button type="submit">Salvează</button>
    </form>
</body>

</html>