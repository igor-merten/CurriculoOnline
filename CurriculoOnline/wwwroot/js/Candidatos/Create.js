
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
                text: item.nome
            }));
        });
    });
}

function listaCidades(idEstado, _callback) {
    var url = "/Candidatos/ListaCidades";
    $.get(url, { idEstado: idEstado }, function (data) {
        $("#cidades").html("<option value=''></option>");
        $.each(data, function (i, item) {
            $('#cidades').append($('<option>', {
                value: item.id,
                text: item.nome,
            }));
        });
        if (_callback)
            _callback();
    });
   
}

function limpaFormulario() {
    $(".alert").slideUp();
    $("#FormCreateCandidato input[name='Id']").val("");
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

    url = '/Candidatos/CreateOrEdit';
    var data = $('#FormCreateCandidato').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    validaCampos(data);
    if ($(".checkValidation ul li").length > 0) {
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

    listaCidades(candidato.IdEstado, function () {
        $("#FormCreateCandidato select[name='IdCidade']").val(candidato.IdCidade);
    });

    $("#FormCreateCandidato input[name='Email']").val(candidato.Email);
    $("#FormCreateCandidato input[name='Telefone']").val(candidato.Telefone);
    $("#FormCreateCandidato input[name='Celular']").val(candidato.Celular);

}