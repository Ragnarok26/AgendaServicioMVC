$(document).ready(
    function () {
        $("#btnBuscar").click(
            function () {
                Buscar($("#txtBuscar").val());
            }
        );
        Buscar(null);
    }
);

function Buscar(nombre) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Cargando...</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    $.ajax(
        {
            type: 'POST',
            url: url_Listado,
            dataType: 'html',
            data: { "name": nombre },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    $("#divLoad").html(data);
                    $("#btnAgregar").click(
                        function () {
                            CargarDatosUsuario(null);
                        }
                    );
                    $("#btnEliminarSeleccionados").click(
                        function () {
                            UserDelete(null);
                        }
                    );
                    $("#usrChkAll").change(
                        function () {
                            var checked = $("#usrChkAll").prop("checked");
                            $(".usrChk:checkbox").each(
                                function () {
                                    $(this).prop("checked", checked);
                                }
                            );
                        }
                    );
                    $(".btnEditModal").click(
                        function (event) {
                            CargarDatosUsuario(event.currentTarget.value);
                        }
                    );
                    $(".btnDelete").click(
                        function (event) {
                            UserDelete(event.currentTarget.value);
                        }
                    );
                    CerrarModal();
                } else {
                    CerrarModal();
                    $.toast(
                        {
                            text: "Se ha producido un error al intentar cargar la información.", // Text that is to be shown in the toast
                            heading: "Error", // Optional heading to be shown on the toast
                            icon: 'error', // Type of toast icon
                            showHideTransition: 'fade', // fade, slide or plain
                            allowToastClose: true, // Boolean value true or false
                            hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                            stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                            position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                            textAlign: 'center',  // Text alignment i.e. left, right or center
                            loader: false // Whether to show loader or not. True by default
                        }
                    );
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                CerrarModal();
                $.toast(
                    {
                        text: errorThrown + "\n(" + JSON.stringify(jqXHR, null, 4) + ")", // Text that is to be shown in the toast
                        heading: textStatus, // Optional heading to be shown on the toast
                        icon: 'error', // Type of toast icon
                        showHideTransition: 'fade', // fade, slide or plain
                        allowToastClose: true, // Boolean value true or false
                        hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                        position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                        textAlign: 'center',  // Text alignment i.e. left, right or center
                        loader: true // Whether to show loader or not. True by default
                    }
                );
            }
        }
    );
}

function CargarDatosUsuario(targetId) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Cargando...</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var Entity = null;
    if (targetId != null) {
        Entity = { "Id": targetId };
    }
    $.ajax(
        {
            type: 'POST',
            url: url_ContEdicion,
            dataType: 'html',
            data: Entity,
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    $("#usuarioAddEdit").html(data);
                    CargarEventosEdicion();
                    CerrarModal();
                    $("#UsuarioAgregarEditarModal").modal("show");
                } else {
                    CerrarModal();
                    $.toast(
                        {
                            text: "Se ha producido un error al intentar cargar la información.", // Text that is to be shown in the toast
                            heading: "Error", // Optional heading to be shown on the toast
                            icon: 'error', // Type of toast icon
                            showHideTransition: 'fade', // fade, slide or plain
                            allowToastClose: true, // Boolean value true or false
                            hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                            stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                            position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                            textAlign: 'center',  // Text alignment i.e. left, right or center
                            loader: false // Whether to show loader or not. True by default
                        }
                    );
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                CerrarModal();
                $.toast(
                    {
                        text: errorThrown + "\n(" + JSON.stringify(jqXHR, null, 4) + ")", // Text that is to be shown in the toast
                        heading: textStatus, // Optional heading to be shown on the toast
                        icon: 'error', // Type of toast icon
                        showHideTransition: 'fade', // fade, slide or plain
                        allowToastClose: true, // Boolean value true or false
                        hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                        position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                        textAlign: 'center',  // Text alignment i.e. left, right or center
                        loader: true // Whether to show loader or not. True by default
                    }
                );
            }
        }
    );
}

function CargarEventosEdicion() {
    $("#btnUserSave").click(
        function (event) {
            UserSave(event.currentTarget.value);
        }
    );
}

