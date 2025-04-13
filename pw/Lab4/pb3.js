let gameBoard = [];
let dim = 0;
let selectedCells = [];
let type = 1;
let foundPairs = 0;
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

$("input[type=button]").click(function () {
    type = $('#tip').find(":selected").val();
    let size = $('#gameSize').find(":selected");
    foundPairs = 0;
    selectedCells = [];

    switch (size.val()) {
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
    $('#gameBoard').empty();
    gameBoard = gameBoard.flat().sort(() => Math.random() - 0.5);
    if (type == "2") {
        for (let i = 0; i < gameBoard.length; i++) {
            gameBoard[i] = ID[gameBoard[i]];
        }
    }

    for (let i = 0; i < dim; i++) {
        let row = $('<tr></tr>');
        for (let j = 0; j < dim; j++) {
            let cell = $('<td></td>');
            cell.text('');
            cell.addClass("cell");
            cell.on('click', handleClick(i, j));
            row.append(cell);
        }
        $('#gameBoard').append(row);
    }
});

function handleClick(i, j) {
    return function () {
        var cell = $(this);
        if (cell.text() != "" || cell.html() != "" || selectedCells.length >= 2) {
            return;
        }
        cell.find("img").remove();
        cell.attr("name", gameBoard[i * dim + j]);
        if (type == "2")
            cell.append($("<img>", { src: gameBoard[i * dim + j] }));
        else
            cell.text(gameBoard[i * dim + j]);

        selectedCells.push(cell);

        if (selectedCells.length === 2) {
            $(".cell").off("click");
            if (selectedCells[0].attr("name") === selectedCells[1].attr("name")) {
                foundPairs++;
                selectedCells = [];

                if (foundPairs == (dim * dim) / 2) {
                    setTimeout(() => {
                        alert("You win!");
                    }, 1000);
                }

                $(".cell:empty").on("click", function () {
                    let index = $('.cell').index(this);
                    let i = Math.floor(index / dim);
                    let j = index % dim;
                    handleClick(i, j).call(this);
                });
            } else {
                setTimeout(function () {
                    if (type == "2") {
                        selectedCells[0].find("img").remove();
                        selectedCells[1].find("img").remove();
                    }
                    else {
                        selectedCells[0].text("")
                        selectedCells[1].text("")
                    }
                    selectedCells[0].removeAttr("name");
                    selectedCells[1].removeAttr("name");
                    selectedCells = [];
                    $(".cell:empty").on("click", function () {
                        let index = $('.cell').index(this);
                        let i = Math.floor(index / dim);
                        let j = index % dim;
                        handleClick(i, j).call(this);
                    });
                }, 500);
            }
        }
    };
}

