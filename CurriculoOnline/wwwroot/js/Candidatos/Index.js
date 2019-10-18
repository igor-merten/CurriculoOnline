
$(document).ready(function () {
    listaCandidatos();

    $("#close-candidato-successbox").click(function () {
        $("#candidato-successbox").slideUp();
    });

})

function listaCandidatos(num_pagina) {
    var url = "/Candidatos/ListaCandidatos";

    paginacao = num_pagina == null ? { PaginaAtual: 0 } : { PaginaAtual: num_pagina };
    $("#candidatos tbody").html("");

    $.post(url, { paginacao: paginacao}, function (data) {
        $.each(data.candidatos, function (i, item) {
            $("#candidatos tbody")
                .append($("<tr>").addClass("")
                    .append($("<td>").addClass("width30 align-middle").html(item.nome))
                    .append($("<td>").addClass("width30 align-middle").html(item.email))
                    .append($("<td>").addClass("width20 text-center align-middle").html(item.dataNascimento))
                    .append($("<td>").addClass("width20 text-center align-middle").html(item.nacionalidade))
                    .append($("<td>").addClass("width10 text-center align-middle").html(
                        "<a href='#/'><img src='/icon/edit.png' /></a> \
                        <a href='#/' class='selecionaCandidato' value=" + item.id + " onclick='limpaFormulario()' data-toggle='modal' data-target='.modal-delete-candidato'><img src='/icon/delete.png' /></a>"
                    )))
        });
        paginar_candidatos(data.paginacao);

        $(".selecionaCandidato").click(function () {
            var id = $(this).attr("value");
            var url = "/Candidatos/BuscaPorId";

            $.get({
                url: url,
                data: { idCandidato: id },
                success: function (data) {
                    confirmacaoDelete(data);
                }
            });
        })
    });
}

function paginar_candidatos(paginacao) {
    for (i = 0; i <= paginacao.totalPaginas; i++) {
        var classe = "page-item ";
        if (paginacao.paginaAtual == i)
            classe += "active";

        $("#paginacaoCandidato").append($("<li>")
            .addClass(classe)
            .append($("<a>").addClass("paginacao-candidatos page-link").attr('href', '#/').append(i + 1)))
    }

    $(".paginacao-candidatos").click(function () {
        $("#paginacaoCandidato").html("");
        pagina = $(this).html() - 1;
        listaCandidatos(pagina);
    })

}


