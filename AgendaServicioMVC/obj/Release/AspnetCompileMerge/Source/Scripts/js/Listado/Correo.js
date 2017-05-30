function CargarEventosCorreo() {
    $("#btnSendEmail").click(
        function () {
            var datos = {
                "CallId": $(this).val(),
                "EmailTo": $("#txtToEmail").val(),
                "EmailCC": $("#txtCCEmail").val(),
                "EmailEng": $("#txtEngEmail").val(),
                "DateAct": $("#txtDateCallEmail").val(),
                "Estatus": $("#txtEstatusCallEmail").val(),
                "UserName": $("#UserLogged").text()
            };
            $("#modalSendEmail").modal("hide");
            AbrirModal(
                {
                    Mensaje: "<p><h3>Cargando...</h3></p>",
                    AlineacionMensaje: "center"
                }
            );
            $.ajax(
                {
                    type: 'POST',
                    url: url_EnviarCorreo,
                    dataType: 'json',
                    data: { "datos": datos },
                    cache: false,
                    success: function (data, textStatus, jqXHR) {
                        if (data != null) {
                            CerrarModal();
                            if (data.Sent === true) {
                                $.toast(
                                    {
                                        text: "Se ha enviado el correo a las direcciones establecidas.", // Text that is to be shown in the toast
                                        heading: "Correo Enviado", // Optional heading to be shown on the toast
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
                                $("#modalSendEmail").modal("show");
                                $.toast(
                                    {
                                        text: "No fue posible enviar el correo.", // Text that is to be shown in the toast
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
                            $("#modalSendEmail").modal("show");
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
    );
}