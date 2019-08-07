$().ready(function () {
	$('#delete-store-form').submit(function (e) {
		e.preventDefault();

		swal({
			title: 'Are you sure?',
			text: 'You will not be able to recover this store!',
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#DD6B55',
			confirmButtonText: 'Yes, delete it!',
			closeOnConfirm: false
		}, function () {
			var postData = $('#delete-store-form').serialize();

			$.post('/store/delete', postData)
				.done(function (response, status, jqxhr) {
					// handle success
					swal({
						title: 'Success',
						text: 'Store successfully deleted!',
						type: 'success'
					}, function () {
						$(location).attr('href', '/store/list');
					});
				})
				.fail(function (jqxhr, status, error) {
					// handle error
					swal('error!', 'something went wrong when deleting the store.', 'error');
				});
		});
	})
