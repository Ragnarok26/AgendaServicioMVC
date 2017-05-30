function CargarDatosLlamada(targetId, valueEstatus) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Cargando...</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var valueCallID = targetId;
    var datePick = $('#datepicker').val(); // get the textbox value
    //var valueEstatus = $("#estatusServicio").val();
    $.ajax(
        {
            type: 'POST',
            url: url_EditarLlamada,
            dataType: 'html',
            data: { "datePick": datePick, "valueEstatus": valueEstatus, "valueCallID": valueCallID },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    $("#llamadaEdit").html(data);
                    CargarEventosEdicion();
                    CerrarModal();
                    $("#LlamadaEditModal").modal("show");
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

function CargarDatosCorreo(targetId, EmailTo, EmailEng, valueEstatus) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Cargando...</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var valueCallID = targetId;
    var datePick = $('#datepicker').val(); // get the textbox value
    //var valueEstatus = $("#estatusServicio").val();
    $.ajax(
        {
            type: 'POST',
            url: url_CorreoLlamada,
            dataType: 'html',
            data: { "datePick": datePick, "valueEstatus": valueEstatus, "valueCallID": valueCallID, "EmailTo": EmailTo, "EmailEng": EmailEng },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    $("#correoEdit").html(data);
                    CargarEventosCorreo();
                    CerrarModal();
                    $("#modalSendEmail").modal("show");
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

function sendEmail() {

}

function closeModal() {
    try {
        document.getElementsByTagName("body").style.paddingRight = "0px";
    } catch (ex) { }
    try {
        document.getElementsByClassName("body.modal-open").style.marginRight = "0px";
    } catch (ex) { }
    $('.modal-backdrop').hide();
    return true;
}

function refreshModal() {
    ChangeEngineerIconModal(true);
}

function IngenieroCCCreate() {
    closeModal();
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var array = [];
    $(".deleteCCM").each(
        function () {
            array.push($(this).val());
        }
    );
    var datePick = $('#datepicker').val();
    $.ajax(
        {
            type: "POST",
            url: url_AgregarIngenieros,
            dataType: "json",
            data: { "date": datePick, "EngineersId": array },
            cache: false,
            success: function (result, textStatus, jqXHR) {
                if (result != null) {
                    if (result.HasError === false) {
                        if (result.RowsAffected > 0) {
                            RecargarIngenierosCallCenterDisponibilidad();
                        } else {
                            CerrarModal();
                            $('#myModal').show();
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
                        CerrarModal();
                        $('#myModal').show();
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
                    CerrarModal();
                    $('#myModal').show();
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
            },
            error: function (jqXHR, textStatus, errorThrown) {
                CerrarModal();
                $('#myModal').show();
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

function IngenieroCCDelete(EngineerID) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Espere por favor...<br/>Se está procesando su solicitud</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    var datePick = $('#datepicker').val();
    var value = $("#" + EngineerID).val();
    $.ajax(
        {
            type: "POST",
            url: url_QuitarIngenieros,
            dataType: "json",
            data: { "date": datePick, "EngineerId": value },
            cache: false,
            success: function (result, textStatus, jqXHR) {
                if (result != null) {
                    if (result.HasError === false) {
                        if (result.RowsAffected > 0) {
                            RecargarIngenierosCallCenterDisponibilidad();
                        } else {
                            CerrarModal();
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
                        CerrarModal();
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
                    CerrarModal();
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

function ChangeEngineerIconModal(EngineerID, clear) {
    if (EngineerID != null && EngineerID != "" && EngineerID != "undefined") {
        if (typeof EngineerID == 'string') {
            var id = EngineerID.replace("btnM_", "");
            id = id.replace("iM_", "");
            if (clear === false) {
                if ($("#iM_" + id).hasClass("glyphicon-plus")) {
                    $("#iM_" + id).removeClass("glyphicon-plus");
                    $("#iM_" + id).addClass("glyphicon-minus");
                    $("#iM_" + id).css("background-color", "red");
                    if (!$("#btnM_" + id).hasClass("deleteCCM")) {
                        $("#btnM_" + id).addClass("deleteCCM");
                        $("#btnM_" + id).css("background-color", "red");
                    }
                } else {
                    $("#iM_" + id).removeClass("glyphicon-minus");
                    $("#iM_" + id).addClass("glyphicon-plus");
                    $("#iM_" + id).css("background-color", "#5cb85c");
                    if ($("#btnM_" + id).hasClass("deleteCCM")) {
                        $("#btnM_" + id).removeClass("deleteCCM");
                        $("#btnM_" + id).css("background-color", "#5cb85c");
                    }
                }
            } else if ($("#iM_" + id).hasClass("glyphicon-minus")) {
                $("#iM_" + id).removeClass("glyphicon-minus");
                $("#iM_" + id).addClass("glyphicon-plus");
                $("#iM_" + id).css("background-color", "#5cb85c");
                if ($("#btnM_" + id).hasClass("deleteCCM")) {
                    $("#btnM_" + id).removeClass("deleteCCM");
                    $("#btnM_" + id).css("background-color", "#5cb85c");
                }
            }
            
        }
    }
}

function CargarListadoReagendados(mostrarModal) {
    if (mostrarModal) {
        if (mostrarModal === true) {
            AbrirModal(
                {
                    Mensaje: "<p><h3>Cargando...</h3></p>",
                    AlineacionMensaje: "center"
                }
            );
        }
    }
    $.ajax(
        {
            type: 'POST',
            url: url_ListadoReprogramadas,
            dataType: 'html',
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    $("#divRescheduler").html(data);
                    FiltrarListado(false);
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

function FiltrarListado(mostrarModal) {
    if (mostrarModal) {
        if (mostrarModal === true) {
            AbrirModal(
                {
                    Mensaje: "<p><h3>Cargando...</h3></p>",
                    AlineacionMensaje: "center"
                }
            );
        }
    }
    //var valueEstatus = $("#estatusServicio").val();
    var valueEstatus = [];
    $(".filterStatus").each(
        function () {
            if ($(this).prop("checked")) {
                valueEstatus.push($(this).val());
            }
        }
    );
    var datePick = $('#datepicker').val();
    var txtCallID = $('#txtCallID').val();
    $.ajax(
        {
            type: 'POST',
            url: url_Listado,
            dataType: 'html',
            //data: { "datePick": datePick, "valueEstatus": valueEstatus, "valueCallID": txtCallID },
            data: { "datePick": datePick, "valuesEstatus": valueEstatus, "valueCallID": txtCallID },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    $("#divLoad").html(data);
                    RecargarIngenierosCallCenterDisponibilidad();
                    //CerrarModal();
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

function RecargarIngenierosCallCenterDisponibilidad() {
    var datePick = $('#datepicker').val();
    var txtCallID = $('#txtCallID').val();
    $.ajax(
        {
            type: "POST",
            url: url_IngenierosCallCenter,
            dataType: "html",
            data: { "date": datePick },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                $("#ContainerIngCC").html(data);
                CargarEventosIngenierosCC();
                RecargarIngenierosDisponibilidad();
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

function RecargarIngenierosDisponibilidad() {
    var datePick = $('#datepicker').val();
    var txtCallID = $('#txtCallID').val();
    $.ajax(
        {
            type: "POST",
            url: url_DisponibilidadIngenieros,
            dataType: "html",
            data: { "date": datePick, "valueCallID": txtCallID },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                $("#ContainerEngAvailable").html(data);
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
}

function CargarEventosIngenierosCC() {
    $(".deleteCC").click(
        function (event) {
            IngenieroCCDelete(event.currentTarget.id);
        }
    );

    $(".btn_AddEng").click(
        function () {
            ChangeEngineerIconModal(event.currentTarget.id, false);
        }
    );

    $('#myModal').on('shown.bs.modal',
        function () {
            refreshModal();
        }
    );

    $('#myModal').on('hidden.bs.modal',
        function (e) {
            closeModal();
        }
    );

    $("#btnModalECC").click(
        function () {
            refreshModal();
        }
    );

    $("#btnCreate").click(
        function (event) {
            IngenieroCCCreate();
        }
    );
}

$(document).ready(
    function () {
        $(window).resize(
            function () {
                width = $(this).width();
                height = $(this).height();
            }
        );

        /*$("#estatusServicio").change(
            function (e) {
                FiltrarListado(true);
            }
        );*/
        $(".filterStatus").change(
            function (e) {
                FiltrarListado(true);
            }
        );

        $("#btnCallIDFilter").click(
            function () {
                FiltrarListado(true);
            }
        );

        $("#txtCallID").keydown(
            function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            }
        );

        $("#datepicker").datetimepicker(
            {
                format: 'DD/MM/YYYY'
            }
        );

        $("#datepicker").on("dp.change",
            function () {
                FiltrarListado(true);
            }
        );

        $("#btn2").click(
            function () {
                $('#datepicker').datetimepicker('show');
            }
        );

        CargarListadoReagendados(true);
    }
);