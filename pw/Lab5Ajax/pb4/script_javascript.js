window.onload = function() {
    let table = document.getElementById('gameBoard');
    let playerSymbol = null;
    let gameId = null;

    function creategame(){
        let xmlhttp = new XMLHttpRequest();
        let url = "creategame.php";
        xmlhttp.onreadystatechange = function() {
            if(xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                jsonResponse = JSON.parse(xmlhttp.responseText);
                playerSymbol = jsonResponse["symbol"];
                gameId = jsonResponse["gameId"];
                if(playerSymbol == 'O') {
                    doMove(0);
                }
            }
        };
        xmlhttp.open("GET", url, true);
        xmlhttp.send();
    }

    function doMove(move){
        let xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
            if(xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                jsonResponse = JSON.parse(xmlhttp.responseText);
                let cell = document.getElementById(jsonResponse["cellId"]);
                cell.innerHTML = jsonResponse["symbol"];
                cell.classList.add('symbol-'+jsonResponse["symbol"]);
                
                if (jsonResponse["status"] == "gameover") {
                    alert("Game Over!\n " + jsonResponse["message"]);
                    table.removeEventListener('click', arguments.callee);
                }
            }
        };

        xmlhttp.open("PUT", "move.php", true);
        xmlhttp.setRequestHeader("Content-Type", "application/json");

        xmlhttp.send(
            JSON.stringify({
                gameId: gameId,
                move: move,
                symbol: playerSymbol
            })
        );
    }

    table.addEventListener('click', function(event) {
        let target = event.target;
        if (target.innerHTML === '') {
            let cellId = target.id;
            console.log("Cell clicked: " + cellId);
            target.innerHTML = playerSymbol; 
            target.classList.add('symbol-'+playerSymbol);
            doMove(cellId);
        }
    });

    creategame();

}

