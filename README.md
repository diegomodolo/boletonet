[![Build status](https://ci.appveyor.com/api/projects/status/tu0uy49drensdlxe?svg=true)](https://ci.appveyor.com/project/carloscds/boletonet)
[![Issues open](http://img.shields.io/github/issues/boletonet/boletonet.svg)](https://huboard.com/boletonet/boletonet)
[![Nuget count](http://img.shields.io/nuget/v/Boleto.Net.svg)](http://www.nuget.org/packages/Boleto.Net/)
[![Join the chat at https://gitter.im/BoletoNet](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/BoletoNet?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![MyGet Ultimo PR](https://img.shields.io/myget/boletonetbuild/v/boleto.net.svg)](https://www.myget.org/gallery/boletonetbuild)

![Alt](https://repobeats.axiom.co/api/embed/739032b0b4224eb3643af4e9e96f19a6b39b14f7.svg "Repobeats analytics image")

Boleto.Net é um componente desenvolvido em C# e ASP.Net e contempla as seguintes funcionalidades:

* Emissão e Impressão de Boletos Bancários
* Geração de Arquivo de Remessa dos layouts
  * CNAB 240
  * CNAB 400
* Leitura do Arquivo de Retorno dos layouts
  * CNAB240
  * CNAB400
  * CBR643

*** ATENÇÃO ***
Para adequação de nova funcionalidade de emissão de boletos pelo banco na CEF, a propriedade ApenasRegistrar foi alterada para TipoEmissao do tipo Enum, com as opções válidas 0 - Emissão pelo Cedente (Default) e 1 - Emissão pelo Banco.

Atualmente estamos implementando mais alguns bancos e toda colaboração será importante. Nosso objetivo é implementar todos os bancos brasileiros.

Para instalar o BoletoNet, utilize o Nuget:

```powershell
Install-Package Boleto.Net
```
Se você é novo por aqui, dê uma olhada nos projetos de testes, que contem exemplos de utilização:
https://github.com/BoletoNet/boletonet/tree/master/src/Boleto.Net.Testes

#### Para build mais atualizados, acesse o MyGet (boletonetBuild)

Para informações sobre o projeto e bancos implementados, consulte o nosso [Wiki](https://github.com/BoletoNet/boletonet/wiki).



