$().ready(function () {
	$('#edit-store-form').submit(function (e) {
		e.preventDefault();

		var storeForm = $(this).serialize();

		$.ajax({
			url: '/store/edit',
			type: 'post',
			data: storeForm,
			success: function (result) {
				if (result.success) {
					swal({
						title: "Success",
						text: "Store successfully saved!",
						type: "success"
					}, function () {
						$(location).attr('href', '/store/list');
					});
				} else {
					swal('error!', 'something went wrong when saving the edit.', 'error');
				}
			},
			error: function (xhr, ajaxOptions, error) {
				swal('error!', 'something went wrong when saving the edit.', 'error');
			}
		});
	});
});