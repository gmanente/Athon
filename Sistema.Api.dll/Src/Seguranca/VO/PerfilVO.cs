namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class PerfilVO : AbstractVO
    {
        public string Descricao { get; set; }
        public bool? Ativar { get; set; }

        public string Regra { get; set; }


        public PerfilVO()
        {

        }

    }
}