// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

load();
function load() {
	$.ajax({
		url: '/api/Contracts/GetContract', //Change this path to your JSON file.
		type: "GET",
		dataType: "json",
		//Remove the "data" attribute, relevant to this example, but isn't necessary in deployment.
		success: function (data, textStatus) {
			drawTable(data);
		}
	});
}

function drawTable(data) {
	var rows = [];

	for (var i = 0; i < data.length; i++) {
		rows.push(drawRow(data[i]));
	}

	$(".table").append(rows);
}

function drawRow(rowData) {
	var options = {
		year: 'numeric',
		month: 'numeric',
		day: 'numeric'
	};


	var row = $("<tr />")
	row.append($("<td scope=\"row\" >" + rowData.id + "</td>"));
	row.append($("<td scope=\"row\" >" + rowData.contractCode + "</td>"));
	row.append($("<td scope=\"row\" >" + rowData.contractName + "</td>"));
	row.append($("<td scope=\"row\" >" + rowData.customer + "</td>"));

	//row.append($("<td scope=\"row\" >" + JSON.stringify(rowData.stages) + "</td>"));
	row.append($("<td scope=\"row\" >" + rowData.contractStages.map(function (st) { return st.nameStage + " [" + new Date(st.startDate).toLocaleString("ru", options) + "/" + new Date(st.stopDate).toLocaleString("ru", options) + "]" }).join("<br>") + "</td>"));

	return row;
	}