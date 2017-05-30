function CallSave(value) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var ddlTipoServicio = $("#CallType").val();
    var ddlSucursal = $("#idSucursal").val();
    var ddlEstatusServicio = $("#idEstatusServicio").val();
    var txtDescripcion = $("#tDescripcion").val();
    $.ajax(
        {
            type: 'POST',
            url: url_ProcesoEditarLlamada,
            dataType: 'json',
            contentType: "application/json",
            data: "{ Llamada: { \"CallId\": \"" + value + "\", \"IdTipoServicio\": \"" + ddlTipoServicio + "\", \"Sucursal\": \"" + ddlSucursal + "\", \"IdEstatusServicio\": \"" + ddlEstatusServicio + "\", \"Descripcion\": \"" + txtDescripcion + "\" } }",
            cache: false,
            success: function (result, textStatus, jqXHR) {
                if (result != null) {
                    if (result == 0) {
                        $.toast(
                            {
                                text: "Se ha procesado la información.", // Text that is to be shown in the toast
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
                        FiltrarListado(true);
                    } else {
                        $.toast(
                            {
                                text: "Se ha producido un error: " + result + ".", // Text that is to be shown in the toast
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
                setTimeout(CerrarModal, 3000);
            },
            error: function (jqXHR, textStatus, errorThrown) {
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
                setTimeout(CerrarModal, 3000);
            }
        }
    );
}

function ActivitySave(value, CallId) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var ddlIngeniero = $("#ddlIngeniero" + value).val();
    var txtFechaInicio = $(".txtFechaInicio" + value).val();
    var txtFechaFin = $(".txtFechaFin" + value).val();
    var txtDetalle = $("#txtActDet" + value).val();
    $.ajax(
        {
            type: 'POST',
            url: url_ProcesoEditarActividad,
            dataType: 'json',
            contentType: "application/json",
            data: "{ \"CallId\": \"" + CallId + "\", \"ActivityId\": \"" + value + "\", \"IdIngeniero\": \"" + ddlIngeniero + "\", \"FechaInicio\": \"" + txtFechaInicio + "\", \"FechaFin\": \"" + txtFechaFin + "\", \"Detalle\": \"" + txtDetalle + "\" }",
            cache: false,
            success: function (result, textStatus, jqXHR) {
                if (result != null) {
                    if (result == 0) {
                        $.toast(
                            {
                                text: "Se ha procesado la información.", // Text that is to be shown in the toast
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
                        FiltrarListado(true);
                    } else {
                        $.toast(
                            {
                                text: "Se ha producido un error: " + result + ".", // Text that is to be shown in the toast
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
                setTimeout(CerrarModal, 3000);
            },
            error: function (jqXHR, textStatus, errorThrown) {
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
                setTimeout(CerrarModal, 3000);
            }
        }
    );
}

function CargarEventosEdicion() {
    $("#btnCallSave").click(
        function (event) {
            CallSave(event.target.value);
        }
    );
    $(".btn-save-activity").click(
        function (event) {
            var str = event.target.value;
            var values = str.split('|');
            ActivitySave(values[0], values[1]);
        }
    );
    $(".txtEditDatePicker").datetimepicker(
        {
            widgetPositioning: {
                vertical: 'bottom'
            },
            format: 'DD/MM/YYYY hh:mm a',
            allowInputToggle: true
        }
    );
    $(".btnEditDatePicker").click(
        function (event) {
            $('#' + String(event.currentTarget.value)).datetimepicker("show");
        }
    );
}