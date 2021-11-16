using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Repositorio.DAO
{
    public class ConsultaDAO : AbstractDAO
    {
        public ConsultaDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {}

        public SelectField GetSelectField(string[] arrStr, ConsultaCampoVO filtroCampoVo)
        {
            string[] arrGet = arrStr[1].Split(',');

            if (arrGet.Length == 1)
            {
                if (arrGet[0].ToLower().Equals("cursosusuarios")) return null;
            }
            else return SelectedLivre(arrStr, filtroCampoVo);

            return new SelectField();
        }

        public SelectField SelectedLivre(string[] arrStr, ConsultaCampoVO filtroCampoVo)
        {
            objSbSelect = null;

            try
            {
                string camposConcatenados = "";
                string[] campos = arrStr[1].Split(',');
                string[] camposTotal = campos[1].Split('+');
                string filtered = campos[0].StartsWith("*") ? campos[0].Substring(1) : campos[0];

                for (int i = 0; i < camposTotal.Length; i++)
                    camposConcatenados += (i == 0 ? camposTotal[i] : "," + camposTotal[i]);
                camposConcatenados = camposConcatenados.Replace("\"", "'");


                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine("SELECT ").AppendLineIf(campos[0].StartsWith("*"), "DISTINCT "); // Coluna usada como Código do input
                objSbSelect.AppendLine((camposTotal.Length > 1 ? "CONCAT(" + camposConcatenados + ")" : "RTRIM(LTRIM(" + campos[1] + "))") + " AS OPT_TEXT," + filtered); // Coluna usada como Descrição do input
                objSbSelect.AppendLine("FROM " + campos[2] + " ORDER BY OPT_TEXT"); // Nome da Tabela
                objSbSelect.Append(campos.Length > 3 ? " " + campos[3] : ""); // ASC ou DESC


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                GetSqlCommand().Parameters.Clear();


                var select = new SelectField()
                {
                    Id = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim(),
                    Name = filtroCampoVo.NomeCampo.Replace(".", "-").Replace("'", "|").Replace(",", "+").Trim(),
                    LabelText = filtroCampoVo.DescricaoCampo,
                    LabelFor = filtroCampoVo.NomeCampo.Replace(".", "-"),
                    InjectDataAttr = "data-type='combo'",
                    Class = "form-control txtField",
                    Validate = "require_from_group: [1,'.txtField']"
                };
                select.AddOption(new Option() { Value = "", Text = "Escolha uma opção" });

                var reader = GetSqlDataReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string cod = reader.GetValue<String>(filtered);
                        string txt = reader.GetValue<String>("OPT_TEXT");

                        select.AddOption(new Option() { Value = cod, Text = txt });
                    }
                }

                return select;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
        }


        /// <summary>
        /// GetIdModulo
        /// </summary>
        /// <param name="idSubModulo"></param>
        /// <returns></returns>
        public long GetIdModulo(long idSubModulo)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                        SELECT Modulo.IdModulo
                        FROM DBAthon.dbo.SubModulo  WITH (NOLOCK)
                        JOIN DBAthon.dbo.Modulo     WITH (NOLOCK) ON Modulo.IdModulo = SubModulo.IdModulo
                        WHERE SubModulo.IdSubModulo = @IdSubModulo
                ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = idSubModulo;

                long idModulo = 0;

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        idModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);
                }

                return idModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
        }


        /// <summary>
        /// GetIdSubModulo
        /// </summary>
        /// <param name="urlSubModulo"></param>
        /// <returns></returns>
        public long GetIdSubModulo(string urlSubModulo)
        {
            try
            {
                if (string.IsNullOrEmpty(urlSubModulo))
                    throw new Exception("O urlSubModulo deve ser informado.");

                objSbSelect = new StringBuilder();

                objSbSelect.Append(@"
                        SELECT MAX(SubModuloUrl.IdSubModulo) AS IdSubModulo
                        FROM DBAthon.dbo.SubModuloUrl  WITH (NOLOCK)
                        JOIN DBAthon.dbo.SubModulo     WITH (NOLOCK) ON SubModulo.IdSubModulo = SubModuloUrl.IdSubModulo
                        JOIN DBAthon.dbo.Modulo        WITH (NOLOCK) ON Modulo.IdModulo = SubModulo.IdModulo
                ");

                var appState = Dominio.AppState;

                objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Debug, "       WHERE CONCAT(Modulo.LinkDebug, '/', SubModuloUrl.Url) = @UrlSubModulo          ");
                objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Teste, "       WHERE CONCAT(Modulo.LinkTeste, '/', SubModuloUrl.Url) = @UrlSubModulo          ");
                objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Homologacao, " WHERE CONCAT(Modulo.LinkHomologacao, '/', SubModuloUrl.Url) = @UrlSubModulo    ");
                objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Producao, "    WHERE CONCAT(Modulo.Link, '/', SubModuloUrl.Url) = @UrlSubModulo               ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("UrlSubModulo", SqlDbType.VarChar).Value = urlSubModulo;

                long idSubModulo = 0;

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        idSubModulo = Convert.ToInt64(GetSqlDataReader()["IdSubModulo"]);
                }

                return idSubModulo;
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
        }

        /// <summary>
        /// GetUsuario
        /// </summary>
        /// <param name="cpfUsuario"></param>
        /// <returns></returns>
        public List<UsuarioVO> GetUsuario(long cpfUsuario)
        {
            try
            {
                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"

            SELECT
                  Usuario.IdUsuario             AS IdUsuario
                , UsuarioCampus.IdUsuarioCampus AS IdUsuarioCampus
                , Usuario.Cpf                   AS UsuarioCpf
                , Usuario.Nome                  AS UsuarioNome

            FROM DBAthon.dbo.Usuario        WITH (NOLOCK)
            JOIN DBAthon.dbo.UsuarioCampus  WITH (NOLOCK) ON UsuarioCampus.IdUsuario = Usuario.IdUsuario AND UsuarioCampus.IdCampus = 1

            WHERE Usuario.Cpf = @cpfUsuario

                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("cpfUsuario", SqlDbType.Int).Value = cpfUsuario;


                var lstUsuarioVO = new List<UsuarioVO>();

                var reader = GetSqlDataReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var UsuarioVO = new UsuarioVO();

                        UsuarioVO.Id = reader.GetValue<long>("IdUsuario");
                        UsuarioVO.UsuarioCampus.Id = reader.GetValue<long>("IdUsuarioCampus");
                        UsuarioVO.Cpf = reader.GetValue<string>("UsuarioCpf");
                        UsuarioVO.Nome = reader.GetValue<string>("UsuarioNome");

                        lstUsuarioVO.Add(UsuarioVO);
                    }
                }

                return lstUsuarioVO;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
        }


        /// <summary>
        /// GetIdProfessor
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public long GetIdProfessor(long idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    throw new Exception("O IdUsuario deve ser informado.");

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"
                        SELECT TOP 1 Professor.IdProfessor
                        FROM DBAthon.dbo.Professor  WITH (NOLOCK)
                        WHERE Professor.IdUsuario = @IdUsuario
                        AND Professor.Ativo = 1
                        ORDER BY Professor.Matricula DESC
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.VarChar).Value = idUsuario;


                long idProfessor = 0;
                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdProfessor"))))
                        idProfessor = Convert.ToInt64(GetSqlDataReader()["IdProfessor"]);
                }

                return idProfessor;
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
        }


        /// <summary>
        /// GetLoginSenhaUsuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public string[] GetLoginSenhaUsuario(long idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    throw new Exception("O IdUsuario deve ser informado.");

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"
                        SELECT Usuario.NomeLogin, UsuarioSenha.Senha
                        FROM DBAthon.dbo.Usuario       WITH (NOLOCK)
                        JOIN DBAthon.dbo.UsuarioSenha  WITH (NOLOCK) ON UsuarioSenha.IdUsuarioSenha = (SELECT MAX(IdUsuarioSenha) FROM DBAthon.dbo.UsuarioSenha X WHERE X.IdUsuario = Usuario.IdUsuario)
                        WHERE Usuario.IdUsuario = @IdUsuario
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;


                string[] LoginSenhaUsuario = new string[2];
                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        LoginSenhaUsuario[0] = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        LoginSenhaUsuario[1] = Convert.ToString(GetSqlDataReader()["Senha"]);
                }

                return LoginSenhaUsuario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
        }

    }
}