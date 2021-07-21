<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Bancos_HSBC, App_Web_1nklxqeh" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<%@ Register Assembly="Boleto.Net" Namespace="BoletoNet" TagPrefix="boletonet" %>
<boletonet:BoletoBancario ID="boletoBancario" runat="server" CodigoBanco="399">
        </boletonet:BoletoBancario>        
        <p>
* Código referente ao HSBC cedido e testado por Leonardo Cooper (leonardo@ecod.com.br)
</p>
</asp:Content>

