﻿//https://codewithmukesh.com/blog/jquery-datatable-in-aspnet-core/
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
        processing: true,
        lengthChange: true,
        lengthMenu: [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
        filter: true,
        sort: true,
        paginate: true,
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
    $('#Id').css('display', 'none');
});

// SUBMIT CONTACT BUTTON
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
        success: function (objData) {
            alert('Data is saved successfully...');

            // Create modal function for modal hide
            //$('#myModal').modal('hide');
            ModalHidePopUp();
            // Clear text box modal
            ClearTextBox();
            GetContact();
        },
        error: function () {
            alert('Data cant Saved');
        }
    });

    
}
// MODAL CLOSE AND HIDE
function ModalHidePopUp() {
    $('#myModal').modal('hide');
}
// CLEAR TEXT BOX
function ClearTextBox() {
    //Id: $('#Id').val(),
    $('#Name').val(''),
        $('#Email').val(''),
        $('#Subject').val(''),
        $('#Message').val(''),
        $('#AddedDate').val(''),
        $('#Status').prop('')
}
// DELETE ITEM FUNCTINALITY
function deleteRow(id) {
    //debugger;
    if (confirm('Are you sure, you want to delete thise record.?')) {
        $.ajax({
            url: "/Demo/DeleteContact?id=" + id,
            success: function () {
            alert("Data deleted successfully from record.!", id);
            //GetContact(); // Call GetContact after editing
            },
            error: function () {
                alert("Data cant be deleted.?");
            }
        });
    }
}
// EDIT OR UPDATE GET FUNCTIONALY
function editRow(id) {
    debugger;
    $.ajax({
        url: '/Demo/EditContact?id=' + id,
        type: 'Get',
        //data: JSON.stringify(objData),
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            //debugger;
            console.log("response",response);
            $('#myModal').modal('show');
            $('#Id').val(response.id);
            $('#Name').val(response.name);
            $('#Email').val(response.email);
            $('#Subject').val(response.subject);
            $('#Message').val(response.message);
            $('#AddedDate').val(response.addedDate);
            // Set the value for the 'Status' checkbox
            $('#Status').prop('checked', response.status);

            // BUTTON HIDE OR DISPLAY USING CSS
            $('#btnSubmit').css('display','none');
            $('#btnUpdate').css('display', 'block');

            // BUTTON HIDE OR DISPLAY USING CSS
            //$('#btnSubmit').hide();
            //$('#btnUpdate').show();
        },
        error: function () {
            alert("Dta not found");
        }
    });
}
function updateContact() {
    debugger;
    var objData = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        Email: $('#Email').val(),
        Subject: $('#Subject').val(),
        Message: $('#Message').val(),
        AddedDate: $('#AddedDate').val(),
        Status: $('#Status').prop('checked')
    }
    $.ajax({
        url: '/Demo/Update',
        type: 'POST',
        data: objData,
        //contentType: 'application/json',
        //dataType: 'json',
        success: function () {
            //debugger;
            alert('Data is Updated successfully...');
            ModalHidePopUp();
            ClearTextBox();
        },
        error: function () {
            alert('Data cant Updated');
        }
    });
}