var ViewType = 'agendaView';

$(document).ready(
    function () {
        CrearCalendario();
        AbrirModal(
            {
                Mensaje: "<p><h3>Cargando Información...</h3></p>",
                AlineacionMensaje: "center"
            }
        );
        CargarCalendario(null, null, null);
    }
);

function DestruirCalendario() {
    $("#calendario").jqxScheduler('destroy');
    $("#CalendarContent").append("<div id=\"calendario\"></div>");
}

function CrearCalendario() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    $("#calendario").jqxScheduler(
        {
            date: new $.jqx.date(yyyy, mm, dd),
            width: 1024,
            height: 840,
            //source: adapter,
            showLegend: true,
            ready: function () {

            },
            appointmentDataFields:
            {
                from: "Start",
                to: "End",
                id: "Id",
                description: "Description",
                location: "Location",
                subject: "Subject",
                readOnly: "ReadOnly",
                draggable: "Draggable",
                resizable: "Resizable",
            },
            view: ViewType,
            views:
            [
                { type: 'dayView' },
                { type: 'weekView' },
                { type: 'monthView' },
                { type: 'agendaView' }
            ],
            localization: {
                // separator of parts of a date (e.g. '/' in 11/05/1955)
                '/': "/",
                // separator of parts of a time (e.g. ':' in 05:44 PM)
                ':': ":",
                // the first day of the week (0 = Sunday, 1 = Monday, etc)
                firstDay: 0,
                days: {
                    // full day names
                    names: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
                    // abbreviated day names
                    namesAbbr: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"],
                    // shortest day names
                    namesShort: ["D", "L", "M", "Mi", "J", "V", "S"]
                },
                months: {
                    // full month names (13 months for lunar calendards -- 13th month should be "" if not lunar)
                    names: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", ""],
                    // abbreviated month names
                    namesAbbr: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sept", "Oct", "Nov", "Dic", ""]
                },
                agendaViewString: "Agenda",
                agendaAllDayString: "Todo el día",
                agendaDateColumn: "Fecha",
                agendaTimeColumn: "Hora",
                agendaAppointmentColumn: "Tarea",
                backString: "Anterior",
                forwardString: "Siguiente",
                toolBarPreviousButtonString: "Anterior",
                toolBarNextButtonString: "Siguiente",
                emptyDataString: "No se encontraron datos para mostrar.",
                loadString: "Cargando...",
                clearString: "Limpiar",
                todayString: "Hoy",
                dayViewString: "Día",
                weekViewString: "Semana",
                monthViewString: "Mes",
                //timelineDayViewString: "Zeitleiste Day",
                //timelineWeekViewString: "Zeitleiste Woche",
                //timelineMonthViewString: "Zeitleiste Monat",
                loadingErrorMessage: "Ocurrió un error al intentar cargar la información."
            }
        }
    );
    $("#calendario").on('viewChange',
        function (event) {
            //DestruirCalendario();
            var args = event.args;
            var from = args.from;
            var to = args.to;
            var date = args.date;
            //var oldViewType = args.oldViewType;
            //var newViewType = args.newViewType;
            //ViewType = args.newViewType;
            AbrirModal(
                {
                    Mensaje: "<p><h3>Cargando Información...</h3></p>",
                    AlineacionMensaje: "center"
                }
            );
            //CrearCalendario();
            CargarCalendario(new $.jqx.date(date).toString(), new $.jqx.date(from).toString(), new $.jqx.date(to).toString());
        }
    );
    $("#calendario").on('dateChange',
        function (event) {
            //DestruirCalendario();
            AbrirModal(
                {
                    Mensaje: "<p><h3>Cargando Información...</h3></p>",
                    AlineacionMensaje: "center"
                }
            );
            var args = event.args;
            var from = args.from;
            var to = args.to;
            var date = args.date;
            //CrearCalendario();
            CargarCalendario(new $.jqx.date(date).toString(), new $.jqx.date(from).toString(), new $.jqx.date(to).toString());
        }
    );
    $("#calendario").on('appointmentClick',
        function (event) {
            var args = event.args;
            var appointment = args.appointment;
            // appointment fields
            // originalData - the bound data.
            // from - jqxDate object which returns when appointment starts.
            // to - jqxDate objet which returns when appointment ends.
            // status - String which returns the appointment's status("busy", "tentative", "outOfOffice", "free", "").
            // resourceId - String which returns the appointment's resouzeId
            // hidden - Boolean which returns whether the appointment is visible.
            // allDay - Boolean which returns whether the appointment is allDay Appointment.
            // resiable - Boolean which returns whether the appointment is resiable Appointment.
            // draggable - Boolean which returns whether the appointment is resiable Appointment.
            // id - String or Number which returns the appointment's ID.
            // subject - String which returns the appointment's subject.
            // location - String which returns the appointment's location.
            // description - String which returns the appointment's description.
            // tooltip - String which returns the appointment's tooltip.
            CargarDatosLlamada(appointment.originalData.Id, appointment.originalData.Date, appointment.originalData.Estatus);
        }
    );
}

function CargarCalendario(date, start, end) {
    $.ajax(
        {
            type: 'POST',
            url: url_Calendario,
            dataType: 'json',
            data: { "actual": date, "inicial": start, "final": end },
            cache: false,
            success: function (data, textStatus, jqXHR) {
                if (data != null) {
                    if (data.HasError === false)
                    {
                        var source =
                        {
                            dataType: 'array',
                            dataFields: [
                                { name: 'Id', type: 'string' },
                                { name: 'Description', type: 'string' },
                                { name: 'Location', type: 'string' },
                                { name: 'Subject', type: 'string' },
                                { name: 'Estatus', type: 'string' },
                                //{ name: 'Calendar', type: 'string' },
                                { name: 'Date', type: 'string' },
                                { name: 'ReadOnly', type: 'boolean' },
                                { name: 'Draggable', type: 'boolean' },
                                { name: 'Resizable', type: 'boolean' },
                                { name: 'Start', type: 'date', format: "yyyy-MM-dd HH:mm" },
                                { name: 'End', type: 'date', format: "yyyy-MM-dd HH:mm" }
                            ],
                            id: 'Id',
                            localData: data.Source
                        };
                        var adapter = new $.jqx.dataAdapter(source);
                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1;
                        var yyyy = today.getFullYear();
                        $("#calendario").jqxScheduler(
                            {
                                source: adapter
                            }
                        );
                    }
                    else
                    {
                        $.toast(
                            {
                                text: "Se ha producido un error al intentar cargar la información (" + data.Message + ").", // Text that is to be shown in the toast
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
                } else {
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
                    CerrarModal();
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

function CargarDatosLlamada(valueCallID, datePick, valueEstatus) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Cargando...</h3></p>",
            AlineacionMensaje: "center"
        }
    );
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