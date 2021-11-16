

--Usuario Atribui Funcionalidades e Perfis
DECLARE @Cpf VARCHAR(20) = '03336874939'

DECLARE @NomeModulo VARCHAR(50) = 'Secretaria Academica';

DECLARE @IdUsuario INT;
DECLARE @IdUsuarioCampus INT;
DECLARE @IdModulo INT;
DECLARE @IdSubModulo INT;
DECLARE @IdFuncionalidade INT;
DECLARE @IdPerfil INT;
DECLARE @IdPerfilModulo int;
DECLARE @IdPerfilSubModulo int;
DECLARE @IdSubModuloPai INT;
DECLARE @Rf varchar(20);
DECLARE @LinkUrlSubModulo VARCHAR(150);



PRINT ('Setting User')
(SELECT TOP 1 @IdUsuario = IdUsuario FROM DBAthon.dbo.Usuario WHERE Cpf = @Cpf)
(SELECT @IdModulo = IdModulo FROM DBAthon.dbo.Modulo WHERE Nome LIKE '%'+ @NomeModulo +'%')




/**************************SUBMODULO***********************************/
IF NOT EXISTS (SELECT * FROM DBAthon.dbo.SubModulo WHERE IdModulo = @IdModulo AND Nome LIKE '%API Moodle%')
BEGIN
	INSERT INTO DBAthon.dbo.SubModulo VALUES (NEXT VALUE FOR DBAthon.dbo.SeqSubModulo, @IdModulo, GETDATE(), 'API Moodle', 'adn', 'api.univag.edu.br', 1, NULL)
END

--PEGA O ID DO SUBMODULO
SELECT @IdSubModulo = IdSubModulo FROM DBAthon.dbo.SubModulo WHERE IdModulo = @IdModulo AND Nome LIKE '%API Moodle%'


/************************FUNCIONALIDADE*******************************/
SET @Rf = 'RF001'
IF NOT EXISTS (SELECT * FROM DBAthon.dbo.Funcionalidade WHERE IdSubModulo = @IdSubModulo AND RequisitoFuncional = @Rf)
BEGIN
	INSERT INTO DBAthon.dbo.Funcionalidade VALUES(NEXT VALUE FOR DBAthon.dbo.SeqFuncionalidade, @IdSubModulo, GETDATE(), 'AcessoApi - Master', @Rf, 'AcessoApi - Master')

	--SET @LinkUrlSubModulo = 'api.univag.edu.br';
	--IF NOT EXISTS(SELECT * FROM DBAthon.dbo.SubModuloUrl WHERE IdSubModulo = @IdSubModulo AND Url = @LinkUrlSubModulo)
	--BEGIN
	--	INSERT INTO DBAthon.dbo.SubModuloUrl VALUES(NEXT VALUE FOR DBAthon.dbo.SeqSubModuloUrl, @IdSubModulo, @LinkUrlSubModulo)
	--END
END

SET @Rf = 'RF002'
IF NOT EXISTS (SELECT * FROM DBAthon.dbo.Funcionalidade WHERE IdSubModulo = @IdSubModulo AND RequisitoFuncional = @Rf)
BEGIN
	INSERT INTO DBAthon.dbo.Funcionalidade VALUES(NEXT VALUE FOR DBAthon.dbo.SeqFuncionalidade, @IdSubModulo, GETDATE(), 'AcessoApi - Academico', @Rf, 'AcessoApi - Sincronia')
END

SET @Rf = 'RF003'
IF NOT EXISTS (SELECT * FROM DBAthon.dbo.Funcionalidade WHERE IdSubModulo = @IdSubModulo AND RequisitoFuncional = @Rf)
BEGIN
	INSERT INTO DBAthon.dbo.Funcionalidade VALUES(NEXT VALUE FOR DBAthon.dbo.SeqFuncionalidade, @IdSubModulo, GETDATE(), 'AcessoApi - Sincronia', @Rf, 'AcessoApi - Sincronia')
END




/**************************************************************************/
/*************------INICIO CONFIGURA��O DO PERFIL------------************/
/**************************************************************************/

GO

