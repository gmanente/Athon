<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestaoAdministrativaSalaCapacidadeRel.aspx.cs" Inherits="Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx.GestaoAdministrativaSalaCapacidadeRel" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2021.3.5.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form2" runat="server">
        <div>
            <cc1:StiWebViewer ID="stiGestaoAdministrativaSalaCapacidadeRel" runat="server" />
        </div>
    </form>
</body>
</html>
