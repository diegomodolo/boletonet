<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Bancos_Itau, App_Web_zo3oovlv" %>
<%@ Register Assembly="Boleto.Net" Namespace="BoletoNet" TagPrefix="boletonet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <boletonet:BoletoBancario ID="boletoBancario" runat="server" CodigoBanco="341" MostrarComprovanteEntrega="true">
        </boletonet:BoletoBancario>
</asp:Content>

