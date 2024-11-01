$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);

        $.ajax({
            url: `/Games/Delete/${btn.data('id')}`,
            method: 'delete',
            success: function () {
                alert("Has been Dleted")
            },
            error: function () {
                alert("Can't Deleted")
            }
        })
        console.log(btn.data('id'));
    });
})