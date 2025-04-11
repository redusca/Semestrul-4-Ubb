
function sortTable(rowIndex, tableId) {
    let table, sorting; 
    switchCount = 0;
    table = document.getElementById(tableId)
    sorting = true;
    let order = "asc";

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
                sorting = true;
                break;
            }
        }
    }

}

document.querySelectorAll("#animalTabel th").forEach((header, index) => {
    console.log("1");
    header.addEventListener("click", () => {console.log("2");
        sortTable(index, 'animalTabel');
    });
});

let orderV = "asc";
function sortTableV(cellIndex, tableId) {
    let table, sorting; 
    switchCount = 0;
    table = document.getElementById(tableId)
    sorting = true;

    while(sorting){
        for(let i = 1 ; i < (table.rows.length-1 ) ; i++){
            swaped = false
            let x = table.rows[i].cells[cellIndex];
            let y = table.rows[i+1].cells[cellIndex]
            let xVal = isNaN(parseFloat(x.innerHTML)) ? x.innerHTML.toLowerCase() : parseFloat(x.innerHTML);
            let yval = isNaN(parseFloat(y.innerHTML)) ? y.innerHTML.toLowerCase() : parseFloat(y.innerHTML);
            console.log("i=",i,"xval=",xVal,"yval=",yval);
            if(orderV == "asc" && xVal > yval || orderV == "desc" && xVal < yval){
                console.log("i+1",i+1,table.rows[i + 1]);
                console.log("i",i,table.rows[i])
                table.insertBefore(table.rows[i + 1], table.rows[i]);
                sorting = true;
                break;
            }
        }

    }
}

let col = 0;
document.querySelectorAll("#animalTabelH th").forEach((header) => {
    console.log("1");
    header.addEventListener("click", () => {console.log("2");
        sortTableV(col++, 'animalTabelH');
    });
});