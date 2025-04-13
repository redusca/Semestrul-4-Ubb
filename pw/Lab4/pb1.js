function changeList(source, destination) {
    destination.append(source.find('option:selected').detach());
}

$('#list1').dblclick(function() {
    changeList($('#list1'), $('#list2'));
});
$('#list2').dblclick(function() {
    changeList($('#list2'), $('#list1'));
});
