function getPlecari(){
    let xmlhttp = new XMLHttpRequest();
    let url = "plecari.php";
    xmlhttp.onreadystatechange = function() {
        if(xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            document.getElementById("orasPlecare").innerHTML = xmlhttp.responseText;
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function getSosiri(value){
    let xmlhttp = new XMLHttpRequest();
    let url = "sosiri.php?plecare="+value;
    xmlhttp.onreadystatechange = function() {
        if(xmlhttp.readyState == 4 && xmlhttp.status == 200){
            document.getElementById("orasSosire").innerHTML = xmlhttp.responseText;
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}