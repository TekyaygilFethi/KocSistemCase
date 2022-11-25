$(function () {

    var $vars = $('#updateStudent\\.js').data();

    fillCourses($vars.model);


    $('#btnEditStudent').click(function () {
        var firstname = $('#firstname').val();
        var lastname = $('#lastname').val();
        var birthdate = $('#birthdate').val();
        var courses = $('#courses').chosen().val();
        var id = $('#id').val();

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: '/student/update-student',
            type: 'PUT',
            data: JSON.stringify({ Id: id, Firstname: firstname, Lastname: lastname, BirthDate: birthdate, CourseIds: courses }),
            success: function (data) {
                if (data.isSuccess) {
                    $('#alertArea').remove();
                    $('#fields').prepend('<div class="col-sm-12" id="alertArea"><div class="alert alert-success" role="alert" id="alert">Kullanıcı başarıyla düzenlendi!</div></div>');
                }
                else {
                    $('#alertArea').remove();
                    $('#fields').prepend('<div class="col-sm-12" id="alertArea"><div class="alert alert-danger" role="alert" id="alert">' + data.message + '</div></div>');
                }
            },
            error: function (request, status, error) {
                $('#alertArea').remove();
                $('#fields').prepend('<div class="col-sm-12" id="alertArea"><div class="alert alert-danger" role="alert" id="alert">' + request.responseText + '</div></div>');
            }
        })
    });
});

function fillCourses(model) {
    $.ajax({
        url: '/course/get-all-courses-for-edit',
        type: 'GET',
        success: function (data) {
            var currentCourseIds = model;
            $('#courses').empty();
            $.each(data, function (idx, val) {
                if ($.inArray(val.courseId, currentCourseIds) !== -1) {
                    $('#courses').append('<option value="' + val.courseId + '" selected>' + val.courseName + ' (' + val.courseId + ')</option>');
                } else {
                    $('#courses').append('<option value="' + val.courseId + '">' + val.courseName + ' (' + val.courseId + ')</option>');
                }
            });
            $("#courses").trigger("chosen:updated");
            $('#courses').chosen({ width: "95%" });
        }
    })
}