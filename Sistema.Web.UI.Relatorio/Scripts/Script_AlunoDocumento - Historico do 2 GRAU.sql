
-- APAGA O REGISTRO (Doc: 4) QUANDO TIVER (Doc: 4 e 13)
DELETE AlunoDocumento
  FROM DBSecretariaAcademica.dbo.AlunoDocumento
  JOIN DBSecretariaAcademica.dbo.AlunoDocumentoTipo
    ON AlunoDocumentoTipo.IdAlunoDocumentoTipo = AlunoDocumento.IdAlunoDocumentoTipo
 WHERE AlunoDocumento.IdAlunoDocumentoTipo = 4
   AND EXISTS (SELECT 1
                 FROM DBSecretariaAcademica.dbo.AlunoDocumento ad
                WHERE ad.IdAluno = AlunoDocumento.IdAluno
                  AND ad.IdAlunoDocumentoTipo = 13)


-- ATUALIZA O REGISTRO (Doc: 4) PARA (Doc: 13)
UPDATE DBSecretariaAcademica.dbo.AlunoDocumento
   SET IdAlunoDocumentoTipo = 13
 WHERE IdAlunoDocumentoTipo = 4

 
-- APAGA O TIPO DOCUMENTO (Doc: 4)
 DELETE DBSecretariaAcademica.dbo.AlunoDocumentoTipo
 WHERE IdAlunoDocumentoTipo = 4