function UserSave(value) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var Entity = {
        "Id": value,
        "Name": $("#txtName").val(),
        "FirstName": $("#txtFirstName").val(),
        "LastName": $("#txtLastName").val(),
        "UserName": $("#txtUserName").val(),
        "Password": $("#txtPassword").val(),
        "IdCompany": $("#ddlCompany").val(),
        "IdRol": $("#ddlRol").val()
    };
    var company = 0;
    var rol = 0;
    try {
        company = parseInt(Entity.IdCompany);
    } catch (ex) {
        company = -1;
    }
    try {
        rol = parseInt(Entity.IdRol);
    } catch (ex) {
        rol = -1;
    }
    if (company > 0 && rol > 0 && Entity.Password.length > 0) {
        $.ajax(
            {
                type: 'POST',
                url: url_Edicion,
                dataType: 'json',
                contentType: "application/json",
                data: "{ usuario: " + JSON.stringify(Entity) + " }",
                cache: false,
                success: function (result, textStatus, jqXHR) {
                    CerrarModal();
                    if (result != null) {
                        if (result.HasError === false) {
                            if (result.RowsAffected > 0) {
                                $.toast(
                                    {
                                        text: "Se ha realizado la operación de forma exitosa.", // Text that is to be shown in the toast
                                        heading: "Exitoso", // Optional heading to be shown on the toast
                                        icon: 'success', // Type of toast icon
                                        showHideTransition: 'fade', // fade, slide or plain
                                        allowToastClose: true, // Boolean value true or false
                                        hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                        position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                        textAlign: 'center',  // Text alignment i.e. left, right or center
                                        loader: false // Whether to show loader or not. True by default
                                    }
                                );
                            } else {
                                $.toast(
                                    {
                                        text: "Se ha producido un error al intentar realizar la operación (" + result.Message + ").", // Text that is to be shown in the toast
                                        heading: "Error", // Optional heading to be shown on the toast
                                        icon: 'error', // Type of toast icon
                                        showHideTransition: 'fade', // fade, slide or plain
                                        allowToastClose: true, // Boolean value true or false
                                        hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                        position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                        textAlign: 'center',  // Text alignment i.e. left, right or center
                                        loader: false // Whether to show loader or not. True by default
                                    }
                                );
                            }
                        } else {
                            $.toast(
                                {
                                    text: "Se ha producido un error al intentar realizar la operación.", // Text that is to be shown in the toast
                                    heading: "Error", // Optional heading to be shown on the toast
                                    icon: 'error', // Type of toast icon
                                    showHideTransition: 'fade', // fade, slide or plain
                                    allowToastClose: true, // Boolean value true or false
                                    hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                    stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                    position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                    textAlign: 'center',  // Text alignment i.e. left, right or center
                                    loader: false // Whether to show loader or not. True by default
                                }
                            );
                        }
                    } else {
                        $.toast(
                            {
                                text: "Se ha producido un error al intentar procesar la información.", // Text that is to be shown in the toast
                                heading: "Error", // Optional heading to be shown on the toast
                                icon: 'error', // Type of toast icon
                                showHideTransition: 'fade', // fade, slide or plain
                                allowToastClose: true, // Boolean value true or false
                                hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                textAlign: 'center',  // Text alignment i.e. left, right or center
                                loader: false // Whether to show loader or not. True by default
                            }
                        );
                    }
                    CerrarModal();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    CerrarModal();
                    $.toast(
                        {
                            text: errorThrown + "\n(" + JSON.stringify(jqXHR, null, 4) + ")", // Text that is to be shown in the toast
                            heading: textStatus, // Optional heading to be shown on the toast
                            icon: 'error', // Type of toast icon
                            showHideTransition: 'fade', // fade, slide or plain
                            allowToastClose: true, // Boolean value true or false
                            hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                            stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                            position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                            textAlign: 'center',  // Text alignment i.e. left, right or center
                            loader: true // Whether to show loader or not. True by default
                        }
                    );
                }
            }
        );
    } else {
        CerrarModal();
        $.toast(
            {
                text: "Favor de proporcionar una Compañía y un Rol válidos.", // Text that is to be shown in the toast
                heading: "Advertencia", // Optional heading to be shown on the toast
                icon: 'warn', // Type of toast icon
                showHideTransition: 'fade', // fade, slide or plain
                allowToastClose: true, // Boolean value true or false
                hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                textAlign: 'center',  // Text alignment i.e. left, right or center
                loader: false // Whether to show loader or not. True by default
            }
        );
    }
}

