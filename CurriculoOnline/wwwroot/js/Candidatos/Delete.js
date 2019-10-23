function confirmacaoDelete(candidato) {
    $("#nomeDeletaCandidato").html(candidato.Nome);
    $("#confirmarDelete").attr("value", candidato.Id);
}
$(document).ready(function () {
    $("#close-delete-candidato-alertbox").click(function () { $("#delete-candidato-alertbox").slideUp(); });

    $("#confirmarDelete").click(function () {
        var id = $(this).attr("value");
        var url = "/Candidatos/Delete";

        $.post({
            url: url,
            data: { IdCandidato: id },
            success: function (data) {
                if (!data.sucesso) {
                    $("#delete-candidato-alertbox .message").html(data.texto);
                    $("#delete-candidato-alertbox").slideDown();
                } else {
                    $('.modal-delete-candidato').modal('hide');
                    $("#candidato-successbox .message").html(data.texto)
                    $("#candidato-successbox").slideDown();
                    listaCandidatos();
                }
            }
        });

    })
})
