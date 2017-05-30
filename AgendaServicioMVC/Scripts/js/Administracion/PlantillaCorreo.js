$(document).ready(
    function () {
        $.FroalaEditor.DefineIcon('save', { NAME: 'save' });
        $.FroalaEditor.RegisterCommand('save',
            {
                title: 'Save',
                focus: false,
                undo: false,
                refreshAfterCallback: false,
                callback: function () {
                    $('#editor').froalaEditor('save.save');
                }
            }
        );


        $('#editor').froalaEditor(
            {
                language: 'es',
                //toolbarBottom: true,
                toolbarButtons: [
                    'fullscreen', 'print', 'bold', 'italic',
                    'underline', 'strikeThrough', 'subscript', 'superscript',
                    'fontFamily', 'fontSize', '|', 'specialCharacters',
                    'color', 'emoticons', 'inlineStyle', 'paragraphStyle',
                    '|', 'paragraphFormat', 'align', 'formatOL',
                    'formatUL', 'outdent', 'indent', 'quote',
                    'insertHR', '-', 'insertLink', 'insertImage',
                    'insertVideo', 'insertFile', 'insertTable', 'undo',
                    'redo', 'clearFormatting', 'selectAll', 'html', 'save'
                ],
                toolbarButtonsMD: [
                    'fullscreen', 'bold', 'italic', 'underline',
                    'fontFamily', 'fontSize', 'color', 'paragraphStyle',
                    'paragraphFormat', 'align', 'formatOL', 'formatUL',
                    'outdent', 'indent', 'quote', 'insertHR',
                    'insertLink', 'insertImage', 'insertVideo', 'insertFile',
                    'insertTable', 'undo', 'redo', 'clearFormatting',
                    'save'
                ],
                toolbarButtonsSM: [
                    'fullscreen', 'bold', 'italic', 'underline', 
                    'fontFamily', 'fontSize', 'insertLink', 'insertImage', 
                    'insertTable', 'undo', 'redo', 'save'
                ],
                toolbarButtonsXS: [
                    'bold', 'italic', 'fontFamily', 'fontSize',
                    'undo', 'redo', 'save'
                ],
                saveInterval: 0,
                saveParam: 'content',
                saveURL: url_PlantillaCorreo,
                saveMethod: 'POST',
                saveParams: { id: 'editor' }
            }
        ).on('froalaEditor.save.before',
            function (e, editor) {
                $.toast(
                    {
                        text: 'Guardando Cambios', // Text that is to be shown in the toast
                        heading: 'Información', // Optional heading to be shown on the toast
                        icon: 'info', // Type of toast icon
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
        ).on('froalaEditor.save.after',
            function (e, editor, response) {
                if (response.HasError !== true) {
                    $.toast(
                        {
                            text: 'Se han guardado los cambios', // Text that is to be shown in the toast
                            heading: 'Exitoso', // Optional heading to be shown on the toast
                            icon: 'success', // Type of toast icon
                            showHideTransition: 'fade', // fade, slide or plain
                            allowToastClose: true, // Boolean value true or false
                            hideAfter: 6000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                            stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                            position: 'bottom-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                            textAlign: 'center',  // Text alignment i.e. left, right or center
                            loader: true // Whether to show loader or not. True by default
                        }
                    );
                } else {
                    $.toast(
                        {
                            text: 'No se guardaron los cambios: ' + response.Message, // Text that is to be shown in the toast
                            heading: 'Error', // Optional heading to be shown on the toast
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
        ).on('froalaEditor.save.error',
            function (e, editor, error) {
                $.toast(
                    {
                        text: 'No se guardaron los cambios: ' + error, // Text that is to be shown in the toast
                        heading: 'Error', // Optional heading to be shown on the toast
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
        );
    }
);