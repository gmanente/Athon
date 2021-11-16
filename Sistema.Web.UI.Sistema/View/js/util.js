
//Autenticar Funções do Botao
function AutenticarRFBtn() {
    var sit = getSessionStorage("DisciplinaSituacao");
    if (sit != "Liberada" && sit != "Em Construção") {
        $(".item").remove();
    }
}