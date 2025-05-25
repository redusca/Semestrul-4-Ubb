$(document).ready(function() {
    let $table = $('#gameBoard');
    let playerSymbol = null;
    let gameId = null;

    function creategame(){
        $.ajax({
            url: "creategame.php",
            type: "GET",
            dataType: "json",
            success: function(jsonResponse) {
                playerSymbol = jsonResponse["symbol"];
                gameId = jsonResponse["gameId"];
                if(playerSymbol == 'O') {
                    doMove(0);
                }
            }
        });
    }

    function doMove(move){
        $.ajax({
            url: "move.php",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify({
                gameId: gameId,
                move: move,
                symbol: playerSymbol
            }),
            dataType: "json",
            success: function(jsonResponse) {
                let $cell = $('#' + jsonResponse["cellId"]);
                $cell.html(jsonResponse["symbol"]);
                $cell.addClass('symbol-' + jsonResponse["symbol"]);
                
                if (jsonResponse["status"] == "gameover") {
                    alert("Game Over!\n " + jsonResponse["message"]);
                    $table.off('click');
                }
            }
        });
    }

    $table.on('click', function(event) {
        let $target = $(event.target);
        if ($target.html() === '') {
            let cellId = $target.attr('id');
            console.log("Cell clicked: " + cellId);
            $target.html(playerSymbol); 
            $target.addClass('symbol-' + playerSymbol);
            doMove(cellId);
        }
    });

    creategame();
});