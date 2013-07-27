var Auction = Auction || {};

Auction.overview = Auction.Overview || {
		refresh: function () {
				alert('adf');
		}
};

$(function () {
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
});