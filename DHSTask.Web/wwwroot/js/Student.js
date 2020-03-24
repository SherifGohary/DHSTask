$(document).on('submit', '#addStudentForm', function (e) {
    e.preventDefault();
    $("#CancelAddStudentModal").click();
    if ($(this).valid()) {

        var formData = new FormData(this);
        let studentFname = $("#studenFName").val();
        let studentLname = $("#studentLname").val();

        $("#studenFName").val('');
        $("#studentLname").val('');

        $.ajax({
            cache: false,
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                let tr = `<tr id="${response.id}">
                                <td class="studentFname">${response.fname}</td>
                                <td class="studentLname">${response.lname}</td>
                                <td>
                                    <a href="#editStudentModal" onclick="EditStudentClick(this,'${response.id}')" class="edit" data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Edit">&#xE254;</i></a>
                                    <a href="#deleteStudentModal" onclick="DeleteStudentClick('${response.id}')" class="delete" data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Delete">&#xE872;</i></a>
                                </td>
                          </tr>`;

                $("table").append(tr);
            }
        });
    }
});

$(document).on('submit', '#deleteStudentForm', function (e) {
    e.preventDefault();
    $("#CanceldeleteStudentModal").click();
    if ($(this).valid()) {

        let studentId = $(this).find(".StudentId").val();
        var formData = new FormData();
        formData.append("id", studentId);
        console.log(formData);

        $.ajax({
            cache: false,
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                $(`#${studentId}`).remove();
                console.log(response);
                if ($("table tbody tr").length == 0) {
                    $("table").attr("hidden", "hidden");
                }
            }
        });
    }
});

$(document).on('submit', '#editStudentForm', function (e) {
    e.preventDefault();
    $("#CanceleditStudentModal").click();
    if ($(this).valid()) {

        var formData = new FormData();
        let studentId = $(this).find(".StudentId").val();
        let studentFname = $(this).find(".studentFname").val();
        let studentLname = $(this).find(".studentLname").val();

        formData.append("Id", studentId);
        formData.append("Fname", studentFname);
        formData.append("Lname", studentLname);

        $.ajax({
            cache: false,
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                $(`#${studentId}`).find(".studentFname").text(studentFname);
                $(`#${studentId}`).find(".studentLname").text(studentLname);
            }
        });
    }
});


function DeleteStudentClick(studentId) {
    $("#deleteStudentModal .StudentId").val(studentId);
}

function EditStudentClick(elem, studentId) {
    $("#editStudentModal .StudentId").val(studentId);
    var studentFname = $(elem.parentElement.parentElement).find(".studentFname").text();
    var studentLname = $(elem.parentElement.parentElement).find(".studentLname").text();
    $("#editStudentModal .studentFname").val(studentFname);
    $("#editStudentModal .studentLname").val(studentLname);
}