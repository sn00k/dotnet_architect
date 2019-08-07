$().ready(function () {
	$('#delete-company-form').submit(function (e) {
		e.preventDefault();

		swal({
			title: 'Are you sure?',
			text: 'You will not be able to recover this company!',
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#DD6B55',
			confirmButtonText: 'Yes, delete it!',
			closeOnConfirm: false
		}, function () {
			var postData = $('#delete-company-form').serialize();

			$.post('/company/delete', postData)
				.done(function (response, status, jqxhr) {
					// handle success
					swal({
						title: 'Success',
						text: 'Company successfully deleted!',
						type: 'success'
					}, function () {
						$(location).attr('href', '/company/list');
					});
				})
				.fail(function (jqxhr, status, error) {
					// handle error
					swal('error!', 'something went wrong when deleting the company.', 'error');
				});
		});
	})
});