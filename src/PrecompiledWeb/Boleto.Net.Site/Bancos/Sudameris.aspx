<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Bancos_Sudameris, App_Web_zo3oovlv" %>
<%@ Register Assembly="Boleto.Net" Namespace="BoletoNet" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
            <cc1:BoletoBancario ID="boletoBancario" runat="server" CodigoBanco="347">
            </cc1:BoletoBancario>
</asp:Content>

