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
                    $(".btnSave").click(
                        function (event) {
                            GuardarCambios(event.currentTarget.value);
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

function GuardarCambios(idRow) {
    var sucursal = $("#idSucursal_" + idRow).val();
    if (typeof (sucursal) !== "undefined" && sucursal != null && sucursal != "") {
        AbrirModal(
            {
                Mensaje: "<p><h3>Guardando...</h3></p>",
                AlineacionMensaje: "center"
            }
        );
        $.ajax(
            {
                type: 'POST',
                url: url_Edicion,
                dataType: 'json',
                data: { "Id": idRow, "Sucursal": { "Id": sucursal } },
                cache: false,
                success: function (data, textStatus, jqXHR) {
                    if (data != null) {
                        if (data.HasError === false) {
                            if (data.RowsAffected > 0) {
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
                                        text: "Se ha producido un error al intentar realizar la operación (" + data.Message + ").", // Text that is to be shown in the toast
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
                        CerrarModal();
                        //Buscar($("#txtBuscar").val());
                    } else {
                        CerrarModal();
                        $.toast(
                            {
                                text: "Se ha producido un error al intentar guardar la información.", // Text that is to be shown in the toast
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
    } else {
        $.toast(
            {
                text: "Favor de Seleccionar una sucursal válida.", // Text that is to be shown in the toast
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