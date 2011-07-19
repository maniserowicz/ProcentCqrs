var cqrs = this.cqrs || {};

function AppAjax() { }

AppAjax.prototype = {
    onError: function () {
        alert('error occured');
    }
};

cqrs.ajax = new AppAjax();