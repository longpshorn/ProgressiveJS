/*
 * progressive
 * https://github.com/longpshorn/ProgressiveJS
 *
 * Copyright (c) 2014 Patrick Smith
 * Licensed under the MIT license.
 */

(function($) {

  // Collection method.
  $.fn.progressive = function() {
    return this.each(function(i) {
      // Do something awesome to each selected element.
      $(this).html('awesome' + i);
    });
  };

  // Static method.
  $.progressive = function(options) {
    // Override default options with passed-in options.
    options = $.extend({}, $.progressive.options, options);
    // Return something awesome.
    return 'awesome' + options.punctuation;
  };

  // Static method default options.
  $.progressive.options = {
    punctuation: '.'
  };

  // Custom selector.
  $.expr[':'].progressive = function(elem) {
    // Is this element awesome?
    return $(elem).text().indexOf('awesome') !== -1;
  };

}(jQuery));
