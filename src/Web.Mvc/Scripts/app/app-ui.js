var ui = {
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
        $('.modal-dialog', ctx).dialog(ui.defaults.modalDialog);
    }
};

$(function () {
    ui.init($(document));
})