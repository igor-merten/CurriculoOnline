
$(document).ready(function () {

    listaEstados("#estados");
    $("#estados").change(function () {
        listaCidades("#cidades", this.value);
    });

    $("#FormCreateCandidato").submit(function () { enviaFormulario(this.value); return false; });
    $(".close-create-candidato-box").click(function () { $(".create-candidato-box").slideUp(); });

});

function listaEstados(id_campo) {
    var url = "/Candidatos/ListaEstados";
    $.get(url, function (data) {
        $(id_campo).html("<option value=''></option>");
        $.each(data, function (i, item) {
            $(id_campo).append($('<option>', {
                value: item.id,
                text: item.nome
            }));
        });
    });
}

function listaCidades(id_campo, idEstado, _callback) {
    var url = "/Candidatos/ListaCidades";
    $.get(url, { idEstado: idEstado }, function (data) {
        $(id_campo).html("<option value=''></option>");
        $.each(data, function (i, item) {
            $(id_campo).append($('<option>', {
                value: item.id,
                text: item.nome,
            }));
        });
        if (_callback)
            _callback();
    });
}

function limpaFormulario() {
    zeraExperiencias();

    $(".alert").hide();
    $("#FormCreateCandidato input[name='Id']").val("");
    $("#FormCreateCandidato").each(function () {
        this.reset();
        $("#cidades").html("<option value=''></option>");
    })
}

function verificaCampos(data) {
    $(".checkValidation ul").html("");
    if (data.Nome == "" || data.Nome == '' || data.Nome == null)
        $(".checkValidation ul").append("<li>Nome</li>");
    if (data.DataNascimento == "" || data.DataNascimento == '' || data.DataNascimento == null)
        $(".checkValidation ul").append("<li>Data de Nascimento</li>");
    if (data.Sexo == "" || data.Sexo == '' || data.Sexo == null)
        $(".checkValidation ul").append("<li>Sexo</li>");
    if (data.Nacionalidade == "" || data.Nacionalidade == '' || data.Nacionalidade == null)
        $(".checkValidation ul").append("<li>Nacionalidade</li>");
    if (data.NomeMae == "" || data.NomeMae == '' || data.NomeMae == null)
        $(".checkValidation ul").append("<li>Nome da Mãe</li>");
    if (data.NomePai == "" || data.NomePai == '' || data.NomePai == null)
        $(".checkValidation ul").append("<li>Nome do Pai</li>");
    if (data.IdEstado == "" || data.IdEstado == '' || data.IdEstado == null)
        $(".checkValidation ul").append("<li>Estado</li>");
    if (data.IdCidade == "" || data.IdCidade == '' || data.IdCidade == null)
        $(".checkValidation ul").append("<li>Cidade</li>");
    if (data.Endereco == "" || data.Endereco == '' || data.Endereco == null)
        $(".checkValidation ul").append("<li>Endereco</li>");
    if (data.Email == "" || data.Email == '' || data.Endereco == null)
        $(".checkValidation ul").append("<li>Email</li>");
}

function mostraAlertBoxCreate(mensagem) {
    $("#create-candidato-alertbox .message").html(mensagem);
    $("#create-candidato-alertbox").slideDown();
}

function enviaFormulario() {
    $(".create-candidato-box").slideUp();

    url = '/Candidatos/CreateOrEdit';
    var data = $('#FormCreateCandidato').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    verificaCampos(data);
    if ($(".checkValidation ul li").length > 0) {
        mostraAlertBoxCreate("Os seguintes campos são obrigatórios:");
        return false;
    }

    data.experiencias = experiencias;

    $.post({
        url: url,
        data: { viewmodel: data },
        success: function (data) {
            if (data.sucesso) {
                $('.modal-create-candidato').modal('hide');
                $("#candidato-successbox .message").html(data.texto)
                $("#candidato-successbox").slideDown();
                listaCandidatos($(".page-item.active a").html() - 1); //index pagina selecionada
            } else {
                mostraAlertBoxCreate(data.texto);
            }
        }
    });
}

function formEdit(candidato) {
    $("#FormCreateCandidato input[name='Id']").val(candidato.Id);
    $("#FormCreateCandidato input[name='Nome']").val(candidato.Nome);
    $("#FormCreateCandidato input[name='DataNascimento']").val(candidato.DataNascimento);
    $("#FormCreateCandidato select[name='Sexo']").val(candidato.Sexo);
    $("#FormCreateCandidato input[name='Nacionalidade']").val(candidato.Nacionalidade);
    $("#FormCreateCandidato input[name='NomeMae']").val(candidato.NomeMae);
    $("#FormCreateCandidato input[name='NomePai']").val(candidato.NomePai);
    $("#FormCreateCandidato input[name='Endereco']").val(candidato.Endereco);
    $("#FormCreateCandidato select[name='IdEstado']").val(candidato.IdEstado);

    listaCidades("#cidades", candidato.IdEstado, function () {
        $("#FormCreateCandidato select[name='IdCidade']").val(candidato.IdCidade);
    });

    $("#FormCreateCandidato input[name='Email']").val(candidato.Email);
    $("#FormCreateCandidato input[name='Telefone']").val(candidato.Telefone);
    $("#FormCreateCandidato input[name='Celular']").val(candidato.Celular);

    listaExperienciasTela(candidato.Experiencias);

}

function zeraExperiencias() {
    experiencias = [];
    $("#experiencia #experienciasNovas").html("");
    $("#experiencia #experienciasSalvas").html("");
    $("#experiencia #experienciasNovas")
        .append($("<tr>")
            .append($("<td>").attr("colspan", "4").addClass("text-center").html("Nenhuma experiência cadastrada.")));
}

function listaExperienciasTela(experiencias) {
    if (experiencias.length != 0)
        $("#experiencia #experienciasNovas").html("");

    $("#experiencia #experienciasSalvas").html("");
    experiencias.forEach(function (item) {
        $("#experiencia #experienciasSalvas")
            .append($("<tr>").addClass("")
                .append($("<td>").addClass("width25 align-middle").html(item.Profissao))
                .append($("<td>").addClass("width30 align-middle").html(item.Empresa))
                .append($("<td>").addClass("width15 text-center align-middle").html(item.Data))
                .append($("<td>").addClass("width10 text-center align-middle").html(
                    "\
                    <a href='#/' class='selecionaExperiencia btnEditExperiencia' value=" + item.Id + " onclick='limpaFormularioExperiencia()' data-toggle='modal' data-target='.modal-adicionar-experiencia'> <img src='/icon/edit.png' /></a > \
                    <a href='#/' class='selecionaExperiencia btnDeletaExperiencia' value=" + item.Id + " onclick='limpaFormularioExperiencia()' data-toggle='modal' data-target='.modal-delete-experiencia'><img src='/icon/delete.png' /></a>\
                    "
                )))
    });

    $(".selecionaExperiencia").click(function () {
        var id = $(this).attr("value");
        var url = "/Experiencia/BuscaPorId";
        var acaoDeletar = $(this).hasClass("btnDeletaExperiencia");

        $.get({
            url: url,
            data: { idCandidato: id },
            success: function (data) {
                //verifica qual funcao de AdicionarExperiencia irá chamar
                if (acaoDeletar)
                    confirmacaoDeleteExperiencia(data);
                else
                    formEditExperiencia(data);
            }
        });
    });

    mostrarNovasExperienciaTela(); //mostra as novas experiencias, caso tenha
}