function sortTable(rowIndex, tableId) { 
    let table = $("#" + tableId);

    order = table.attr("order")
    if(order == undefined){
        table.attr("order", "asc");
        order = "asc";
    } else if(order == "asc"){
        table.attr("order", "desc");
        order = "desc";
    }
    else {
        table.attr("order", "asc");
        order = "asc";
    }

    let sorting = true;

    while(sorting){
        sorting = false;
        for(let i = 0 ; i < (table.find('tr').eq(rowIndex).find('td').length - 1) ; i++){
            let x = table.find("tr").eq(rowIndex).find("td").eq(i);
            let y = table.find("tr").eq(rowIndex).find("td").eq(i+1);
            let xVal = isNaN(parseFloat(x.text())) ? x.text().toLowerCase() : parseFloat(x.text());
            let yval = isNaN(parseFloat(y.text())) ? y.text().toLowerCase() : parseFloat(y.text());
            console.log("xVal: " + xVal + " yval: " + yval);
            if(order == "asc" && xVal > yval || order == "desc" && xVal < yval){
                console.log("Switching " + x.text() + " with " + y.text());
                table.find('tr').each(function() {
                    $(this).find('td').eq(i+1).insertBefore($(this).find('td').eq(i));
                });
                switchCount++;
                sorting = true;
                break;
            }
        }
    }
}

$("#animalTabel th").each(function(index) {
    console.log(index);
    $(this).click(function() {
        sortTable(index, 'animalTabel');
    });
});

function sortTableV(cellIndex, tableId) {
    let table = $("#" + tableId);

    let order = table.attr("order")
    if(order == undefined){
        table.attr("order", "asc");
        order = "asc";
    } else if(order == "asc"){
        table.attr("order", "desc");
        order = "desc";
    }
    else {
        table.attr("order", "asc");
        order = "asc";
    }

    $('tr',table).slice(1).sort(function(a, b) {
        let x = $('td',a).eq(cellIndex);
        let y = $('td',b).eq(cellIndex);

        let xVal = isNaN(parseFloat(x.text())) ? x.text().toLowerCase() : parseFloat(x.text());
        let yval = isNaN(parseFloat(y.text())) ? y.text().toLowerCase() : parseFloat(y.text());

        if(order == "asc"){
            return xVal > yval ? 1 : xVal < yval ? -1 : 0;
        }  
        else{
            return xVal < yval ? 1 : xVal > yval ? -1 : 0;
        }
    }).appendTo(table);
}

$("#animalTabelH th").each(function(index) {
    console.log(index);
    $(this).click(function() {
        sortTableV(index, 'animalTabelH');
    });
});