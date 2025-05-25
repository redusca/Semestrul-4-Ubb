<?php
$con = mysqli_connect("localhost", "root", "", "ajaxDB");
if(!$con) {
    die("Connection failed: " . mysqli_connect_error());
}

$data = json_decode(file_get_contents('php://input'), true);

$gameId = $data['gameId'];
$symbol = $data['symbol'];
$cellId = $data['move'];

if($cellId != 0) {
    $sql = "UPDATE tictactoe SET _$cellId = '$symbol' WHERE id = $gameId";
    mysqli_query($con, $sql);

    if(checkWin($con, $gameId)) {
        $response = [
            'status' => 'gameover',
            'message' => "Player '$symbol' wins!",
            'cellId' => $cellId,
            'symbol' => $symbol
        ];
        
        echo json_encode($response);
        mysqli_close($con);

        exit;
    }
    if(checkDraw($con, $gameId)) {
        $response = [
            'status' => 'gameover',
            'message' => "It's a draw!",
            'cellId' => $cellId,
            'symbol' => $symbol
        ];
        
        echo json_encode($response);
        mysqli_close($con);

        exit;
    }
        sleep(1);
}
    $emptyCells = [];
    $sql = "SELECT * FROM tictactoe WHERE id = $gameId";
    $result = mysqli_query($con, $sql);
    $row = mysqli_fetch_assoc($result);
    $cells = [
        $row['_1'], $row['_2'], $row['_3'],
        $row['_4'], $row['_5'], $row['_6'],
        $row['_7'], $row['_8'], $row['_9']
    ];

    for( $i = 0; $i < count($cells); $i++) {
        if(empty($cells[$i])) {
            $emptyCells[] = $i + 1; 
        }
    }

    $computerMove = $emptyCells[array_rand($emptyCells)];
    $computerSymbol = $symbol == 'X' ? 'O' : 'X';

    $sql = "UPDATE tictactoe SET _$computerMove = '$computerSymbol' WHERE id = $gameId";
    mysqli_query($con, $sql);

    if(checkWin($con, $gameId)) {
        $response = [
            'status' => 'gameover',
            'message' => "Computer '$computerSymbol' wins!",
            'cellId' => $computerMove,
            'symbol' => $computerSymbol
        ];
        
        echo json_encode($response);
        mysqli_close($con);

        exit;
    }
    if(checkDraw($con, $gameId)) {
        $response = [
            'status' => 'gameover',
            'message' => "It's a draw!",
            'cellId' => $computerMove,
            'symbol' => $computerSymbol
        ];
        
        echo json_encode($response);
        mysqli_close($con);

        exit;
    }

    echo json_encode([
        'status' => 'ok',
        'cellId' => $computerMove,
        'symbol' => $computerSymbol
    ]);
    mysqli_close($con);
    exit;


function checkWin($con, $gameId) {
    $sql = "SELECT * FROM tictactoe WHERE id = $gameId";
    $result = mysqli_query($con, $sql);
    $row = mysqli_fetch_assoc($result);

    $cells = [
        $row['_1'], $row['_2'], $row['_3'],
        $row['_4'], $row['_5'], $row['_6'],
        $row['_7'], $row['_8'], $row['_9']
    ];

    // Check rows, columns, and diagonals
    for ($i = 0; $i < 3; $i++) {
        if ($cells[$i * 3] && $cells[$i * 3] == $cells[$i * 3 + 1] && $cells[$i * 3] == $cells[$i * 3 + 2]) {
            return true;
        }
        if ($cells[$i] && $cells[$i] == $cells[$i + 3] && $cells[$i] == $cells[$i + 6]) {
            return true;
        }
    }
    if ($cells[0] && $cells[0] == $cells[4] && $cells[0] == $cells[8]) {
        return true;
    }
    if ($cells[2] && $cells[2] == $cells[4] && $cells[2] == $cells[6]) {
        return true;
    }

    return false;
}

function checkDraw($con, $gameId) {
    $sql = "SELECT * FROM tictactoe WHERE id = $gameId";
    $result = mysqli_query($con, $sql);
    $row = mysqli_fetch_assoc($result);

    foreach ($row as $cell) {
        if (empty($cell)) {
            return false; // Found an empty cell, not a draw
        }
    }

    return true; // No empty cells, it's a draw
}

?>