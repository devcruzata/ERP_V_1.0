$(document).ready(function () {
    //var e = [
    //        [0, 150708],
    //        [1, 502507],
    //        [2, 220627],
    //        [3, 821182],
    //        [4, 233599],
    //        [5, 4087866],
    //        [6, 364625],
    //        [7, 3064625],
    //        [8, 236585],
    //        [9, 1040222],
    //        [10, 516876],
    //        [11, 292003]],
    //    o = [
    //        [0, 650708],
    //        [1, 1102507],
    //        [2, 417012],
    //        [3, 495497],
    //        [4, 887603],
    //        [5, 564775],
    //        [6, 2580159],
    //        [7, 607998],
    //        [8, 1906411],
    //        [9, 346237],
    //        [10, 315699],
    //        [11, 202003]],
    //    t = [
    //        [0, "Jan"],
    //        [1, "Feb"],
    //        [2, "Mar"],
    //        [3, "Apr"],
    //        [4, "May"],
    //        [5, "Jun"],
    //        [6, "Jul"],
    //        [7, "Aug"],
    //        [8, "Sep"],
    //        [9, "Oct"],
    //        [10, "Nov"],
    //        [11, "Dec"]],
    //    a = [
    //           { label: "Ordered", data: e, color: "#0667D6" },
    //           { label: "Returned", data: o, color: "#1F364F" }
    //        ],
    //   r = {
    //                    series: {
    //                        lines: { show: !0, lineWidth: 4 },
    //                        points: { show: !0, lineWidth: 4, radius: 6 }, shadowSize: 0
    //                    },
    //                    grid: { borderWidth: 0, hoverable: !0, labelMargin: 15 },
    //                    xaxis: {
    //                        ticks: t, tickLength: 0,
    //                        font: { color: "#9a9a9a", size: 11 }
    //                    },
    //                    yaxis: {
    //                        tickLength: 0, tickSize: 1e6, font: { color: "#9a9a9a", size: 11 },
    //                        tickFormatter: function (e, o) { return e > 0 ? (e / 1e6).toFixed(o.tickDecimals) + " M" : (e / 1e6).toFixed(o.tickDecimals) }
    //                    }, tooltip: { show: !1 }, legend: { show: !0, position: "ne", noColumns: 4, labelBoxBorderColor: "#FFF", margin: 0 }
    //        };
    //$.plot($("#flot-order"), a, r),
    //$("#flot-order").bind("plothover", function (e, o, t) {
    //    t ? $(".flotTip").text(t.series.label + ": $" + t.datapoint[1].toFixed(0).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")).css({ top: t.pageY + 15, left: t.pageX + 10 }).show() : $(".flotTip").hide()
    //});
    //var l = [
    //        [0, 1021],
    //        [1, 1370],
    //        [2, 904],
    //        [3, 690],
    //        [4, 904],
    //        [5, 929],
    //        [6, 789],
    //        [7, 579],
    //        [8, 1039],
    //        [9, 1204],
    //        [10, 1120],
    //        [11, 809]],
    //    i = [{ label: "Sales", data: l, color: "#0667D6" }],
    //    n = [
    //        [0, "Jan"],
    //        [1, "Feb"],
    //        [2, "Mar"],
    //        [3, "Apr"],
    //        [4, "May"],
    //        [5, "Jun"],
    //        [6, "Jul"],
    //        [7, "Aug"],
    //        [8, "Sep"],
    //        [9, "Oct"],
    //        [10, "Nov"],
    //        [11, "Dec"]],
    //    s = {
    //        series: { bars: { show: !0, fill: .2, align: "center", barWidth: .5, lineWidth: 2 } },
    //        grid: { borderWidth: 0, hoverable: !0, tickColor: "#fff", labelMargin: 15 },
    //        xaxis: { font: { color: "#9a9a9a", size: 11 }, ticks: n },
    //        yaxis: {
    //            font: { color: "#9a9a9a", size: 11 },
    //            tickFormatter: function (e, o) { return e.toString().replace(/\B(?=(?:\d{3})+(?!\d))/g, ",") }
    //        }, tooltip: { show: !0, content: "%x: %y", defaultTheme: !1 }, legend: { show: !1 }
    //    };
    //$.plot($("#flot-sales"), i, s),
    //Morris.Donut({
    //    element: "morris-category",
    //    data: [{ label: "Cosmetics", value: 40 },
    //        { label: "Accessories", value: 35 },
    //        { label: "Books", value: 25 }],
    //    resize: !0,
    //    colors: ["#1F364F", "#0667D6", "#E6E6E6"],
    //    formatter: function (e) { return e + "%" }
    //});
    var c = $("#order-table").DataTable({ lengthChange: !1, pageLength: 5, colReorder: !0, buttons: ["copy", "excel", "pdf", "print"], language: { search: "", searchPlaceholder: "Search records" }, columnDefs: [{ orderable: !1, targets: 6 }] }); c.buttons().container().appendTo("#order-table_wrapper .col-sm-6:eq(0)")
});