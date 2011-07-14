var utils = {
    string: {
        format: function(template) {
            for (i = 1; i < arguments.length; i++) {
                template = template.replace('{' + (i - 1) + '}', arguments[i]);
            }
            return template;
        }
    }
};

var su = utils.string;