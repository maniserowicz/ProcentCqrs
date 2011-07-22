var cqrs = this.cqrs || {};

function AppUtils() { }

AppUtils.prototype = {
    string: {
        format: function (template) {
            for (i = 1; i < arguments.length; i++) {
                template = template.replace('{' + (i - 1) + '}', arguments[i]);
            }
            return template;
        }
    }
    , validation: {
        parse: function (form) {
            // first: remove existing validation for this form
            // reference: http://blog.gutek.pl/post/2011/05/19/Edycja-zagniezdzonych-list-w-MVC-3-(Czesc-3).aspx
            $.removeData(form, 'validator');
            // then: recreate validation for this form
            // reference: http://www.maciejaniserowicz.com/post/2011/03/23/Odswiezenie-walidacji-client-side-w-MVC3.aspx
            $.validator.unobtrusive.parse(form);
        }
    }
};

cqrs.utils = new AppUtils();
cqrs.su = cqrs.utils.string;
cqrs.vu = cqrs.utils.validation;