function UserDelete(value) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var Entities = [];
    if (value == null) {
        $(".usrChk:checkbox:checked").each(
            function () {
                Entities.push({ "Id": $(this).val() });
            }
        );
    } else {
        Entities = [{ "Id": value }];
    }
    if (Entities.length > 0) {
        $.ajax(
            {
                type: 'POST',
                url: url_Eliminacion,
                dataType: 'json',
                contentType: "application/json",
                data: "{ usuarios: " + JSON.stringify(Entities) + " }",
                cache: false,
                success: function (result, textStatus, jqXHR) {
                    CerrarModal();
                    if (result != null) {
                        if (result.HasError === false) {
                            if (result.RowsAffected > 0) {
                                $.toast(
                                    {
                                        text: "Se ha realizado la operación de forma exitosa.", // Text that is to be shown in the toast
                                        heading: "Exitoso", // Optional heading to be shown on the toast
                                        icon: 'success', // Type of toast icon
                                        showHideTransition: 'fade', // fade, slide or plain
                                        allowToastClose: true, // Boolean value true or false
                                        hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                        position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                        textAlign: 'center',  // Text alignment i.e. left, right or center
                                        loader: false // Whether to show loader or not. True by default
                                    }
                                );
                            } else {
                                $.toast(
                                    {
                                        text: "Se ha producido un error al intentar realizar la operación (" + result.Message + ").", // Text that is to be shown in the toast
                                        heading: "Error", // Optional heading to be shown on the toast
                                        icon: 'error', // Type of toast icon
                                        showHideTransition: 'fade', // fade, slide or plain
                                        allowToastClose: true, // Boolean value true or false
                                        hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                        position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                        textAlign: 'center',  // Text alignment i.e. left, right or center
                                        loader: false // Whether to show loader or not. True by default
                                    }
                                );
                            }
                        } else {
                            $.toast(
                                {
                                    text: "Se ha producido un error al intentar realizar la operación.", // Text that is to be shown in the toast
                                    heading: "Error", // Optional heading to be shown on the toast
                                    icon: 'error', // Type of toast icon
                                    showHideTransition: 'fade', // fade, slide or plain
                                    allowToastClose: true, // Boolean value true or false
                                    hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                    stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                    position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                    textAlign: 'center',  // Text alignment i.e. left, right or center
                                    loader: false // Whether to show loader or not. True by default
                                }
                            );
                        }
                    } else {
                        $.toast(
                            {
                                text: "Se ha producido un error al intentar procesar la información.", // Text that is to be shown in the toast
                                heading: "Error", // Optional heading to be shown on the toast
                                icon: 'error', // Type of toast icon
                                showHideTransition: 'fade', // fade, slide or plain
                                allowToastClose: true, // Boolean value true or false
                                hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                                stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                                position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                                textAlign: 'center',  // Text alignment i.e. left, right or center
                                loader: false // Whether to show loader or not. True by default
                            }
                        );
                    }
                    CerrarModal();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    CerrarModal();
                    $.toast(
                        {
                            text: errorThrown + "\n(" + JSON.stringify(jqXHR, null, 4) + ")", // Text that is to be shown in the toast
                            heading: textStatus, // Optional heading to be shown on the toast
                            icon: 'error', // Type of toast icon
                            showHideTransition: 'fade', // fade, slide or plain
                            allowToastClose: true, // Boolean value true or false
                            hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                            stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                            position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                            textAlign: 'center',  // Text alignment i.e. left, right or center
                            loader: true // Whether to show loader or not. True by default
                        }
                    );
                }
            }
        );
    } else {
        CerrarModal();
        $.toast(
            {
                text: "No hay registros a procesar.", // Text that is to be shown in the toast
                heading: "Error", // Optional heading to be shown on the toast
                icon: 'error', // Type of toast icon
                showHideTransition: 'fade', // fade, slide or plain
                allowToastClose: true, // Boolean value true or false
                hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                textAlign: 'center',  // Text alignment i.e. left, right or center
                loader: false // Whether to show loader or not. True by default
            }
        );
    }
}