BEGIN TRY
	BEGIN TRANSACTION;

		DECLARE @Cpf VARCHAR(50) = '03336874939'

		DECLARE @NomeModulo VARCHAR(50) = 'Secretaria Academica';
		DECLARE @NomePerfil VARCHAR(50) = 'API Moodle';

		DECLARE @IdUsuario INT;
		DECLARE @IdUsuarioCampus INT;
		DECLARE @IdModulo INT;
		DECLARE @IdSubModulo INT;
		DECLARE @IdSubModuloUrl INT;
		DECLARE @IdFuncionalidade INT;
		DECLARE @IdPerfil INT;
		DECLARE @IdPerfilModulo INT;
		DECLARE @IdPerfilSubModulo INT;


		/*CONSULTA*/
		SELECT TOP 1 @IdModulo = IdModulo FROM DBAthon.dbo.Modulo WHERE Nome = @NomeModulo
		SELECT TOP 1 @IdUsuario = IdUsuario FROM DBAthon.dbo.Usuario WHERE Cpf = @Cpf

		IF EXISTS (SELECT * FROM DBAthon.dbo.SubModulo WHERE IdModulo = @IdModulo)
		BEGIN

		    /*VERIFICA SE EXISTE O PERFIL E INSERE OU PEGA O ID*/
			IF NOT EXISTS (SELECT * FROM DBAthon.dbo.Perfil WHERE Descricao LIKE '%'+ @NomePerfil +'%')
			BEGIN
				INSERT INTO DBAthon.dbo.Perfil VALUES(NEXT VALUE FOR DBAthon.dbo.SeqPerfil, @NomePerfil, 1)
			END

			/*CONSULTA*/
			SELECT TOP 1 @IdPerfil = IdPerfil FROM DBAthon.dbo.Perfil WHERE Descricao LIKE '%'+ @NomePerfil +'%';


			/*INSERE ACESSO DO MODULO AO PERFIL*/
			IF NOT EXISTS (SELECT * FROM DBAthon.dbo.PerfilModulo WHERE IdModulo = @IdModulo AND IdPerfil = @IdPerfil AND Ativar = 1)
			BEGIN
				IF(EXISTS (SELECT * FROM DBAthon.dbo.PerfilModulo WHERE IdModulo = @IdModulo AND IdPerfil = @IdPerfil AND Ativar = 0))
					BEGIN
					UPDATE DBAthon.dbo.PerfilModulo SET Ativar = 1, AcessoExterno = 1 WHERE IdModulo = @IdModulo AND IdPerfil = @IdPerfil AND Ativar = 0
					END
				ELSE
					BEGIN
					INSERT INTO DBAthon.dbo.PerfilModulo VALUES (NEXT VALUE FOR DBAthon.dbo.SeqPerfilModulo, @IdModulo, @IdPerfil, 1, 1)
					END
			END

			/*CONSULTA*/
			SELECT @IdPerfilModulo = IdPerfilModulo FROM DBAthon.dbo.PerfilModulo WHERE IdModulo = @IdModulo AND IdPerfil = @IdPerfil AND Ativar = 1


			/*INSERE DEPARTAMENTO*/
			IF NOT EXISTS (SELECT * FROM DBAthon.dbo.PerfilDepartamento WHERE IdPerfil = @IdPerfil AND IdDepartamento = 74 AND Ativar = 1)
			BEGIN
				IF EXISTS (SELECT * FROM DBAthon.dbo.PerfilDepartamento WHERE IdPerfil = @IdPerfil AND IdDepartamento = 74 AND Ativar = 0)
					BEGIN
					UPDATE DBAthon.dbo.PerfilDepartamento SET Ativar = 1 WHERE IdPerfil = @IdPerfil AND IdDepartamento = 74 AND Ativar = 0
					END
				ELSE
					BEGIN
					INSERT INTO DBAthon.dbo.PerfilDepartamento VALUES(NEXT VALUE FOR DBAthon.dbo.SeqPerfilDepartamento, @IdPerfil, 74, 1)
					END
			END

			/*INSERE USU�RIO CAMPUS*/
			IF NOT EXISTS (SELECT * FROM DBAthon.dbo.UsuarioCampus WHERE IdUsuario = @IdUsuario AND IdCampus = 1 AND Ativar = 1)
			BEGIN
				IF EXISTS (SELECT * FROM DBAthon.dbo.UsuarioCampus WHERE IdUsuario = @IdUsuario AND IdCampus = 1 AND Ativar = 0)
					BEGIN
					UPDATE DBAthon.dbo.UsuarioCampus SET Ativar = 1, AcessoExterno = 1 WHERE IdUsuario = @IdUsuario AND IdCampus = 1 AND Ativar = 0
					END
				ELSE
					BEGIN
					INSERT INTO DBAthon.dbo.UsuarioCampus VALUES(NEXT VALUE FOR DBAthon.dbo.SeqUsuarioCampus, @IdUsuario, 1, 1, 1)
					END
			END

			/*CONSULTA*/
			SELECT @IdUsuarioCampus = IdUsuarioCampus FROM DBAthon.dbo.UsuarioCampus WHERE IdUsuario = @IdUsuario AND IdCampus = 1 AND Ativar = 1

			/*INSERE USU�RIO PERFIL*/
			IF NOT EXISTS (SELECT * FROM DBAthon.dbo.UsuarioPerfil WHERE IdPerfil = @IdPerfil and IdUsuarioCampus = @IdUsuarioCampus and Ativar = 1)
			BEGIN
				IF EXISTS (SELECT * FROM DBAthon.dbo.UsuarioPerfil WHERE IdPerfil = @IdPerfil and IdUsuarioCampus = @IdUsuarioCampus and Ativar = 0)
					BEGIN
					UPDATE DBAthon.dbo.UsuarioPerfil SET Ativar = 1 WHERE IdPerfil = @IdPerfil and IdUsuarioCampus = @IdUsuarioCampus and Ativar = 0
					END
				ELSE
					BEGIN
					INSERT INTO DBAthon.dbo.UsuarioPerfil VALUES(NEXT VALUE FOR DBAthon.dbo.SeqUsuarioPerfil, @IdPerfil, GETDATE(), DATEADD(M,6,GETDATE()), 1, @IdUsuarioCampus)
					END
			END



			DECLARE submodulo_cursor CURSOR FOR
				SELECT IdSubModulo FROM DBAthon.dbo.SubModulo WHERE IdModulo = @IdModulo ORDER BY IdSubModulo

			OPEN submodulo_cursor
			FETCH NEXT FROM submodulo_cursor
			INTO @IdSubModulo
			WHILE @@FETCH_STATUS = 0
			BEGIN
				/*INSERE SUBMODULO AO PERFIL*/
				IF NOT EXISTS (SELECT * FROM DBAthon.dbo.PerfilSubModulo WHERE IdPerfilModulo = @IdPerfilModulo AND IdSubModulo = @IdSubModulo AND Ativar = 1)
				BEGIN
					IF EXISTS (SELECT * FROM DBAthon.dbo.PerfilSubModulo WHERE IdPerfilModulo = @IdPerfilModulo AND IdSubModulo = @IdSubModulo AND Ativar = 0)
						BEGIN
						UPDATE DBAthon.dbo.PerfilSubModulo SET Ativar = 1, AcessoExterno = 1 WHERE IdPerfilModulo = @IdPerfilModulo AND IdSubModulo = @IdSubModulo AND Ativar = 0
						END
					ELSE
						BEGIN
						INSERT INTO DBAthon.dbo.PerfilSubModulo VALUES (NEXT VALUE FOR DBAthon.dbo.SeqPerfilSubModulo, @IdPerfilModulo, @IdSubModulo,1 ,1)
						END
				END

				SELECT @IdPerfilSubModulo = IdPerfilSubModulo FROM DBAthon.dbo.PerfilSubModulo WHERE IdPerfilModulo = @IdPerfilModulo AND IdSubModulo = @IdSubModulo AND Ativar = 1

				/*INSERE PERFILFUNCIONALIDADE*/
				DECLARE _cursor CURSOR FOR
					SELECT IdFuncionalidade FROM DBAthon.dbo.Funcionalidade WHERE IdSubModulo = @IdSubModulo

				OPEN _cursor;
				FETCH NEXT FROM _cursor
				INTO @IdFuncionalidade
				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF NOT EXISTS (SELECT * FROM DBAthon.dbo.PerfilFuncionalidade WHERE IdPerfilSubModulo = @IdPerfilSubModulo AND IdFuncionalidade = @IdFuncionalidade AND Ativar = 1)
					BEGIN
						IF EXISTS (SELECT * FROM DBAthon.dbo.PerfilFuncionalidade WHERE IdPerfilSubModulo = @IdPerfilSubModulo AND IdFuncionalidade = @IdFuncionalidade AND Ativar = 0)
						BEGIN
						UPDATE DBAthon.dbo.PerfilFuncionalidade SET Ativar = 1, AcessoExterno = 1 WHERE IdPerfilSubModulo = @IdPerfilSubModulo AND IdFuncionalidade = @IdFuncionalidade AND Ativar = 0
						END
					ELSE
						BEGIN
						INSERT INTO DBAthon.dbo.PerfilFuncionalidade VALUES (NEXT VALUE FOR DBAthon.dbo.SeqPerfilFuncionalidade, @IdPerfilSubModulo, @IdFuncionalidade, 1, 1)
						END
					END
					FETCH NEXT FROM _cursor
					INTO @IdFuncionalidade
				END
				CLOSE _cursor;
				DEALLOCATE _cursor;


				FETCH NEXT FROM submodulo_cursor
				INTO @IdSubModulo
			END
			CLOSE submodulo_cursor;
			DEALLOCATE submodulo_cursor;
		END
	COMMIT;
END TRY

BEGIN CATCH
	ROLLBACK;
	SELECT ERROR_NUMBER() AS ErrorNumber
		,  ERROR_SEVERITY() AS ErrorSeverity
		,  ERROR_STATE() AS ErrorState
		,  ERROR_PROCEDURE() AS ErrorProcedure
		,  ERROR_LINE() AS ErrorLine
		,  ERROR_MESSAGE() AS ErrorMessage;
END CATCH;

/**************************************************************************/
/*************------FIM CONFIGURA��O DO PERFIL------------************/
/**************************************************************************/
