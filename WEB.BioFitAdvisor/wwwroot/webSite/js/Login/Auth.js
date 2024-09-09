const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});

$(function () {
    //var valor = $("#isChangeCompany").text();
    //var UserData = getUserData();

    //if (UserData != null && valor == 'false' && UserData.keepSession) {
    //    $.ajax({
    //        url: '/Login/ReLogin',
    //        type: 'POST',
    //        contentType: 'application/json',
    //        data: JSON.stringify(UserData),
    //        success: function (response) {
    //            if (response.success) {
    //                setUserData(response.userData);
    //                window.location.href = '/Home/Index';
    //            }
    //        },
    //        error: function (xhr, status, error) {
    //            console.error(error);
    //        }
    //    });
    //}

    $('#btnUserLogIn').on("click", function () {

        var email = $("#txtUserEmail").val();
        var password = $("#txtUserPass").val();

        if (email == "") {
            Alert("", "Ingrese su correo electronico", "info", 3000);
            return;
        }

        if (password == "") {
            Alert("", "Ingrese su contraseña", "info", 3000);
            return;
        }

        var datos = {
            email: email,
            password: password
        };

        $.ajax({
            url: '/Login/AutenticateUser',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(datos),
            success: function (response) {
                if (response.success) {
                    window.location.href = '/Home/Index';
                }
                else {
                    Alert("", "El usuario o la contraseña no son validos", "warning", 3000);
                }
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });

    $('#btnSelectCompany').on("click", function () {
        var valCboEmp = parseInt($("#cboCompaniesUser").val());
        
        if (isNaN(valCboEmp) || valCboEmp === null || valCboEmp === 0) {
            Alert("", "Debe seleccionar un gimanacio para continuar", "warning", 3000);
            return;
        }
        else {
            SelectCompany(false)
        }
    });

    $('#btnMantenerSi').on("click", function () {
        SelectCompany(true)
    });

    $('#btnMantenerNo').on("click", function () {
        SelectCompany(false)
    });
});

function SetUserDataSesion(UserData) {
    $.ajax({
        url: '/Login/setUserData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(UserData),
        success: function (response) {
            if (response.success) {
                setUserData(response.userData);
                window.location.href = '/Home/Index';
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

function SelectCompany(keepSession) {
    var valCboEmp = $("#cboCompaniesUser").val();

    var datos = { CompanyId: valCboEmp, keepSession: keepSession }

    $.ajax({
        url: '/Login/SelectCompany',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(datos),
        success: function (response) {

            if (response.success) {
                setUserData(response.userData);
                $.ajax({
                    url: '/Login/SetUserData',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(response.userData),
                    success: function (response) {
                    },
                    error: function (xhr, status, error) {
                        console.error(error);
                    }
                });

                window.location.href = '/Home/Index';

            } else if (response.error !== null || typeof response.error !== undefined || response.error !== "") {
                Alert("Cuidado!", response.error, "warning", 6000)
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}