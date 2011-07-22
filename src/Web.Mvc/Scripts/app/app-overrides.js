/***************    KNOCKOUT.JS    *****************************/

/* override default knockout.js value binding handler
to update value on 'afterkeypress' by default instead of 'change' only */
var valueBindingHandler = ko.bindingHandlers['value'];
var originalInit = valueBindingHandler.init;

valueBindingHandler.init = function (element, valueAccessor, allBindingsAccessor) {
    var oldValueUpdate = allBindingsAccessor()["valueUpdate"];
    var newValueUpdate = ['afterkeypress'];
    
    if (oldValueUpdate) {
        if (typeof oldValueUpdate == "string") {
            oldValueUpdate = [oldValueUpdate];
        }
        ko.utils.arrayPushAll(newValueUpdate, oldValueUpdate);
    }

    allBindingsAccessor()["valueUpdate"] = newValueUpdate;

    originalInit(element, valueAccessor, allBindingsAccessor);
};

/**************  // KNOCKOUT.JS    *****************************/