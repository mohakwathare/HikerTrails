var events = [];
$(".hikes").each(function () {
    var id = $(".identity", this).text().trim();
    var title = $(".title", this).text().trim();
    var start = $(".start", this).text().trim();
    var end = $(".end", this).text().trim();
    var sd = moment(start, "DD/MM/YYYY hh:mm A").format("YYYY-MM-DD hh:mm");
    var ed = moment(end, "DD/MM/YYYY hh:mm A").format("YYYY-MM-DD hh:mm");
    var event = {
        "id": id,
        "title": title,
        "start": sd,
        "end": ed,
        "url": "/Hikes/ViewHike/" + id
    };
    events.push(event);
});
$("#calendar").fullCalendar({
    locale: 'au',
    events: events
});