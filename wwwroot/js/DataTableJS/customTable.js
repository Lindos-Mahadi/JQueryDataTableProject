$(document).ready(function () {

    GetContact();

});

function GetContact() {
    $.ajax({
        url: '/Demo/GetContactList',
        type: 'Get',
        dataType: 'json',
        success: OnSuccess
    });
}

function OnSuccess(response) {
    console.log(response);
    $("#dataTableList").DataTable({
        bProcessing: true,
        bLengthChange: true,
        bLengthMenu: [[5, 10, 25, -1][5, 10, 25, "All"]],
        bFilter: true,
        bSort: true,
        bPaginate: true,
        data: response,
        columns: [
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'Name',
                render: function (data, type, row, meta) {
                    return row.name
                }
            },
            {
                data: 'Email',
                render: function (data, type, row, meta) {
                    return row.email
                }
            },
            {
                data: 'Status',
                render: function (data, type, row, meta) {
                    return row.status
                }
            },
            {
                data: 'AddedDate',
                render: function (data, type, row, meta) {
                    var date = new Date(row.addedDate);

                    // Format the date as "dd/MM/yyyy"
                    var formattedDate = date.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });

                    return formattedDate;
                }
            }
        ]
    });

}