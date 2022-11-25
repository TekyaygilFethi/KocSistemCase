$(function () {

    var $vars = $('#getStudents\\.js').data();

    //$vars.model
    var studentList = $vars.model;

    //filtreleme
    $("#studentTableFilterInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#studentTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $('[data-toggle="tooltip"]').tooltip();

    var exampleModal = document.getElementById('studentViewModal');
    exampleModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget;
        var idx = button.getAttribute('data-bs-idx');
        var modalTitle = exampleModal.querySelector('.modal-title');


        $('#username').val(studentList[idx - 1].username);
        $('#name').val(studentList[idx - 1].fullname);
        $('#birthday').val(studentList[idx - 1].birthDate);
        $('#takenCourseNames').val(studentList[idx - 1].courseNames);
        $('#takenCourseIds').val(studentList[idx - 1].courseIds);

        modalTitle.textContent = studentList[idx - 1].fullname + ' Öğrenci Bilgisi';
    });
});

function deleteStudent(elem) {
    var studentName = elem.getAttribute('data-bs-studentName');

    if (window.confirm(studentName + " isimli kullanıcıyı silmek istediğinize emin misiniz?")) {

        var studentId = elem.getAttribute('data-bs-studentId');
        $.ajax({
            url: '/student/delete-student?studentId=' + studentId,
            type: 'DELETE',
            success: function (data) {
                if (data.isSuccess) {
                    window.location.reload();
                }
                else {
                    alert('Kullanıcı silinirken bir hata oluştu! Hata:'+data.message);
                }
            },
            error: function (data, msg, error) {
                alert('Kullanıcı silinirken bir hata oluştu!');
            }
        });
    }
};
