using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Relatorio.Form
{
    public class BolsaIniciacaoCientificaFormularioTemplate : FormularioModalTemplate
    {
        // Inputs Usuario Cadastro
        public Hidden HiddenUsuario { get; set; }
        public InputText NomeUsuarioInputText { get; set; }
        public InputText DataOperacaoInputText { get; set; }

        // Informações da Anulo
        public Hidden HiddenAluno { get; set; }
        public InputText EmpresaConveniadaInputText { get; set; }
        public InputText GestorInputText { get; set; }
        public InputText ConvenioInputText { get; set; }
        public SelectField IncideSelectField { get; set; }
        public InputText MatriculaAlunoInputText { get; set; }
        public InputText NomeAlunoInputText { get; set; }
        public InputText DescricaoCursoInputText { get; set; }
        public InputText ModalidadeInputText { get; set; }
        public InputText TurnoInputText { get; set; }
        public InputText TurmaInputText { get; set; }
        public InputText SerieInputText { get; set; }
        public InputText PeriodoLetivoInputText { get; set; }
        public InputText SituacaoAcademicaInputText { get; set; }

        // Informacoes de % de bolsas
        public InputText PercentualNBBInputText { get; set; }
        public InputText PercentualBolFunInputText { get; set; }
        public InputText PercentualProuniInputText { get; set; }
        public InputText PercentualCOVInputText { get; set; }
        public InputText ValorLiberadoInputText { get; set; }

        // Informações do convenio
        public Hidden HiddenConvenioEmpresa { get; set; }
        public Check AlunoFiesCheck { get; set; }
        public InputNumber QuantidadeParcelaInputNumber { get; set; }
        public DatePicker DataInicioDatePicker { get; set; }
        public InputText ValorBolsaInputText { get; set; }
        public DatePicker DataTerminoDatePicker { get; set; }
        public Check SituacaoCheck { get; set; }

        public BolsaIniciacaoCientificaFormularioTemplate()
            : base()
        {
            HiddenUsuario = new Hidden();
            NomeUsuarioInputText = new InputText();
            DataOperacaoInputText = new InputText();

            HiddenAluno = new Hidden();
            EmpresaConveniadaInputText = new InputText();
            GestorInputText = new InputText();
            ConvenioInputText = new InputText();
            IncideSelectField = new SelectField();
            MatriculaAlunoInputText = new InputText();
            NomeAlunoInputText = new InputText();
            DescricaoCursoInputText = new InputText();
            ModalidadeInputText = new InputText();
            TurnoInputText = new InputText();
            TurmaInputText = new InputText();
            SerieInputText = new InputText();
            PeriodoLetivoInputText = new InputText();
            SituacaoAcademicaInputText = new InputText();

            PercentualNBBInputText = new InputText();
            PercentualBolFunInputText = new InputText();
            PercentualProuniInputText = new InputText();
            PercentualCOVInputText = new InputText();
            ValorLiberadoInputText = new InputText();

            HiddenConvenioEmpresa = new Hidden();
            AlunoFiesCheck = new Check();
            QuantidadeParcelaInputNumber = new InputNumber();
            DataInicioDatePicker = new DatePicker();
            ValorBolsaInputText = new InputText();
            DataTerminoDatePicker = new DatePicker();
            SituacaoCheck = new Check();
        }

        public void SetBody()
        {
            var dvPanel = new Div();

            var dvPanelHeading = new Div();

            var dvPanelBody = new Div();

            var dvLinha = new Div();

            var dvCol = new Div();

            var head = new Heading();

            ModalDialogStyle = "width:50%;";

            #region Panel DADOS CONVENIO
            //dvPanel = new Div()
            //{
            //    Class = "panel panel-default",
            //    Id = "panelDadosConvenio"
            //};

            //dvPanelHeading = new Div()
            //{
            //    Class = "panel-heading"
            //};

            //dvPanelBody = new Div()
            //{
            //    Class = "panel-body"
            //};

            //#region Linha01 - Head Convenio
            //head = new Heading()
            //{
            //    HeadingType = HeadingType.H4,
            //    Text = "Identificação do Convênio"
            //};

            //dvPanelHeading.AddComponentContent(head);
            //#endregion

            //#region Linha02
            //dvLinha = new Div()
            //{
            //    Class = "row"
            //};

            //dvCol = new Div()
            //{
            //    Class = "col-md-4"
            //};

            ////Inputtext Empresa Conveniada
            //EmpresaConveniadaInputText.Id = "EmpresaConveniada";
            //EmpresaConveniadaInputText.Name = "EmpresaConveniada";
            //EmpresaConveniadaInputText.LabelText = "Empresa Conveniada";
            //EmpresaConveniadaInputText.Class = "form-control w5";
            //EmpresaConveniadaInputText.Disabled = true;
            //EmpresaConveniadaInputText.Validate = "required: true, rangelength:[1,150]";
            //dvCol.AddComponentContent(EmpresaConveniadaInputText);

            //dvLinha.AddComponentContent(dvCol);

            //dvCol = new Div()
            //{
            //    Class = "col-md-4"
            //};

            ////SelectField Convenio
            //ConvenioInputText.Id = "Convenio";
            //ConvenioInputText.Name = "Convenio";
            //ConvenioInputText.LabelText = "Convênio Vinculado";
            //ConvenioInputText.Class = "form-control w5";
            //ConvenioInputText.Disabled = true;
            //ConvenioInputText.Validate = "required: true, rangelength:[1,150]";
            //dvCol.AddComponentContent(ConvenioInputText);

            //dvLinha.AddComponentContent(dvCol);

            //dvCol = new Div()
            //{
            //    Class = "col-md-4"
            //};

            ////SelectField Gestor
            //GestorInputText.Id = "Gestor";
            //GestorInputText.Name = "Gestor";
            //GestorInputText.LabelText = "Gestor do Convênio";
            //GestorInputText.Class = "form-control w5";
            //GestorInputText.Disabled = true;
            //GestorInputText.Validate = "required: true, rangelength:[1,150]";
            //dvCol.AddComponentContent(GestorInputText);

            //dvLinha.AddComponentContent(dvCol);
            //dvPanelBody.AddComponentContent(dvLinha);
            //#endregion

            //dvPanel.AddComponentContent(dvPanelHeading);
            //dvPanel.AddComponentContent(dvPanelBody);

            //AddComponentBody(dvPanel);
            #endregion

            #region Panel DADOS DO ALUNO
            dvPanel = new Div()
            {
                Class = "panel panel-default"
            };

            dvPanelHeading = new Div()
            {
                Class = "panel-heading"
            };

            dvPanelBody = new Div()
            {
                Class = "panel-body"
            };

            #region Head Dados Aluno
            head = new Heading()
            {
                HeadingType = HeadingType.H4,
                Text = "Identificação do Aluno"
            };

            dvPanelHeading.AddComponentContent(head);
            #endregion

            #region linha01
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-12"
            };

            //Inputtext Matricula Aluno
            MatriculaAlunoInputText.Id = "MatriculaAluno";
            MatriculaAlunoInputText.Name = "MatriculaAluno";
            MatriculaAlunoInputText.LabelText = "Matricula Aluno";
            MatriculaAlunoInputText.Class = "form-control w5";
            MatriculaAlunoInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(MatriculaAlunoInputText);

            dvLinha.AddComponentContent(dvCol);
            dvPanelBody.AddComponentContent(dvLinha);
            #endregion

            #region Linha02
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-6"
            };

            //Inputtext Matricula Aluno
            NomeAlunoInputText.Id = "NomeAluno";
            NomeAlunoInputText.Name = "NomeAluno";
            NomeAlunoInputText.LabelText = "Nome Aluno";
            NomeAlunoInputText.Class = "form-control w5";
            NomeAlunoInputText.Disabled = true;
            NomeAlunoInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(NomeAlunoInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-6"
            };

            //Inputtext Descricao Curso
            DescricaoCursoInputText.Id = "DescricaoCurso";
            DescricaoCursoInputText.Name = "DescricaoCurso";
            DescricaoCursoInputText.LabelText = "Descrição do Curso";
            DescricaoCursoInputText.Class = "form-control w5";
            DescricaoCursoInputText.Disabled = true;
            DescricaoCursoInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(DescricaoCursoInputText);

            dvLinha.AddComponentContent(dvCol);
            dvPanelBody.AddComponentContent(dvLinha);
            #endregion

            #region Linha03
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Inputtext Modalidade
            ModalidadeInputText.Id = "Modalidade";
            ModalidadeInputText.Name = "Modalidade";
            ModalidadeInputText.LabelText = "Modalidade";
            ModalidadeInputText.Class = "form-control w5";
            ModalidadeInputText.Disabled = true;
            ModalidadeInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(ModalidadeInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Inputtext Turno
            TurnoInputText.Id = "Turno";
            TurnoInputText.Name = "Turno";
            TurnoInputText.LabelText = "Turno";
            TurnoInputText.Class = "form-control w5";
            TurnoInputText.Disabled = true;
            TurnoInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(TurnoInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Inputtext Turma
            TurmaInputText.Id = "Turma";
            TurmaInputText.Name = "Turma";
            TurmaInputText.LabelText = "Turma";
            TurmaInputText.Class = "form-control w5";
            TurmaInputText.Disabled = true;
            TurmaInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(TurmaInputText);

            dvLinha.AddComponentContent(dvCol);
            dvPanelBody.AddComponentContent(dvLinha);
            #endregion

            #region Linha04
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Inputtext Serie
            SerieInputText.Id = "Serie";
            SerieInputText.Name = "Serie";
            SerieInputText.LabelText = "Série";
            SerieInputText.Class = "form-control w5";
            SerieInputText.Disabled = true;
            SerieInputText.Validate = "required: true, rangelength:[1,50]";
            dvCol.AddComponentContent(SerieInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Inputtext PeriodoLetivo
            PeriodoLetivoInputText.Id = "PeriodoLetivo";
            PeriodoLetivoInputText.Name = "PeriodoLetivo";
            PeriodoLetivoInputText.LabelText = "Período Letivo";
            PeriodoLetivoInputText.Class = "form-control w5";
            PeriodoLetivoInputText.Disabled = true;
            PeriodoLetivoInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(PeriodoLetivoInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Inputtext Turma
            SituacaoAcademicaInputText.Id = "SituacaoAcademica";
            SituacaoAcademicaInputText.Name = "SituacaoAcademica";
            SituacaoAcademicaInputText.LabelText = "Situação Acadêmica";
            SituacaoAcademicaInputText.Class = "form-control w5";
            SituacaoAcademicaInputText.Disabled = true;
            SituacaoAcademicaInputText.Validate = "required: true, rangelength:[1,150]";
            dvCol.AddComponentContent(SituacaoAcademicaInputText);

            dvLinha.AddComponentContent(dvCol);
            dvPanelBody.AddComponentContent(dvLinha);
            #endregion

            dvPanel.AddComponentContent(dvPanelHeading);
            dvPanel.AddComponentContent(dvPanelBody);

            AddComponentBody(dvPanel);

            #endregion

            #region Panel DADOS BIC
            dvPanel = new Div()
            {
                Class = "panel panel-default"
            };

            dvPanelHeading = new Div()
            {
                Class = "panel-heading"
            };

            dvPanelBody = new Div()
            {
                Class = "panel-body"
            };

            #region Linha01 - Head Regra

            head = new Heading()
            {
                HeadingType = HeadingType.H4,
                Text = "Dados da Bolsa de Iniciação Científica"
            };

            dvPanelHeading.AddComponentContent(head);
            #endregion

            #region Linha01
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-3"
            };

            // Percentual NBB Inputtext
            PercentualNBBInputText.Id = "PercentualNBB";
            PercentualNBBInputText.Name = "PercentualNBB";
            PercentualNBBInputText.LabelText = "NBB";
            PercentualNBBInputText.LabelFor = "NBB";
            PercentualNBBInputText.LabelEnabled = true;
            PercentualNBBInputText.InputGroupAddon = true;
            PercentualNBBInputText.Simbolo = "%";
            PercentualNBBInputText.Class = "form-control w5";
            PercentualNBBInputText.Disabled = true;
            PercentualNBBInputText.Validate = "rangelength:[1,100]";
            dvCol.AddComponentContent(PercentualNBBInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-3"
            };

            // ValorBolsa Inputtext
            PercentualBolFunInputText.Id = "PercentualBolFun";
            PercentualBolFunInputText.Name = "PercentualBolFun";
            PercentualBolFunInputText.LabelText = "Bolsa Funcionário";
            PercentualBolFunInputText.LabelFor = "Bolsa Funcionário";
            PercentualBolFunInputText.LabelEnabled = true;
            PercentualBolFunInputText.InputGroupAddon = true;
            PercentualBolFunInputText.Simbolo = "%";
            PercentualBolFunInputText.Class = "form-control w5";
            PercentualBolFunInputText.Disabled = true;
            PercentualBolFunInputText.Validate = "rangelength:[1,100]";
            dvCol.AddComponentContent(PercentualBolFunInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-3"
            };

            // ValorBolsa Inputtext
            PercentualProuniInputText.Id = "PercentualProuni";
            PercentualProuniInputText.Name = "PercentualProuni";
            PercentualProuniInputText.LabelText = "Prouni";
            PercentualProuniInputText.LabelFor = "Prouni";
            PercentualProuniInputText.LabelEnabled = true;
            PercentualProuniInputText.InputGroupAddon = true;
            PercentualProuniInputText.Simbolo = "%";
            PercentualProuniInputText.Class = "form-control w5";
            PercentualProuniInputText.Disabled = true;
            PercentualProuniInputText.Validate = "rangelength:[1,100]";
            dvCol.AddComponentContent(PercentualProuniInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-3"
            };

            // ValorBolsa Inputtext
            PercentualCOVInputText.Id = "PercentualCOV";
            PercentualCOVInputText.Name = "PercentualCOV";
            PercentualCOVInputText.LabelText = "Outros Convênios";
            PercentualCOVInputText.LabelFor = "Outros Convênios";
            PercentualCOVInputText.LabelEnabled = true;
            PercentualCOVInputText.InputGroupAddon = true;
            PercentualCOVInputText.Simbolo = "%";
            PercentualCOVInputText.Class = "form-control w5";
            PercentualCOVInputText.Disabled = true;
            PercentualCOVInputText.Validate = "rangelength:[1,100]";
            dvCol.AddComponentContent(PercentualCOVInputText);

            dvLinha.AddComponentContent(dvCol);

            Div divInfo = new Div()
            {
                Id = "divInfo",
                Name = "divInfo",
                Class = "alert alert-info"
            };

            head = new Heading()
            {
                HeadingType = HeadingType.H4,
                Text = "Percentual de Bolsas Ativas"
            };

            divInfo.AddComponentContent(head);

            divInfo.AddComponentContent(dvLinha);

            dvPanelBody.AddComponentContent(divInfo);
            #endregion

            #region Linha02
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Data início
            DataInicioDatePicker.Id = "DataInicio";
            DataInicioDatePicker.Name = "DataInicio";
            DataInicioDatePicker.LabelText = "Data de Início";
            DataInicioDatePicker.Class = "form-control w5";
            DataInicioDatePicker.Validate = "required: true, dateBR:true";
            dvCol.AddComponentContent(DataInicioDatePicker);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            //Data fim
            DataTerminoDatePicker.Id = "DataTermino";
            DataTerminoDatePicker.Name = "DataTermino";
            DataTerminoDatePicker.LabelText = "Data Término";
            DataTerminoDatePicker.Class = "form-control w5";
            DataTerminoDatePicker.Validate = "required: true, dateBR:true";
            dvCol.AddComponentContent(DataTerminoDatePicker);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-4"
            };

            // Quantidade de Parcelas Inputnumber
            QuantidadeParcelaInputNumber.Id = "QuantidadeParcela";
            QuantidadeParcelaInputNumber.Name = "QuantidadeParcela";
            QuantidadeParcelaInputNumber.LabelText = "Quantidade de Parcelas";
            QuantidadeParcelaInputNumber.Class = "form-control w5";
            QuantidadeParcelaInputNumber.Validate = "required:true,rangelength:[1,100]";
            dvCol.AddComponentContent(QuantidadeParcelaInputNumber);

            dvLinha.AddComponentContent(dvCol);
            dvPanelBody.AddComponentContent(dvLinha);
            #endregion

            #region Linha03
            dvLinha = new Div()
            {
                Class = "row"
            };

            dvCol = new Div()
            {
                Class = "col-md-3"
            };

            // ValorBolsa Inputtext
            ValorLiberadoInputText.Id = "ValorLiberado";
            ValorLiberadoInputText.Name = "ValorLiberado";
            ValorLiberadoInputText.LabelText = "Valor Liberado";
            ValorLiberadoInputText.LabelFor = "Valor Liberado";
            ValorLiberadoInputText.LabelEnabled = true;
            ValorLiberadoInputText.InputGroupAddon = true;
            ValorLiberadoInputText.Simbolo = "R$";
            ValorLiberadoInputText.Class = "form-control w5";
            ValorLiberadoInputText.Disabled = true;
            ValorLiberadoInputText.Validate = "rangelength:[1,100]";
            dvCol.AddComponentContent(ValorLiberadoInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-3"
            };

            // ValorBolsa Inputtext
            ValorBolsaInputText.Id = "ValorBolsa";
            ValorBolsaInputText.Name = "ValorBolsa";
            ValorBolsaInputText.LabelText = "Valor da Bolsa";
            ValorBolsaInputText.LabelFor = "Valor da Bolsa";
            ValorBolsaInputText.LabelEnabled = true;
            ValorBolsaInputText.InputGroupAddon = true;
            ValorBolsaInputText.Simbolo = "R$";
            ValorBolsaInputText.Class = "form-control w5";
            ValorBolsaInputText.Validate = "required: true,rangelength:[1,100]";
            dvCol.AddComponentContent(ValorBolsaInputText);

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-3",
                Style = "margin-top:20px;"
            };

            // Pontualidade Check
            AlunoFiesCheck.Id = "AlunoFies";
            AlunoFiesCheck.Name = "AlunoFies";
            AlunoFiesCheck.LabelFor = "AlunoFies";
            AlunoFiesCheck.Text = "Aluno Fies?";
            dvCol.AddComponentContent(AlunoFiesCheck);            

            dvLinha.AddComponentContent(dvCol);

            dvCol = new Div()
            {
                Class = "col-md-3",
                Style = "margin-top:20px;"
            };

            //Check Situacao
            SituacaoCheck.Id = "Situacao";
            SituacaoCheck.Name = "Situacao";
            SituacaoCheck.LabelFor = "Situacao";
            SituacaoCheck.Text = "Ativo?";
            dvCol.AddComponentContent(SituacaoCheck);

            dvLinha.AddComponentContent(dvCol);
            dvPanelBody.AddComponentContent(dvLinha);
            #endregion

            dvPanel.AddComponentContent(dvPanelHeading);
            dvPanel.AddComponentContent(dvPanelBody);

            AddComponentBody(dvPanel);
            #endregion

            //Usuario
            HiddenUsuario.Id = "IdUsuarioCadastro";
            HiddenUsuario.Name = "IdUsuarioCadastro";
            AddComponentBody(HiddenUsuario);

            //AlunoSemestre
            HiddenConvenioEmpresa.Id = "IdConvenioEmpresa";
            HiddenConvenioEmpresa.Name = "IdConvenioEmpresa";
            AddComponentBody(HiddenConvenioEmpresa);

            //AlunoSemestre
            HiddenAluno.Id = "IdAluno";
            HiddenAluno.Name = "IdAluno";
            AddComponentBody(HiddenAluno);

            //Set footer
            SetFooter();
        }

        //ToString
        public override string ToString()
        {
            SetBody();
            return base.ToString();
        }

        //Render
        public virtual void Render()
        {
            SetBody();
            base.Render();
        }
    }
}
