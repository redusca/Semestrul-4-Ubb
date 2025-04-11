function changeList(soruce,destination){
    let source = document.getElementById(soruce);
    let des = document.getElementById(destination);
    let selectedOptions = source.options[source.selectedIndex];
    source.remove(source.selectedIndex);
    des.add(selectedOptions);
}