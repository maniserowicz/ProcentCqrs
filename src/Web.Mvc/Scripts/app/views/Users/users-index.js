var cqrs = this.cqrs || {};

cqrs.v = {
    onRenamed: function () {
    }
};

$(function () {
    var dlgRenameUser = $('#dlgRenameUser');

    function User(data) {
        this.Id = data.Id;
        this.Email = ko.observable(data.Email);
        this.FirstName = ko.observable(data.FirstName);
        this.LastName = ko.observable(data.LastName);

        this.fullName = function () {
            return cqrs.su.format("{0} {1}", this.FirstName(), this.LastName());
        };
    };

    var viewModel = {
        userIdBeingRenamed: ko.observable()
        , users: ko.observableArray()
    };

    viewModel.requestRename = function (id) {
        this.userIdBeingRenamed(id);
        var self = this;

        cqrs.v.onRenamed = function () {
          dlgRenameUser.dialog('close');

            var renaming = self.renaming();
            var user = self.findUser(renaming.Id);
            user.FirstName(renaming.FirstName());
            user.LastName(renaming.LastName());

          self.userIdBeingRenamed(null);
        };

        dlgRenameUser.dialog('open');
    };

    viewModel.findUser = function (id) {
        return ko.utils.arrayFirst(this.users(), function (item) {
            return item.Id == id;
        });
    };

    viewModel.renaming = ko.dependentObservable(function () {
        var id = this.userIdBeingRenamed();

        if (!this.currentRename || this.currentRename.Id || this.currentRename.Id != id) {
            var user = this.findUser(id);
            this.currentRename = new User(ko.toJS(user) || {});
        }

        return this.currentRename;
    }, viewModel);

    viewModel.renaming.subscribe(function (newValue) {
        dlgRenameUser.dialog('option', 'title', cqrs.su.format('Rename ' + (newValue || {}).fullName()));
    });

    window.viewModel = viewModel;

    for (var i = 0, l = cqrs.allUsers.length; i < l; i++) {
        viewModel.users.push(new User(cqrs.allUsers[i], viewModel));
    }

    ko.applyBindings(viewModel);

    // need to re-parse form client-side validation because it's in template
    cqrs.vu.parse($('form', dlgRenameUser)[0]);
});