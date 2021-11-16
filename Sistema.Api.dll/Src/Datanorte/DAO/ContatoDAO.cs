using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.DAO
{
    public class ContatoDAO : AbstractDAO, IDAO<ContatoVO>
    {
        public ContatoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Alterar(ContatoVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public ContatoVO Consultar(ContatoVO objVO)
        {
            throw new NotImplementedException();
        }

        public void Deletar(ContatoVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(ContatoVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<ContatoVO> Listar(ContatoVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContatoVO> Selecionar(ContatoVO objVO, int top = 0)
        {
            ContatoVO ContatoVO = null;

            List<ContatoVO> lstContatoVO = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstContatoVO = new List<ContatoVO>();
                objSbSelect.AppendLine(@"SELECT                                                                                                                                 ");
                objSbSelect.AppendLine(@"       DBAthon.dbo.UvwConsultarContato.Contatos_Id                                                                                     ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Cpf                                                                                             ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Nome                                                                                            ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Sexo                                                                                            ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Nasc                                                                                            ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Nome_Civil                                                                                      ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Nome_Mae                                                                                        ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Nome_Pai                                                                                        ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Cadastro_Id                                                                                     ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Rg                                                                                              ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.RgOrgao                                                                                         ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.RgUF                                                                                            ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Logr_Tipo                                                                                       ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Logr_Nome                                                                                       ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Logr_Numero                                                                                     ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Bairro                                                                                          ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Cep                                                                                             ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Cidade                                                                                          ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.Uf                                                                                              ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.TelefoneTipo                                                                                    ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.TelefoneDDD                                                                                     ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UvwConsultarContato.TelefoneNumero                                                                                  ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.ContatoHistorico.IdContatoHistorico                                                                                 ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.ContatoHistorico.DataOperacao                                                                                       ");
                objSbSelect.AppendLine(@"      ,CONVERT(VARCHAR(15), DBAthon.dbo.ContatoHistorico.DataOperacao, 103) AS DataOperacaoString                                      ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.ContatoHistorico.Idusuario                                                                                          ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.ContatoHistorico.Observacao                                                                                         ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.Usuario.Nome AS UsuarioNome                                                                                         ");                                                                                                                                          
                                                                                                                                                                                
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.UvwConsultarContato                                                                                                 ");

                objSbSelect.AppendLine(@"  LEFT JOIN DBAthon.dbo.ContatoHistorico                                                                                               ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.ContatoHistorico.IdContatoHistorico = (SELECT MAX(HIST.IdContatoHistorico)                                     ");
                objSbSelect.AppendLine(@"                                                                 FROM DBAthon.dbo.ContatoHistorico HIST                                ");
                objSbSelect.AppendLine(@"                                                                WHERE HIST.Contatos_Id = DBAthon.dbo.UvwConsultarContato.Contatos_Id)  ");

                objSbSelect.AppendLine(@"  LEFT JOIN DBAthon.dbo.Usuario                                                                         ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.Usuario.IdUsuario = DBAthon.dbo.ContatoHistorico.Idusuario                      ");


                objSbSelect.AppendLine(@" WHERE 1 = 1 ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    // dddFinal, dddInicial, cidade, uf


                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Contatos_Id = @Contatos_Id ");
                        GetSqlCommand().Parameters.AddWithNullable("Contatos_Id", objVO.Id);
                    }
                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Nome LIKE @Nome + '%'");
                        GetSqlCommand().Parameters.AddWithNullable("Nome", objVO.Nome);
                    }
                    if (!string.IsNullOrEmpty(objVO.Cpf))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Cpf = @Cpf ");
                        GetSqlCommand().Parameters.AddWithNullable("Cpf", objVO.Cpf);
                    }
                    if (!string.IsNullOrEmpty(objVO.Cep))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Cep LIKE @Cep + '%'");
                        GetSqlCommand().Parameters.AddWithNullable("Cep", objVO.Cep);
                    }
                    if (!string.IsNullOrEmpty(objVO.Bairro))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Bairro = @Bairro ");
                        GetSqlCommand().Parameters.AddWithNullable("Bairro", objVO.Bairro);
                    }
                    if (!string.IsNullOrEmpty(objVO.Logr_Numero))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Logr_Numero LIKE @Logr_Numero + '%'");
                        GetSqlCommand().Parameters.AddWithNullable("Logr_Numero", objVO.Bairro);
                    }
                    if (!string.IsNullOrEmpty(objVO.Logr_Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Logr_Nome LIKE @Logr_Nome + '%'");
                        GetSqlCommand().Parameters.AddWithNullable("Logr_Nome", objVO.Logr_Nome);
                    }
                    if (!string.IsNullOrEmpty(objVO.TelefoneNumero))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.TelefoneNumero LIKE @TelefoneNumero + '%'");
                        GetSqlCommand().Parameters.AddWithNullable("TelefoneNumero", objVO.TelefoneNumero);
                    }
                    if (!string.IsNullOrEmpty(objVO.DDDInicial))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.TelefoneDDD >= @DDDInicial");
                        GetSqlCommand().Parameters.AddWithNullable("DDDInicial", objVO.DDDInicial);
                    }
                    if (!string.IsNullOrEmpty(objVO.DDDFinal))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.TelefoneDDD <= @DDDFinal");
                        GetSqlCommand().Parameters.AddWithNullable("DDDFinal", objVO.DDDFinal);
                    }

                    if (!string.IsNullOrEmpty(objVO.Cidade))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Cidade = @Cidade ");
                        GetSqlCommand().Parameters.AddWithNullable("Cidade", objVO.Cidade);
                    }
                    if (!string.IsNullOrEmpty(objVO.Uf))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Uf = @Uf ");
                        GetSqlCommand().Parameters.AddWithNullable("Uf", objVO.Uf);
                    }

                    
                    if (!string.IsNullOrEmpty(objVO.Rg))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarContato.Cpf = @Rg ");
                        GetSqlCommand().Parameters.AddWithNullable("Rg", objVO.Rg);
                    }


                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    ContatoVO = new ContatoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Contatos_Id"))))
                        ContatoVO.Id = Convert.ToInt64(GetSqlDataReader()["Contatos_Id"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        ContatoVO.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        ContatoVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sexo"))))
                        ContatoVO.Sexo = Convert.ToString(GetSqlDataReader()["Sexo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nasc"))))
                        ContatoVO.Nasc = Convert.ToDateTime(GetSqlDataReader()["Nasc"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome_Civil"))))
                        ContatoVO.Nome_Civil = Convert.ToString(GetSqlDataReader()["Nome_Civil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome_Mae"))))
                        ContatoVO.Nome_Mae = Convert.ToString(GetSqlDataReader()["Nome_Mae"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome_Pai"))))
                        ContatoVO.Nome_Pai = Convert.ToString(GetSqlDataReader()["Nome_Pai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Rg"))))
                        ContatoVO.Rg = Convert.ToString(GetSqlDataReader()["Rg"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("RgOrgao"))))
                        ContatoVO.RgOrgao = Convert.ToString(GetSqlDataReader()["RgOrgao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("RgUF"))))
                        ContatoVO.RgUF = Convert.ToString(GetSqlDataReader()["RgUF"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Logr_Tipo"))))
                        ContatoVO.Logr_Tipo = Convert.ToString(GetSqlDataReader()["Logr_Tipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Logr_Tipo"))))
                        ContatoVO.Logr_Tipo = Convert.ToString(GetSqlDataReader()["Logr_Tipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Logr_Nome"))))
                        ContatoVO.Logr_Nome = Convert.ToString(GetSqlDataReader()["Logr_Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Logr_Numero"))))
                        ContatoVO.Logr_Numero = Convert.ToString(GetSqlDataReader()["Logr_Numero"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Bairro"))))
                        ContatoVO.Bairro = Convert.ToString(GetSqlDataReader()["Bairro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cep"))))
                        ContatoVO.Cep = Convert.ToString(GetSqlDataReader()["Cep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cidade"))))
                        ContatoVO.Cidade = Convert.ToString(GetSqlDataReader()["Cidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Uf"))))
                        ContatoVO.Uf = Convert.ToString(GetSqlDataReader()["Uf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TelefoneTipo"))))
                        ContatoVO.TelefoneTipo = Convert.ToString(GetSqlDataReader()["TelefoneTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TelefoneDDD"))))
                        ContatoVO.TelefoneDDD = Convert.ToString(GetSqlDataReader()["TelefoneDDD"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TelefoneNumero"))))
                        ContatoVO.TelefoneNumero = Convert.ToString(GetSqlDataReader()["TelefoneNumero"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Observacao"))))
                        ContatoVO.ContatoHistorico.Observacao = Convert.ToString(GetSqlDataReader()["Observacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataOperacaoString"))))
                        ContatoVO.ContatoHistorico.DataOperacaoString = Convert.ToString(GetSqlDataReader()["DataOperacaoString"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataOperacao"))))
                        ContatoVO.ContatoHistorico.Dataoperacao = Convert.ToDateTime(GetSqlDataReader()["DataOperacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdContatoHistorico"))))
                        ContatoVO.ContatoHistorico.Id = Convert.ToInt32(GetSqlDataReader()["IdContatoHistorico"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        ContatoVO.ContatoHistorico.Usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioNome"))))
                        ContatoVO.ContatoHistorico.Usuario.Nome = Convert.ToString(GetSqlDataReader()["UsuarioNome"]);



                    lstContatoVO.Add(ContatoVO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }

            return lstContatoVO;
        }
    }
}
