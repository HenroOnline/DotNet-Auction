var Auction = Auction || {};

Auction.overview = Auction.Overview || {
	
};

$(function () {
		$.validator.methods.number = function (value, element) {
				return this.optional(element) || !isNaN(Globalize.parseFloat(value));
		}

		Globalize.culture('nl-NL');
		/*
		var theWindow = $(window),
				$bg = $("#bg"),
				aspectRatio = $bg.width() / $bg.height();

		function resizeBg() {

				if ((theWindow.width() / theWindow.height()) < aspectRatio) {
						$bg
							.removeClass()
							.addClass('bgheight');
				} else {
						$bg
							.removeClass()
							.addClass('bgwidth');
				}

		}

		theWindow.resize(resizeBg).trigger("resize");
		*/
});