$(document).ready(
    function () {
        $("#login").click(
            function () {
                AbrirModal(
                    {
                        Mensaje: "<p align='center'><h3>Iniciando Sesión</h3></p>",
                        AlineacionMensaje: "center"
                    }
                );
            }
        );
    }
);