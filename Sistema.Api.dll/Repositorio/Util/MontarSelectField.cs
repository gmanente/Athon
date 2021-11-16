using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class MontarSelectField
    {
        //MontarCampus
        public static void MontarCampus(List<CampusVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o campus",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Nome,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarBloco
        public static void MontarBloco(List<BlocoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Bloco",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Montar Sexo
        public static void MontarSexo(List<SexoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Sexo",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false,
                        InjectDataAttr = "data-sigla='" + objVO.Sigla + "'"
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar Cor
        public static void MontarCor(List<CorVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione Cor / Raça",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar Pne
        public static void MontarPne(List<PneVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione Pne",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarClero
        public static void MontarClero(List<CleroVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Clero",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarNacionalidade
        public static void MontarNacionalidade(List<NacionalidadeVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione a nacionalidade",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarEstadoCivil
        public static void MontarEstadoCivil(List<EstadoCivilVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Estado Civil",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar MontarEstado
        public static void MontarEstado(List<EstadoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Estado",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false,
                        InjectDataAttr = "data-sigla='" + objVO.Sigla + "'"
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar Cidade
        public static void MontarCidade(List<CidadeVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione a Cidade",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Nome,
                        Selected = (id == objVO.Id) ? true : false,
                        InjectDataAttr = "data-nome='" + objVO.Nome + "'"
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar MontarCidadeEstado
        public static void MontarCidadeEstado(List<CidadeVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione a Cidade",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Nome + "(" + objVO.Estado.Sigla + ")",
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar Pais
        public static void MontarPais(List<PaisVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o País",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarDisciplina
        public static void MontarDiaSemana(List<DiaSemanaVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Dia da semana",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Nome,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarDisciplina
        public static void MontarPeriodoDia(List<PeriodoDiaVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o turno",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}