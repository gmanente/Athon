using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Api.dll.Src.Comum.DAO
{
	public class ParametroDAO : AbstractDAO
	{
		public ParametroDAO(SqlCommand sqlComm)
			: base(sqlComm)
		{ }

		public ParametroCampusVO Inserir(ParametroCampusVO objVO)
		{
			try
			{
				objSbInsert = new StringBuilder();
				objVO.Parametro.Id = GetCodigoSequece("DBAthon.dbo.SeqParametro");
				objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.Parametro
										(
													 IdParametro
												  ,  Nome
												  ,  Descricao
												  ,  IdModulo
										)
										VALUES
										(
													 @IdParametro
												  ,  @Nome
												  ,  @Descricao
												  ,  @IdModulo
										)");

				objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqParametroCampus");
				objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.ParametroCampus
										(
													 IdParametroCampus
												  ,  IdParametro
												  ,  IdCampus
												  ,  IdUsuario
												  ,  DataCadastro
												  ,  Valor
												  ,  Ativo
										)
										VALUES
										(
													 @IdParametroCampus
												  ,  @IdParametro
												  ,  @IdCampus
												  ,  @IdUsuario
												  ,  @DataCadastro
												  ,  @Valor
												  ,  @Ativo
										)");

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbInsert.ToString();
				GetSqlCommand().Parameters.Clear();
				GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.Id;
				GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Parametro.Id;
				GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
				GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
				GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Parametro.Nome;
				GetSqlCommand().Parameters.Add("Valor", SqlDbType.VarChar).Value = objVO.Valor;
				GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Parametro.Descricao;
				GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
				GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Parametro.IdModulo;
				GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo.Value;
				GetSqlCommand().ExecuteNonQuery();

				return objVO;

			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbInsert != null)
				{
					objSbInsert = null;
				}
			}
		}

		public ParametroCampusVO InserirParametroCampus(ParametroCampusVO objVO)
		{
			try
			{
				objSbInsert = new StringBuilder();
				objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqParametroCampus");
				objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.ParametroCampus
										(
													 IdParametroCampus
												  ,  IdParametro
												  ,  IdCampus
												  ,  IdUsuario
												  ,  DataCadastro
												  ,  Valor
												  ,  Ativo
										)
										VALUES
										(
													 @IdParametroCampus
												  ,  @IdParametro
												  ,  @IdCampus
												  ,  @IdUsuario
												  ,  @DataCadastro
												  ,  @Valor
												  ,  @Ativo
										)");

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbInsert.ToString();
				GetSqlCommand().Parameters.Clear();
				GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.Id;
				GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Parametro.Id;
				GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
				GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
				GetSqlCommand().Parameters.Add("Valor", SqlDbType.VarChar).Value = objVO.Valor;
				GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
				GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo.Value;
				GetSqlCommand().ExecuteNonQuery();

				return objVO;

			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbInsert != null)
				{
					objSbInsert = null;
				}
			}
		}

		public long Alterar(ParametroCampusVO objVO)
		{
			try
			{

				objSbUpdate = new StringBuilder();
				objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Parametro
											SET
												  Nome           = @Nome
												, Descricao      = @Descricao
												, IdModulo       = @IdModulo
									   ");

				objSbUpdate.AppendLine(" WHERE IdParametro = @IdParametro");

				objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.ParametroCampus
											SET
												  IdCampus              = @IdCampus
												, IdUsuario             = @IdUsuario
												, Valor                 = @Valor
												, Ativo                 = @Ativo
									   ");

				objSbUpdate.AppendLine(" WHERE IdParametroCampus = @IdParametroCampus");

				if (objVO != null)
				{
					GetSqlCommand().CommandText = "";
					GetSqlCommand().CommandText = objSbUpdate.ToString();
					GetSqlCommand().Parameters.Clear();
					GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.Id;
					GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Parametro.Id;
					GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
					GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
					GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Parametro.Nome;
					GetSqlCommand().Parameters.Add("Valor", SqlDbType.VarChar).Value = objVO.Valor;
					GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Parametro.Descricao;
					GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Parametro.IdModulo;
					GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo.Value;

					GetSqlCommand().ExecuteNonQuery();
				}
				return objVO.Id;

			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbUpdate != null)
				{
					objSbUpdate = null;
				}
			}
		}

		public long AlterarValorParametro(ParametroCampusVO objVO)
		{
			try
			{

				objSbUpdate = new StringBuilder();
				objSbUpdate.AppendLine("UPDATE DBAthon.dbo.ParametroCampus  ");
				objSbUpdate.AppendLine("   SET Valor = @Valor               ");
				objSbUpdate.AppendLine(" WHERE IdParametro = @IdParametro   ");
				objSbUpdate.AppendLine("   AND IdCampus = @IdCampus         ");

				if (objVO != null)
				{
					GetSqlCommand().CommandText = "";
					GetSqlCommand().CommandText = objSbUpdate.ToString();
					GetSqlCommand().Parameters.Clear();
					GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Parametro.Id;
					GetSqlCommand().Parameters.Add("Valor", SqlDbType.VarChar).Value = objVO.Valor;
					GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
					GetSqlCommand().ExecuteNonQuery();
				}
				return objVO.Id;

			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbUpdate != null)
				{
					objSbUpdate = null;
				}
			}
		}

		public void Deletar(ParametroVO objVO)
		{
			try
			{
				CheckDelete("DBAthon.dbo.Parametro", "IdParametro", objVO.Id);
				objSbDelete = new StringBuilder();
				objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Parametro ");
				objSbDelete.AppendLine(" WHERE IdParametro = @IdParametro");

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbDelete.ToString();
				GetSqlCommand().Parameters.Clear();
				GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Id;

				GetSqlCommand().ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbDelete != null)
				{
					objSbDelete = null;
				}
			}
		}

		public void DeletarParametroCampus(ParametroCampusVO objVO)
		{
			try
			{
				CheckDelete("DBAthon.dbo.ParametroCampus", "IdParametroCampus", objVO.Id);
				objSbDelete = new StringBuilder();
				objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.ParametroCampus ");
				objSbDelete.AppendLine(" WHERE IdParametroCampus = @IdParametroCampus");

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbDelete.ToString();
				GetSqlCommand().Parameters.Clear();
				GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.Id;

				GetSqlCommand().ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbDelete != null)
				{
					objSbDelete = null;
				}
			}
		}


		public void LogarParametro(ParametroLogVO objVO)
		{
			try
			{
				objSbInsert = new StringBuilder();
				objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.ParametroLog
										(
													 IdReferencia
												   , Tipo
												   , Objeto
												   , Campo
												   , DataOperacao
												   , IdUsuario
												   , ValorAntigo
												   , ValorNovo
										)
										VALUES
										(
													 @IdReferencia
												   , @Tipo
												   , @Objeto
												   , @Campo
												   , @DataOperacao
												   , @IdUsuario
												   , @ValorAntigo
												   , @ValorNovo
										)");

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbInsert.ToString();
				GetSqlCommand().Parameters.Clear();
				GetSqlCommand().Parameters.Add("IdReferencia", SqlDbType.Int).Value = objVO.IdReferencia;
				GetSqlCommand().Parameters.Add("Tipo", SqlDbType.Char).Value = objVO.Tipo;
				GetSqlCommand().Parameters.Add("Objeto", SqlDbType.VarChar).Value = objVO.Objeto;
				GetSqlCommand().Parameters.Add("Campo", SqlDbType.VarChar).Value = objVO.Campo;
				GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.DateTime).Value = objVO.DataOperacao;
				GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
				GetSqlCommand().Parameters.Add("ValorAntigo", SqlDbType.VarChar).Value = objVO.ValorAntigo;
				GetSqlCommand().Parameters.Add("ValorNovo", SqlDbType.VarChar).Value = objVO.ValorNovo;
				GetSqlCommand().ExecuteNonQuery();

			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (objSbInsert != null)
				{
					objSbInsert = null;
				}
			}
		}


		public List<ParametroCampusVO> Selecionar(ParametroCampusVO objVO, int top = 0)
		{
			try
			{
				objSbSelect = new StringBuilder();

				objSbSelect.AppendLine(@"     SELECT
													 Parametro.IdParametro
													,Parametro.Nome
													,Parametro.Descricao
													,Parametro.IdModulo
													,ParametroCampus.IdParametroCampus
													,ParametroCampus.IdCampus
													,ParametroCampus.IdUsuario
													,ParametroCampus.DataCadastro
													,ParametroCampus.Valor
													,ParametroCampus.Ativo

												FROM DBAthon.dbo.Parametro WITH(NOLOCK)

										  INNER JOIN DBAthon.dbo.ParametroCampus  WITH(NOLOCK)
												  ON ParametroCampus.IdParametro = Parametro.IdParametro

										  INNER JOIN DBAthon.dbo.Campus  WITH(NOLOCK)
												  ON Campus.IdCampus = ParametroCampus.IdCampus

										  INNER JOIN DBAthon.dbo.Modulo  WITH(NOLOCK)
												  ON Modulo.IdModulo = Parametro.IdModulo

											   WHERE 1 = 1");

				if (objVO != null)
				{
					GetSqlCommand().Parameters.Clear();

					if (objVO.Id > 0)
					{
						objSbSelect.AppendLine(@" AND ParametroCampus.IdParametroCampus = @IdParametroCampus");
						GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.Id;
					}

					if (!string.IsNullOrEmpty(objVO.Parametro.Nome))
					{
						objSbSelect.AppendLine(@" AND Parametro.Nome = @Nome");
						GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Parametro.Nome;
					}

					if (!string.IsNullOrEmpty(objVO.Parametro.Descricao))
					{
						objSbSelect.AppendLine(@" AND Parametro.Descricao = @Descricao");
						GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Parametro.Descricao;
					}

					if (objVO.Parametro.Id > 0)
					{
						objSbSelect.AppendLine(@" AND Parametro.IdParametro = @IdParametro");
						GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Parametro.Id;
					}

					if (objVO.Parametro.IdModulo > 0)
					{
						objSbSelect.AppendLine(@" AND Parametro.IdModulo = @IdModulo");
						GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Parametro.IdModulo;
					}

					if (objVO.IdCampus > 0)
					{
						objSbSelect.AppendLine(@" AND ParametroCampus.IdCampus = @IdCampus");
						GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
					}

					if (!string.IsNullOrEmpty(objVO.Valor))
					{
						objSbSelect.AppendLine(@" AND ParametroCampus.Valor = @Valor");
						GetSqlCommand().Parameters.Add("Valor", SqlDbType.VarChar).Value = objVO.Valor;
					}

					if (objVO.Ativo != null)
					{
						objSbSelect.AppendLine(@" AND ParametroCampus.Ativo = @Ativo");
						GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;
					}
				}

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbSelect.ToString();

				return GetLista();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objSbSelect != null)
				{
					objSbSelect = null;
				}

				Close();
			}
		}

		public List<ParametroCampusVO> Listar(ParametroCampusVO objVO)
		{
			try
			{
				return Selecionar(objVO);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public ParametroCampusVO Consultar(ParametroCampusVO objVO)
		{
			try
			{
				var lstParametroVO = Selecionar(objVO);

				return lstParametroVO.Count > 0 ? (ParametroCampusVO)lstParametroVO.ToArray().GetValue(0) : null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public List<ParametroModeloVO> GetListaModelo()
		{
			ParametroModeloVO parametroModeloVO = null;
			List<ParametroModeloVO> lstParametroModeloVO = null;

			try
			{
				lstParametroModeloVO = new List<ParametroModeloVO>();
				while (GetSqlDataReader().Read())
				{
					parametroModeloVO = new ParametroModeloVO();

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdParametroCampus"))))
						parametroModeloVO.ParametroCampus.Id = Convert.ToInt64(GetSqlDataReader()["IdParametroCampus"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdParametro"))))
						parametroModeloVO.ParametroCampus.Parametro.Id = Convert.ToInt64(GetSqlDataReader()["IdParametro"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
						parametroModeloVO.ParametroCampus.Parametro.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
						parametroModeloVO.ParametroCampus.Parametro.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Valor"))))
						parametroModeloVO.ParametroCampus.Valor = Convert.ToString(GetSqlDataReader()["Valor"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
						parametroModeloVO.ParametroCampus.IdCampus = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
						parametroModeloVO.ParametroCampus.Parametro.IdModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
						parametroModeloVO.ParametroCampus.IdUsuario = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
						parametroModeloVO.ParametroCampus.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
						parametroModeloVO.ParametroCampus.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Campus"))))
						parametroModeloVO.CampusNome = Convert.ToString(GetSqlDataReader()["Campus"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Modulo"))))
						parametroModeloVO.NomeModulo = Convert.ToString(GetSqlDataReader()["Modulo"]);

					lstParametroModeloVO.Add(parametroModeloVO);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
			return lstParametroModeloVO;
		}

		public List<ParametroCampusVO> GetLista()
		{
			ParametroCampusVO parametroCampusVO = null;
			List<ParametroCampusVO> lstParametroCampusVO = null;

			try
			{
				lstParametroCampusVO = new List<ParametroCampusVO>();

				while (GetSqlDataReader().Read())
				{
					parametroCampusVO = new ParametroCampusVO();

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdParametroCampus"))))
						parametroCampusVO.Id = Convert.ToInt64(GetSqlDataReader()["IdParametroCampus"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdParametro"))))
						parametroCampusVO.Parametro.Id = Convert.ToInt64(GetSqlDataReader()["IdParametro"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
						parametroCampusVO.Parametro.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
						parametroCampusVO.Parametro.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Valor"))))
						parametroCampusVO.Valor = Convert.ToString(GetSqlDataReader()["Valor"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
						parametroCampusVO.IdCampus = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
						parametroCampusVO.Parametro.IdModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
						parametroCampusVO.IdUsuario = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
						parametroCampusVO.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
						parametroCampusVO.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

					lstParametroCampusVO.Add(parametroCampusVO);
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return lstParametroCampusVO;
		}


		public List<ParametroVO> GetListaParametro()
		{
			ParametroVO parametroVO = null;
			List<ParametroVO> lstParametroVO = null;
			try
			{
				lstParametroVO = new List<ParametroVO>();
				while (GetSqlDataReader().Read())
				{
					parametroVO = new ParametroVO();

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdParametro"))))
						parametroVO.Id = Convert.ToInt64(GetSqlDataReader()["IdParametro"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
						parametroVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
						parametroVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

					if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
						parametroVO.IdModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

					lstParametroVO.Add(parametroVO);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
			return lstParametroVO;
		}


		public List<ParametroVO> SelecionarParametros(ParametroVO objVO)
		{

			try
			{
				objSbSelect = new StringBuilder();
				objSbSelect.AppendLine(@"     SELECT
													 Parametro.IdParametro
													,Parametro.Nome
													,Parametro.Descricao
													,Parametro.IdModulo

												FROM DBAthon.dbo.Parametro
											   WHERE 1 = 1                  ");

				if (objVO != null)
				{
					GetSqlCommand().Parameters.Clear();
					if (!string.IsNullOrEmpty(objVO.Nome))
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.Nome = @Nome");
						GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
					}
					if (!string.IsNullOrEmpty(objVO.Descricao))
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.Descricao = @Descricao");
						GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
					}
					if (objVO.Id > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.IdParametro = @IdParametro");
						GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Id;
					}
					if (objVO.IdModulo > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.IdModulo = @IdModulo");
						GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.IdModulo;
					}
				}

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbSelect.ToString();
				return GetListaParametro();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objSbSelect != null)
				{
					objSbSelect = null;
				}
				Close();
			}
		}




		public List<ParametroCampusVO> SelecionarParametrosServicoProtocolo(ParametroCampusVO objVO)
		{

			try
			{
				objSbSelect = new StringBuilder();
				objSbSelect.AppendLine(@"     SELECT
													 Parametro.IdParametro
													,Parametro.Nome
													,Parametro.Descricao
													,Parametro.IdModulo
													,ParametroCampus.IdParametroCampus
													,ParametroCampus.IdCampus
													,ParametroCampus.IdUsuario
													,ParametroCampus.DataCadastro
													,ParametroCampus.Valor
													,ParametroCampus.Ativo

												FROM DBAthon.dbo.Parametro
										  INNER JOIN DBAthon.dbo.ParametroCampus
												  ON ParametroCampus.IdParametro = Parametro.IdParametro
										  INNER JOIN DBAthon.dbo.Campus
												  ON Campus.IdCampus = ParametroCampus.IdCampus
										  INNER JOIN DBAthon.dbo.Modulo
												  ON Modulo.IdModulo = Parametro.IdModulo
											   WHERE 1 = 1
												 AND DBAthon.dbo.Parametro.Nome LIKE '%Servi%'
												 AND DBAthon.dbo.Parametro.IdModulo = 3                   ");

				if (objVO != null)
				{
					GetSqlCommand().Parameters.Clear();
					if (objVO.Id > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.ParametroCampus.IdParametroCampus = @IdParametroCampus");
						GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.Id;
					}
					if (objVO.Parametro.Id > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.IdParametro = @IdParametro");
						GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.Parametro.Id;
					}
					if (!string.IsNullOrEmpty(objVO.Parametro.Nome))
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.Nome = @Nome");
						GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Parametro.Nome;
					}
					if (!string.IsNullOrEmpty(objVO.Parametro.Descricao))
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.Descricao = @Descricao");
						GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Parametro.Descricao;
					}
					if (objVO.Parametro.IdModulo > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.IdModulo = @IdModulo");
						GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Parametro.IdModulo;
					}
					if (objVO.IdCampus > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.ParametroCampus.IdCampus = @IdCampus");
						GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
					}

				}

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbSelect.ToString();
				return GetLista();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objSbSelect != null)
				{
					objSbSelect = null;
				}
				Close();
			}
		}

		public List<ParametroModeloVO> SelecionarModeloPaginar(ParametroModeloVO objVO)
		{

			try
			{
				objSbSelect = new StringBuilder();

				objSbSelect.AppendLine(@"     SELECT
													 Parametro.IdParametro
													,Parametro.Nome
													,Parametro.Descricao
													,Parametro.IdModulo
													,ParametroCampus.IdParametroCampus
													,ParametroCampus.IdCampus
													,ParametroCampus.IdUsuario
													,ParametroCampus.DataCadastro
													,ParametroCampus.Valor
													,ParametroCampus.Ativo
													,Modulo.Nome Modulo
													,Campus.Nome Campus

												FROM DBAthon.dbo.Parametro
										  INNER JOIN DBAthon.dbo.ParametroCampus
												  ON ParametroCampus.IdParametro = Parametro.IdParametro
										  INNER JOIN DBAthon.dbo.Campus
												  ON Campus.IdCampus = ParametroCampus.IdCampus
										  INNER JOIN DBAthon.dbo.Modulo
												  ON Modulo.IdModulo = Parametro.IdModulo
											   WHERE 1 = 1                  ");

				// Trazer somente parametros dos Departamentos do Usuario
				objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Modulo.IdDepartamento IN ({0}) ", objVO.idsDepartamentoUsuario));

				if (objVO != null)
				{
					GetSqlCommand().Parameters.Clear();
					if (objVO.ParametroCampus.Id > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.ParametroCampus.IdParametroCampus = @IdParametroCampus");
						GetSqlCommand().Parameters.Add("IdParametroCampus", SqlDbType.Int).Value = objVO.ParametroCampus.Id;
					}
					if (objVO.ParametroCampus.Parametro.Id > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.IdParametro = @IdParametro");
						GetSqlCommand().Parameters.Add("IdParametro", SqlDbType.Int).Value = objVO.ParametroCampus.Parametro.Id;
					}
					if (!string.IsNullOrEmpty(objVO.ParametroCampus.Parametro.Nome))
					{
						if (objVO.FiltroNome == 1)
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Nome LIKE '%{0}%' ", objVO.ParametroCampus.Parametro.Nome));
						else if (objVO.FiltroNome == 2)
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Nome LIKE '{0}%' ", objVO.ParametroCampus.Parametro.Nome));
						else if (objVO.FiltroNome == 3)
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Nome LIKE '%{0}' ", objVO.ParametroCampus.Parametro.Nome));
						else
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Nome = '{0}' ", objVO.ParametroCampus.Parametro.Nome));
					}
					if (!string.IsNullOrEmpty(objVO.ParametroCampus.Parametro.Descricao))
					{
						if (objVO.FiltroDescricao == 1)
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Descricao LIKE '%{0}%' ", objVO.ParametroCampus.Parametro.Descricao));
						else if (objVO.FiltroDescricao == 2)
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Descricao LIKE '{0}%' ", objVO.ParametroCampus.Parametro.Descricao));
						else if (objVO.FiltroDescricao == 3)
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Descricao LIKE '%{0}' ", objVO.ParametroCampus.Parametro.Descricao));
						else
							objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Parametro.Descricao = '{0}' ", objVO.ParametroCampus.Parametro.Descricao));
					}
					if (objVO.ParametroCampus.Parametro.IdModulo > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.Parametro.IdModulo = @IdModulo");
						GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.ParametroCampus.Parametro.IdModulo;
					}
					else
					{
						// Trazer somente parametros dos Modulos do Usuario
						objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.Modulo.IdModulo IN ({0}) ", objVO.idsModulosUsuario));
					}

					if (objVO.ParametroCampus.IdCampus > 0)
					{
						objSbSelect.AppendLine(@" AND DBAthon.dbo.ParametroCampus.IdCampus = @IdCampus");
						GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.ParametroCampus.IdCampus;
					}
					else
					{
						// Trazer somente parametros dos Campus do Usuario
						objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.ParametroCampus.IdCampus IN ({0}) ", objVO.idsCampusUsuario));
					}
				}

				GetSqlCommand().CommandText = "";
				GetSqlCommand().CommandText = objSbSelect.ToString();
				return GetListaModelo();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objSbSelect != null)
				{
					objSbSelect = null;
				}
				Close();
			}
		}


		public Dictionary<int, List<ParametroModeloVO>> Paginar(ParametroModeloVO objVO)
		{
			Dictionary<int, List<ParametroModeloVO>> dictionary = null;
			try
			{
				List<ParametroModeloVO> lstParametroModeloVO;
				dictionary = new Dictionary<int, List<ParametroModeloVO>>();
				var sbPaginar = new StringBuilder();
				lstParametroModeloVO = SelecionarModeloPaginar(objVO);
				dictionary.Add(lstParametroModeloVO.Count, lstParametroModeloVO);
				return dictionary;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

	}
}
