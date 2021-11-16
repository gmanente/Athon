using System.Text;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{

    public abstract class AbstractComponent
    {
        public string ContainerClass { get; set; }
        public string ContainerId { get; set; }
        public string ContainerStyle { get; set; }
        public string Class { get; set; }
        public string InjectDataAttr { get; set; }
        public string Validate { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Style { get; set; }
        public string LabelText { get; set; }
        public string StrongText { get; set; }
        public bool Disabled { get; set; }
        public bool Readonly { get; set; }
        public string Onload { get; set; }
        public string Onclick { get; set; }
        public string Onblur { get; set; }
        public string LabelFor { get; set; }
        public string DataToggle { get; set; }
        public string DataTarget { get; set; }
        protected StringBuilder SbComponent { get; set; }

        protected AbstractComponent()
        {
            ContainerId = "";
            ContainerClass = "form-group";
            ContainerStyle = "";
            Class = "";
            LabelText = "Campo";
        }

        protected AbstractComponent(string[] paramters)
        {
            ContainerClass = "form-group";
            Class = "";
            LabelText = "Campo";


            string strFor = "";
            foreach (string p in paramters)
            {

                if (GetRecord(p).Param.ToLower() == "ContainerClass".ToLower())
                {
                    ContainerClass = GetRecord(p).Value;
                }

                if (GetRecord(p).Param.ToLower() == "LabelText".ToLower())
                {
                    LabelText = GetRecord(p).Value;
                }

                if (GetRecord(p).Param.ToLower() == "StrongText".ToLower())
                {
                    StrongText = GetRecord(p).Value;
                }

                if (GetRecord(p).Param.ToLower() == "Class".ToLower())
                {
                    Class = GetRecord(p).Value + " ";
                }
                if (GetRecord(p).Param.ToLower() == "Name".ToLower())
                {
                    LabelFor = GetRecord(p).Value;
                    Name = GetRecord(p).Value;
                }

                if (GetRecord(p).Param.ToLower() == "Id".ToLower())
                {
                    Id = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "Data-Toggle".ToLower())
                {
                    DataToggle = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "Data-Target".ToLower())
                {
                    DataTarget = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "Style".ToLower())
                {
                    Style = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "InjectDataAttr".ToLower())
                {
                    InjectDataAttr = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "Onload".ToLower())
                {
                    Onload = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "Onclick".ToLower())
                {
                    Onclick = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "Onblur".ToLower())
                {
                    Onblur = GetRecord(p).Value;
                }
                if (GetRecord(p).Param.ToLower() == "InputValidate".ToLower())
                {
                    Validate = GetRecord(p).Value;                    
                }
            }
        }

        protected Record GetRecord(string str)
        {
            string[] arrStr = str.Split(':');
            var r = new Record()
            {
                Param = arrStr[0],
                Value = arrStr[1]
            };

            return r;
        }

        protected class Record
        {
            public string Param { get; set; }
            public string Value { get; set; }
        }

        public override string ToString()
        {
            return SbComponent.ToString();
        }

        public void Render()
        {
            HttpContext.Current.Response.Write(SbComponent.ToString());
        }
        
    }
    
}
