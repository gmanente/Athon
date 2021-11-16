namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Check : AbstractCheckRadio
    {
        public Check() 
            : base()
        {
        }

        public Check(string[] paramters)
            : base(paramters)
        {
        }

        public override string ToString()
        {
            if (IsInLine)
            {
                SetRadioOrCheckInline("checkbox");
            }
            else
            {
                SetRadioOrCheck("checkbox");
            }

            return base.ToString();
        }
    }
}
