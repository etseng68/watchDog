$('document').ready(function () {
	//let action = '<div class="dropdown">'+
	//                '<a class="btn btn-link font-24 p-0 line-height-1 no-arrow dropdown-toggle" href="#" role="button" data-toggle="dropdown">'+
	//                    '<i class="dw dw-more"></i>'+
	//				'</a>'+
	//  					'<div class="dropdown-menu dropdown-menu-right dropdown-menu-icon-list">'+
	//						'<a class="dropdown-item" href="#"><i class="dw dw-eye"></i> 狀態</a>'+
	//						'<a class="dropdown-item" href="#" data-toggle="modal" data-target="#bd-example-modal-lg-edit"><i class="dw dw-edit2"></i> 編輯</a>'+
	//						'<a class="dropdown-item" href="#"><i class="dw dw-delete-3"></i>刪除</a>'+
	//			        '</div>'+
	//			 '</div>';

	$.ajax({
		//url: "http://34.92.11.110/DomainStatus/API.ASPX?check=statuscode",
		url: "datasource/statuscode.csv",
		dataType: "text",
		//charset: "utf-8",
		success: function (data) {
			var lines = data.split("\n");
			//var headers =[{title:'編號'},{title:'檢測網域'},{title:'狀態'},{title:'檢測時間'},{title:'商戶'},{title:'功能'}];
			var headers = [{ title: '檢測網域' }, { title: '狀態' }, { title: '檢測時間' }, { title: '商戶' }];
			var dataArray = [];
			for (var i = 1; i < lines.length - 1; i++) {
				var dataLine = lines[i].split(",");
				var dataObject = [];
				//dataObject.push(i);
				for (var j = 0; j < dataLine.length; j++) {
					dataObject.push(dataLine[j]);
				}
				//dataObject.push(action);
				dataArray.push(dataObject);
			}
			$('.watch-table').DataTable({
				data: dataArray,
				columns: headers,
				scrollCollapse: true,
				autoWidth: false,
				responsive: true,
				columnDefs: [{
					targets: "datatable-nosort",
					orderable: false,
				}],
				"lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
				// "language": {
				// 	"info": "_START_-_END_ of _TOTAL_ entries",
				// 	searchPlaceholder: "Search",
				// 	paginate: {
				// 		next: '<i class="ion-chevron-right"></i>',
				// 		previous: '<i class="ion-chevron-left"></i>'
				// 	}
				// },
				"language": {
					url: "//cdn.datatables.net/plug-ins/1.13.4/i18n/zh-HANT.json"
				},
			});
		},
		error: function (error) {
			console.error(error);
		}
	});



	$('.data-table-export').DataTable({
		scrollCollapse: true,
		autoWidth: false,
		responsive: true,
		columnDefs: [{
			targets: "datatable-nosort",
			orderable: false,
		}],
		"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
		"language": {
			"info": "_START_-_END_ of _TOTAL_ entries",
			searchPlaceholder: "Search",
			paginate: {
				next: '<i class="ion-chevron-right"></i>',
				previous: '<i class="ion-chevron-left"></i>'
			}
		},
		dom: 'Bfrtp',
		buttons: [
			'copy', 'csv', 'pdf', 'print'
		]
	});

	var table = $('.select-row').DataTable();
	$('.select-row tbody').on('click', 'tr', function () {
		if ($(this).hasClass('selected')) {
			$(this).removeClass('selected');
		}
		else {
			table.$('tr.selected').removeClass('selected');
			$(this).addClass('selected');
		}
	});

	var multipletable = $('.multiple-select-row').DataTable();
	$('.multiple-select-row tbody').on('click', 'tr', function () {
		$(this).toggleClass('selected');
	});
	var table = $('.checkbox-datatable').DataTable({
		'scrollCollapse': true,
		'autoWidth': false,
		'responsive': true,
		"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
		//"language": {
		//	"info": "_START_-_END_ of _TOTAL_ entries",
		//	searchPlaceholder: "Search",
		//	paginate: {
		//		next: '<i class="ion-chevron-right"></i>',
		//		previous: '<i class="ion-chevron-left"></i>'
		//	}
		//},
		"language": {
			url: "//cdn.datatables.net/plug-ins/1.13.4/i18n/zh-HANT.json"
		},
		'columnDefs': [{
			'targets': 0,
			'searchable': false,
			'orderable': false,
			'className': 'dt-body-center',
			'render': function (data, type, full, meta) {
				return '<div class="dt-checkbox"><input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '"><span class="dt-checkbox-label"></span></div>';
			}
		}],
		'order': [[1, 'asc']]
	});

	$('#example-select-all').on('click', function () {
		var rows = table.rows({ 'search': 'applied' }).nodes();
		$('input[type="checkbox"]', rows).prop('checked', this.checked);
	});

	$('.checkbox-datatable tbody').on('change', 'input[type="checkbox"]', function () {
		if (!this.checked) {
			var el = $('#example-select-all').get(0);
			if (el && el.checked && ('indeterminate' in el)) {
				el.indeterminate = true;
			}
		}
	})
});