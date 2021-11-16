<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlunoCarteirinhaTotalCursoRel.aspx.cs" Inherits="Sistema.Web.UI.Relatorio.View.Report.CarteirinhaAluno.Aspx.AlunoCarteirinhaTotalCursoRel" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2021.3.5.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:StiWebViewer ID="stiAlunoCarteirinhaTotalCursoRel" runat="server" />
        </div>
    </form>
</body>
</html>
