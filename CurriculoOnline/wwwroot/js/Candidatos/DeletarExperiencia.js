$(document).ready(function () {
    $("#close-delete-experiencia-alertbox").click(function () { $("#delete-experiencia-alertbox").slideUp(); });

    $("#confirmarDeleteExperiencia").click(function () {
        var id = $(this).attr("value");
        var url = "/Experiencia/Delete";

        if (!id) {
            //var index = experiencias.indexOf(experienciaSelecionada)
            //if (index > -1) {
            //    experiencias.splice(index, 1);
            //}

            experiencias.splice(experienciaSelecionada, 1);

            mostraMensagemDeleteSucesso("Experiência deletada com sucesso!");
            mostrarNovasExperienciaTela();
            return false;
        }

        $.post({
            url: url,
            data: { IdExperiencia: id },
            success: function (data) {
                if (!data.sucesso) {
                    $("#delete-experiencia-alertbox .message").html(data.texto);
                    $("#delete-experiencia-alertbox").slideDown();
                } else {
                    mostraMensagemDeleteSucesso(data.texto);
                    listaExperiencias();
                }
            }
        });

    })
})

function confirmacaoDeleteExperiencia(experiencia) {
    $("#profissaoDeletaExperiencia").html(experiencia.Profissao);
    $("#empresaDeletaExperiencia").html(experiencia.Empresa);
    $("#confirmarDeleteExperiencia").attr("value", experiencia.Id);
}

function mostraMensagemDeleteSucesso(mensagem) {
    $('.modal-delete-experiencia').modal('hide');
    $("#create-candidato-successbox .message").html(mensagem)
    $("#create-candidato-successbox").slideDown();
}