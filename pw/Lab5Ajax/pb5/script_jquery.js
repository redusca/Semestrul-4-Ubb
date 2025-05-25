let pathMap = {};
pathMap["1"] = "C:/xampp/htdocs/ajax/pb5/bin";
let counter = 1;

function loadFolders(id) {
    let $element = $('#' + id);
    if ($element.hasClass('opened')) {
        return;
    }
    $element.addClass("opened");
    let path = pathMap[id];
    let $list = $element.children().first();
    
    $.ajax({
        url: 'getFolders.php',
        type: 'GET',
        data: { path: path },
        dataType: 'json',
        success: function(folders) {
            if (folders.length > 2) {
                for (let i = 2; i < folders.length; i++) {
                    counter++;
                    let folder = folders[i];
                    let $newItem = $('<li>').text(folder);
                    
                    if (folder.indexOf('.') === -1) {
                        let $newList = $('<ul>');
                        pathMap[counter] = path + '/' + folder;
                        (function(currentCounter) {
                            $newItem.on('click', function() {
                                loadFolders(currentCounter);
                            });
                        })(counter);
                        $newItem.attr('id', counter);
                        $newItem.append($newList);
                        $newItem.css('color', 'green');
                    } else {
                        pathMap[counter] = path + '/' + folder;
                        (function(currentCounter) {
                            $newItem.on('click', function() {
                                loadFile(currentCounter);
                            });
                        })(counter);
                        if (folder.endsWith('.txt')) {
                            $newItem.css('color', 'black');
                        }
                    }
                    $list.append($newItem);
                }
            }
        }
    });
}

function loadFile(id) {
    let path = pathMap[id];
    $.ajax({
        url: 'getFile.php',
        type: 'GET',
        data: { path: path },
        success: function(data) {
            $('#explorer').val(data);
        }
    });
}