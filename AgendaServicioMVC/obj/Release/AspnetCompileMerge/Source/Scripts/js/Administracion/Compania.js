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
                            CargarDatosCompany(null);
                        }
                    );
                    $("#btnEliminarSeleccionados").click(
                        function () {
                            CompanyDelete(null);
                        }
                    );
                    $("#compChkAll").change(
                        function () {
                            var checked = $("#compChkAll").prop("checked");
                            $(".compChk:checkbox").each(
                                function () {
                                    $(this).prop("checked", checked);
                                }
                            );
                        }
                    );
                    $(".btnEditModal").click(
                        function (event) {
                            CargarDatosCompany(event.currentTarget.value);
                        }
                    );
                    $(".btnDelete").click(
                        function (event) {
                            CompanyDelete(event.currentTarget.value);
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

function CargarDatosCompany(targetId) {
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
                    $("#companiaAddEdit").html(data);
                    CargarEventosEdicion();
                    CerrarModal();
                    $("#CompaniaAgregarEditarModal").modal("show");
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
    $("#btnCompanySave").click(
        function (event) {
            CompanySave(event.currentTarget.value);
        }
    );
}

function CompanySave(value) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var Entity = {
        "Id": value,
        "Name": $("#txtName").val()
    };
    if (Entity.Name.length > 0) {
        $.ajax(
            {
                type: 'POST',
                url: url_Edicion,
                dataType: 'json',
                contentType: "application/json",
                data: "{ compania: " + JSON.stringify(Entity) + " }",
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

function CompanyDelete(value) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var Entities = [];
    if (value == null) {
        $(".compChk:checkbox:checked").each(
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
                data: "{ companias: " + JSON.stringify(Entities) + " }",
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