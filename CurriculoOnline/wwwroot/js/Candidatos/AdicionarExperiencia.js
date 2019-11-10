var experiencias = [];
var experienciaSelecionada;

$(document).ready(function () {

    listaEstados("#estados_experiencia"); // função em create.js

    $("#estados_experiencia").change(function () {
        listaCidades("#cidades_experiencia", this.value); // função em create.js
    });

    $("#btnSalvaExperiencia").click(function () { salvaExperiencia() });
    $("#FormExperiencia").submit(function () { salvaExperiencia(); return false; });
    $("#close-adicionar-experiencia-alertbox").click(function () { $("#adicionar-experiencia-alertbox").slideUp(); });

});

function salvaExperiencia() {
    url = '/Experiencia/VerficaCampos';

    var data = $('#FormExperiencia').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    verificaCamposExperiencia(data);
    if ($(".checkValidationExperiencia ul li").length > 0) {
        mostraAlertBoxExperiencia("Os seguintes campos são obrigatórios:");
        return false;
    }

    $.post({
        url: url,
        data: { viewmodel: data },
        success: function (data_be) {
            if (data.Id != "") {
                EditExperiencia(data)
                return false;
            }

            if (data_be.sucesso) {
                $('.modal-adicionar-experiencia').modal('hide');
                $("#create-candidato-successbox .message").html(data_be.texto)
                $("#create-candidato-successbox").slideDown();

                if (experienciaSelecionada == null || experienciaSelecionada == "")
                    experiencias.push(data);
                else
                    experiencias[experienciaSelecionada] = data;

                mostrarNovasExperienciaTela();
            }else
                mostraAlertBoxExperiencia(data_be.texto);
        }
    });
}

function mostrarNovasExperienciaTela() {
    $("#experiencia #experienciasNovas").html("");

    if ($("#experiencia #experienciasSalvas").html().trim() == "" && experiencias.length == 0) {
        $("#experiencia #experienciasNovas")
            .append($("<tr>")
                .append($("<td>").attr("colspan", "4").addClass("text-center").html("Nenhuma experiência cadastrada.")));
        return false;
    }

    experiencias.forEach(function (item, i) {
        var data = converterData(item.DataInicio);
        data += " - "
        if (item.DataFim == "" || item.DataFim == null)
            data += "Atualmente"
        else
            data += converterData(item.DataFim)

        $("#experiencia #experienciasNovas")
            .append($("<tr>").addClass("")
                .append($("<td>").addClass("width25 align-middle").html(item.Profissao))
                .append($("<td>").addClass("width30 align-middle").html(item.Empresa))
                .append($("<td>").addClass("width15 text-center align-middle").html(data))
                .append($("<td>").addClass("width10 text-center align-middle").html(
                    "\
                    <a href='#/' class='selecionaExperienciaNova btnEditExperiencia' value="+ i +" onclick='limpaFormularioExperiencia()' data-toggle='modal' data-target='.modal-adicionar-experiencia'> <img src='/icon/edit.png' /></a > \
                    <a href='#/' class='selecionaExperienciaNova btnDeletaExperiencia' value="+ i +" onclick='limpaFormularioExperiencia()' data-toggle='modal' data-target='.modal-delete-experiencia'><img src='/icon/delete.png' /></a>\
                    "))
            )
    });

    $(".selecionaExperienciaNova").click(function () {
        var acaoDeletar = $(this).hasClass("btnDeletaExperiencia");

        experienciaSelecionada = $(this).attr("value");
        var experiencia = experiencias[experienciaSelecionada];

        if (acaoDeletar)
            confirmacaoDeleteExperiencia(experiencia);
        else
            formEditExperiencia(experiencia);

    });
}

function limpaFormularioExperiencia() {
    experienciaSelecionada = null;
    $(".alert").slideUp();
    $("#FormExperiencia input[name='Id']").val("");
    $("#FormExperiencia").each(function () {
        this.reset();
        $("#cidades_experiencia").html("<option value=''></option>");
    })
}

function verificaCamposExperiencia(data) {
    $(".checkValidationExperiencia ul").html("");
    if (data.Profissao == "" || data.Profissao == '' || data.Profissao == null)
        $(".checkValidationExperiencia ul").append("<li>Profissão</li>");
    if (data.Empresa == "" || data.Empresa == '' || data.Empresa == null)
        $(".checkValidationExperiencia ul").append("<li>Empresa</li>");
    if (data.DataInicio == "" || data.DataInicio == '' || data.DataInicio == null)
        $(".checkValidationExperiencia ul").append("<li>Data de Entrada</li>");
    if (data.IdCidade == "" || data.IdCidade == '' || data.IdCidade == null)
        $(".checkValidationExperiencia ul").append("<li>Cidade</li>");
    if (data.IdEstado == "" || data.IdEstado == '' || data.IdEstado == null)
        $(".checkValidationExperiencia ul").append("<li>Estado</li>");
}

function mostraAlertBoxExperiencia(mensagem) {
    $("#adicionar-experiencia-alertbox .message").html(mensagem);
    $("#adicionar-experiencia-alertbox").slideDown();
}

function converterData(data) {
    dataArr = data.split("-");
    return dataArr[2] + "/" + dataArr[1] + "/" + dataArr[0];
}

//edicao
function formEditExperiencia(experiencia) {
    $("#FormExperiencia input[name='Id']").val(experiencia.Id);
    $("#FormExperiencia input[name='Profissao']").val(experiencia.Profissao);
    $("#FormExperiencia input[name='Empresa']").val(experiencia.Empresa);
    $("#FormExperiencia input[name='DataInicio']").val(experiencia.DataInicio);
    $("#FormExperiencia input[name='DataFim']").val(experiencia.DataFim);

    listaCidades("#cidades_experiencia", experiencia.IdEstado, function () {
        $("#FormExperiencia select[name='IdCidade']").val(experiencia.IdCidade);
    });

    $("#FormExperiencia select[name='IdEstado']").val(experiencia.IdEstado);
    $("#FormExperiencia input[name='Detalhes']").val(experiencia.Detalhes);
}

function listaExperiencias(idCandidato) {
    var url = "/Experiencia/ListaExperienciaCandidato";
    if (!idCandidato)
        idCandidato = $("#FormCreateCandidato input[name='Id']").val();

    $.post(url, { idCandidato: idCandidato }, function (data) {
        listaExperienciasTela(data); // create.js
    })
}

function EditExperiencia(experiencia) {
    var url = "/Experiencia/Edit";

    $.post({
        url: url,
        data: { viewmodel: experiencia },
        success: function (data) {
            if (data.sucesso) {
                $('.modal-adicionar-experiencia').modal('hide');
                $("#create-candidato-successbox .message").html(data.texto)
                $("#create-candidato-successbox").slideDown();

                listaExperiencias($("#FormCreateCandidato input[name='Id']").val());
            }
            else
                mostraAlertBoxExperiencia(data.texto);
        }
    });
}