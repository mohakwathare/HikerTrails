function sendRating() {
    var radiobuttons = document.getElementsByName("rating");
    var rating;
    for (var i = 0; i < radiobuttons.length; i++) {
        if (radiobuttons[i].checked) {
            rating = radiobuttons[i].value;
            break;
        }
    }
    if (rating == null) {
        alert("Please select rating first!!")
    }
    else {
        var id = document.getElementById("Id").value;
        window.location.href = "/Hikes/RateHikeCalculate?id=" + id + "&rating=" + rating;
    }
}