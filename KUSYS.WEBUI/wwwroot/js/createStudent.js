$(function () {
    $('#btnCreateStudent').click(function () {
        var username = $('#username').val();
        var firstname = $('#firstname').val();
        var lastname = $('#lastname').val();
        var birthdate = $('#birthdate').val();
        var password = $('#password').val();
        var role = $('#role').val();

        $.ajax({
            url: '/student/create-student',
            method: 'POST',
            data: { Username: username, Firstname: firstname, Lastname: lastname, BirthDate: birthdate, Password: password, Role: role },
            success: function (data) {
                if (data.isSuccess) {
                    $('#alertArea').remove();
                    $('#fields').prepend('<div class="col-sm-12" id="alertArea"><div class= "alert alert-success" role = "alert" id="alert">Kullanıcı başarıyla eklendi!</div></div>');
                    $('#username').val('');
                    $('#firstname').val('');
                    $('#lastname').val('');
                    $('#birthdate').val('');
                    $('#password').val('');

                }
                else {
                    //eğer responseObject.issuccess false ise hatayı kullanıcıya bastır!
                    $('#alertArea').remove();
                    $('#fields').prepend('<div class="col-sm-12" id="alertArea"><div class= "alert alert-danger" role = "alert" id="alert">' + data.message + '</div></div>');
                }
            },
            error: function (request, status, error) {
                //eğer badrequest dönmüşse false ise hatayı kullanıcıya bastır!
                $('#alertArea').remove();
                $('#fields').prepend('<div class="col-sm-12" id="alertArea"><div class= "alert alert-danger" role = "alert" id="alert">' + request.responseText + '</div></div>');
            }
        })
    });

});

function togglePassword() {
    var pwdComponent = $('#password');
    var pwdIcon = $('#pwdIcon');

    if (pwdComponent.attr("type") === "text") {
        pwdComponent.attr("type", "password");
        pwdIcon.removeClass('fa-eye-slash');
        pwdIcon.addClass('fa-eye');
    } else {
        pwdComponent.attr("type", "text");
        pwdIcon.removeClass('fa-eye');
        pwdIcon.addClass('fa-eye-slash');
    }

}
