$(document).ready(() => {

    $('#btnEnviar').click(function () {

        let LoginViewModel = {};
        LoginViewModel.password = $('#Password').val();
        LoginViewModel.UserName = $('#UserName').val();

        $.ajax({
            type: "Post",
            url: "/Account/Login",
            data: { loginview: LoginViewModel },
            dataType: "Json",
            sucess: function (data) {
                console.log(data);
            },
            error: function (xhr, txtStatus, errorThrow) {
                console.log(xhr);
            }
        });
    });
});