$(function () {
    var dlgRenameUser = $('#dlgRenameUser');
    var dlgRenameUserTitleTemplate = dlgRenameUser.data('title-template');

    $('[data-command=rename-user]').click(function (e) {
        e.preventDefault();

        var $row = $(this).closest('tr');

        var userId = $row.data('id');
        var firstName = $('[data-role=first-name]', $row).text();
        var lastName = $('[data-role=last-name]', $row).text();

        $('#UserId', dlgRenameUser).val(userId);
        $('#FirstName', dlgRenameUser).val(firstName);
        $('#LastName', dlgRenameUser).val(lastName);

        dlgRenameUser.dialog('option', { title: su.format(dlgRenameUserTitleTemplate, firstName, lastName) });
        dlgRenameUser.dialog('open');
    });
})