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
    public class MenuRapidoItemDAO : AbstractDAO, IDAO<MenuRapidoItemVO>
    {
        public MenuRapidoItemDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(MenuRapidoItemVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idMenuRapidoItem = GetCodigoSequece("DBAthon.dbo.SeqMenuRapidoItem");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.MenuRapidoItem  ");
                objSbInsert.AppendLine(@"(                                           ");
                objSbInsert.AppendLine(@"       IdMenuRapidoItem                     ");
                objSbInsert.AppendLine(@"     , IdMenuRapido                         ");
                objSbInsert.AppendLine(@"     , IdFuncionalidade                     ");
                objSbInsert.AppendLine(@"     , Descricao                            ");
                objSbInsert.AppendLine(@"     , Link                                 ");
                objSbInsert.AppendLine(@"     , Icone                                ");
                objSbInsert.AppendLine(@"     , CorIcone                             ");
                objSbInsert.AppendLine(@"     , CorFundo                             ");
                objSbInsert.AppendLine(@"     , Ordem                                ");
                objSbInsert.AppendLine(@"     , Ativo                                ");
                objSbInsert.AppendLine(@"     , DataCadastro                         ");
                objSbInsert.AppendLine(@"     , IdUsuarioCadastro                    ");
                objSbInsert.AppendLine(@")                                           ");
                objSbInsert.AppendLine(@"VALUES                                      ");
                objSbInsert.AppendLine(@"(                                           ");
                objSbInsert.AppendLine(@"       @IdMenuRapidoItem                    ");
                objSbInsert.AppendLine(@"     , @IdMenuRapido                        ");
                objSbInsert.AppendLine(@"     , @IdFuncionalidade                    ");
                objSbInsert.AppendLine(@"     , @Descricao                           ");
                objSbInsert.AppendLine(@"     , @Link                                ");
                objSbInsert.AppendLine(@"     , @Icone                               ");
                objSbInsert.AppendLine(@"     , @CorIcone                            ");
                objSbInsert.AppendLine(@"     , @CorFundo                            ");
                objSbInsert.AppendLine(@"     , @Ordem                               ");
                objSbInsert.AppendLine(@"     , @Ativo                               ");
                objSbInsert.AppendLine(@"     , GETDATE()                            ");
                objSbInsert.AppendLine(@"     , @IdUsuarioCadastro                   ");
                objSbInsert.AppendLine(@")                                           ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMenuRapidoItem", SqlDbType.Int).Value = idMenuRapidoItem;               
                GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.MenuRapido.Id;
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("Link", SqlDbType.VarChar).Value = objVO.Link;
                GetSqlCommand().Parameters.Add("Icone", SqlDbType.VarChar).Value = objVO.Icone;
                GetSqlCommand().Parameters.Add("CorIcone", SqlDbType.VarChar).Value = objVO.CorIcone;
                GetSqlCommand().Parameters.Add("CorFundo", SqlDbType.VarChar).Value = objVO.CorFundo;
                GetSqlCommand().Parameters.Add("Ordem", SqlDbType.Int).Value = objVO.Ordem;
                GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;
                GetSqlCommand().Parameters.Add("IdUsuarioCadastro", SqlDbType.Int).Value = objVO.Usuario.Id;

                GetSqlCommand().ExecuteNonQuery();

                return idMenuRapidoItem;
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

        public long Alterar(MenuRapidoItemVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@" UPDATE DBAthon.dbo.MenuRapidoItem                 
                                             SET                                              
                                               --IdMenuRapido      = @IdMenuRapido
                                                 IdFuncionalidade  = @IdFuncionalidade
                                               , Descricao         = @Descricao
                                               , Link              = @Link 
                                               , Icone             = @Icone
                                               , CorIcone          = @CorIcone
                                               , CorFundo          = @CorFundo
                                               , Ordem             = @Ordem
                                               , Ativo             = @Ativo
                ");
             
                //if (objVO.IconeItem != null){
                //    objSbUpdate.AppendLine(@"    ,IconeItem          = @IconeItem               ");
                //}

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdMenuRapidoItem = @IdMenuRapidoItem               ");
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
                    //GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.MenuRapido.Id;
                    GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("Link", SqlDbType.VarChar).Value = objVO.Link;
                    GetSqlCommand().Parameters.Add("Icone", SqlDbType.VarChar).Value = objVO.Icone;
                    GetSqlCommand().Parameters.Add("CorIcone", SqlDbType.VarChar).Value = objVO.CorIcone;
                    GetSqlCommand().Parameters.Add("CorFundo", SqlDbType.VarChar).Value = objVO.CorFundo;
                    GetSqlCommand().Parameters.Add("Ordem", SqlDbType.Int).Value = objVO.Ordem;
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;

                    //if (objVO.CorIconeItem != null){
                    //    GetSqlCommand().Parameters.Add("CorIconeItem", SqlDbType.VarChar).Value = objVO.CorIconeItem;
                    //}

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdMenuRapidoItem", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(MenuRapidoItemVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@" DELETE FROM DBAthon.dbo.MenuRapidoItem         ");
                objSbDelete.AppendLine(@"       WHERE IdMenuRapidoItem =  @IdMenuRapidoItem  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMenuRapidoItem", SqlDbType.Int).Value = objVO.Id;

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

        public void DeletarPorMenuRapido(MenuRapidoItemVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@" DELETE FROM DBAthon.dbo.MenuRapidoItem         ");
                objSbDelete.AppendLine(@"       WHERE IdMenuRapido =  @IdMenuRapido          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.MenuRapido.Id;

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

        public List<MenuRapidoItemVO> Selecionar(MenuRapidoItemVO objVO = null, int top = 0)
        {

            MenuRapidoItemVO MenuRapidoItemVO = null;

            List<MenuRapidoItemVO> lstMenuRapidoItemVO = null;

            try
            {
                lstMenuRapidoItemVO = new List<MenuRapidoItemVO>();

                objSbSelect = new StringBuilder();              

                objSbSelect.AppendLine(@" SELECT
                                                 MenuRapidoItem.IdMenuRapidoItem
                                               , MenuRapidoItem.IdMenuRapido    
                                               , MenuRapidoItem.IdFuncionalidade
                                               , MenuRapidoItem.Descricao       
                                               , MenuRapidoItem.Link            
                                               , MenuRapidoItem.Icone           
                                               , MenuRapidoItem.CorIcone        
                                               , MenuRapidoItem.CorFundo        
                                               , MenuRapidoItem.Ordem           
                                               , MenuRapidoItem.Ativo           
                                               , MenuRapidoItem.DataCadastro    
                                               , MenuRapidoItem.IdUsuarioCadastro
                                            FROM DBAthon.dbo.MenuRapidoItem
                                           WHERE 1 = 1      
                ");

                if (objVO != null)
                {

                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.IdMenuRapidoItem = @IdMenuRapidoItem");
                        GetSqlCommand().Parameters.Add("IdMenuRapidoItem", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.MenuRapido.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.IdMenuRapido = @IdMenuRapido");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.MenuRapido.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.Descricao = @Descricao");
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
                    MenuRapidoItemVO = new MenuRapidoItemVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapidoItem"))))
                        MenuRapidoItemVO.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapidoItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapido"))))
                        MenuRapidoItemVO.MenuRapido.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        MenuRapidoItemVO.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        MenuRapidoItemVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        MenuRapidoItemVO.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        MenuRapidoItemVO.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorIcone"))))
                        MenuRapidoItemVO.CorIcone = Convert.ToString(GetSqlDataReader()["CorIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorFundo"))))
                        MenuRapidoItemVO.CorFundo = Convert.ToString(GetSqlDataReader()["CorFundo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ordem"))))
                        MenuRapidoItemVO.Ordem = Convert.ToInt32(GetSqlDataReader()["Ordem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        MenuRapidoItemVO.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        MenuRapidoItemVO.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCadastro"))))
                        MenuRapidoItemVO.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioCadastro"]);

                    lstMenuRapidoItemVO.Add(MenuRapidoItemVO);
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

            return lstMenuRapidoItemVO;

        }

        public List<MenuRapidoItemVO> SelecionarMenuRapidoItem(MenuRapidoItemVO objVO = null, int top = 0)
        {

            MenuRapidoItemVO MenuRapidoItemVO = null;

            List<MenuRapidoItemVO> lstMenuRapidoItemVO = null;

            try
            {
                lstMenuRapidoItemVO = new List<MenuRapidoItemVO>();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT
                                                 MenuRapidoItem.IdMenuRapidoItem
                                               , MenuRapidoItem.IdMenuRapido    
                                               , MenuRapidoItem.IdFuncionalidade
                                               , MenuRapidoItem.Descricao       
                                               , MenuRapidoItem.Link            
                                               , MenuRapidoItem.Icone           
                                               , MenuRapidoItem.CorIcone        
                                               , MenuRapidoItem.CorFundo        
                                               , MenuRapidoItem.Ordem           
                                               , MenuRapidoItem.Ativo           
                                               , MenuRapidoItem.DataCadastro    
                                               , MenuRapidoItem.IdUsuarioCadastro
											   , MenuRapido.Descricao                  MenuRapidoDescricao
											   , Funcionalidade.Nome                   FuncionalidadeNome
											   , Funcionalidade.RequisitoFuncional     FuncionalidadeRequisitoFuncional
											   , SubModulo.IdSubModulo
											   , SubModulo.Nome                        SubModuloNome
											   , SubModulo.Link                        SubModuloLink
											   , Modulo.IdModulo
											   , Modulo.Nome                           ModuloNome
											   , Modulo.Link                           ModuloLink
											   , Modulo.LinkHomologacao                ModuloLinkHomologacao 
											   , Modulo.LinkTeste                      ModuloLinkTeste
											   , Usuario.Nome                          UsuarioCadastroNome
											   , Campus.Nome                           CampusNome

                                            FROM DBAthon.dbo.MenuRapidoItem

                                      INNER JOIN DBAthon.dbo.MenuRapido     ON MenuRapido.IdMenuRapido = MenuRapidoItem.IdMenuRapido
									  INNER JOIN DBAthon.dbo.Funcionalidade ON Funcionalidade.IdFuncionalidade = MenuRapidoItem.IdFuncionalidade
									  INNER JOIN DBAthon.dbo.SubModulo      ON SubModulo.IdSubModulo = Funcionalidade.IdSubModulo
									  INNER JOIN DBAthon.dbo.Modulo         ON Modulo.IdModulo = SubModulo.IdModulo
									  INNER JOIN DBAthon.dbo.Usuario        ON Usuario.IdUsuario = MenuRapidoItem.IdUsuarioCadastro
									  INNER JOIN DBAthon.dbo.Campus         ON Campus.IdCampus = MenuRapido.IdCampus
                                           WHERE 1 = 1      
                ");

                if (objVO != null)
                {

                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.IdMenuRapidoItem = @IdMenuRapidoItem");
                        GetSqlCommand().Parameters.Add("IdMenuRapidoItem", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.MenuRapido.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.IdMenuRapido = @IdMenuRapido");
                        GetSqlCommand().Parameters.Add("IdMenuRapido", SqlDbType.Int).Value = objVO.MenuRapido.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND MenuRapidoItem.Descricao = @Descricao");
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
                    MenuRapidoItemVO = new MenuRapidoItemVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapidoItem"))))
                        MenuRapidoItemVO.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapidoItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapido"))))
                        MenuRapidoItemVO.MenuRapido.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        MenuRapidoItemVO.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        MenuRapidoItemVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        MenuRapidoItemVO.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        MenuRapidoItemVO.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorIcone"))))
                        MenuRapidoItemVO.CorIcone = Convert.ToString(GetSqlDataReader()["CorIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CorFundo"))))
                        MenuRapidoItemVO.CorFundo = Convert.ToString(GetSqlDataReader()["CorFundo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ordem"))))
                        MenuRapidoItemVO.Ordem = Convert.ToInt32(GetSqlDataReader()["Ordem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        MenuRapidoItemVO.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        MenuRapidoItemVO.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCadastro"))))
                        MenuRapidoItemVO.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoDescricao"))))
                        MenuRapidoItemVO.MenuRapido.Descricao = Convert.ToString(GetSqlDataReader()["MenuRapidoDescricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("FuncionalidadeNome"))))
                        MenuRapidoItemVO.Funcionalidade.Nome = Convert.ToString(GetSqlDataReader()["FuncionalidadeNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("FuncionalidadeRequisitoFuncional"))))
                        MenuRapidoItemVO.Funcionalidade.RequisitoFuncional = Convert.ToString(GetSqlDataReader()["FuncionalidadeRequisitoFuncional"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Id = Convert.ToInt64(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloNome"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["SubModuloNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloLink"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Link = Convert.ToString(GetSqlDataReader()["SubModuloLink"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Modulo.Id = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloNome"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Modulo.Nome = Convert.ToString(GetSqlDataReader()["ModuloNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLink"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Modulo.Link = Convert.ToString(GetSqlDataReader()["ModuloLink"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLinkHomologacao"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Modulo.LinkHomologacao = Convert.ToString(GetSqlDataReader()["ModuloLinkHomologacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLinkTeste"))))
                        MenuRapidoItemVO.Funcionalidade.SubModulo.Modulo.LinkTeste = Convert.ToString(GetSqlDataReader()["ModuloLinkTeste"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioCadastroNome"))))
                        MenuRapidoItemVO.Usuario.Nome = Convert.ToString(GetSqlDataReader()["UsuarioCadastroNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CampusNome"))))
                        MenuRapidoItemVO.MenuRapido.Campus.Nome = Convert.ToString(GetSqlDataReader()["CampusNome"]);                 

                    lstMenuRapidoItemVO.Add(MenuRapidoItemVO);
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

            return lstMenuRapidoItemVO;

        }

        public MenuRapidoItemVO Consultar(MenuRapidoItemVO objVO)
        {
            try
            {
                List<MenuRapidoItemVO> lstMenuRapidoItem = Selecionar(objVO);
                return lstMenuRapidoItem.Count() > 0 ? (MenuRapidoItemVO)lstMenuRapidoItem.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MenuRapidoItemVO> Listar(MenuRapidoItemVO objVO)
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

        public List<MenuRapidoItemVO> AutenticarMenuRapido(long idUsuario, long idCampus, bool acessoExterno, bool? portal = null, long idModulo = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"

                                   --DECLARE @IdUsuario     INT = 146;  
							       --DECLARE @AcessoExterno INT = 0;
								   --DECLARE @IdCampus      INT = 1;
							       --DECLARE @Portal        INT = 0; 
                                    
						            SELECT DISTINCT 					                   
                                           Modulo.IdSistema
										 , Modulo.IdModulo
                                         , Modulo.IdDepartamento
                                         , Modulo.Nome                             ModuloNome
										 , Modulo.Cor                              ModuloCor 
										 , Modulo.Icone                            ModuloIcone
                                         , Modulo.Link                             ModuloLink  
                                         , Modulo.LinkTeste                        ModuloLinkTeste
                                         , Modulo.LinkDebug                        ModuloLinkDebug
                                         , Modulo.LinkHomologacao                  ModuloLinkHomologacao 
                                         , Modulo.Portal                           ModuloPortal
                                         , SubModulo.IdSubModulo
                                         , SubModulo.Nome                          SubModuloNome
                                         , SubModulo.Icone                         SubModuloIcone
                                         , SubModulo.Link                          SubModuloLink
                                         , SubModulo.Ordem                         SubModuloOrdem
                                         , SubModulo.IdSubModuloPai
                                         , MenuRapidoItem.IdMenuRapidoItem
                                         , MenuRapidoItem.IdMenuRapido
                                         , MenuRapidoItem.IdFuncionalidade
                                         , MenuRapidoItem.Descricao                MenuRapidoItemDescricao
                                         , MenuRapidoItem.Link	                   MenuRapidoItemLink
                                         , MenuRapidoItem.Icone                    MenuRapidoItemIcone
                                         , MenuRapidoItem.CorIcone                 MenuRapidoItemCorIcone
                                         , MenuRapidoItem.CorFundo	               MenuRapidoItemCorFundo
                                         , MenuRapidoItem.Ordem                    MenuRapidoItemOrdem
                                         , MenuRapidoItem.Ativo                    MenuRapidoItemAtivo
                                         , MenuRapido.IdCampus                     MenuRapidoIdCampus
                                         , MenuRapido.Descricao                    MenuRapidoDescricao
                                         , MenuRapido.CorFundo                     MenuRapidoCorFundo
                                         , MenuRapido.CorBorda                     MenuRapidoCorBorda
                                         , MenuRapido.Ordem                        MenuRapidoOrdem
                                         , MenuRapido.Ativo                        MenuRapidoAtivo
                                         , MenuRapido.IconeItem                    MenuRapidoIconeItem
                                         , MenuRapido.CorIconeItem                 MenuRapidoCorIconeItem
                                         , MenuRapido.CorFundoItem                 MenuRapidoCorFundoItem
                                      FROM DBAthon.dbo.PerfilFuncionalidade
                                INNER JOIN DBAthon.dbo.PerfilSubModulo  ON PerfilSubModulo.IdPerfilSubModulo = PerfilFuncionalidade.IdPerfilSubModulo
                                INNER JOIN DBAthon.dbo.PerfilModulo     ON PerfilModulo.IdPerfilModulo = PerfilSubModulo.IdPerfilModulo 
								INNER JOIN DBAthon.dbo.MenuRapidoItem   ON MenuRapidoItem.IdFuncionalidade = PerfilFuncionalidade.IdFuncionalidade
								                                           AND MenuRapidoItem.Ativo = 1
								INNER JOIN DBAthon.dbo.MenuRapido       ON MenuRapido.IdMenuRapido = MenuRapidoItem.IdMenuRapido
								                                           AND MenuRapido.Ativo = 1
								INNER JOIN DBAthon.dbo.SubModulo        ON SubModulo.IdSubModulo = PerfilSubModulo.IdSubModulo
                                INNER JOIN DBAthon.dbo.Modulo           ON Modulo.IdModulo = PerfilModulo.IdModulo
                                INNER JOIN DBAthon.dbo.Perfil           ON Perfil.IdPerfil = PerfilModulo.IdPerfil
                                INNER JOIN DBAthon.dbo.UsuarioPerfil    ON UsuarioPerfil.IdPerfil = Perfil.IdPerfil
                                INNER JOIN DBAthon.dbo.UsuarioCampus    ON UsuarioCampus.IdUsuarioCampus = UsuarioPerfil.IdUsuarioCampus
                                     WHERE UsuarioCampus.IdUsuario = @IdUsuario
                                       AND PerfilModulo.Ativar = 1
                                       AND PerfilSubModulo.Ativar = 1 
                                       AND PerfilFuncionalidade.Ativar = 1
                                       AND PerfilModulo.AcessoExterno = IIF(@AcessoExterno = 0, PerfilModulo.AcessoExterno, 1)
                                       AND Perfil.Ativar = 1
                                       AND UsuarioPerfil.Ativar = 1
                                       AND GETDATE() BETWEEN UsuarioPerfil.DataInicio AND UsuarioPerfil.DataTermino
                                       AND UsuarioCampus.Ativar = 1 	
                                       AND UsuarioCampus.AcessoExterno = IIF(@AcessoExterno = 0, UsuarioCampus.AcessoExterno, 1) 
                ");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = acessoExterno;

                if (idCampus > 0)
                {
                    objSbSelect.AppendLine(@" AND MenuRapido.IdCampus = @IdCampus");
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = idCampus;
                }

                if (portal != null)
                {
                    objSbSelect.AppendLine(@" AND Modulo.Portal = @Portal");
                    GetSqlCommand().Parameters.Add("Portal", SqlDbType.Bit).Value = portal;
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lstMenuRapidoItemVO = new List<MenuRapidoItemVO>();

                while (GetSqlDataReader().Read())
                {
                    var item = new MenuRapidoItemVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        item.Funcionalidade.SubModulo.Modulo.Sistema.Id = Convert.ToInt64(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        item.Funcionalidade.SubModulo.Modulo.Id = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        item.Funcionalidade.SubModulo.Modulo.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloNome"))))
                        item.Funcionalidade.SubModulo.Modulo.Nome = Convert.ToString(GetSqlDataReader()["ModuloNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloIcone"))))
                        item.Funcionalidade.SubModulo.Modulo.Icone = Convert.ToString(GetSqlDataReader()["ModuloIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloCor"))))
                        item.Funcionalidade.SubModulo.Modulo.Cor = Convert.ToString(GetSqlDataReader()["ModuloCor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLink"))))
                        item.Funcionalidade.SubModulo.Modulo.Link = Convert.ToString(GetSqlDataReader()["ModuloLink"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLinkTeste"))))
                        item.Funcionalidade.SubModulo.Modulo.LinkTeste = Convert.ToString(GetSqlDataReader()["ModuloLinkTeste"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLinkDebug"))))
                        item.Funcionalidade.SubModulo.Modulo.LinkDebug = Convert.ToString(GetSqlDataReader()["ModuloLinkDebug"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloLinkHomologacao"))))
                        item.Funcionalidade.SubModulo.Modulo.LinkHomologacao = Convert.ToString(GetSqlDataReader()["ModuloLinkHomologacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ModuloPortal"))))
                        item.Funcionalidade.SubModulo.Modulo.Portal = Convert.ToBoolean(GetSqlDataReader()["ModuloPortal"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        item.Funcionalidade.SubModulo.Id = Convert.ToInt64(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloNome"))))
                        item.Funcionalidade.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["SubModuloNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloIcone"))))
                        item.Funcionalidade.SubModulo.Icone = Convert.ToString(GetSqlDataReader()["SubModuloIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloLink"))))
                        item.Funcionalidade.SubModulo.Link = Convert.ToString(GetSqlDataReader()["SubModuloLink"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloOrdem"))))
                        item.Funcionalidade.SubModulo.Ordem = Convert.ToInt32(GetSqlDataReader()["SubModuloOrdem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModuloPai"))))
                        item.Funcionalidade.SubModulo.IdSubModuloPai = Convert.ToInt32(GetSqlDataReader()["IdSubModuloPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapidoItem"))))
                        item.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapidoItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMenuRapido"))))
                        item.MenuRapido.Id = Convert.ToInt64(GetSqlDataReader()["IdMenuRapido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        item.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemDescricao"))))
                        item.Descricao = Convert.ToString(GetSqlDataReader()["MenuRapidoItemDescricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemLink"))))
                        item.Link = Convert.ToString(GetSqlDataReader()["MenuRapidoItemLink"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemIcone"))))
                        item.Icone = Convert.ToString(GetSqlDataReader()["MenuRapidoItemIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemCorIcone"))))
                        item.CorIcone = Convert.ToString(GetSqlDataReader()["MenuRapidoItemCorIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemCorFundo"))))
                        item.CorFundo = Convert.ToString(GetSqlDataReader()["MenuRapidoItemCorFundo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemOrdem"))))
                        item.Ordem = Convert.ToInt32(GetSqlDataReader()["MenuRapidoItemOrdem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoItemAtivo"))))
                        item.Ativo = Convert.ToBoolean(GetSqlDataReader()["MenuRapidoItemAtivo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoIdCampus"))))
                        item.MenuRapido.Campus.Id = Convert.ToInt64(GetSqlDataReader()["MenuRapidoIdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoDescricao"))))
                        item.MenuRapido.Descricao = Convert.ToString(GetSqlDataReader()["MenuRapidoDescricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoCorFundo"))))
                        item.MenuRapido.CorFundo = Convert.ToString(GetSqlDataReader()["MenuRapidoCorFundo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoCorBorda"))))
                        item.MenuRapido.CorBorda = Convert.ToString(GetSqlDataReader()["MenuRapidoCorBorda"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoOrdem"))))
                        item.MenuRapido.Ordem = Convert.ToInt32(GetSqlDataReader()["MenuRapidoOrdem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoAtivo"))))
                        item.MenuRapido.Ativo = Convert.ToBoolean(GetSqlDataReader()["MenuRapidoAtivo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoIconeItem"))))
                        item.MenuRapido.IconeItem = Convert.ToString(GetSqlDataReader()["MenuRapidoIconeItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoCorIconeItem"))))
                        item.MenuRapido.CorIconeItem = Convert.ToString(GetSqlDataReader()["MenuRapidoCorIconeItem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MenuRapidoCorFundoItem"))))
                        item.MenuRapido.CorFundoItem = Convert.ToString(GetSqlDataReader()["MenuRapidoCorFundoItem"]);

                    lstMenuRapidoItemVO.Add(item);
                }

                return lstMenuRapidoItemVO;
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

    }
}