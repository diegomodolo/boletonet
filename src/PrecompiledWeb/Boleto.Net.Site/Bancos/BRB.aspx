<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Bancos_BRB, App_Web_oztjrqkl" %>
<%@ Register Assembly="Boleto.Net" Namespace="BoletoNet" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:BoletoBancario id="boletoBancario" runat="server" CodigoBanco="070"></cc1:BoletoBancario>
</asp:Content>

