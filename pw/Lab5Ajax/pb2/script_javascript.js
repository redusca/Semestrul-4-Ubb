window.onload = function() {
    let page = 1;

    function loadData(){
        let xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                let response = JSON.parse(this.responseText);
                let data = response.data;
                let total = response.total;
                let table = document.getElementById("data-table");
                for (let i = 0; i < data.length; i++) {
                    let row = table.insertRow();
                    row.insertCell(0).innerText = data[i].nume;
                    row.insertCell(1).innerText = data[i].prenume;
                    row.insertCell(2).innerText = data[i].telefon;
                    row.insertCell(3).innerText = data[i].email;
                }
                document.getElementById("prev-btn").disabled = page === 1;
                document.getElementById("next-btn").disabled = page * 3 >= total
            }
        };
        xmlhttp.open("GET", "get_data.php?page=" + page, true);
        xmlhttp.send();
    }

    document.getElementById("prev-btn").addEventListener("click", function() {
        page--;
        loadData();
    });

    document.getElementById("next-btn").addEventListener("click", function() {
        page++;
        loadData();
    });

    loadData();
}
