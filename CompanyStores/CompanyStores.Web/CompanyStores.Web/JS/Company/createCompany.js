$().ready(function () {
	$('#create-company-form').submit(function (e) {
		e.preventDefault();

		var postData = $(this).serialize();

		$.post('/company/create', postData)
			.done(function (response, status, jqxhr) {
				// handle success
				swal({
					title: "Success",
					text: "Company successfully created!",
					type: "success"
				}, function () {
					$(location).attr('href', '/company/list');
				});
			})
			.fail(function (jqxhr, status, error) {
				// handle error
				swal('error!', 'something went wrong when saving the edit.', 'error');
			});
	})
});