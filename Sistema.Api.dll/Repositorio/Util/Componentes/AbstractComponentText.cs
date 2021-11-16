namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public abstract class AbstractComponentText : AbstractComponetInput
    {
        public string Placeholder { get; set; }

        protected AbstractComponentText()
        {
        }

        protected AbstractComponentText(string[] paramters)
            : base(paramters)
        {
            Placeholder = "Preencha o campo";
            foreach (string p in paramters)
            {


                if (GetRecord(p).Param.ToLower() == "Placeholder".ToLower())
                {
                    Placeholder = GetRecord(p).Value;
                    break;
                }


            }
        }
    }
}
