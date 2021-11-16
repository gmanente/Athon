
/**
 * Custom positioner
 * @function Chart.Tooltip.positioners.custom
 * @param elements {Chart.Element[]} the tooltip elements
 * @param eventPosition {Point} the position of the event in canvas coordinates
 * @returns {Point} the tooltip position
 */
Chart.Tooltip.positioners.custom = function (elements, eventPosition) {
    /** @type {Chart.Tooltip} */
    var tooltip = this;

    /* ... */

    return {
        x: 0,
        y: 0
    };
}


// Define a plugin to provide data labels
Chart.plugins.register({
    afterDatasetsDraw: function (chart, easing) {
        // To only draw at the end of animation, check for easing === 1
        var ctx = chart.ctx;
        chart.data.datasets.forEach(function (dataset, i) {
            var meta = chart.getDatasetMeta(i);



            if (!meta.hidden) {
                meta.data.forEach(function (element, index) {

                    if (!element.hidden) {
                        // Draw the text in black, with the specified font
                        ctx.fillStyle = 'rgb(0, 0, 0)';
                        var fontSize = 12;
                        var fontStyle = 'normal';
                        var fontFamily = '"Open Sans",Arial,Helvetica,Sans-Serif';
                        ctx.font = Chart.helpers.fontString(fontSize, fontStyle, fontFamily);
                        // Just naively convert to string for now
                        var dataString = dataset.data[index].toString();

                        // Make sure alignment settings are correct
                        ctx.textAlign = 'center';
                        ctx.textBaseline = 'middle';
                        var padding = 5;
                        var position = element.tooltipPosition();


                        if (dataString != "0")
                            ctx.fillText(dataString, position.x, position.y - (fontSize / 4) - padding);

                    }
                });
            }
        });



    }
});