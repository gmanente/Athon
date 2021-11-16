namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public abstract class AbstractComponetInput : AbstractComponent
    {
        public string Value { get; set; }

        protected AbstractComponetInput()
        {

        }
        protected AbstractComponetInput(string[] paramters)
            : base(paramters)
        {
            foreach (string p in paramters)
            {
                if (GetRecord(p).Param.ToLower() == "Value".ToLower())
                {
                    Value = GetRecord(p).Value;
                    break;
                }
           
            }
        }
    }
}
