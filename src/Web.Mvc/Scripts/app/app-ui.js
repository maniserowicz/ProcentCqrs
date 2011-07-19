var cqrs = this.cqrs || {};

function AppUI() { }

AppUI.prototype = {
    defaults: {
        modalDialog: {
            autoOpen: false,
            resizable: false,
            draggable: false,
            modal: true,
            width: 455
        }
    }
    , init: function (ctx) {
        $('.modal-dialog', ctx).dialog(this.defaults.modalDialog);
    }
};

cqrs.ui = new AppUI();

$(function () {
    cqrs.ui.init($(document));
});