using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class MenuRapidoDAO : AbstractDAO, IDAO<MenuRapidoVO>
    {
        public MenuRapidoDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(MenuRapidoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idMenuRapido = GetCodigoSequece("DBAthon.dbo.SeqMenuRapido");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.MenuRapido    ");
                objSbInsert.AppendLine(@"(                                         ");
                objSbInsert.AppendLine(@"      IdMenuRapido                        ");
                objSbInsert.AppendLine(@"     ,IdCampus                            ");
                objSbInsert.AppendLine(@"     ,Descricao                           ");
                objSbInsert.AppendLine(@"     ,CorFundo                            ");
                objSbInsert.AppendLine(@"     ,CorBorda                            ");
                objSbInsert.AppendLine(@"     ,Ordem                               ");
                objSbInsert.AppendLine(@"     ,Ativo                               ");
                objSbInsert.AppendLine(@"     ,IconeItem                           ");
                objSbInsert.AppendLine(@"     ,CorIconeItem                        ");
                objSbInsert.AppendLine(@"     ,CorFundoItem                        ");
                objSbInsert.AppendLine(@"     ,DataCadastro                        ");
                objSbInsert.AppendLine(@"     ,IdUsuarioCadastro                   ");
                objSbInsert.AppendLine(@")                                         ");
                objSbInsert.AppendLine(@"VALUES                                    ");
                objSbInsert.AppendLine(@"(                                         ");
                objSbInsert.AppendLine(@"      @IdMenuRapido                       ");
                objSbInsert.AppendLine(@"     ,@IdCampus                           ");
                objSbInsert.AppendLine(@"     ,@Descricao                          ");
                objSbInsert.AppendLine(@"     ,@CorFundo                           ");
                objSbInsert.AppendLine(@"     ,@CorBorda                           ");
                objSbInsert.AppendLine(@"     ,@Ordem                              ");
                objSbInsert.AppendLine(@"     ,@Ativo                              ");
                objSbInsert.AppendLine(@"     ,@IconeItem                          ");
                objSbInsert.AppendLine(@"     ,@CorIconeItem                       ");
                objSbInsert.AppendLine(@"     ,@CorFundoItem                       ");
                objSbInsert.AppendLine(@"     ,GETDATE()                           ");
                objSbInsert.AppendLine(@"     ,@IdUsuarioCadastro                  ");
                objSbInsert.AppendLine(@")                                         ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = idMenuRapido;               
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("CorFundo", SqlDbType.VarChar).Value = objVO.CorFundo;
                GetSqlCommand().Parameters.Add("CorBorda", SqlDbType.VarChar).Value = objVO.CorBorda;
                GetSqlCommand().Parameters.Add("Ordem", SqlDbType.Int).Value = objVO.Ordem;
                GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;
                GetSqlCommand().Parameters.Add("IconeItem", SqlDbType.VarChar).Value = objVO.IconeItem;
                GetSqlCommand().Parameters.Add("CorIconeItem", SqlDbType.VarChar).Value = objVO.CorIconeItem;
                GetSqlCommand().Parameters.Add("CorFundoItem", SqlDbType.VarChar).Value = objVO.CorFundoItem;
                GetSqlCommand().Parameters.Add("IdUsuarioCadastro", SqlDbType.Int).Value = objVO.Usuario.Id;

                GetSqlCommand().ExecuteNonQuery();

                return idMenuRapido;
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

        public long Alterar(MenuRapidoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@" UPDATE DBAthon.dbo.MenuRapido                 
                                             SET                                              
                                                 IdCampus     = @IdCampus
                                               , Descricao    = @Descricao
                                               , CorFundo     = @CorFundo
                                               , CorBorda     = @CorBorda
                                               , Ordem        = @Ordem
                                               , Ativo        = @Ativo
                                               , IconeItem    = @IconeItem
                                               , CorIconeItem = @CorIconeItem
                                               , CorFundoItem = @CorFundoItem                 
                ");
             
                //if (objVO.IconeItem != null){
                //    objSbUpdate.AppendLine(@"    ,IconeItem          = @IconeItem               ");
                //}

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdMenuRapido = @IdMenuRapido               ");
                }
                else
                {
                    objSbUpdate.AppendLine(where);
                }

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("CorFundo", SqlDbType.VarChar).Value = objVO.CorFundo;
                    GetSqlCommand().Parameters.Add("CorBorda", SqlDbType.VarChar).Value = objVO.CorBorda;
                    GetSqlCommand().Parameters.Add("Ordem", SqlDbType.Int).Value = objVO.Ordem;
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;
                    GetSqlCommand().Parameters.Add("IconeItem", SqlDbType.VarChar).Value = objVO.IconeItem;
                    GetSqlCommand().Parameters.Add("CorIconeItem", SqlDbType.VarChar).Value = objVO.CorIconeItem;
                    GetSqlCommand().Parameters.Add("CorFundoItem", SqlDbType.VarChar).Value = objVO.CorFundoItem;

                    //if (objVO.CorIconeItem != null){
                    //    GetSqlCommand().Parameters.Add("CorIconeItem", SqlDbType.VarChar).Value = objVO.CorIconeItem;
                    //}

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.Id;
                    }

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

        public void Deletar(MenuRapidoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@" DELETE FROM DBAthon.dbo.MenuRapido      ");
                objSbDelete.AppendLine(@"       WHERE IdMenuRapido =  @IdMenuRapido   ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
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

        public List<MenuRapidoVO> Selecionar(MenuRapidoVO objVO = null, int top = 0)
        {

            MenuRapidoVO MenuRapidoVO = null;

            List<MenuRapidoVO> lstMenuRapidoVO = null;

            try
            {
                lstMenuRapidoVO = new List<MenuRapidoVO>();

                objSbSelect = new StringBuilder();              

                objSbSelect.AppendLine(@" SELECT
                                                 MenuRapido.IdMenuRapido
                                               , MenuRapido.IdCampus    
                                               , MenuRapido.Descricao   
                                               , MenuRapido.CorFundo    
                                               , MenuRapido.CorBorda    
                                               , MenuRapido.Ordem       
                                               , MenuRapido.Ativo       
                                               , MenuRapido.IconeItem   
                                               , MenuRapido.CorIconeItem
                                               , MenuRapido.CorFundoItem
                                               , MenuRapido.DataCadastro    
                                               , MenuRapido.IdUsuarioCadastro
                                            FROM DBAthon.dbo.MenuRapido
                                           WHERE 1 = 1      
                ");

                if (objVO != null)
                {

                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapido.IdMenuRapido = @IdMenuRapido");
                        GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapido.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND MenuRapido.Descricao = @Descricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }
                    if (objVO.Ativo != null)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.Ativo = @Ativo");
                        GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    MenuRapidoVO = new MenuRapidoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapido"))))
                        MenuRapidoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        MenuRapidoVO.Campus.Id = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        MenuRapidoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorFundo"))))
                        MenuRapidoVO.Descricao = Convert.ToString(GetSqlDataReader()["CorFundo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorBorda"))))
                        MenuRapidoVO.Descricao = Convert.ToString(GetSqlDataReader()["CorBorda"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ordem"))))
                        MenuRapidoVO.Ordem = Convert.ToInt32(GetSqlDataReader()["Ordem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        MenuRapidoVO.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IconeItem"))))
                        MenuRapidoVO.IconeItem = Convert.ToString(GetSqlDataReader()["IconeItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorIconeItem"))))
                        MenuRapidoVO.CorIconeItem = Convert.ToString(GetSqlDataReader()["CorIconeItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorFundoItem"))))
                        MenuRapidoVO.CorFundoItem = Convert.ToString(GetSqlDataReader()["CorFundoItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        MenuRapidoVO.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCadastro"))))
                        MenuRapidoVO.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioCadastro"]);

                    lstMenuRapidoVO.Add(MenuRapidoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();
            }

            return lstMenuRapidoVO;

        }

        public List<MenuRapidoVO> SelecionarMenuRapido(MenuRapidoVO objVO)
        {
            MenuRapidoVO MenuRapidoVO = null;

            List<MenuRapidoVO> lstMenuRapidoVO = null;

            try
            {
                lstMenuRapidoVO = new List<MenuRapidoVO>();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT
                                                 MenuRapido.IdMenuRapido
                                               , MenuRapido.IdCampus    
                                               , MenuRapido.Descricao   
                                               , MenuRapido.CorFundo    
                                               , MenuRapido.CorBorda    
                                               , MenuRapido.Ordem       
                                               , MenuRapido.Ativo       
                                               , MenuRapido.IconeItem   
                                               , MenuRapido.CorIconeItem
                                               , MenuRapido.CorFundoItem
                                               , MenuRapido.DataCadastro    
                                               , MenuRapido.IdUsuarioCadastro
											   , Campus.Nome                   CampusNome
											   , Usuario.Nome                  UsuarioCadastroNome
                                            FROM DBAthon.dbo.MenuRapido
									  INNER JOIN DBAthon.dbo.Campus      ON Campus.IdCampus = MenuRapido.IdCampus
									  INNER JOIN DBAthon.dbo.Usuario ON Usuario.IdUsuario = MenuRapido.IdUsuarioCadastro
                                           WHERE 1 = 1        
                ");

                if (objVO != null)
                {

                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapido.IdMenuRapido = @IdMenuRapido");
                        GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapido.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND MenuRapido.Descricao LIKE  '%' + @Descricao + '%'");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }
                    if (objVO.Ativo != null)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.Ativo = @Ativo");
                        GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    MenuRapidoVO = new MenuRapidoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapido"))))
                        MenuRapidoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        MenuRapidoVO.Campus.Id = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        MenuRapidoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorFundo"))))
                        MenuRapidoVO.CorFundo = Convert.ToString(GetSqlDataReader()["CorFundo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorBorda"))))
                        MenuRapidoVO.CorBorda = Convert.ToString(GetSqlDataReader()["CorBorda"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ordem"))))
                        MenuRapidoVO.Ordem = Convert.ToInt32(GetSqlDataReader()["Ordem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        MenuRapidoVO.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IconeItem"))))
                        MenuRapidoVO.IconeItem = Convert.ToString(GetSqlDataReader()["IconeItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorIconeItem"))))
                        MenuRapidoVO.CorIconeItem = Convert.ToString(GetSqlDataReader()["CorIconeItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorFundoItem"))))
                        MenuRapidoVO.CorFundoItem = Convert.ToString(GetSqlDataReader()["CorFundoItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        MenuRapidoVO.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCadastro"))))
                        MenuRapidoVO.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CampusNome"))))
                        MenuRapidoVO.Campus.Nome = Convert.ToString(GetSqlDataReader()["CampusNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioCadastroNome"))))
                        MenuRapidoVO.Usuario.Nome = Convert.ToString(GetSqlDataReader()["UsuarioCadastroNome"]);

                    lstMenuRapidoVO.Add(MenuRapidoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();
            }

            return lstMenuRapidoVO;

        }

        public MenuRapidoVO Consultar(MenuRapidoVO objVO)
        {
            try
            {
                List<MenuRapidoVO> lstMenuRapido = Selecionar(objVO);
                return lstMenuRapido.Count() > 0 ? (MenuRapidoVO)lstMenuRapido.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MenuRapidoVO> Listar(MenuRapidoVO objVO = null)
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

    }
}