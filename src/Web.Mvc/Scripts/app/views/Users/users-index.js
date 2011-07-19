var cqrs = this.cqrs || {};

var v = {
    onRenamed: function () {
    }
};

$(function () {
    var dlgRenameUser = $('#dlgRenameUser');
    var dlgRenameUserTitleTemplate = dlgRenameUser.data('title-template');

    $('[data-command=rename-user]').click(function (e) {
        e.preventDefault();

        var $row = $(this).closest('tr');

        var userId = $row.data('id');
        var firstName = $('[data-role=first-name]', $row);
        var lastName = $('[data-role=last-name]', $row);

        var newFirstName = $('#FirstName', dlgRenameUser);
        var newLastName = $('#LastName', dlgRenameUser);

        $('#UserId', dlgRenameUser).val(userId);
        newFirstName.val(firstName.text());
        newLastName.val(lastName.text());

        v.onRenamed = function () {
            dlgRenameUser.dialog('close');

            firstName.text(newFirstName.val());
            lastName.text(newLastName.val());
        };

        var newTitle = cqrs.su.format(dlgRenameUserTitleTemplate, firstName.text(), lastName.text());
        dlgRenameUser.dialog('option', { title: newTitle });
        dlgRenameUser.dialog('open');
    });
})