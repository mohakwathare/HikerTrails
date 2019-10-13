var events = [];
$(".hikes").each(function () {
    var id = $(".identity", this).text().trim();
    var title = $(".title", this).text().trim();
    var start = $(".start", this).text().trim();
    var event = {
        "id": id,
        "title": title,
        "start": start
    };
    events.push(event);
});
$("#calendar").fullCalendar({
    locale: 'au',
    events: events,
    eventClick: function (event, jsEvent, view) {
        var id = event.id;
        var uri = "/Hikes/ViewHike/" + id;
        $(location).attr('href', uri);
    }
});