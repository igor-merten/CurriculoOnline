
$(document).ready(function () {

    listaEstados();

    $("#estados").change(function () {
        listaCidades(this.value);
    });

    $("#FormCreateCandidato").submit(function () { enviaFormulario(this.value); return false; });
    $("#close-create-alert-box").click(function () { $("#create-candidato-alertbox").slideUp(); });

});

function listaEstados() {
    var url = "/Candidatos/ListaEstados";
    $.get(url, function (data) {
        $("#estados").html("<option value=''></option>");
        $.each(data, function (i, item) {
            $('#estados').append($('<option>', {
                value: item.id,
                text: item.uf
            }));
        });
    });
}

function listaCidades(idEstado) {
    var url = "/Candidatos/ListaCidades";
    $.get(url, { idEstado: idEstado }, function (data) {
        $("#cidades").html("<option value=''></option>");
        $.each(data, function (i, item) {
            $('#cidades').append($('<option>', {
                value: item.id,
                text: item.nome
            }));
        });
    });
}

function limpaFormulario() {
    $(".alert").hide();
    $("#FormCreateCandidato").each(function () {
        this.reset();
        $("#cidades").html("<option value=''></option>");
    })
}

function validaCampos(data) {
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

    url = '/Candidatos/Create';
    var data = $('#FormCreateCandidato').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});
    console.log(data);
    validaCampos(data);
    if ($(".checkValidation ul li").length > 0) {
        console.log($(".checkValidation ul li").length);
        mostraAlertBoxCreate("Os seguintes campos são obrigatórios:");
        return false;
    }

    $.post({
        url: url,
        data: { viewmodel: data },
        success: function (data) {
            if (data.sucesso) {
                $('.modal-create-candidato').modal('hide');
                $("#candidato-successbox .message").html(data.texto)
                $("#candidato-successbox").slideDown();
            } else {
                console.log(0)
                mostraAlertBoxCreate(data.texto);
            }
        }
    });
}
