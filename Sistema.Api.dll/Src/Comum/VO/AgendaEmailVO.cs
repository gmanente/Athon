namespace Sistema.Api.dll.Src.Comum.VO
{
    public class AgendaEmailVO : AbstractVO
    {
        public string Email { get; set; }
        public string Parametros { get; set; }
        public System.DateTime DataHoraAgendamento { get; set; }

    }
}