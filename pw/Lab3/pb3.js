let gameBoard = [];
let dim = 0;
let selectedCells = [];
let foundPairs = 0;
let type = 1;
let ID = {
    1: "images/1.png",
    2: "images/2.png",
    3: "images/3.png",
    4: "images/4.png",
    5: "images/5.png",
    6: "images/6.png",
    7: "images/7.png",
    8: "images/8.png",
    9: "images/9.png",
    10: "images/10.png",
    11: "images/11.png",
    12: "images/12.png",
    13: "images/13.png",
    14: "images/14.png",
    15: "images/15.png",
    16: "images/16.png",
    17: "images/17.png",
    18: "images/18.png",
}

function createBoardGame(){
    type = document.getElementById("tip").value;
    let size = document.getElementById("gameSize").value;
    foundPairs = 0;
    selectedCells = [];

    switch(size){
        case "1":
            dim = 2;
            gameBoard = [
                [1, 2],
                [2, 1]
            ]
            break;
        case "2":
            dim = 4;
            gameBoard = [
                [1, 2, 3, 4],
                [2, 1, 4, 3],
                [5, 6, 7, 8],
                [6, 5, 8, 7]
            ]
            break;
        case "3":
            dim = 6;
            gameBoard = [
                [1, 2, 3, 4, 5, 6],
                [2, 1, 4, 3, 6, 5],
                [7, 8, 9, 10, 11, 12],
                [8, 7, 10, 9, 12, 11],
                [13, 14, 15, 16, 17, 18],
                [14, 13, 16, 15, 18, 17]
            ]
            break;
        default:
            alert("Invalid size selected. Please select a valid size." + size);
            return;
    }

    gameBoard = gameBoard.flat().sort(() => Math.random() - 0.5);
    if(type == "2"){
        for(let i = 0; i < gameBoard.length; i++){
            gameBoard[i] = ID[gameBoard[i]];
        }
    }

    let table = document.getElementById("gameBoard");
    table.innerHTML = "";
    for ( let i = 0; i < dim; i++){
        let row = document.createElement("tr");
        for (let j = 0; j < dim; j++){
            let cell = document.createElement("td");
            cell.textContent = "";
            cell.classList.add("cell");
            cell.addEventListener("click", handleClick(i, j)); 
            row.appendChild(cell);
        }
        table.appendChild(row);
    }
}

function handleClick(i, j) {
    return function() {
        var cell = this;
        if (cell.hasChildNodes() || cell.textContent || selectedCells.length >= 2) {
            return;
        }
        cell.innerHTML = ""
        cell.name = gameBoard[i * dim + j];
        if(type == "2")
            cell.innerHTML = '<img src="' + gameBoard[i * dim + j] + '" />';
        else
            cell.textContent = gameBoard[i * dim + j];

        selectedCells.push(cell);

        if (selectedCells.length === 2) {
            var cells = document.querySelectorAll(".cell");
            cells.forEach(function(cell) {
                cell.removeEventListener("click", handleClick(i, j));
            });
        
            if(selectedCells[0].name === selectedCells[1].name){
                foundPairs++;
                selectedCells = [];

                if(foundPairs == (dim * dim) / 2){
                    setTimeout(() => {
                        alert("You win!");
                    }, 1000);
                }
                cells.forEach(function(cell) {
                    cell.addEventListener("click", handleClick(i, j));
                });
            } else {
                setTimeout(function() {
                    if(type == "2"){
                        selectedCells[0].innerHTML = "";
                        selectedCells[1].innerHTML = "";
                    }
                    else{
                        selectedCells[0].textContent = "";
                        selectedCells[1].textContent = "";
                    }
                    selectedCells = [];
                    cells.forEach(function(cell) {
                        cell.addEventListener("click", handleClick(i, j));
                    });
                }, 500);
            }
        }
    };
}

