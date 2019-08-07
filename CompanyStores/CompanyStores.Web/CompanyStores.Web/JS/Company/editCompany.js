$(document).ready(function () {
	$(document).on('click', '#save-company-store', function (e) {
		e.preventDefault();

		var companyJson = $('#edit-form').serializeJSON(); // .serializeObject();

		var storeJson = $('#create-store-partial').find('#create-store-form').serializeJSON();

		$.ajax({
			url: '/company/edit',
			type: 'post',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({ companyJson, storeJson }),
			success: function (result) {
				if (result.success) {
					swal({
						title: "Success",
						text: "Company & Store successfully saved!",
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

	$('#save-company').click(function (e) {
		e.preventDefault();

		var companyJson = $('#edit-form').serializeJSON();

		$.ajax({
			url: '/company/edit',
			type: 'post',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({ companyJson }),
			success: function (result) {
				if (result.success) {
					swal({
						title: "Success",
						text: "Company successfully saved!",
						type: "success"
					}, function () {
						$(location).attr('href', '/company/list');
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

	$('#create-store').click(function () {
		$(this).hide();
		var companyId = 'id=' + $('#create-store').data('company-id');

		$.post('/store/create', companyId)
			.done(function (data) {
				$('#create-store-partial').html(data);
			})
			.fail(function () {

			});
	});
});