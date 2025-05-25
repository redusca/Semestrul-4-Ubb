$(document).ready(function() {
    var proprieties = ['platform','publisher','genre'];

    proprieties.forEach(function(property){
        $.ajax({
            url: "get" + property + ".php",
            type: "GET",
            dataType: "json",
            success: function(items) {
                var $select = $('#' + property);
                items.forEach(function(item) {
                    $select.append($('<option>').val(item).text(item));
                });
            }
        });
    });

    $("#filterButton").on("click", function() {
        var platform = $("#platform").val();
        var publisher = $("#publisher").val();
        var genre = $("#genre").val();
        var stock = $("#stock").val();
        var price = $("#price").val();

        $.ajax({
            url: "filter.php",
            type: "GET",
            data: {
                platform: platform,
                publisher: publisher,
                genre: genre,
                stock: stock,
                price: price
            },
            dataType: "json",
            success: function(data) {
                var $results = $("#results");
                
                var $table = $('<table>');
                var $thead = $('<thead>');
                var $tbody = $('<tbody>');

                var $tr = $('<tr>');
                for (var key in data[0]) {
                    $tr.append($('<th>').text(key));
                }
                $thead.append($tr);

                data.forEach(function(item) {
                    var $tr = $('<tr>');
                    for (var key in item) {
                        $tr.append($('<td>').text(item[key]));
                    }
                    $tbody.append($tr);
                });

                $table.append($thead).append($tbody);
                $results.empty().append($table);
            }
        });
    });

    $("#filterButton").click();
});