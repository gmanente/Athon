﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlunoCarteirinhaNaoImpressaRel.aspx.cs" Inherits="Sistema.Web.UI.Relatorio.View.Report.CarteirinhaAluno.Aspx.AlunoCarteirinhaNaoImpressaRel" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2021.3.5.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:StiWebViewer ID="stiAlunoCarteirinhaNaoImpressaRel" runat="server" />
        </div>
    </form>
</body>
</html>
