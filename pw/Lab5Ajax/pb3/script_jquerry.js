$(document).ready(function() {
    let $table = $("#table");
    let $saveButton = $("#saveButton");
    let savedChanges = true;

    function loadTable() {
        $.ajax({
            url: "get_data.php",
            type: "GET",
            dataType: "json",
            success: function(response) {
                $table.html("<tr><th>ID</th><th>Title</th><th>Platform</th><th>Genre</th><th>Publisher</th><th>Release Date</th><th>Stock</th><th>Price $</th></tr>");
                
                $.each(response, function(i, item) {
                    let row = $("<tr>").addClass("table-row");
                    row.append($("<td>").text(item.id));
                    row.append($("<td>").text(item.title));
                    row.append($("<td>").text(item.platform));
                    row.append($("<td>").text(item.genre));
                    row.append($("<td>").text(item.publisher));
                    row.append($("<td>").text(item.release_date));
                    row.append($("<td>").text(item.stock));
                    row.append($("<td>").text(item.price));
                    $table.append(row);
                });
            }
        });
    }

    loadTable();

    function saveData() {
        $.ajax({
            url: "save_data.php",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify({
                id: $("#id").val(),
                title: $("#title").val(),
                platform: $("#platform").val(),
                genre: $("#genre").val(),
                publisher: $("#publisher").val(),
                release_date: $("#release\\ Date").val(),
                stock: $("#stock").val(),
                price: $("#price").val()
            }),
            success: function() {
                loadTable();
                savedChanges = true;
            }
        });
    }

   $table.on('click', '.table-row', function() {
        let $targetRow = $(this);
        let $cells = $targetRow.find('td');

        if ($cells.eq(0).text() == $("#id").val()) {
            return;
        }

        if (savedChanges == false) {
            if (!confirm("You have unsaved changes. Do you want to continue?")) {
                return;
            }
        }

        if ($cells.length >= 8) {
            $("#id").val($cells.eq(0).text());
            $("#title").val($cells.eq(1).text());
            $("#platform").val($cells.eq(2).text());
            $("#genre").val($cells.eq(3).text());
            $("#publisher").val($cells.eq(4).text());
            $("#release\\ Date").val($cells.eq(5).text());
            $("#stock").val($cells.eq(6).text());
            $("#price").val($cells.eq(7).text());
        }

        $saveButton.prop("disabled", true);
        savedChanges = true;
    });

    $("#formular input").on('input', function() {
        let $inputs = $("#formular input");
        let hasContent = $inputs.toArray().some(input => $(input).val().trim() !== '');
        $saveButton.prop("disabled", !hasContent);
        savedChanges = false;
    });

    $saveButton.click(function() {
        saveData();
        $saveButton.prop("disabled", true);
        alert("Data saved successfully!");
    });
});