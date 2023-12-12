$(document).ready(function () {

    GetContact();
    //AddNewItem();
    //ContactCreate();

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
        lengthMenu: [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
        bFilter: true,
        bSort: true,
        bPaginate: true,
        data: response,
        columns: [
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return row.id;
                }
            },
            {
                data: 'Name',
                render: function (data, type, row, meta) {
                    return row.name;
                }
            },
            {
                data: 'Email',
                render: function (data, type, row, meta) {
                    return row.email;
                }
            },
            {
                data: 'Status',
                render: function (data, type, row, meta) {
                    return row.status;
                }
            },
            {
                data: 'AddedDate',
                render: function (data, type, row, meta) {
                    var date = new Date(row.addedDate);
                    var formattedDate = date.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
                    return formattedDate;
                }
            },
            // Operations column with Delete and Edit icons
            {
                data: null,
                render: function (data, type, row, meta) {
                    return '<i class="fa fa-trash text-danger" style="cursor: pointer;" onclick="deleteRow(' + row.id + ')" title="Delete"></i>' +
                        '&nbsp;<i class="fa fa-edit text-primary" style="cursor: pointer;" onclick="editRow(' + row.id + ')" title="Edit"></i>';
                }
            }
        ]
    });
}

$("#addNewItem").click(function () {
    $('#myModal').modal('show');
});

function submitContact() {debugger
    var objData = {
        //Id: $('#Id').val(),
        Name: $('#Name').val(),
        Email: $('#Email').val(),
        Subject: $('#Subject').val(),
        Message: $('#Message').val(),
        AddedDate: $('#AddedDate').val(),
        Status: $('#Status').prop('checked')
    }

    $.ajax({
        url: '/Demo/CreateContact',
        type: 'POST',
        data: JSON.stringify(objData),
        contentType: 'application/json',
        dataType: 'json',
        success: function () {
            alert('Data is Saved');
        },
        error: function () {
            alert('Data cant Saved');
        }
    });
}

