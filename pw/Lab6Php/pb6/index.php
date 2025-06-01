<?php

session_start();

$conn = new mysqli('localhost', 'root', '', 'phpDB');
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$result = $conn->query("SELECT author, content FROM comms WHERE approved = 1");
?>

<!DOCTYPE html>
<html lang="ro">

<head>
    <meta charset="UTF-8">
        <link rel="stylesheet" href="style.css">
    <title>Game Article</title>
</head>

<body>
    <?php if (isset($_SESSION["adminId"])): ?>
        <a href="comms.php">Commentari</a>
        <a href="logout.php">Logout</a>
    <?php else: ?>
        <a href="login.php">Login</a>
    <?php endif; ?>
    <article>
        <h1>Elden Ring - Capodopera Dark Fantasy a celor de la FromSoftware</h1>

        <img src="https://media.npr.org/assets/img/2022/02/23/eldenring_21_4k-25120461292d0c3a0414.08944875_wide-e8f10694d264c26b3b42b65774ea218344b2286e.jpg?s=900&c=85&f=webp" alt="Elden Ring" />

        <p><strong>Elden Ring</strong> este un joc de acțiune RPG dezvoltat de FromSoftware și publicat de Bandai Namco. Lansat în februarie 2022, jocul a redefinit standardele genului printr-o lume deschisă masivă, o poveste misterioasă și o dificultate provocatoare.</p>
        
        <p>Cu un univers conceput în colaborare cu George R. R. Martin, creatorul <em>Game of Thrones</em>, Elden Ring te plasează în <strong>The Lands Between</strong>, un tărâm vast și periculos plin de creaturi fantastice, șefi nemiloși și secrete ascunse în fiecare colț.</p>
        
        <p>Sistemul de luptă este o evoluție naturală a stilului Souls, oferind mai multă libertate prin mecanici precum stealth, magie extinsă și posibilitatea de a explora lumea călare pe Torrente, calul spectral.</p>
        
        <p>Fiecare zonă este plină de povești nespuse, iar jucătorii sunt încurajați să le descopere în ritmul propriu. Jocul nu ține jucătorul de mână, dar tocmai această libertate este ceea ce îl face atât de captivant.</p>
        
        <p>Fie că ești veteran al jocurilor Souls sau ești la primul contact cu acest stil, <strong>Elden Ring</strong> îți promite o aventură pe care nu o vei uita prea curând.</p>
    
    </article>

    <form action="postComm.php" method="POST">
        <h4>Postează un comentariu:</h4>
        <label for="author">Nume</label>
        <input type="text" name="author" id="author" required>
        <br><br>
        <label for="content">Comentariu</label>
        <textarea name="content" id="content" required cols="50" rows="5" style="overflow: auto"></textarea>
        <br><br>
        <button type="submit">Postează</button>
    </form>

    <div>
        <h4>Comentarii:</h4>
        <ul>
            <?php while ($row = $result->fetch_assoc()): ?>
                <li>
                    <strong><?php echo htmlspecialchars($row["author"]) ?>:</strong>
                    <p><?php echo htmlspecialchars($row["content"]) ?></p>
                    <br><br>
                </li>
            <?php endwhile; ?>
        </ul>
    </div>
</body>

</html>