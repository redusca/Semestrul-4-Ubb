<!DOCTYPE html>
<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="style.css">
    <title>Produse</title>
</head>
<body>
<div class="container">
    <h1>Produse</h1>
    <form method="GET" action="index.php">
        <label for="num_per_page">Număr produse pe pagină:</label>
        <select id="num_per_page" name="num_per_page" onchange="this.form.submit()">
            <option value="3" <?php echo (isset($_GET['num_per_page']) && $_GET['num_per_page'] == 3) ? 'selected' : ''; ?>>3</option>
            <option value="5" <?php echo (isset($_GET['num_per_page']) && $_GET['num_per_page'] == 5) ? 'selected' : ''; ?>>5</option>
            <option value="10" <?php echo (isset($_GET['num_per_page']) && $_GET['num_per_page'] == 10) ? 'selected' : ''; ?>>10</option>
        </select>
    </form>
    
    <?php

    session_start();

    function validate_input($data) {
        return htmlspecialchars(stripslashes(trim($data)));
    }

    $num_per_page = isset($_GET['num_per_page']) ? (int)validate_input($_GET['num_per_page']) : 10;
    $page = isset($_GET['page']) ? (int)validate_input($_GET['page']) : 1;
    $offset = ($page - 1) * $num_per_page;

    $conn = new mysqli("localhost", "root", "", "phpDB");

    if ($conn->connect_error) {
        die("Conexiune eșuată: " . $conn->connect_error);
    }

    $stmt = $conn->prepare("SELECT COUNT(*) AS total FROM Produse");
    $stmt->execute();
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();
    $total_products = $row['total'];
    $total_pages = ceil($total_products / $num_per_page);

    $stmt = $conn->prepare("SELECT * FROM produse LIMIT ?, ?");
    $stmt->bind_param("ii", $offset, $num_per_page);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        echo "<table><tr><th>ID</th><th>Denumire</th><th>Preț</th><th>Cantitate</th></tr>";
        while ($row = $result->fetch_assoc()) {
            echo "<tr><td>" . htmlspecialchars($row["id"]) . "</td><td>" . htmlspecialchars($row["denumire"]) . "</td><td>" . htmlspecialchars($row["pret"]) . "</td><td>" . htmlspecialchars($row["cantitate"]) . "</td></tr>";
        }
        echo "</table>";
    } else {
        echo "Nu există produse disponibile.";
    }

    $stmt->close();
    $conn->close();
    ?>

    <div class="pagination">
        <?php
        if ($page > 1) {
            echo '<a href="?num_per_page=' . $num_per_page . '&page=' . ($page - 1) . '"> Previous</a>';
        }
        if ($page < $total_pages) {
            echo '<a href="?num_per_page=' . $num_per_page . '&page=' . ($page + 1) . '"> Next</a>';
        }
        ?>
    </div>
</div>
</body>
</html>