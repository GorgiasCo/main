(function (app) {
    'use strict';

    app.directive('revSlider', revSlider);

    function revSlider($timeout) {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/layout/directive/rev-slider.html',
            link: function (scope, element, attr) {
                $timeout(function () {
                    var tpj = jQuery;
                    tpj.noConflict();
                    var revapi1;
                    tpj(document).ready(function () {
                        if (tpj("#rev_slider_1_2").revolution == undefined) {
                            revslider_showDoubleJqueryError("#rev_slider_1_2");
                        } else {
                            revapi1 = tpj("#rev_slider_1_2").show().revolution({
                                sliderType: "standard",
                                sliderLayout: "fullscreen",
                                dottedOverlay: "none",
                                delay: 6000,
                                navigation: {
                                    keyboardNavigation: "off",
                                    keyboard_direction: "horizontal",
                                    mouseScrollNavigation: "off",
                                    onHoverStop: "on",
                                    touch: {
                                        touchenabled: "on",
                                        swipe_threshold: 0.7,
                                        swipe_min_touches: 1,
                                        swipe_direction: "horizontal",
                                        drag_block_vertical: false
                                    }
                                },
                                gridwidth: 1180,
                                gridheight: 1000,
                                lazyType: "none",
                                shadow: 0,
                                spinner: "spinner3",
                                stopLoop: "off",
                                stopAfterLoops: -1,
                                stopAtSlide: -1,
                                shuffle: "off",
                                autoHeight: "off",
                                fullScreenAlignForce: "off",
                                fullScreenOffsetContainer: "",
                                fullScreenOffset: "",
                                disableProgressBar: "on",
                                hideThumbsOnMobile: "on",
                                hideSliderAtLimit: 0,
                                hideCaptionAtLimit: 0,
                                hideAllCaptionAtLilmit: 0,
                                startWithSlide: 0,
                                debugMode: false,
                                fallbacks: {
                                    simplifyAll: "off",
                                    nextSlideOnWindowFocus: "off",
                                    disableFocusListener: "off",
                                }
                            });
                        }
                    });

                });
            }
        }
    }

})(angular.module('gorgiasapp'));