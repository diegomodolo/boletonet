<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Bancos_Basa, App_Web_1nklxqeh" %>
<%@ Register Assembly="Boleto.Net" Namespace="BoletoNet" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<cc1:BoletoBancario id="boletoBancario" runat="server" CodigoBanco="003"></cc1:BoletoBancario>
</asp:Content>

