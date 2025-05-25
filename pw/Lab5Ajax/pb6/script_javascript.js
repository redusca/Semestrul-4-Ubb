window.onload  = function() {
    var proprieties = ['platform','publisher','genre'];

    proprieties.forEach(function(property){
        let xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                var items = JSON.parse(this.response);
                var select = document.getElementById(property);
                items.forEach(function(item) {
                    var option = document.createElement('option');
                    option.value = item;
                    option.textContent = item;
                    select.appendChild(option);
                });
            }
        };
        xmlhttp.open("GET", "get" + property + ".php", true);
        xmlhttp.send();
    });

    document.getElementById("filterButton").addEventListener("click", function() {
    var platform = document.getElementById("platform").value;
    var publisher = document.getElementById("publisher").value;
    var genre = document.getElementById("genre").value;
    var stock = document.getElementById("stock").value;
    var price = document.getElementById("price").value;

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            var data = JSON.parse(this.response);
            var results = document.getElementById("results");
            
            var table = document.createElement('table');
            var thead = document.createElement('thead');
            var tbody = document.createElement('tbody');

            var tr = document.createElement('tr');
            for (var key in data[0]) {
                var th = document.createElement('th');
                th.textContent = key;
                tr.appendChild(th);
            }
            thead.appendChild(tr);

            data.forEach(function(item) {
                var tr = document.createElement('tr');
                for (var key in item) {
                    var td = document.createElement('td');
                    td.textContent = item[key];
                    tr.appendChild(td);
                }
                tbody.appendChild(tr);
            });

            table.appendChild(thead);
            table.appendChild(tbody);

            results.innerHTML = '';
            results.appendChild(table);
        }
    };
    xmlhttp.open("GET", "filter.php?platform=" + platform + "&publisher=" + publisher + "&genre=" + genre + "&stock=" + stock + "&price=" + price, true);
    xmlhttp.send();
    });

    document.getElementById("filterButton").click();
}


