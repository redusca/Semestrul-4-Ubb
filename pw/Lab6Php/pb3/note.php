<?php

session_start();

$sql = "SELECT s.nume AS nume_student, m.nume AS nume_materie, n.nota
        FROM note n
        JOIN student s on s.nr_matricol = n.id_student
        JOIN materii m on m.id = n.id_materie
        ORDER BY s.nume, m.nume";

$connection = new mysqli('localhost','root','','phpDB');
if ($connection->connect_error) {
    die("Connection failed: " . $connection->connect_error);
}

$result = $connection->query($sql);
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
    <title>Note </title>
    <link rel="stylesheet" href="style.css">
</head>

<body>
    <h1>Catalog</h1>
    <?php if (!isset($_SESSION['profesor_id'])): ?>
        <a href="login.php">Login Profesori</a>
    <?php else: ?>
        <a href="profesori.php">Pagina Profesori</a>
    <?php endif; ?>

    <table>
        <thead>
            <tr>
                <th>Student</th>
                <th>Materie</th>
                <th>Notă</th>
            </tr>
        </thead>
        <tbody>
            <?php while ($row = $result->fetch_assoc()): ?>
                <tr>
                    <td><?php echo htmlspecialchars($row["nume_student"]) ?></td>
                    <td><?php echo htmlspecialchars($row["nume_materie"]) ?></td>
                    <td><?php echo htmlspecialchars($row["nota"]) ?></td>
                </tr>
            <?php endwhile; ?>
        </tbody>
    </table>
</body>

</html>