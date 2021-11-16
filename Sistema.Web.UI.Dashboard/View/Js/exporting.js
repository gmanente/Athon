/*
 Highcharts JS v6.0.7 (2018-02-16)
 Exporting module

 (c) 2010-2017 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (m) { "object" === typeof module && module.exports ? module.exports = m : m(Highcharts) })(function (m) {
    (function (f) {
        var m = f.defaultOptions, u = f.doc, B = f.Chart, w = f.addEvent, K = f.removeEvent, I = f.fireEvent, q = f.createElement, E = f.discardElement, F = f.css, x = f.merge, C = f.pick, t = f.each, G = f.objectEach, A = f.extend, y = f.win, J = y.navigator.userAgent, H = f.SVGRenderer, L = f.Renderer.prototype.symbols, M = /Edge\/|Trident\/|MSIE /.test(J), N = /firefox/i.test(J); A(m.lang, {
            printChart: "Print chart", downloadPNG: "Download PNG image", downloadJPEG: "Download JPEG image",
            downloadPDF: "Download PDF document", downloadSVG: "Download SVG vector image", contextButtonTitle: "Chart context menu"
        }); m.navigation = { buttonOptions: { theme: {}, symbolSize: 14, symbolX: 12.5, symbolY: 10.5, align: "right", buttonSpacing: 3, height: 22, verticalAlign: "top", width: 24 } }; m.exporting = {
            type: "image/png", url: "https://export.highcharts.com/", printMaxWidth: 780, scale: 2, buttons: {
                contextButton: {
                    className: "highcharts-contextbutton", menuClassName: "highcharts-contextmenu", symbol: "menu", _titleKey: "contextButtonTitle",
                    menuItems: "printChart separator downloadPNG downloadJPEG downloadPDF downloadSVG".split(" ")
                }
            }, menuItemDefinitions: {
                printChart: { textKey: "printChart", onclick: function () { this.print() } }, separator: { separator: !0 }, downloadPNG: { textKey: "downloadPNG", onclick: function () { this.exportChart() } }, downloadJPEG: { textKey: "downloadJPEG", onclick: function () { this.exportChart({ type: "image/jpeg" }) } }, downloadPDF: { textKey: "downloadPDF", onclick: function () { this.exportChart({ type: "application/pdf" }) } }, downloadSVG: {
                    textKey: "downloadSVG",
                    onclick: function () { this.exportChart({ type: "image/svg+xml" }) }
                }
            }
        }; f.post = function (a, b, c) { var d = q("form", x({ method: "post", action: a, enctype: "multipart/form-data" }, c), { display: "none" }, u.body); G(b, function (a, b) { q("input", { type: "hidden", name: b, value: a }, null, d) }); d.submit(); E(d) }; A(B.prototype, {
            sanitizeSVG: function (a, b) {
                if (b && b.exporting && b.exporting.allowHTML) {
                    var c = a.match(/<\/svg>(.*?$)/); c && c[1] && (c = '\x3cforeignObject x\x3d"0" y\x3d"0" width\x3d"' + b.chart.width + '" height\x3d"' + b.chart.height + '"\x3e\x3cbody xmlns\x3d"http://www.w3.org/1999/xhtml"\x3e' +
                        c[1] + "\x3c/body\x3e\x3c/foreignObject\x3e", a = a.replace("\x3c/svg\x3e", c + "\x3c/svg\x3e"))
                } return a = a.replace(/zIndex="[^"]+"/g, "").replace(/isShadow="[^"]+"/g, "").replace(/symbolName="[^"]+"/g, "").replace(/jQuery[0-9]+="[^"]+"/g, "").replace(/url\(("|&quot;)(\S+)("|&quot;)\)/g, "url($2)").replace(/url\([^#]+#/g, "url(#").replace(/<svg /, '\x3csvg xmlns:xlink\x3d"http://www.w3.org/1999/xlink" ').replace(/ (|NS[0-9]+\:)href=/g, " xlink:href\x3d").replace(/\n/, " ").replace(/<\/svg>.*?$/, "\x3c/svg\x3e").replace(/(fill|stroke)="rgba\(([ 0-9]+,[ 0-9]+,[ 0-9]+),([ 0-9\.]+)\)"/g,
                    '$1\x3d"rgb($2)" $1-opacity\x3d"$3"').replace(/&nbsp;/g, "\u00a0").replace(/&shy;/g, "\u00ad")
            }, getChartHTML: function () { this.inlineStyles(); return this.container.innerHTML }, getSVG: function (a) {
                var b, c, d, v, h, g = x(this.options, a); c = q("div", null, { position: "absolute", top: "-9999em", width: this.chartWidth + "px", height: this.chartHeight + "px" }, u.body); d = this.renderTo.style.width; h = this.renderTo.style.height; d = g.exporting.sourceWidth || g.chart.width || /px$/.test(d) && parseInt(d, 10) || 600; h = g.exporting.sourceHeight ||
                    g.chart.height || /px$/.test(h) && parseInt(h, 10) || 400; A(g.chart, { animation: !1, renderTo: c, forExport: !0, renderer: "SVGRenderer", width: d, height: h }); g.exporting.enabled = !1; delete g.data; g.series = []; t(this.series, function (a) { v = x(a.userOptions, { animation: !1, enableMouseTracking: !1, showCheckbox: !1, visible: a.visible }); v.isInternal || g.series.push(v) }); t(this.axes, function (a) { a.userOptions.internalKey || (a.userOptions.internalKey = f.uniqueKey()) }); b = new f.Chart(g, this.callback); a && t(["xAxis", "yAxis", "series"], function (d) {
                        var c =
                            {}; a[d] && (c[d] = a[d], b.update(c))
                    }); t(this.axes, function (a) { var d = f.find(b.axes, function (b) { return b.options.internalKey === a.userOptions.internalKey }), c = a.getExtremes(), e = c.userMin, c = c.userMax; !d || void 0 === e && void 0 === c || d.setExtremes(e, c, !0, !1) }); d = b.getChartHTML(); d = this.sanitizeSVG(d, g); g = null; b.destroy(); E(c); return d
            }, getSVGForExport: function (a, b) {
                var c = this.options.exporting; return this.getSVG(x({ chart: { borderRadius: 0 } }, c.chartOptions, b, {
                    exporting: {
                        sourceWidth: a && a.sourceWidth || c.sourceWidth,
                        sourceHeight: a && a.sourceHeight || c.sourceHeight
                    }
                }))
            }, exportChart: function (a, b) { b = this.getSVGForExport(a, b); a = x(this.options.exporting, a); f.post(a.url, { filename: a.filename || "chart", type: a.type, width: a.width || 0, scale: a.scale, svg: b }, a.formAttributes) }, print: function () {
                var a = this, b = a.container, c = [], d = b.parentNode, f = u.body, h = f.childNodes, g = a.options.exporting.printMaxWidth, e, n; if (!a.isPrinting) {
                a.isPrinting = !0; a.pointer.reset(null, 0); I(a, "beforePrint"); if (n = g && a.chartWidth > g) e = [a.options.chart.width, void 0,
                !1], a.setSize(g, void 0, !1); t(h, function (a, b) { 1 === a.nodeType && (c[b] = a.style.display, a.style.display = "none") }); f.appendChild(b); y.focus(); y.print(); setTimeout(function () { d.appendChild(b); t(h, function (a, b) { 1 === a.nodeType && (a.style.display = c[b]) }); a.isPrinting = !1; n && a.setSize.apply(a, e); I(a, "afterPrint") }, 1E3)
                }
            }, contextMenu: function (a, b, c, d, v, h, g) {
                var e = this, n = e.chartWidth, p = e.chartHeight, r = "cache-" + a, k = e[r], l = Math.max(v, h), D, m; k || (e[r] = k = q("div", { className: a }, {
                    position: "absolute", zIndex: 1E3, padding: l +
                        "px"
                }, e.container), D = q("div", { className: "highcharts-menu" }, null, k), m = function () { F(k, { display: "none" }); g && g.setState(0); e.openMenu = !1 }, e.exportEvents.push(w(k, "mouseleave", function () { k.hideTimer = setTimeout(m, 500) }), w(k, "mouseenter", function () { clearTimeout(k.hideTimer) }), w(u, "mouseup", function (b) { e.pointer.inClass(b.target, a) || m() })), t(b, function (a) {
                "string" === typeof a && (a = e.options.exporting.menuItemDefinitions[a]); if (f.isObject(a, !0)) {
                    var b; b = a.separator ? q("hr", null, null, D) : q("div", {
                        className: "highcharts-menu-item",
                        onclick: function (b) { b && b.stopPropagation(); m(); a.onclick && a.onclick.apply(e, arguments) }, innerHTML: a.text || e.options.lang[a.textKey]
                    }, null, D); e.exportDivElements.push(b)
                }
                }), e.exportDivElements.push(D, k), e.exportMenuWidth = k.offsetWidth, e.exportMenuHeight = k.offsetHeight); b = { display: "block" }; c + e.exportMenuWidth > n ? b.right = n - c - v - l + "px" : b.left = c - l + "px"; d + h + e.exportMenuHeight > p && "top" !== g.alignOptions.verticalAlign ? b.bottom = p - d - l + "px" : b.top = d + h - l + "px"; F(k, b); e.openMenu = !0
            }, addButton: function (a) {
                var b = this,
                c = b.renderer, d = x(b.options.navigation.buttonOptions, a), f = d.onclick, h = d.menuItems, g, e, n = d.symbolSize || 12; b.btnCount || (b.btnCount = 0); b.exportDivElements || (b.exportDivElements = [], b.exportSVGElements = []); if (!1 !== d.enabled) {
                    var p = d.theme, r = p.states, k = r && r.hover, r = r && r.select, l; delete p.states; f ? l = function (a) { a.stopPropagation(); f.call(b, a) } : h && (l = function () { b.contextMenu(e.menuClassName, h, e.translateX, e.translateY, e.width, e.height, e); e.setState(2) }); d.text && d.symbol ? p.paddingLeft = C(p.paddingLeft, 25) : d.text ||
                        A(p, { width: d.width, height: d.height, padding: 0 }); e = c.button(d.text, 0, 0, l, p, k, r).addClass(a.className).attr({ title: C(b.options.lang[d._titleKey], ""), zIndex: 3 }); e.menuClassName = a.menuClassName || "highcharts-menu-" + b.btnCount++; d.symbol && (g = c.symbol(d.symbol, d.symbolX - n / 2, d.symbolY - n / 2, n, n).addClass("highcharts-button-symbol").attr({ zIndex: 1 }).add(e)); e.add().align(A(d, { width: e.width, x: C(d.x, b.buttonOffset) }), !0, "spacingBox"); b.buttonOffset += (e.width + d.buttonSpacing) * ("right" === d.align ? -1 : 1); b.exportSVGElements.push(e,
                            g)
                }
            }, destroyExport: function (a) { var b = a ? a.target : this; a = b.exportSVGElements; var c = b.exportDivElements, d = b.exportEvents, f; a && (t(a, function (a, d) { a && (a.onclick = a.ontouchstart = null, f = "cache-" + a.menuClassName, b[f] && delete b[f], b.exportSVGElements[d] = a.destroy()) }), a.length = 0); c && (t(c, function (a, d) { clearTimeout(a.hideTimer); K(a, "mouseleave"); b.exportDivElements[d] = a.onmouseout = a.onmouseover = a.ontouchstart = a.onclick = null; E(a) }), c.length = 0); d && (t(d, function (a) { a() }), d.length = 0) }
        }); H.prototype.inlineToAttributes =
            "fill stroke strokeLinecap strokeLinejoin strokeWidth textAnchor x y".split(" "); H.prototype.inlineBlacklist = [/-/, /^(clipPath|cssText|d|height|width)$/, /^font$/, /[lL]ogical(Width|Height)$/, /perspective/, /TapHighlightColor/, /^transition/, /^length$/]; H.prototype.unstyledElements = ["clipPath", "defs", "desc"]; B.prototype.inlineStyles = function () {
                function a(a) { return a.replace(/([A-Z])/g, function (a, b) { return "-" + b.toLowerCase() }) } function b(c) {
                    function k(b, g) {
                        q = u = !1; if (h) {
                            for (z = h.length; z-- && !u;)u = h[z].test(g);
                            q = !u
                        } "transform" === g && "none" === b && (q = !0); for (z = f.length; z-- && !q;)q = f[z].test(g) || "function" === typeof b; q || m[g] === b && "svg" !== c.nodeName || e[c.nodeName][g] === b || (-1 !== d.indexOf(g) ? c.setAttribute(a(g), b) : r += a(g) + ":" + b + ";")
                    } var l, m, r = "", v, q, u, z; if (1 === c.nodeType && -1 === g.indexOf(c.nodeName)) {
                        l = y.getComputedStyle(c, null); m = "svg" === c.nodeName ? {} : y.getComputedStyle(c.parentNode, null); e[c.nodeName] || (n = p.getElementsByTagName("svg")[0], v = p.createElementNS(c.namespaceURI, c.nodeName), n.appendChild(v), e[c.nodeName] =
                            x(y.getComputedStyle(v, null)), n.removeChild(v)); if (N || M) for (var w in l) k(l[w], w); else G(l, k); r && (l = c.getAttribute("style"), c.setAttribute("style", (l ? l + ";" : "") + r)); "svg" === c.nodeName && c.setAttribute("stroke-width", "1px"); "text" !== c.nodeName && t(c.children || c.childNodes, b)
                    }
                } var c = this.renderer, d = c.inlineToAttributes, f = c.inlineBlacklist, h = c.inlineWhitelist, g = c.unstyledElements, e = {}, n, p, c = u.createElement("iframe"); F(c, { width: "1px", height: "1px", visibility: "hidden" }); u.body.appendChild(c); p = c.contentWindow.document;
                p.open(); p.write('\x3csvg xmlns\x3d"http://www.w3.org/2000/svg"\x3e\x3c/svg\x3e'); p.close(); b(this.container.querySelector("svg")); n.parentNode.removeChild(n)
            }; L.menu = function (a, b, c, d) { return ["M", a, b + 2.5, "L", a + c, b + 2.5, "M", a, b + d / 2 + .5, "L", a + c, b + d / 2 + .5, "M", a, b + d - 1.5, "L", a + c, b + d - 1.5] }; B.prototype.renderExporting = function () {
                var a = this, b = a.options.exporting, c = b.buttons, d = a.isDirtyExporting || !a.exportSVGElements; a.buttonOffset = 0; a.isDirtyExporting && a.destroyExport(); d && !1 !== b.enabled && (a.exportEvents = [],
                    G(c, function (b) { a.addButton(b) }), a.isDirtyExporting = !1); w(a, "destroy", a.destroyExport)
            }; B.prototype.callbacks.push(function (a) { a.renderExporting(); w(a, "redraw", a.renderExporting); t(["exporting", "navigation"], function (b) { a[b] = { update: function (c, d) { a.isDirtyExporting = !0; x(!0, a.options[b], c); C(d, !0) && a.redraw() } } }) })
    })(m)
});
