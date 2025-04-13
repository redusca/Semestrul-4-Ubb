

function sortTable(rowIndex, tableId) {
    let table, sorting; 
    switchCount = 0;
    table = document.getElementById(tableId)
    let order = "asc";
    sorting = true;

    while(sorting){
        sorting = false;

        for(let i = 1 ; i < (table.rows[rowIndex].cells.length - 1) ; i++){
            let x = table.rows[rowIndex].cells[i];
            let y = table.rows[rowIndex].cells[i+1]
            let xVal = isNaN(parseFloat(x.innerHTML)) ? x.innerHTML.toLowerCase() : parseFloat(x.innerHTML);
            let yval = isNaN(parseFloat(y.innerHTML)) ? y.innerHTML.toLowerCase() : parseFloat(y.innerHTML);
            if(order == "asc" && xVal > yval || order == "desc" && xVal < yval){
                for (let j = 0; j < table.rows.length; j++) {
                    table.rows[j].insertBefore(table.rows[j].cells[i + 1], table.rows[j].cells[i]);
                }
                switchCount++;
                sorting = true;
                break;
            }
        }

        if(sorting == false && switchCount == 0){
            order = "desc";
            sorting = true;
        }
    }

}

document.querySelectorAll("#animalTabel th").forEach((header, index) => {
    header.addEventListener("click", () => {
        sortTable(index, 'animalTabel');
    });
});

function sortTableV(cellIndex, tableId) {
    let table = document.getElementById(tableId)
    let rows = Array.from(table.tBodies[0].rows).slice(1); 
    let copyRows = [...rows];

    rows.sort((a, b) => {
        const valoareA = a.cells[cellIndex].innerText;
        const valoareB = b.cells[cellIndex].innerText;

        const isNumber = !isNaN(parseFloat(valoareA)) && !isNaN(parseFloat(valoareB));
        if(isNumber) {
            return parseFloat(valoareA) - parseFloat(valoareB);
        }
        else {
            return valoareA.localeCompare(valoareB);
        }
    });

    let isSame = rows.every((row, index) => row === copyRows[index]);
    if(isSame) {
        rows.reverse();
    }

    table.tBodies[0].append(...rows);
}

document.querySelectorAll("#animalTabelH th").forEach((header,index) => {
    console.log(index,header);
    header.addEventListener("click", () => {
        sortTableV(index, 'animalTabelH');
    });
});