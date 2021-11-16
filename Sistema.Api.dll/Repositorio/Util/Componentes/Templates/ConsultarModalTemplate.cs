using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.BE;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util.Componentes.Templates
{
    public class ConsultarModalTemplate : FormularioModalTemplate
    {
        public Div RowContainer { get; set; }
        public Div Col { get; set; }
        public Div Col2 { get; set; }
        public Div Col3 { get; set; }
        public SelectField StringOptions { get; set; }
        public SelectField IntOptions { get; set; }
        public DatePicker Date1 { get; set; }
        public DatePicker Date2 { get; set; }
        public InputText Field1 { get; set; }
        public ConsultaVO FiltroVo { get; set; }


        public ConsultarModalTemplate()
            : base()
        {
            ModalHeaderStyle = "background-color:#444;color:#666;";
            ModalFooterStyle = "background-color:#444;color:#666;";

            RowContainer = new Div();
            Col = new Div();
            Col2 = new Div();
            Col3 = new Div();
            StringOptions = new SelectField();
            IntOptions = new SelectField();
            Date1 = new DatePicker();
            Date2 = new DatePicker();
            Field1 = new InputText();
            FiltroVo = new ConsultaVO();

            MountStringOptions();
            MountIntOptions();
        }

        // Check FieldType
        public void CheckFieldType(List<ConsultaCampoVO> lstFiltroCampoVo)
        {
            if (lstFiltroCampoVo != null)
            {
                foreach (var filtroCampoVo in lstFiltroCampoVo)
                {
                    string[] arr = filtroCampoVo.TipoCampo.Split(new char[] { '[', ']' });
                    if (arr[0].ToLower() == "string" && filtroCampoVo.Ativar)
                    {
                        MountStringField(filtroCampoVo);
                    }
                    else if (arr[0].ToLower() == "int" && filtroCampoVo.Ativar)
                    {
                        MountIntField(filtroCampoVo);
                    }
                    else if (arr[0].ToLower() == "date" && filtroCampoVo.Ativar)
                    {
                        MountDateField(filtroCampoVo);
                    }
                    else if (arr[0].ToLower() == "combo" && filtroCampoVo.Ativar)
                    {
                        MountComboField(arr, filtroCampoVo);
                    }
                    else if (arr[0].ToLower() == "bit" && filtroCampoVo.Ativar)
                    {
                        MountBitField(filtroCampoVo);
                    }
                }
            }
        }

        // Mount - IntOptions
        // [Options] ( Contém = 1 | Parcial início = 2 | Parcial fim = 3 | Exato = 4 | Listagem = 5 )
        public void MountIntOptions()
        {
            IntOptions.LabelText = "Parâmetro do filtro";
            var option = new Option();

            //Exato = 4
            option.Text = "Exato";
            option.Value = "4";
            option.InjectDataAttr = "data-char-inicio='' data-char-fim='' data-type='int'";
            IntOptions.AddOption(option);

            //Contém = 1
            option.Text = "Contém";
            option.Value = "1";
            option.InjectDataAttr = "data-char-inicio=\"'%'+ \" data-char-fim=\" +'%'\" data-type='int'";
            IntOptions.AddOption(option);

            //Parcial início = 2
            option.Text = "Parcial início";
            option.Value = "2";
            option.InjectDataAttr = "data-char-inicio='' data-char-fim=\" +'%'\" data-type='int'";
            IntOptions.AddOption(option);

            //Parcial fim = 3
            option.Text = "Parcial fim";
            option.Value = "3";
            option.InjectDataAttr = "data-char-inicio=\"'%'+ \" data-char-fim='' data-type='int'";
            IntOptions.AddOption(option);

            //Listagem = 5
            option.Text = "Listagem";
            option.Value = "5";
            option.InjectDataAttr = "data-char-inicio='' data-char-fim='' data-type='int'";
            IntOptions.AddOption(option);
        }

        // Mount - StringOptions
        // [Options] ( Contém = 1 | Parcial início = 2 | Parcial fim = 3 | Exato = 4 )
        public void MountStringOptions()
        {
            StringOptions.Class = "form-control";
            StringOptions.LabelText = "Parâmetro do filtro";
            var option = new Option();

            //Exato = 4
            option.Text = "Exato";
            option.Value = "4";
            option.InjectDataAttr = "data-char-inicio='' data-char-fim='' data-type='string'";
            StringOptions.AddOption(option);

            //Contém = 1
            option.Text = "Contém";
            option.Value = "1";
            option.InjectDataAttr = "data-char-inicio=\"'%'+ \" data-char-fim=\" +'%'\" data-type='string'";
            StringOptions.AddOption(option);

            //Parcial início = 2
            option.Text = "Parcial início";
            option.Value = "2";
            option.InjectDataAttr = "data-char-inicio='' data-char-fim=\" +'%'\" data-type='string'";
            StringOptions.AddOption(option);

            //Parcial fim = 3
            option.Text = "Parcial fim";
            option.Value = "3";
            option.InjectDataAttr = "data-char-inicio=\"'%'+ \" data-char-fim='' data-type='string'";
            StringOptions.AddOption(option);
        }

        // Mount - DateField
        public void MountDateField(ConsultaCampoVO filtroCampoVo)
        {
            RowContainer = new Div();
            RowContainer.Class = "row";
            Date1 = new DatePicker();
            Date1.Id = filtroCampoVo.NomeCampo.Replace(".", "-").Trim() + "1";
            Date1.Name = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim() + "1";
            Date1.Class = "form-control w5 focusDate txtField";
            Date1.Validate = "rangelength:[10,10],require_from_group: [1,'.txtField']";
            Date1.Placeholder = "Digite a data início";
            Date1.LabelText = filtroCampoVo.DescricaoCampo + " (INÍCIO)";
            Date1.LabelFor = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Date1.InjectDataAttr = "data-type='date'";


            Date2 = new DatePicker();
            Date2.Id = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim() + "2";
            Date2.Name = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim() + "2";
            Date2.Class = "form-control w5 focusDate txtField";
            Date2.Validate = "rangelength:[10,10],require_from_group: [1,'.txtField']";
            Date2.Placeholder = "Digite a data fim focusDate";
            Date2.LabelText = filtroCampoVo.DescricaoCampo + " (FIM)";
            Date2.LabelFor = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Date2.InjectDataAttr = "data-type='date'";

            Col = new Div();
            Col.Class = "col-md-5";
            Col2 = new Div();
            Col2.Class = "col-md-1";
            Col3 = new Div();
            Col3.Class = "col-md-5";

            var p = new P()
            {
                Text = "até",
                Style = "position:relative;top:35px;text-align:center;"
            };

            Col.AddComponentContent(Date1);
            Col2.AddComponentContent(p);
            Col3.AddComponentContent(Date2);

            RowContainer.AddComponentContent(Col);
            RowContainer.AddComponentContent(Col2);
            RowContainer.AddComponentContent(Col3);
            ComponetsBody.Add(RowContainer);
        }

        // Mount - BitField
        public void MountBitField(ConsultaCampoVO filtroCampoVo)
        {
            RowContainer = new Div();
            RowContainer.Class = "row";
            Col = new Div();
            Col.Class = "col-md-8";


            SelectField select = new SelectField()
            {
                Id = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim(),
                Name = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim(),
                LabelText = filtroCampoVo.DescricaoCampo,
                LabelFor = filtroCampoVo.NomeCampo.Replace(".", "-"),
                InjectDataAttr = "data-type='combo'",
                Class = "form-control txtField",
                Validate = "require_from_group: [1,'.txtField']"
            };

            Option opt1 = new Option()
            {
                Value = "",
                Text = "Escolha uma opção"
            };
            select.AddOption(opt1);


            opt1 = new Option()
            {
                Value = "1",
                Text = "Sim"
            };
            select.AddOption(opt1);

            opt1 = new Option()
            {
                Value = "0",
                Text = "Não"
            };
            select.AddOption(opt1);

            Col.AddComponentContent(select);
            RowContainer.AddComponentContent(Col);
            ComponetsBody.Add(RowContainer);

        }

        // Mount - StringField
        public void MountStringField(ConsultaCampoVO filtroCampoVo)
        {
            RowContainer = new Div();
            RowContainer.Class = "row";
            Col = new Div();
            Col.Class = "col-md-8";
            Col2 = new Div();
            Col2.Class = "col-md-4";
            Field1 = new InputText();
            Field1.Id = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Field1.Name = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Field1.LabelText = filtroCampoVo.DescricaoCampo;
            Field1.LabelFor = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Field1.Class = "form-control w5 txtField";
            Field1.Placeholder = "Digite um valor para " + filtroCampoVo.DescricaoCampo;
            Field1.InjectDataAttr = "data-type='string'";
            Field1.Validate = "require_from_group: [1,'.txtField']";


            Col.AddComponentContent(Field1);
            StringOptions.Id = "StringOptions" + filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Replace(" ", "").Trim();
            Col2.AddComponentContent(StringOptions);
            RowContainer.AddComponentContent(Col);
            RowContainer.AddComponentContent(Col2);
            ComponetsBody.Add(RowContainer);
        }

        // Mount - IntField
        public void MountIntField(ConsultaCampoVO filtroCampoVo)
        {
            RowContainer = new Div();
            RowContainer.Class = "row";
            Col = new Div();
            Col.Class = "col-md-8";
            Col2 = new Div();
            Col2.Class = "col-md-4";
            Field1 = new InputText();
            Field1.Id = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Field1.Name = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim();
            Field1.LabelText = filtroCampoVo.DescricaoCampo;
            Field1.LabelFor = filtroCampoVo.NomeCampo.Replace(".", "-");
            Field1.Class = "form-control w5 digits txtField";
            Field1.Placeholder = "Digite um valor para " + filtroCampoVo.DescricaoCampo;
            Field1.InjectDataAttr = "data-type='int'";
            Field1.Validate = "require_from_group: [1,'.txtField']";

            Col.AddComponentContent(Field1);
            IntOptions.Id = "IntOptions" + filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Replace(" ", "").Trim();
            IntOptions.Class = "intoptions form-control";

            Col2.AddComponentContent(IntOptions);
            RowContainer.AddComponentContent(Col);
            RowContainer.AddComponentContent(Col2);
            ComponetsBody.Add(RowContainer);
        }

        // Mount - ComboField
        //
        // IdFiltroCampo  : 3084
        // IdFiltro       : 48
        // NomeCampo      : DBSecretariaAcademica.dbo.UvwAluno.IdCurso
        // DescricaoCampo : Curso
        // TipoCampo      : combo[*codigoReferencia,Descricao,DBAthon.dbo.Curso,ASC]   =>  O asterisco é usado quando é necessário fazer o DISTINCT
        //
        public void MountComboField(string[] strArr, ConsultaCampoVO filtroCampoVo)
        {
            ConsultaBE consultaBE = null;
            SelectField select = null;
            try
            {
                RowContainer = new Div();
                RowContainer.Class = "row";
                Col = new Div();
                Col.Class = "col-md-8";
                consultaBE = new ConsultaBE();
                select = consultaBE.GetSelectField(strArr, filtroCampoVo);
                Col.AddComponentContent(select);
                RowContainer.AddComponentContent(Col);
                ComponetsBody.Add(RowContainer);
            }
            finally
            {
                if (consultaBE != null)
                    consultaBE.FecharConexao();
            }
        }

        public void SetHeader()
        {
            Id = "modal-consultar";
            Titulo = "Consultar";
            Descricao = "Preencha as informações abaixo para realizar a consulta dos registros.";
        }

        public override string ToString()
        {
            SetHeader();
            SetFooter();
            return base.ToString();
        }

        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }
    }

}