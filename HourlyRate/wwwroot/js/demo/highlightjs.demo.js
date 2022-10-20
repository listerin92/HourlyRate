/*
Template Name: ASPSTUDIO - Responsive Bootstrap 5 Admin Template
Version: 3.2.0
Author: Sean Ngu
Website: http://www.seantheme.com/asp-studio/
*/

var handleInitHighlightJs = function() {
	$('.hljs-container pre code').each(function(i, block) {
		hljs.highlightElement(block);
	});
};


/* Controller
------------------------------------------------ */
$(document).ready(function() {
	handleInitHighlightJs();
});