function Cargando(url) {
    AbrirModal(
        {
            Mensaje: "<p><h3>Cargando...</h3></p>",
            AlineacionMensaje: "center"
        }
    );
    window.location = url;
}

function Actualizar() {
    CargarListadoReagendados(true);
}

function Salir() {
    $('#logoutForm').submit();
}