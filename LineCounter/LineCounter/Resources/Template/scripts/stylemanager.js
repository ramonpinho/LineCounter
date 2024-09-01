// *** TOTALLY CUSTOMISED ***
/*
  20171014 [Ramon] Fixing switch_style function that was calling set_cookie several times.
  Fixed cookie string format.
  I dont need the expiration.
  Changing to use html5 storage instead of cookies.
  Cookie is not been updated. Probably because if you don't set the expiration time, it keeps the current value.
  I'll get the value from a html select.

  How to use: Add the onload function to your page like this: <body onload="styleManager.initializeStyleManager();">

  TODO: Creating styleList HTML from code, so the component works independently from what the user defined in HTML. Create the DOM elements in initialization function.

  20210308 [Ramon] Removing jquery dependency.
                   Refactoring: making it as an object structured.
 */

var styleManager = {
    last_style: "",
    control: null,

    change_style: function (s) {
        // var v = $('#' + s + ' option:selected').val();
        if (this.control == null)
            return false;

        var v = this.control.value;
        this.switch_style(v);
        return false;
    },

    initialize: function () {

        this.control = document.getElementById('styleList');

        this.set_style_from_cookie();

        for (let i = 0; i < this.control.options.length; i++)
            if (this.control.options[i].value == this.last_style)
                this.control.options[i].selected = true;

    },
    /** Alter the current style from the page. */
    switch_style: function (css_title) {
        // You may use this script on your site free of charge provided
        // you do not remove this notice or the URL below. Based on Script from
        // https://www.thesitewizard.com/javascripts/change-style-sheets.shtml
        var i, link_tag;
        var dic = {};

        for (i = 0, link_tag = document.getElementsByTagName("link");
            i < link_tag.length; i++) {
            if ((link_tag[i].rel.indexOf("stylesheet") != -1) &&
                link_tag[i].title) {
                link_tag[i].disabled = true;
                if (link_tag[i].title == css_title) {
                    link_tag[i].disabled = false;
                }
            }

            if (dic[css_title] == undefined) {
                dic[css_title] = css_title;
                this.last_style = css_title;
                this.set_cookie(css_title);
            }
        }
    },
    /** Get the stored value from storage and define it to the page's use. */
    set_style_from_cookie: function () {
        var css_title = this.get_cookie();
        if (css_title) { //&& css_title.length
            this.switch_style(css_title);
        }
    },
    /** Set the value to be stored in localStorage. We don't use cookie anymore. */
    set_cookie: function (cookie_value) {
        if (typeof (Storage) !== "undefined") {
            localStorage.styleName = encodeURIComponent(cookie_value);
        }
    },
    /** Get the stored value from localStorage. No more cookie actually */
    get_cookie() {
        if (typeof (Storage) !== "undefined") {
            // Code for localStorage/sessionStorage.
            if (localStorage.styleName != undefined) {
                return localStorage.styleName;
            }
        } else {
            // Sorry! No Web Storage support...
            // It could be possible mixing two storage types then, if HTML 5 Storage capability is not available, could try using old fashion Cookie way here (?)
            return ""; // default
        }
    }

}