let listItems = document.querySelectorAll('#myList li')
let currentIndex = 0;
let numItems = listItems.length
let intervalNo

function showNextItem(){
    listItems[currentIndex].style.display = 'none';
    currentIndex = (currentIndex + 1) % numItems;
    listItems[currentIndex].style.display = 'flex';
    resetInterval();
}


function showPrevItem() {
    listItems[currentIndex].style.display = 'none';
    currentIndex = (currentIndex - 1 + numItems) % numItems;
    listItems[currentIndex].style.display = 'flex';
    resetInterval();
}

document.getElementById('next').addEventListener('click', showNextItem);
document.getElementById('prev').addEventListener('click', showPrevItem);

function resetInterval() {
    clearInterval(intervalNo);
    intervalNo = setInterval(showNextItem, 5000);
}

resetInterval();