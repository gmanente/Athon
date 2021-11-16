/* jQuery Resizable Columns v0.1.0 | http://dobtco.github.io/jquery-resizable-columns/ | Licensed MIT | Built Wed Apr 30 2014 14:24:25 */
var __bind = function (a, b) {
    return function () {
        return a.apply(b, arguments)
    }
},
    __slice = [].slice;
! function (a, b) {
    var c, d, e, f;
    return d = function (a) {
        return parseFloat(a.style.width.replace("%", ""))
    }, f = function (a, b) {
        return b = b.toFixed(2), a.style.width = "" + b + "%"
    }, e = function (a) {
        return 0 === a.type.indexOf("touch") ? (a.originalEvent.touches[0] || a.originalEvent.changedTouches[0]).pageX : a.pageX
    }, c = function () {
        function c(c, d) {
            this.pointerdown = __bind(this.pointerdown, this), this.constrainWidth = __bind(this.constrainWidth, this), this.options = a.extend({}, this.defaults, d), this.$table = c, this.setHeaders(), this.restoreColumnWidths(), this.syncHandleWidths(), a(b).on("resize.rc", function (a) {
                return function () {
                    return a.syncHandleWidths()
                }
            }(this)), this.options.start && this.$table.bind("column:resize:start.rc", this.options.start), this.options.resize && this.$table.bind("column:resize.rc", this.options.resize), this.options.stop && this.$table.bind("column:resize:stop.rc", this.options.stop)
        }
        return c.prototype.defaults = {
            selector: "tr th:visible",
            store: b.store,
            syncHandlers: !0,
            resizeFromBody: !0,
            maxWidth: null,
            minWidth: null
        }, c.prototype.triggerEvent = function (b, c, d) {
            var e;
            return e = a.Event(b), e.originalEvent = a.extend({}, d), this.$table.trigger(e, [this].concat(c || []))
        }, c.prototype.getColumnId = function (a) {
            return this.$table.data("resizable-columns-id") + "-" + a.data("resizable-column-id")
        }, c.prototype.setHeaders = function () {
            return this.$tableHeaders = this.$table.find(this.options.selector), this.assignPercentageWidths(), this.createHandles()
        }, c.prototype.destroy = function () {
            return this.$handleContainer.remove(), this.$table.removeData("resizableColumns"), this.$table.add(b).off(".rc")
        }, c.prototype.assignPercentageWidths = function () {
            return this.$tableHeaders.each(function (b) {
                return function (c, d) {
                    var e;
                    return e = a(d), f(e[0], e.outerWidth() / b.$table.width() * 100)
                }
            }(this))
        }, c.prototype.createHandles = function () {
            var b;
            return null != (b = this.$handleContainer) && b.remove(), this.$table.before(this.$handleContainer = a("<div class='rc-handle-container' />")), this.$tableHeaders.each(function (b) {
                return function (c, d) {
                    var e;
                    if (0 !== b.$tableHeaders.eq(c + 1).length && null == b.$tableHeaders.eq(c).attr("data-noresize") && null == b.$tableHeaders.eq(c + 1).attr("data-noresize")) return e = a("<div class='rc-handle' />"), e.data("th", a(d)), e.appendTo(b.$handleContainer)
                }
            }(this)), this.$handleContainer.on("mousedown touchstart", ".rc-handle", this.pointerdown)
        }, c.prototype.syncHandleWidths = function () {
            return this.$handleContainer.width(this.$table.width()).find(".rc-handle").each(function (b) {
                return function (c, d) {
                    var e;
                    return e = a(d), e.css({
                        left: e.data("th").outerWidth() + (e.data("th").offset().left - b.$handleContainer.offset().left),
                        height: b.options.resizeFromBody ? b.$table.height() : b.$table.find("thead").height()
                    })
                }
            }(this))
        }, c.prototype.saveColumnWidths = function () {
            return this.$tableHeaders.each(function (b) {
                return function (c, e) {
                    var f;
                    return f = a(e), null == f.attr("data-noresize") && null != b.options.store ? b.options.store.set(b.getColumnId(f), d(f[0])) : void 0
                }
            }(this))
        }, c.prototype.restoreColumnWidths = function () {
            return this.$tableHeaders.each(function (b) {
                return function (c, d) {
                    var e, g;
                    return e = a(d), null != b.options.store && (g = b.options.store.get(b.getColumnId(e))) ? f(e[0], g) : void 0
                }
            }(this))
        }, c.prototype.totalColumnWidths = function () {
            var b;
            return b = 0, this.$tableHeaders.each(function () {
                return function (c, d) {
                    return b += parseFloat(a(d)[0].style.width.replace("%", ""))
                }
            }(this)), b
        }, c.prototype.constrainWidth = function (a) {
            return null != this.options.minWidth && (a = Math.max(this.options.minWidth, a)), null != this.options.maxWidth && (a = Math.min(this.options.maxWidth, a)), a
        }, c.prototype.pointerdown = function (b) {
            var c, g, h, i, j, k, l;
            return b.preventDefault(), h = a(b.currentTarget.ownerDocument), k = e(b), c = a(b.currentTarget), g = c.data("th"), i = this.$tableHeaders.eq(this.$tableHeaders.index(g) + 1), l = {
                left: d(g[0]),
                right: d(i[0])
            }, j = {
                left: l.left,
                right: l.right
            }, this.$handleContainer.add(this.$table).addClass("rc-table-resizing"), g.add(i).add(c).addClass("rc-column-resizing"), this.triggerEvent("column:resize:start", [g, i, j.left, j.right], b), h.on("mousemove.rc touchmove.rc", function (a) {
                return function (b) {
                    var c;
                    return c = (e(b) - k) / a.$table.width() * 100, f(g[0], j.left = a.constrainWidth(l.left + c)), f(i[0], j.right = a.constrainWidth(l.right - c)), null != a.options.syncHandlers && a.syncHandleWidths(), a.triggerEvent("column:resize", [g, i, j.left, j.right], b)
                }
            }(this)), h.one("mouseup touchend", function (a) {
                return function () {
                    return h.off("mousemove.rc touchmove.rc"), a.$handleContainer.add(a.$table).removeClass("rc-table-resizing"), g.add(i).add(c).removeClass("rc-column-resizing"), a.syncHandleWidths(), a.saveColumnWidths(), a.triggerEvent("column:resize:stop", [g, i, j.left, j.right], b)
                }
            }(this))
        }, c
    }(), a.fn.extend({
        resizableColumns: function () {
            var b, d;
            return d = arguments[0], b = 2 <= arguments.length ? __slice.call(arguments, 1) : [], this.each(function () {
                var e, f;
                return e = a(this), f = e.data("resizableColumns"), f || e.data("resizableColumns", f = new c(e, d)), "string" == typeof d ? f[d].apply(f, b) : void 0
            })
        }
    })
}(window.jQuery, window);