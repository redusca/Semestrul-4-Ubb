

function sortTable(rowIndex, tableId) {
    table = document.getElementById(tableId)
    let order = table.getAttribute("order")

    if (order == undefined) {
        table.setAttribute("order", "asc");
        order = "asc";
    } else if (order == "asc") {
        table.setAttribute("order", "desc");
        order = "desc";
    }
    else {
        table.setAttribute("order", "asc");
        order = "asc";
    }
    let sorting = true;

    while (sorting) {
        sorting = false;

        for (let i = 1; i < (table.rows[rowIndex].cells.length - 1); i++) {
            let x = table.rows[rowIndex].cells[i];
            let y = table.rows[rowIndex].cells[i + 1]
            let xVal = isNaN(parseFloat(x.innerHTML)) ? x.innerHTML.toLowerCase() : parseFloat(x.innerHTML);
            let yval = isNaN(parseFloat(y.innerHTML)) ? y.innerHTML.toLowerCase() : parseFloat(y.innerHTML);
            if (order == "asc" && xVal > yval || order == "desc" && xVal < yval) {
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
    header.addEventListener("click", () => {
        sortTable(index, 'animalTabel');
    });
});

function sortTableV(cellIndex, tableId) {
    let table = document.getElementById(tableId)
    let rows = Array.from(table.tBodies[0].rows).slice(1);
    let order = table.getAttribute("order")
    if (order == undefined) {
        table.setAttribute("order", "asc");
        order = "asc";
    } else if (order == "asc") {
        table.setAttribute("order", "desc");
        order = "desc";
    }
    else {
        table.setAttribute("order", "asc");
        order = "asc";
    }

    rows.sort((a, b) => {
        const x = a.cells[cellIndex];
        const y = b.cells[cellIndex];

        let xVal = isNaN(parseFloat(x.innerHTML)) ? x.innerHTML.toLowerCase() : parseFloat(x.innerHTML);
        let yval = isNaN(parseFloat(y.innerHTML)) ? y.innerHTML.toLowerCase() : parseFloat(y.innerHTML);

        if (order === "asc") {
            return xVal > yval ? 1 : xVal < yval ? -1 : 0;
        }
        else {
            return xVal < yval ? 1 : xVal > yval ? -1 : 0;
        }
    });
    table.tBodies[0].append(...rows);
}

document.querySelectorAll("#animalTabelH th").forEach((header, index) => {
    console.log(index, header);
    header.addEventListener("click", () => {
        sortTableV(index, 'animalTabelH');
    });
});