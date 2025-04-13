$(document).ready(function () {
    let listItems = $('#myList li');
    let numItems = listItems.length;
    let currentIndex = 0;
    let intervalNo;

    function showNextItem() {
        listItems.eq(currentIndex).css('display', 'none');
        currentIndex = (currentIndex + 1) % numItems;
        listItems.eq(currentIndex).css('display', 'flex');
        resetInterval();
    }


    function showPrevItem() {
        listItems.eq(currentIndex).css('display', 'none');
        currentIndex = (currentIndex - 1 + numItems) % numItems;
        listItems.eq(currentIndex).css('display', 'flex');
        resetInterval();
    }

    function resetInterval() {
        clearInterval(intervalNo);
        intervalNo = setInterval(showNextItem, 3000);
    }

    $('#next').click(showNextItem);
    $('#prev').click(showPrevItem);

    resetInterval()
});