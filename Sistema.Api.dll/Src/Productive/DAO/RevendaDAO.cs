using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Productive.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Productive.DAO
{
    public class RevendaDAO : AbstractDAO, IDAO<RevendaVO>
    {
        public RevendaDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Alterar(RevendaVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                GetSqlCommand().Parameters.Clear();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Revenda 
                                            SET IdUsuario          = @IdUsuario         
                                               ,IdCidade           = @IdCidade          
                                               ,NomeRevenda        = @NomeRevenda       
                                               ,NomeContato        = @NomeContato       
                                               ,Telefone           = @Telefone          
                                               ,Celular            = @Celular           
                                               ,Email              = @Email             
                                               ,Observacao         = @Observacao        
                                               ,IdUsuarioInclusao  = @IdUsuarioInclusao 
                                               ,DataInclusao       = @DataInclusao      
                                               ,IdUsuarioAlteracao = @IdUsuarioAlteracao
                                               ,DataAlteracao      = @DataAlteracao 
                                               ,Ativar             = @Ativar
                ");


                objSbUpdate.AppendLine(where ?? @"WHERE DBAthon.dbo.Revenda.IdRevenda = @IdRevenda");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();
                GetSqlCommand().Parameters.Add("IdRevenda", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("IdCidade", SqlDbType.Int).Value = objVO.Cidade.Id;
                GetSqlCommand().Parameters.Add("NomeRevenda", SqlDbType.VarChar).Value = objVO.NomeRevenda;
                GetSqlCommand().Parameters.Add("NomeContato", SqlDbType.VarChar).Value = objVO.NomeContato;
                GetSqlCommand().Parameters.Add("Telefone", SqlDbType.VarChar).Value = objVO.Telefone;
                GetSqlCommand().Parameters.Add("Celular", SqlDbType.VarChar).Value = objVO.Celular;
                GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                GetSqlCommand().Parameters.Add("Observacao", SqlDbType.VarChar).Value = objVO.Observacao;
                GetSqlCommand().Parameters.Add("DataInclusao", SqlDbType.Int).Value = objVO.UsuarioInclusao.Id;
                GetSqlCommand().Parameters.Add("DataInclusao", SqlDbType.DateTime).Value = objVO.DataInclusao;
                GetSqlCommand().Parameters.Add("IdUsuarioAlteracao", SqlDbType.Int).Value = objVO.UsuarioAlteracao.Id;
                GetSqlCommand().Parameters.Add("DataAlteracao", SqlDbType.DateTime).Value = objVO.DataAlteracao;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;

                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            finally
            {
                objSbUpdate = null;
            }
        }
        
        public void Deletar(RevendaVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.Revenda WHERE IdRevenda = @IdRevenda ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdRevenda", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            finally
            {
                objSbDelete = null;
            }
        }
        
        public long Inserir(RevendaVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqRevenda");
                GetSqlCommand().Parameters.Clear();
                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Revenda
                                         (
                                               IdRevenda         
                                              ,IdUsuario         
                                              ,IdCidade          
                                              ,NomeRevenda       
                                              ,NomeContato       
                                              ,Telefone          
                                              ,Celular           
                                              ,Email             
                                              ,Observacao        
                                              ,IdUsuarioInclusao 
                                              ,DataInclusao      
                                              ,IdUsuarioAlteracao
                                              ,DataAlteracao 
                                         )                
                                          VALUES               
                                         (     
                                               @IdRevenda         
                                              ,@IdUsuario         
                                              ,@IdCidade          
                                              ,@NomeRevenda       
                                              ,@NomeContato       
                                              ,@Telefone          
                                              ,@Celular           
                                              ,@Email             
                                              ,@Observacao        
                                              ,@IdUsuarioInclusao 
                                              ,@DataInclusao      
                                              ,@IdUsuarioAlteracao
                                              ,@DataAlteracao
                                          )");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Add("IdRevenda", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("IdCidade", SqlDbType.Int).Value = objVO.Cidade.Id;
                GetSqlCommand().Parameters.Add("NomeRevenda", SqlDbType.VarChar).Value = objVO.NomeRevenda;
                GetSqlCommand().Parameters.Add("NomeContato", SqlDbType.VarChar).Value = objVO.NomeContato;
                GetSqlCommand().Parameters.Add("Telefone", SqlDbType.VarChar).Value = objVO.Telefone;
                GetSqlCommand().Parameters.Add("Celular", SqlDbType.VarChar).Value = objVO.Celular;
                GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                GetSqlCommand().Parameters.Add("Observacao", SqlDbType.VarChar).Value = objVO.Observacao;
                GetSqlCommand().Parameters.Add("DataInclusao", SqlDbType.Int).Value = objVO.UsuarioInclusao.Id;
                GetSqlCommand().Parameters.Add("DataInclusao", SqlDbType.DateTime).Value = objVO.DataInclusao;
                GetSqlCommand().Parameters.Add("IdUsuarioAlteracao", SqlDbType.Int).Value = objVO.UsuarioAlteracao.Id;
                GetSqlCommand().Parameters.Add("DataAlteracao", SqlDbType.DateTime).Value = objVO.DataAlteracao;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;

                GetSqlCommand().ExecuteNonQuery();
                return objVO.Id;
            }
            finally
            {
                objSbInsert = null;
            }
        }
        
        public RevendaVO Consultar(RevendaVO objVO)
        {
            return Selecionar(objVO)?.FirstOrDefault();
        }

        public List<RevendaVO> Listar(RevendaVO objVO)
        {
            return Selecionar(objVO);
        }

        public List<RevendaVO> Selecionar(RevendaVO objVO, int top = 0)
        {
            List<RevendaVO> lstRevendaVO;

            try
            {
                objSbSelect = new StringBuilder();

                lstRevendaVO = new List<RevendaVO>();
                objSbSelect.AppendLine(@" SELECT 
                                               DBAthon.dbo.Revenda.IdRevenda
                                              ,DBAthon.dbo.Revenda.IdUsuario
                                              ,DBAthon.dbo.Usuario.Nome AS NomeUsuario
                                              ,DBAthon.dbo.Usuario.Cpf  As CpfUsuario                                        
                                              ,DBAthon.dbo.Revenda.IdCidade
                                              ,DBAthon.dbo.Cidade.Nome As NomeCidade
                                              ,DBAthon.dbo.Cidade.IdEstado AS IdEstado
                                              ,DBAthon.dbo.Estado.Nome As NomeEstado
                                              ,DBAthon.dbo.Estado.Sigla As UF
                                              ,DBAthon.dbo.Revenda.NomeRevenda
                                              ,DBAthon.dbo.Revenda.NomeContato
                                              ,DBAthon.dbo.Revenda.Telefone
                                              ,DBAthon.dbo.Revenda.Celular
                                              ,DBAthon.dbo.Revenda.Email
                                              ,DBAthon.dbo.Revenda.Observacao
                                              ,DBAthon.dbo.Revenda.IdUsuarioInclusao
                                              ,UsuarioInclusao.Nome AS NomeUsuarioInclusao
                                              ,DBAthon.dbo.Revenda.DataInclusao
                                              ,DBAthon.dbo.Revenda.Ativar
                                        
                                              ,DBAthon.dbo.Revenda.IdUsuarioAlteracao
                                              ,UsuarioAlteracao.Nome AS NomeUsuarioAlteracao
                                              ,DBAthon.dbo.Revenda.DataAlteracao
                                          FROM DBAthon.dbo.Revenda
                                        
                                        JOIN DBAthon.dbo.Cidade
                                          ON DBAthon.dbo.Cidade.CodigoIbge = DBAthon.dbo.Revenda.IdCidade
                                        
                                        JOIN DBAthon.dbo.Estado
                                          ON DBAthon.dbo.Estado.CodigoIbge = DBAthon.dbo.Cidade.IdEstado
                                        
                                        JOIN DBAthon.dbo.Usuario
                                          ON DBAthon.dbo.Usuario.IdUsuario = DBAthon.dbo.Revenda.IdUsuario
                                        
                                        JOIN DBAthon.dbo.Usuario UsuarioInclusao
                                          ON UsuarioInclusao.IdUsuario = DBAthon.dbo.Revenda.IdUsuarioInclusao
                                        
                                        LEFT JOIN DBAthon.dbo.Usuario UsuarioAlteracao
                                               ON UsuarioAlteracao.IdUsuario = DBAthon.dbo.Revenda.IdUsuarioAlteracao                                                                       
                                        WHERE 1 = 1 ");

                GetSqlCommand().Parameters.Clear();
                if (objVO != null)
                {

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Revenda.IdRevenda = @IdRevenda ");
                        GetSqlCommand().Parameters.Add("IdRevenda", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.IdUsuario = @IdUsuario ");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.NomeRevenda))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Revenda.NomeRevenda LIKE '%' + @Nome + '%'");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.NomeRevenda;
                    }
                }
                objSbSelect.AppendLine(@" ORDER BY DBAthon.dbo.Revenda.NomeRevenda ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    var RevendaVO = new RevendaVO();

                    RevendaVO.Id = GetColumnValue<long>("IdRevenda");
                    RevendaVO.Usuario.Id = GetColumnValue<long>("IdUsuario");
                    RevendaVO.Usuario.Nome = GetColumnValue<string>("NomeUsuario");
                    RevendaVO.Usuario.Cpf = GetColumnValue<string>("CpfUsuario");
                    
                    RevendaVO.Cidade.Id = GetColumnValue<long>("IdCidade");
                    RevendaVO.Cidade.Nome = GetColumnValue<string>("NomeCidade");

                    RevendaVO.Cidade.Estado.Id = GetColumnValue<long>("IdEstado");
                    RevendaVO.Cidade.Estado.Descricao = GetColumnValue<string>("NomeEstado");
                    RevendaVO.Cidade.Estado.Sigla = GetColumnValue<string>("UF");

                    RevendaVO.NomeRevenda = GetColumnValue<string>("NomeRevenda");
                    RevendaVO.NomeContato= GetColumnValue<string>("NomeContato");
                    RevendaVO.Telefone = GetColumnValue<string>("Telefone");
                    RevendaVO.Celular = GetColumnValue<string>("Celular");
                    RevendaVO.Email = GetColumnValue<string>("Email");
                    RevendaVO.Observacao = GetColumnValue<string>("Observacao");

                    RevendaVO.UsuarioInclusao.Id = GetColumnValue<long>("IdUsuarioInclusao");
                    RevendaVO.UsuarioInclusao.Nome = GetColumnValue<string>("NomeUsuarioInclusao");
                    RevendaVO.DataInclusao = GetColumnValue<DateTime>("DataInclusao");

                    RevendaVO.UsuarioAlteracao.Id = GetColumnValue<long>("IdUsuarioAlteracao");
                    RevendaVO.UsuarioAlteracao.Nome = GetColumnValue<string>("NomeUsuarioAlteracao");
                    RevendaVO.DataAlteracao = GetColumnValue<DateTime>("DataAlteracao");
                    RevendaVO.Ativar = GetColumnValue<bool>("Ativar");

                    lstRevendaVO.Add(RevendaVO);
                }
            }
            finally
            {
                objSbSelect = null;
                Close();
            }

            return lstRevendaVO;
        }
    }
}
