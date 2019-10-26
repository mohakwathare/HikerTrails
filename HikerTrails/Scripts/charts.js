var datapoints = [];
$(".chartpoints").each(function () {
    var name = $(".name", this).text().trim();
    var rating = $(".rating", this).text().trim();
    var event = {
        "label": name,
        "y": parseFloat(rating)
    };
    datapoints.push(event);
});
window.onload = function () {
    var chart = new CanvasJS.Chart("chartContainer", {
        title: {
            text: "My First Chart in CanvasJS"
        },
        data: [
            {
                // Change type to "doughnut", "line", "splineArea", etc.
                type: "column",
                dataPoints: datapoints
            }
        ]
    });
    chart.render();
}