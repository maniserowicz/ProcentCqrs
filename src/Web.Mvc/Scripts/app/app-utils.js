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
};

cqrs.utils = new AppUtils();
cqrs.su = cqrs.utils.string;