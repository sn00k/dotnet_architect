$().ready(function () {

	function initialize(e, longitude, latitude) {
		if (longitude == null || latitude == null) {
			longitude = $('#edit-store-form').find('#Longitude').val();
			latitude = $('#edit-store-form').find('#Latitude').val();

			if (!longitude || !latitude) {
				return;
			}
		}

		var map = new google.maps.Map(
		  document.getElementById('map'), {
		  	center: new google.maps.LatLng(longitude, latitude),
		  	zoom: 13,
		  	mapTypeId: google.maps.MapTypeId.ROADMAP
		  });

		var marker = new google.maps.Marker({
			position: new google.maps.LatLng(longitude, latitude),
			map: map
		});

	}
	google.maps.event.addDomListener(window, 'load', initialize);

	// apiKey should be stored in an environmental variable.
	var apiKey = 'apiKey';
	var baseUrl = 'https://maps.googleapis.com/maps/api/geocode/json?';
	var urlParameters = '';

	var getUrl = '';
	$('#fetch-map-coordinates').click(function (e) {
		e.preventDefault();

		var address = $('#edit-store-form').find('#Address').val();
		var city = $('#edit-store-form').find('#City').val();
		var zip = $('#edit-store-form').find('#Zip').val();
		var country = $('#edit-store-form').find('#Country').val();

		// rudimentary (and a bit ugly) validation.
		// a better idea is to have spans or other elements styled with css shown, 
		// when the input either lost focus or when the form is submitted.
		var validationFields =
			[
				address,
				city,
				zip,
				country
			];

		for (var i = 0; i < validationFields.length; i++) {
			if (typeof (validationFields[i]) !== 'string' || !validationFields[i]) {
				swal('validation failed!', 'data type on fields should be of string. all fields are required.', 'error');
				return;
			}
		}

		var storeId = $('#edit-store-form').find('#store-id').val();

		//$.ajax({
		//	type: 'get',
		//	url: '/api/store?id=' + storeId,
		//	async: false,
		//	success: function (response) {
		//		var storeObject = $.parseJSON(response);

		//		// could be looped instead
		//		urlParameters = 'address='
		//			+ storeObject.Address
		//			+ ','
		//			+ storeObject.Zip
		//			+ ','
		//			+ storeObject.City
		//			+ ','
		//			+ storeObject.Country
		//			+ '&key='
		//			+ apiKey;

		//		urlParameters = urlParameters.split(' ').join('+');

		//		getUrl = baseUrl + urlParameters;
		//	},
		//	error: function (err) {
		//		// error
		//		console.log(err);
		//	}
		//});
		urlParameters = 'address='
			+ address
			+ ','
			+ city
			+ ','
			+ zip
			+ ','
			+ country
			+ '&key='
			+ apiKey;

		urlParameters = urlParameters.split(' ').join('+');

		getUrl = baseUrl + urlParameters;


		getCoordinates(getUrl);

	});

	function getCoordinates(getUrl) {
		$.get(getUrl)
			.done(function (response, status, jqxhr) {
				if (response.status == 'OK') {
					var longitude = response.results[0].geometry.location.lat.toString();
					var latitude = response.results[0].geometry.location.lng.toString();

					if (longitude.length > 15) {
						longitude = longitude.substring(0, 10);
					}

					if (latitude.length > 15) {
						latitude = latitude.substring(0, 10);
					}

					$('#edit-store-form').find('#Longitude').val(longitude);
					$('#edit-store-form').find('#Latitude').val(latitude);

					// update map with coordinates
					initialize(null, longitude, latitude);

					var formData = $('#edit-store-form').serialize();
					$.post('/store/edit', formData)
						.done(function (response, status, jqxhr) {
							swal({
								title: "Success",
								text: "Longitude & Latitude successfully saved to Store!",
								type: "success"
							}, function () {
								$(location).attr('href', '/store/list');
							});
						})
						.fail(function (error, status, jqxhr) {
							swal('error!', 'something went wrong when saving the edit.', 'error');
						});
				} if (response.status === 'ZERO_RESULTS') {
					swal('error!', 'No results found for that address.', 'error');
				}
			})
			.fail(function (status, error, jqxhr) {
				swal('error!', 'something went wrong when saving the edit.', 'error');
			});
	}
});