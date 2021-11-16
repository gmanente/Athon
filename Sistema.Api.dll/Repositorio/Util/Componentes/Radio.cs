namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Radio : AbstractCheckRadio
    {

        public Radio()
            : base()
        {
        }
        public Radio(string[] paramters)
            : base(paramters)
        {
        }

        public override string ToString()
        {
            if(IsInLine){
                SetRadioOrCheckInline("radio");
            }else{
                SetRadioOrCheck("radio");
            }
            
            return base.ToString();
        }
    }
}
