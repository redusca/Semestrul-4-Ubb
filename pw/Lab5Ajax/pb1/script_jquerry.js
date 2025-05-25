function getPlecari(){
    $.ajax({
        url: "plecari.php",
        type: "GET",
        success: function(data) {
            $("#orasPlecare").html(data);
        }
    });
}

function getSosiri(value){
    $.ajax({
        url: "sosiri.php",
        type: "GET",
        data: { plecare: value },
        success: function(data) {
            $("#orasSosire").html(data);
        }
    });
}