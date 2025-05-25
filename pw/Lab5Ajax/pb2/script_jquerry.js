$(document).ready(function() {
    let page = 1;

    function loadData(){
        $.ajax({
            url: "get_data.php",
            type: "GET",
            data: { page: page },
            dataType: "json",
            success: function(response) {
                let data = response.data;
                let total = response.total;
                let table = $("#data-table");
                
                $.each(data, function(i, item) {
                    let row = $("<tr>");
                    row.append($("<td>").text(item.nume));
                    row.append($("<td>").text(item.prenume));
                    row.append($("<td>").text(item.telefon));
                    row.append($("<td>").text(item.email));
                    table.append(row);
                });
                
                $("#prev-btn").prop("disabled", page === 1);
                $("#next-btn").prop("disabled", page * 3 >= total);
            }
        });
    }

    $("#prev-btn").click(function() {
        page--;
        loadData();
    });

    $("#next-btn").click(function() {
        page++;
        loadData();
    });

    loadData();
});