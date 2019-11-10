
$(document).ready(function () {
    listaCandidatos();

    $("#close-candidato-successbox").click(function () {
        $("#candidato-successbox").slideUp();
    });

    //modal multiplo
    $(document).on({
        'show.bs.modal': function () {
            var zIndex = 1040 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);
            setTimeout(function () {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);
        },
        'hidden.bs.modal': function () {
            if ($('.modal:visible').length > 0) {
                // restore the modal-open class to the body element, so that scrolling works
                // properly after de-stacking a modal.
                setTimeout(function () {
                    $(document.body).addClass('modal-open');
                }, 0);
            }
        }
    }, '.modal');

})

function listaCandidatos(num_pagina) {

    $("#paginacaoCandidato").html("");
    var url = "/Candidatos/ListaCandidatos";

    paginacao = num_pagina == null ? { PaginaAtual: 0 } : { PaginaAtual: num_pagina };

    $.post(url, { paginacao: paginacao }, function (data) {
        $("#candidatos tbody").html("");
        
        //if (data.candidatos.length == 0)
        //    $("#candidatos tbody")
        //        .append($("<tr>").append($("<td colspan='5'>").addClass("text-center").html("Nenhum candidato encontrado.")));

        if (data.candidatos.length != 0)
            $("#candidatos tbody").html("")

        $.each(data.candidatos, function (i, item) {
            $("#candidatos tbody")
                .append($("<tr>").addClass("")
                    .append($("<td>").addClass("width25 align-middle").html(item.nome))
                    .append($("<td>").addClass("width30 align-middle").html(item.email))
                    .append($("<td>").addClass("width20 text-center align-middle").html(item.dataNascimento))
                    .append($("<td>").addClass("width15 text-center align-middle").html(item.nacionalidade))
                    .append($("<td>").addClass("width10 text-center align-middle").html(
                        "\
                            <a href='#/' class='selecionaCandidato btnEditCandidato' value=" + item.id + " onclick='limpaFormulario()' data-toggle='modal' data-target='.modal-create-candidato'> <img src='/icon/edit.png' /></a > \
                            <a href='/Candidatos/Details' class='selecionaCandidato btnDetailsCandidato' value=" + item.id + " onclick='limpaFormulario()'><img src='/icon/details.png' /></a>\
                            <a href='#/' class='selecionaCandidato btnDeletaCandidato' value=" + item.id + " onclick='limpaFormulario()' data-toggle='modal' data-target='.modal-delete-candidato'><img src='/icon/delete.png' /></a>\
                            "
                    )))
        });

        paginar_candidatos(data.paginacao);

        $(".selecionaCandidato").click(function () {
            var id = $(this).attr("value");
            var url = "/Candidatos/BuscaPorId";
            var acaoDeletar = $(this).hasClass("btnDeletaCandidato");
            zeraExperiencias();

            $.get({
                url: url,
                data: { idCandidato: id },
                success: function (data) {
                    //verifica qual funcao do create irá chamar
                    if (acaoDeletar)
                        confirmacaoDelete(data);
                    else
                        formEdit(data);
                }
            });
        });
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


