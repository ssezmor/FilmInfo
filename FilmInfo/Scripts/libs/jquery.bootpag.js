/**
 * @preserve 
 * bootpag - jQuery plugin for dynamic pagination
 *
 * Project home: https://github.com/Nicolai8/jquery-bootpag
 *
 * This is the fork of https://github.com/botmonster/jquery-bootpag
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Version:  1.0.0
 *
 */
(function ($, window) {

	$.fn.bootpag = function (options) {

		var $owner = this,
            settings = $.extend({
            	total: 0,
            	page: 1,
            	maxVisible: null,
            	leaps: true,
            	href: 'javascript:void(0);',
            	hrefVariable: '{{number}}',
            	next: '&raquo;',
            	prev: '&laquo;'
            },
            $owner.data('settings') || {},
            options || {});

		if (settings.total <= 0)
			return this;

		if (!$.isNumeric(settings.maxVisible) && !settings.maxVisible) {
			settings.maxVisible = settings.total;
		}

		$owner.data('settings', settings);

		function renderPage($bootpag, page) {

			var lp,
                maxV = settings.maxVisible == 0 ? 1 : settings.maxVisible,
                step = settings.maxVisible == 1 ? 0 : 1,
                vis = Math.floor((page - 1) / maxV) * maxV,
                $page = $bootpag.find('li');
			settings.page = page = page < 0 ? 0 : page > settings.total ? settings.total : page;
			$page.removeClass('disabled');
			lp = page - 1 < 1 ? 1 :
                    settings.leaps && page - 1 >= settings.maxVisible ?
                        Math.floor((page - 1) / maxV) * maxV : page - 1;
			$page
                .first()
                .toggleClass('disabled', page === 1)
                .attr('data-lp', lp)
                .find('a').attr('href', href(lp));

			var step = settings.maxVisible == 1 ? 0 : 1;

			lp = page + 1 > settings.total ? settings.total :
                    settings.leaps && page + 1 < settings.total - settings.maxVisible ?
                        vis + settings.maxVisible + step : page + 1;

			$page
                .last()
                .toggleClass('disabled', page === settings.total)
                .attr('data-lp', lp)
                .find('a').attr('href', href(lp));;

			var $currPage = $page.filter('[data-lp=' + page + ']');
			if (!$currPage.not('.next,.prev').length) {
				var d = page <= vis ? -settings.maxVisible : 0;
				$page.not('.next,.prev').each(function (index) {
					lp = index + 1 + vis + d;
					$(this)
                        .attr('data-lp', lp)
                        .toggle(lp <= settings.total)
                        .find('a').html(lp).attr('href', href(lp));
				});
				$currPage = $page.filter('[data-lp=' + page + ']');
			}
			$currPage.addClass('disabled');
			$owner.data('settings', settings);
		}

		function href(c) {
			return settings.href.replace(settings.hrefVariable, c);
		}

		function makeDropdown() {
			var $select = $("<select/>");
			$select.addClass("bootpag form-control visible-xs");
			for (var c = 1; c <= settings.total; c++) {
				var $option = $("<option/>").text(c).attr("data-lp", c);
				if (c == settings.page) {
					$option.attr("selected", "");
				}
				$select.append($option);
			}
			return $select;
		}

		function makeUl() {
			var $ul = $("<ul class='pagination bootpag hidden-xs'>");

			if (settings.prev) {
				$ul.append("<li data-lp='1' class='prev'><a href='" + href(1) + "'>" + settings.prev + "</a></li>");
			}
			for (var c = 1; c <= Math.min(settings.total, settings.maxVisible) ; c++) {
				$ul.append("<li data-lp='" + c + "'><a href='" + href(c) + "'>" + c + "</a></li>");
			}
			if (settings.next) {
				var lp = settings.leaps && settings.total > settings.maxVisible
                    ? Math.min(settings.maxVisible + 1, settings.total) : 2;
				$ul.append("<li data-lp='" + lp + "' class='next'><a href='" + href(lp) + "'>" + settings.next + "</a></li>");
			}

			return $ul;
		}

		return this.each(function () {

			var $this = $(this),
                $bootpag = makeUl(),
				$bootpagDropdown = makeDropdown();

			$this.find("ul.bootpag, select.bootpag").remove();

			$this.append($bootpag);
			$this.append($bootpagDropdown);

			$bootpag.find("li").click(function paginationClick() {
				var $this = $(this);
				if ($this.hasClass("disabled")) {
					return;
				}
				var page = parseInt($this.attr("data-lp"), 10);
				renderPage($bootpag, page);
				$owner.trigger("page", page);
			});

			$bootpagDropdown.change(function () {
				var $this = $(this);
				var page = parseInt($this.find(":selected").attr("data-lp"), 10);
				renderPage($bootpag, page);
				$owner.trigger("page", page);
			});

			renderPage($bootpag, settings.page);
		});
	}

})(jQuery, window);
